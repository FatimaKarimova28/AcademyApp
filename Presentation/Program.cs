using Core.Constants;
using Core.Entities;
using Core.Helpers;
using Data.Repositories_of_methods.Concrete;
using Presentation.Services;
using System;
using System.Data;
using System.Globalization;

namespace Presentation
{
    public static class Program
    {
        static GroupService _groupService;
         static Program()
        {
            _groupService= new GroupService();
        }
        public static object DateTimeStyle { get; private set; }

        static void Main(string[] args)
        {
            

            while (true)
            {
                ConsoleHelper.WriteWithColor("|||||||||||||| Welcome ||||||||||||||", ConsoleColor.Cyan);
                Console.WriteLine("     ");
                ConsoleHelper.WriteWithColor("0) Exit", ConsoleColor.DarkYellow);
                ConsoleHelper.WriteWithColor("1) Create Group", ConsoleColor.DarkYellow);
                ConsoleHelper.WriteWithColor("2) Update Group", ConsoleColor.DarkYellow);
                ConsoleHelper.WriteWithColor("3) Delete Group", ConsoleColor.DarkYellow);
                ConsoleHelper.WriteWithColor("4) Get All Groups", ConsoleColor.DarkYellow);
                ConsoleHelper.WriteWithColor("5) Get Group By Id", ConsoleColor.DarkYellow);
                ConsoleHelper.WriteWithColor("6) Get Group By Name", ConsoleColor.DarkYellow);
                Console.WriteLine("                  ");

                ConsoleHelper.WriteWithColor("***********Select Option***********", ConsoleColor.Cyan);
                int number;
                bool isSucceeded = int.TryParse(Console.ReadLine(), out number);
                if(!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Inputed number is not correct format!", ConsoleColor.Red);

                }
                else
                {
                    if(!(number >=0 && number<7))
                    {
                        ConsoleHelper.WriteWithColor("Inputed number is not exist!", ConsoleColor.Red);
                    }
                    else
                    {
                        switch (number)
                        {
                            case (int)GroupOptions.Exit:
                                _groupService.Exit();
                                break;
                               

                            case (int)GroupOptions.CreateGroup:

                                _groupService.Create();

                                
                                break;


                            case (int)GroupOptions.UpdateGroup:
                                _groupService.Update();
                                break;


                            case (int)GroupOptions.DeleteGroup:

                                _groupService.Delete();

                                break;
                            case (int)GroupOptions.GetAllGroups:

                                _groupService.GetAll();


                                break;
                            case (int)GroupOptions.GetGroupById:

                                _groupService.GetGroupById();
                                break;
                            case (int)GroupOptions.GetGroupByName:
                                _groupService.GetGroupByName();
                                break;
                            default:
                                break;

                        }

                    
                    






                    }
                }


            }




        }

       
    }
}