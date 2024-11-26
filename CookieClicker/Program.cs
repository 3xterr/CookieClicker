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
        public void viewStats(ref int cookieCounter, ref int amountOfAutoClickers, ref int threadTime, ref int totalCookies, ref int clickCounter)
        {
            Console.Clear();
            
            if (clickCounter == 1) { clickCounter = 0; }

            Console.WriteLine($"You have {amountOfAutoClickers} Auto Clickers that each generate a cookie every {threadTime}ms\n\nYou have collected {totalCookies} cookies in total and now have {cookieCounter} cookies.\n\nYou have {clickCounter} extra clicks in total.");
            Console.WriteLine("\nPress any key to go back.");
            Console.ReadKey();
            clickCounter++;
        }

        public void goShop(ref List<string> upgradesHistory, ref List<upgrades> upgradeList, ref int cookieCounter, ref int amountOfAutoClickers, ref int threadTime, ref int clickCounter)
        {
            Console.Clear();
            bool running = true;
            int i = 1;
            Console.WriteLine("Welcome to the upgrades shop! Choose what you want to buy or type \"exit\" to go back to the menu.\n");
            foreach (var upgrade in upgradeList)
            {
                Console.WriteLine($"{i}. {upgrade.cost} - {upgrade.title}\n\tDescription: {upgrade.description}\n");
                i++;
            }

            while (running == true)
            {
                string command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        buyAutoClicker(ref upgradesHistory, ref cookieCounter, ref amountOfAutoClickers, ref threadTime);
                        break;

                    case "2":
                        fasterAutoClicker(ref upgradesHistory, ref cookieCounter, ref amountOfAutoClickers, ref threadTime);
                        break;

                    case "3":
                        buyExtraClick(ref upgradesHistory, ref cookieCounter, ref clickCounter);
                        break;

                    case "exit":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a number of what you want to buy or type \"exit\"");
                        break;
                }
            }
        }

        public void gameInfo(ref int cookieCounter)
        {
            Console.WriteLine($"Press any key to bake a cookie. You now have {cookieCounter} cookies. Press \"E\" to go back to the main menu.");
        }

        public void buyAutoClicker(ref List<string> upgradesHistory, ref int cookieCounter, ref int amountOfAutoClickers, ref int threadTime)
        {
            if (cookieCounter >= 50)
            {
                cookieCounter -= 50;
                amountOfAutoClickers++;
                Console.WriteLine("You have successfully purchased an Auto Clicker!");
                upgradesHistory.Add(new string("50 cookies - Auto Clicker"));

            }
            else { Console.WriteLine("Not enough cookies to buy Auto Clicker"); }
        }

        public void AutoClicker(ref int cookieCounter, ref int amountOfAutoClickers, ref int threadTime, ref int totalCookies)
        {
            while (true)
            {
                cookieCounter += amountOfAutoClickers;
                totalCookies += amountOfAutoClickers;
                Thread.Sleep(threadTime);
            }
        }

        public void buyExtraClick(ref List<string> upgradesHistory, ref int cookieCounter, ref int clickCounter)
        {
            if (cookieCounter >= 50)
            {
                clickCounter++;
                cookieCounter -= 50;
                Console.WriteLine("You have successfully purchased an Extra Click!");
                upgradesHistory.Add(new string("50 cookies - Extra Click"));

            }
            else { Console.WriteLine("Not enough cookies to buy Extra Click"); }
        }

        public void fasterAutoClicker(ref List<string> upgradesHistory, ref int cookieCounter, ref int amountOfAutoClickers, ref int threadTime)
        {
            if (cookieCounter >= 100 && amountOfAutoClickers > 0)
            {
                if (threadTime <= 200)
                {
                    threadTime = 200;
                    Console.WriteLine("Your auto clickers are already going full throttle!");
                }
                else
                {
                    cookieCounter -= 100;
                    threadTime -= 500;
                    upgradesHistory.Add(new string("100 cookies - Faster Auto Clicker"));
                    Console.WriteLine($"Faster Auto Clicker purchased! AutoClicker count: {amountOfAutoClickers}, Speed: {threadTime}ms per click.");
                }
            } else if (amountOfAutoClickers == 0) { Console.WriteLine("You should have at least one Auto Clicker."); } else { Console.WriteLine("Not enough cookies to purchase Faster Auto Clicker!"); }
            
        }

        public void viewHistory(ref List<string> upgradesHistory)
        {
            upgradesHistory.Reverse();
            int i = 1;
            foreach (var upgrade in upgradesHistory)
            {
                Console.WriteLine($"{i}. {upgrade}");
                i++;
            }
        }

        class Program
        {
            public static void Main(string[] args)
            {
                methods myObj = new methods();
                int amountOfAutoClickers = 0;
                int cookieCounter = 0;
                int totalCookies = 0;
                int threadTime = 4100;
                int clickCounter = 1;

                List<upgrades> upgradeList = new List<upgrades>();
                upgradeList.Add(new upgrades { title = "Auto Clicker", cost = "50 cookies", description = "Generates a cookie autmatically every 4 seconds. Can be upgraded." });
                upgradeList.Add(new upgrades { title = "Faster Auto Clicker", cost = "100 cookies", description = "Speeds up your Auto Clicker!" });
                upgradeList.Add(new upgrades { title = "Extra click", cost = "50 cookies", description = "Adds an extra click each purchase to every click you make." });

                List<string> upgradesHistory = new List<string>();

                Task.Run(() =>
                {
                    myObj.AutoClicker(ref cookieCounter, ref amountOfAutoClickers, ref threadTime, ref totalCookies);
                });

                do
                {
                    Console.Clear();
                    Console.WriteLine($"Welcome to cookie clicker! You have {cookieCounter} cookies.\n\n1. Start the game.\n2. Go to the upgrades shop.\n3. See your purchase history.\n4. See your statistics.\n5. Exit");
                    string command = Console.ReadLine();
                    switch (command)
                    {
                        case "1":
                            initializeGame();
                            break;

                        case "2":
                            myObj.goShop(ref upgradesHistory, ref upgradeList, ref cookieCounter, ref amountOfAutoClickers, ref threadTime, ref clickCounter);
                            break;

                        case "3":
                            myObj.viewHistory(ref upgradesHistory);
                            break;

                        case "4":
                            myObj.viewStats(ref cookieCounter, ref amountOfAutoClickers, ref threadTime, ref totalCookies, ref clickCounter);
                            break;

                        case "5":
                            Console.WriteLine("Exiting... Goodbye!");
                            Environment.Exit(0);
                            break;

                        default:
                            Console.WriteLine("Please enter the numberof the option you want to choose.");
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
                        cookieCounter += clickCounter;
                        totalCookies += clickCounter;
                    }
                    while (true);
                }

            }
        }
    }
}