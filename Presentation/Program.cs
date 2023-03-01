using Core.Constants;
using Core.Entities;
using Core.Extensions;
using Core.Helpers;
using Data;
using Data.Contexts;
using Data.Repositories_of_methods.Concrete;
using Presentation.Services;
using System;
using System.Data;
using System.Globalization;
using System.Text;

namespace Presentation
{
    public static class Program
    {
        private readonly static GroupService _groupService;
        private readonly static StudentService _studentService;
        private readonly static AdminService _adminService;
        private readonly static TeacherService _teacherService;
        private readonly static GroupFieldService _groupFieldService;
        static Program()
        {
            Console.OutputEncoding = Encoding.UTF8;
            DbInitializer.SeedAdmins();
            _groupService = new GroupService();
            _studentService = new StudentService();
            _adminService = new AdminService();
            _teacherService = new TeacherService();
            _groupFieldService = new GroupFieldService();
           
        }
        public static object DateTimeStyle { get; private set; }

        static void Main(string[] args)
        {
           
            
            ConsoleHelper.WriteWithColor("|||||||||||||| Welcome ||||||||||||||", ConsoleColor.Cyan);
            Thread.Sleep(1000);

            AuthorizeDescription:  var admin = _adminService.Authorize();
            if (admin is not null)
            {
                ConsoleHelper.WriteWithColor($"|||||||||||||| Welcome, {admin.Username}||||||||||||||", ConsoleColor.Cyan);
                Thread.Sleep(1000);
                while (true)

                {
                MainMenuDescription: ConsoleHelper.WriteWithColor("0 - Logout", ConsoleColor.Magenta);
                    ConsoleHelper.WriteWithColor("1 - Groups", ConsoleColor.Magenta);
                    ConsoleHelper.WriteWithColor("2 - Students", ConsoleColor.Magenta);
                    ConsoleHelper.WriteWithColor("3 - Teachers", ConsoleColor.Magenta);
                    ConsoleHelper.WriteWithColor("4 - Group Field", ConsoleColor.Magenta);


                    int number;
                    bool isSucceeded = int.TryParse(Console.ReadLine(), out number);
                    if (!isSucceeded)

                    {

                        ConsoleHelper.WriteWithColor("Inputed number is not correct format!", ConsoleColor.Red);
                        Thread.Sleep(1000);
                        goto MainMenuDescription;

                    }

                    else
                    {
                        switch (number)
                        {
                            case (int)MainMenuOptions.Groups:
                                while (true)
                                {

                                    Console.WriteLine("     ");
                                GroupDescription: ConsoleHelper.WriteWithColor("0) Back to Main Menu", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("1) Create Group", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("2) Update Group", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("3) Delete Group", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("4) Get All Groups", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("5) Get Group By Id", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("6) Get Group By Name", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("7) Get All Groups By Teacher", ConsoleColor.DarkYellow);
                                    Console.WriteLine("                  ");

                                    ConsoleHelper.WriteWithColor("***********Select Option***********", ConsoleColor.Cyan);

                                    isSucceeded = int.TryParse(Console.ReadLine(), out number);
                                    if (!isSucceeded)
                                    {
                                        ConsoleHelper.WriteWithColor("Inputed number is not correct format!", ConsoleColor.Red);

                                    }
                                    else
                                    {

                                        switch (number)
                                        {
                                            case (int)GroupOptions.BackTomainManu:
                                                goto MainMenuDescription;
                                                break;
                                            case (int)GroupOptions.CreateGroup:

                                                _groupService.Create(admin);


                                                break;
                                            case (int)GroupOptions.DeleteGroup:

                                                _groupService.Delete();

                                                break;
                                            case (int)GroupOptions.GetAllGroups:

                                                _groupService.GetAll(admin);


                                                break;
                                            case (int)GroupOptions.GetGroupById:

                                                _groupService.GetGroupById(admin);
                                           
                                                break;
                                            case (int)GroupOptions.GetGroupByName:
                                                _groupService.GetGroupByName(admin);
                                                break;

                                            case (int)GroupOptions.GetAllGroupsByTeacher:
                                                _groupService.GetAllGroupsByTeacher();
                                                break;

                                                
                                            


                                            case (int)GroupOptions.UpdateGroup:
                                                _groupService.Update(admin);
                                                break;
                                            case (int)GroupOptions.GetAllGroupsByField:
                                                _groupService.GetAllGroupsByField();
                                                break;





                                            default:
                                                ConsoleHelper.WriteWithColor("Inputed number is not exist!", ConsoleColor.Red);
                                                goto GroupDescription;
                                        }
                                    }
                                }
                            case (int)MainMenuOptions.Logout:
                                goto AuthorizeDescription;
                            case (int)MainMenuOptions.Students:
                                while (true)
                                {

                                    ConsoleHelper.WriteWithColor("0 - Go to Main Menu", ConsoleColor.DarkBlue);
                                    ConsoleHelper.WriteWithColor("1 - Create Student", ConsoleColor.DarkBlue);
                                    ConsoleHelper.WriteWithColor("2 - Update Student", ConsoleColor.DarkBlue);
                                    ConsoleHelper.WriteWithColor("3 - Delete Student", ConsoleColor.DarkBlue);
                                    ConsoleHelper.WriteWithColor("4 - Get All Student", ConsoleColor.DarkBlue);
                                    ConsoleHelper.WriteWithColor("5 - Get All Student By Group", ConsoleColor.DarkBlue);


                                    isSucceeded = int.TryParse(Console.ReadLine(), out number);
                                    if (!isSucceeded)
                                    {
                                        ConsoleHelper.WriteWithColor("Inputed number is not correct format!", ConsoleColor.Red);


                                    }
                                    else
                                    {

                                        switch (number)
                                        {
                                            case (int)StudentOptions.BackToMainMenu:
                                                goto MainMenuDescription;
                                                break;

                                            case (int)StudentOptions.CreateStudent:

                                                _studentService.Create(admin);

                                                break;

                                            case (int)StudentOptions.UpdateStudent:
                                                _studentService.Update(admin);
                                                break;
                                            case (int)StudentOptions.DeleteStudent:
                                                _studentService.Delete(admin);
                                                break;

                                            case (int)StudentOptions.GetAllStudents:
                                                _studentService.GetAll(admin);
                                                break;

                                            case (int)StudentOptions.GetAllStudentsByGroup:

                                                _studentService.GetAllByGroup(admin);
                                                break;

                                        }

                                    }



                                }
                            case (int)MainMenuOptions.Teachers:
                                while (true)
                                {

                                    ConsoleHelper.WriteWithColor("0 - Go to Main Menu", ConsoleColor.DarkBlue);
                                    ConsoleHelper.WriteWithColor("1 - Create Teacher", ConsoleColor.DarkBlue);
                                    ConsoleHelper.WriteWithColor("2 - Update Teacher", ConsoleColor.DarkBlue);
                                    ConsoleHelper.WriteWithColor("3 - Delete Teacher", ConsoleColor.DarkBlue);
                                    ConsoleHelper.WriteWithColor("4 - Get All Teachers", ConsoleColor.DarkBlue);
                                    


                                    isSucceeded = int.TryParse(Console.ReadLine(), out number);
                                    if (!isSucceeded)
                                    {
                                        ConsoleHelper.WriteWithColor("Inputed number is not correct format!", ConsoleColor.Red);


                                    }
                                    else
                                    {

                                        switch (number)
                                        {
                                            case (int)TeacherOptions.BackToMainMenu:
                                                goto MainMenuDescription;
                                                break;

                                            case (int)TeacherOptions.CreateTeacher:

                                                _teacherService.Create();

                                                break;

                                            case (int)TeacherOptions.UpdateTeacher:
                                                _teacherService.Update();
                                                break;
                                            case (int)TeacherOptions.DeleteTeacher:
                                                _teacherService.Delete();
                                     
                                                break;

                                            case (int)TeacherOptions.GetAllTeachers:
                                                _teacherService.GetAll();
                                             
                                                break;

                                          
                                        }

                                    }


                                }

                            case (int)MainMenuOptions.GroupFields:
                                while (true)
                                {
                                    GroupFieldDescription:
                                    ConsoleHelper.WriteWithColor("0 - Go to the Main Menu", ConsoleColor.DarkBlue);
                                    ConsoleHelper.WriteWithColor("1 - Create Group Field", ConsoleColor.DarkBlue);
                                    ConsoleHelper.WriteWithColor("2 - Delete Group Field", ConsoleColor.DarkBlue);
                                    ConsoleHelper.WriteWithColor("3 - Update Group Field", ConsoleColor.DarkBlue);
                                    ConsoleHelper.WriteWithColor("4 - Get All Group Fields", ConsoleColor.DarkBlue);

                                    int desc;
                                    isSucceeded = int.TryParse(Console.ReadLine(), out desc);
                                    if (!isSucceeded)
                                    {
                                        ConsoleHelper.WriteWithColor("Inputed number is not correct format!", ConsoleColor.Red);
                                        Thread.Sleep(1000);
                                        goto GroupFieldDescription;


                                    }

                                    switch (desc)
                                    {


                                        case (int)GroupFieldOptions.AddGroupField:
                                            _groupFieldService.Create();
                                            break;
                                        case (int)GroupFieldOptions.RemoveGroupField:
                                            _groupFieldService.Remove();
                                            break;
                                        case (int)GroupFieldOptions.GetAllGroupField:
                                            _groupFieldService.GetAll();
                                            break;

                                        case (int)GroupFieldOptions.MainMenu:
                                            goto MainMenuDescription;

                                        case (int)GroupFieldOptions.UpdateGroupField:
                                            _groupFieldService.Update();
                                            break;
                                        default:
                                            ConsoleHelper.WriteWithColor("Inputed number is not exist!", ConsoleColor.Red);
                                            break;

                                    }


                                }

                            default:
                                ConsoleHelper.WriteWithColor("Inputed number is not exist!", ConsoleColor.Red);
                                goto MainMenuDescription;
                        }



                    }
                }
            }
           
           

           
        }
    }

}


            

                    
                    






                    
                


            




        

       
    
