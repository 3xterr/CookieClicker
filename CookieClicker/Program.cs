using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading;

namespace CookieClikcer
{

    public class upgrades
    {
        public string title { get; set; }
        public string cost { get; set; }
        public string description { get; set; }
    }

    public class methods
    {
        public void viewStats(ref int cookieCounter, ref int amountOfAutoClickers, ref int threadTime/*, ref int totalCookies*/) 
        {
            Console.WriteLine($"You have {amountOfAutoClickers} Auto Clickers that each generate a cookie every {threadTime}ms\n You have collected (totalCookies) cookies in total."); //TODO: add rest of the shit that needs to be here.
            Console.WriteLine("\nPress any key to go back.");
            Console.ReadKey();
        }
        
        public void goShop(ref List<upgrades> upgradeList, ref int cookieCounter, ref int amountOfAutoClickers, ref int threadTime)
        {
            int i = 1;
            Console.WriteLine("Welcome to the upgrades shop! Choose what you want to buy:\n");
            foreach(var upgrade in upgradeList)
            {
                Console.WriteLine($"{i}. {upgrade.cost} - {upgrade.title}\n\tDescription: {upgrade.description}\n");
                i++;
            }
            string command = Console.ReadLine();
            switch (command)
            {
                case "1":
                    buyAutoClicker(ref cookieCounter, ref amountOfAutoClickers, ref threadTime);
                    break;

                case "2":
                    fasterAutoClicker(ref cookieCounter, ref amountOfAutoClickers, ref threadTime);
                    break;
            }
        }

        public void gameInfo( ref int cookieCounter)
        {
            Console.WriteLine($"Press any key to bake a cookie. You now have {cookieCounter} cookies. Press \"E\" to go back to the main menu.");
        }

        public void buyAutoClicker(ref int cookieCounter, ref int amountOfAutoClickers, ref int threadTime)
        {
            if (cookieCounter >= 50)
            {
                cookieCounter -= 50;
                amountOfAutoClickers++;
                Console.WriteLine("You have successfully purchased an Auto Clicker! Press any key to go back.");
                Console.ReadLine();

            } else { Console.WriteLine("Not enough cookies to buy Auto Clicker"); }
        }

        public void AutoClicker(ref int cookieCounter, ref int amountOfAutoClickers, ref int threadTime)
        {
                while (true)
                {
                    cookieCounter += amountOfAutoClickers;
                    Thread.Sleep(threadTime);
                }
        }

        public void buyExtraClick() 
        {

        }

        public void fasterAutoClicker(ref int cookieCounter, ref int amountOfAutoClickers, ref int threadTime) 
        {
            if (cookieCounter >= 100 && amountOfAutoClickers > 0) 
            {
                cookieCounter -= 100; 

                threadTime -= 300;

                if (threadTime < 200)
                {
                    threadTime = 200;
                    cookieCounter += 100;
                    Console.WriteLine("Your auto clickers are already going full throttle!");
                }

                Console.WriteLine($"Faster AutoClicker purchased! AutoClicker count: {amountOfAutoClickers}, Speed: {threadTime}ms per click.");
            }
            else if (amountOfAutoClickers !> 0)
            {
                Console.WriteLine("You should have at least one Auto Clikcer!");
            } else
            {
                Console.WriteLine("Not enough cookies to buy Faster AutoClicker!");
            }
        }

        public void moreExtraClicks() 
        {

        }
    }

    class Program
    {
        public static void Main(string[] args) 
        {
            //int totalCookies;
            methods myObj = new methods ();
            int amountOfAutoClickers = 0;
            int cookieCounter = 0;
            int threadTime = 4100;

            List<upgrades> upgradeList = new List<upgrades> ();
            upgradeList.Add(new upgrades { title = "Auto Clicker", cost = "50 cookies", description = "Generates a cookie autmatically every 4 seconds. Can be upgraded."});
            upgradeList.Add(new upgrades { title = "Faster Auto Clicker", cost = "100 cookies", description = "Speeds up your Auto Clicker!"});
            upgradeList.Add(new upgrades { title = "Extra click", cost = "50 cookies", description = "Add an extra click to each one of your clicks."});
            
            //List<string> upgradesHistory = new List<string>();

            //TODO: (TOP PRIORITY!!!!!) Add a battle pass.
            //TODO: add a "view purchase history" button to the menu (optional)
            //TODO: add a "Statistics" button to the menu, which displays total amount of cookies, current upgrades and their level.(optional)
            //TODO: make "Extra Clcicks" upgrade actually functional (optional)

            Task.Run(() => 
            {
                myObj.AutoClicker(ref cookieCounter, ref amountOfAutoClickers, ref threadTime);
            });
            
            do
            {
                Console.WriteLine($"\nWelcome to cookie clicker! You have {cookieCounter} cookies.\nEnter 1 to start the game.\nEnter 2 to go to the upgrades shop.\nEnter 3 to see your purchase history.\nEnter 4 to see your statistics.");
                string command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        initializeGame();
                        break;

                    case "2":
                        myObj.goShop(ref upgradeList, ref cookieCounter, ref amountOfAutoClickers, ref threadTime);
                        break;

                    case "3":
                        //myObj.viewHistory();
                        break;

                    case "4":
                        myObj.viewStats(ref cookieCounter, ref amountOfAutoClickers, ref threadTime/*, ref totalCookies*/);
                        break;

                    default:
                        Console.WriteLine("Please enter a whole number.");
                        break;
                }
            } while (true);

            void initializeGame()
            {
                do
                {
                    myObj.gameInfo(ref cookieCounter);
                    var keyInfo = Console.ReadKey();
                    Console.Clear();
                    if (keyInfo.Key == ConsoleKey.E)
                    break;
                    cookieCounter++;
                }
                while (true);
            }

        }
    }
}