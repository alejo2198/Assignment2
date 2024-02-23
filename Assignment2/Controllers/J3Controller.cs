using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment2.Controllers
{
    /*
     * Following lines were added in the Web.Config files to allow the "+" char to be accepted in the url 
     * without returning an error 
     * REFERENCE: https://learn.microsoft.com/en-us/iis/application-frameworks/building-and-running-aspnet-applications/aspnet-20-breaking-changes-on-iis
     <security>
          <requestFiltering allowDoubleEscaping = "true" />
    </security>
    */
    public class J3Controller : ApiController
    {
        /// <summary>
        /// This Controller accepts tuning instructions for a harp in one long string. 
        /// It separates the instructions into individual strings.
        /// It also makes the instructions more human readable
        /// </summary>
        /// <param name="tuningInstructions">Long string containing one to multiple tuning instructions</param>
        /// <returns>List of human readable tuning instructions</returns>
        /// <example>
        /// GET api/J3/HarpTuner/ABC+123 -> ABC tighten 123
        /// </example>
        /// /// <example>
        /// GET api/J3/HarpTuner/ABC+123BC+2D+73H-1
        /// -> 
        /// ABC tighten 123
        /// BC tighten 2
        /// D tighten 73
        /// H loosen 1
        /// </example>
        /// /// <example>
        /// GET api/J3/HarpTuner/AFB+8SC-4H-2GDPE+9
        /// -> AFB tighten 8
        /// SC loosen 4
        /// H loosen 2
        /// GDPE tighten 9
        /// </example>
        [HttpGet]
        [Route("api/J3/HarpTuner/{tuningInstructions}")]
        public IEnumerable<string> HarpTuner(string tuningInstructions)
        {
            int tuningInstructionsLength = tuningInstructions.Length;
            List<string> encodedInstructions = new List<string> (){ };
            string instuction = "";

            //this loop is responsible for separating instructions indivually and adding them to the encodedInstructions list
            for (int i = 0; i < tuningInstructionsLength; i++)
            {
                char currentChar = tuningInstructions[i];
               
                instuction += currentChar;
                //this if block checks if the current char is a number
                if (Char.IsDigit(currentChar) == true)
                {
                    //Check to see if the next char is still in range 
                    if (i + 1 < tuningInstructionsLength)
                    {
                        char nextChar = tuningInstructions[i + 1];
                        //Checks if the next char is a not digit.
                        if (Char.IsDigit(nextChar) == false)
                        {
                            //if its not a digit. Then it is okay to split the current string into a new instruction
                            encodedInstructions.Add(instuction);
                            //clear the instruction to start anew
                            instuction = "";
                        }
                    }
                    else
                    {
                        //Checks if the next char is not in range,
                        //if thats the case then we reached the end of the sequence
                        //It's safe to appened the last instruction
                        encodedInstructions.Add(instuction);
                    }
                }
            }

            List<string> decodedInstructions = new List<string>() { };
            //These are the chars that we want to split our instruction string
            char[] delimiterChars = {'+','-'};

            foreach (string instruction in encodedInstructions)
            {
                //Check to see if we have to tighten or loosen the strings
                var action = "";
                if (instruction.Contains("+")){
                    action = "tighten";
                }else if (instruction.Contains("-"))
                {
                    action = "loosen";
                }

                //creates an array with the strings and the number of turns
                string[] words = instruction.Split(delimiterChars);

                //creates the complete instruction 
                var decodedInstruction = words[0] + " " + action + " " + words[1];

                decodedInstructions.Add(decodedInstruction);
                
            }

            return decodedInstructions;
            
        }
    }
}