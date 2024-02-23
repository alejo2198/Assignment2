using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment2.Controllers
{
    public class J2Controller : ApiController
    {
        /// <summary>
        /// This Controller determines how many times the sum of two dice can equal 10, 
        /// if dice 1 has m sides and dice 2 has n sides
        /// </summary>
        /// <param name="m">amount of sides for dice 1</param>
        /// <param name="n">amount of sides for dice 2</param>
        /// <returns>amount of times the sum of the two dice equals 10</returns>
        /// <example>
        /// GET api/J2/DiceGame/6/5 -> There are 3 total ways to get the sum 10.
        /// </example>
        ///  /// <example>
        /// GET api/J2/DiceGame/12/4 -> There are 4 total ways to get the sum 10.
        /// </example>
        ///  /// <example>
        /// GET api/J2/DiceGame/5/5 -> There is 1 total way to get the sum 10.
        /// </example>
        [HttpGet]
        [Route("api/J2/DiceGame/{m}/{n}")]
        public string DiceGame(int m, int n)
        {
            int totalSumsToTen = 0;
            ///i represents the first dice, j represents the second dice
            ///this nested loop adds the second dice values to every frist dice value and checks if the sum is ten
            for(int i = 1; i <= m; i++)
            {
                for(int j = 1; j <= n; j++)
                {
                    if( i+j == 10)
                    {
                        //if the sum is ten, our counter increases
                        totalSumsToTen++;
                    }
                }
            }
            var returnStatement = "";
            //Change diction to be gramatically correct if there is only 1 way to get a sum of ten
            if (totalSumsToTen == 1)
            {
                returnStatement = "There is " + totalSumsToTen.ToString() + " total way to get the sum 10.";
            }
            else
            {
                returnStatement = "There are " + totalSumsToTen.ToString() + " total ways to get the sum 10.";
            }

            return returnStatement;
        }
    }
}
