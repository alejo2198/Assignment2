using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment2.Controllers
{
    public class J1Controller : ApiController
    {
        /// <summary>
        /// This Controller is responsible for calculating the total amount of calories for a meal.
        /// The user can select a burger,drink,side and desert. Each one of these items and their variants have a caloric value.
        /// This controller will add those values and return the total caloric value of a meal
        /// </summary>
        /// <param name="burger">User enters a value from 1-4 which match respectively to a Cheeseburger,fish burger,Veggie burge, no burger</param>
        /// <param name="drink">User enters a value from 1-4 which match respectively to a soft drink, orange juice ,milk</param>
        /// <param name="side">User enters a value from 1-4 which match respectively to fries, baked potato, chef's salad</param>
        /// <param name="dessert">User enters a value from 1-4 which match respectively to apple pie, sundae, fruit cup</param>
        /// <returns>The total caloric value of a user inputted meal</returns>
        /// <example>
        /// GET api/J1/Menu/4/4/4/4 -> Your total calorie count is 0
        /// </example>
        ///  /// <example>
        /// GET api/J1/Menu/1/4/3/4 -> Your total calorie count is 531
        /// </example>
        ///  /// <example>
        /// GET api/J1/Menu/3/2/1/2 -> Your total calorie count is 946
        /// </example>
        /// 
        [HttpGet]
        [Route("api/J1/Menu/{burger}/{drink}/{side}/{dessert}")]
        
        public string Menu(int burger,int drink,int side,int dessert)
        {
           
            //create lists to contain calorie information for each food category
            List<int> burgerCalories = new List<int>() { 461, 431, 420, 0 };
            List<int> drinkCalories = new List<int>() { 130, 160, 118, 0 };
            List<int> sideCalories = new List<int>() { 100, 57, 70, 0 };
            List<int> dessertCalories = new List<int>() { 167, 266, 75, 0 };

            //update user input to match the index of the item they selected
            int burgerIndex = burger - 1;
            int drinkIndex = drink - 1;
            int sideIndex = side - 1;
            int dessertIndex = dessert - 1;

            //calculate calorie total
            int totaCalories = 
                burgerCalories[burgerIndex] + 
                drinkCalories[drinkIndex] + 
                sideCalories[sideIndex] + 
                dessertCalories[dessertIndex];


            return "Your total calorie count is " + totaCalories.ToString();
        }
    }
}
