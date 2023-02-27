using System;
using Core.Entities;
using Core.Extensions;
using Core.Helpers;
using Data.Repositories_of_methods.Concrete;
using System.Globalization;
using Data.Repositoriesofmethods.Concrete;

namespace Presentation.Services
{
	public class StudentService
	{

        private readonly GroupService _groupService;
        private readonly GroupRepository _groupRepository;
        private readonly StudentRepository _studentRepository;
        public StudentService()
        {
            _groupService = new GroupService();
            _groupRepository = new GroupRepository();
            _studentRepository = new StudentRepository();
        }

        public void GetAll(Admin admin)
        {
            var students = _studentRepository.GetAll();
            if (students.Count == 0)
            {
            AreYouSureDescription: ConsoleHelper.WriteWithColor("There is no any student!", ConsoleColor.Red);
                Thread.Sleep(1000);
            
                ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Do you want to create new group?=*=*=*=*=*=*=*= \n =*=*=*=*=*=*=*=<<<<Y>>>> or <<<<N>>>>=*=*=*=*=*=*=*=", ConsoleColor.DarkCyan);
                char decision;
                bool isSucceededResult = char.TryParse(Console.ReadLine(), out decision);
                if (!isSucceededResult)
                {
                    ConsoleHelper.WriteWithColor("Your choice is not correct format!", ConsoleColor.Red);
                    Thread.Sleep(1000);
                    goto AreYouSureDescription;

                }
                if (!(decision == 'Y' || decision == 'N'))
                {
                    ConsoleHelper.WriteWithColor("Your choice is not correct!", ConsoleColor.Red);
                    Thread.Sleep(1000);
                    goto AreYouSureDescription;
                }
                if (decision == 'Y')
                {
                    Create(admin);
                }

            }
            ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=ALL STUDENTS=*=*=*=*=*=*=*=", ConsoleColor.DarkCyan);
            foreach (var student in students)
            {
                ConsoleHelper.WriteWithColor($"Id: {student.Id} \n Fullname: {student.Name} {student.Surname} \n Student's Email: {student.Email} \n Group of the student: {student.Group.Name} \n Created By; {student.CreatedBy}", ConsoleColor.DarkYellow);

            }

        }

        public void GetAllByGroup(Admin admin)
        {
            _groupService.GetAll(admin);
        GroupIdDescription: ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter Group's ID=*=*=*=*=*=*=*=", ConsoleColor.DarkYellow);


            int groupId;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out groupId);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Group's Id is not correct format!", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto GroupIdDescription;

            }
            var group = _groupRepository.Get(groupId);
            if (group is null)
            {

                ConsoleHelper.WriteWithColor("There is no any group in this ID!", ConsoleColor.Red);
                

            }
            if (group.Students.Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no studentin this group!", ConsoleColor.Red);
            }

            else
            {
                foreach (var student in group.Students)
                {
                    ConsoleHelper.WriteWithColor($"Id: {student.Id} \n Fullname: {student.Name} {student.Surname} \n Student's Email: {student.Email} \n ", ConsoleColor.DarkYellow);
                }

            }



        }

        public void Create(Admin admin)
		{
            if (_groupRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("First you must to create a group!", ConsoleColor.Red);
                return;

            }
            ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter Student's Name:=*=*=*=*=*=*=*=", ConsoleColor.DarkMagenta);
            string name = Console.ReadLine();
            ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter Student's Surname=*=*=*=*=*=*=*=", ConsoleColor.DarkMagenta);
            string surname = Console.ReadLine();
        EmailDescription: ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter Student's Email=*=*=*=*=*=*=*=", ConsoleColor.DarkMagenta);
            string email = Console.ReadLine();
            if (!email.IsEmail())
            {
                ConsoleHelper.WriteWithColor("Email is not a correct format!", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto EmailDescription;
            }
            if (_studentRepository.IsDuplicateEmail(email))
            {
                ConsoleHelper.WriteWithColor("This Email is already used!", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto EmailDescription;
            }
        BirthDateDescription: ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter Birth Date=*=*=*=*=*=*=*=", ConsoleColor.DarkMagenta);
            DateTime birthDate;
            bool isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Birth date is not correct format!", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto BirthDateDescription;
            }
        GroupDescription: _groupService.GetAll(admin);
            ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter group Id=*=*=*=*=*=*=*=", ConsoleColor.DarkMagenta);
            int groupId;
            isSucceeded = int.TryParse(Console.ReadLine(), out groupId);
            if (!isSucceeded)
            {

                ConsoleHelper.WriteWithColor("Group Id is not correct format!", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto GroupDescription;
            }
            var group = _groupRepository.Get(groupId);
            if (group is null)
            {

                ConsoleHelper.WriteWithColor("Group is not exist in this id", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto GroupDescription;
            }
            if (group.MaxSize <= group.Students.Count)
            {
                ConsoleHelper.WriteWithColor("This group is full!", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto GroupDescription;
            }
            var student = new Student
            {
                Name = name,
                Surname = surname,
                BirthDate = birthDate,
                Group = group,
                GroupId = group.Id,
                Email = email,
                CreatedBy = admin.Username

            };

            group.Students.Add(student);
            _studentRepository.Add(student);
            ConsoleHelper.WriteWithColor($"{student.Name} {student.Surname} is successfully added", ConsoleColor.Green);


        }

        public void Update(Admin admin)
        {
        StudentsIdDescription: GetAll(admin);
         
           ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter student's Id=*=*=*=*=*=*=*=", ConsoleColor.Yellow);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Inputed ID is not correct format!", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto StudentsIdDescription;
            }
            var student = _studentRepository.Get(id);
            if (student is null)
            {
                ConsoleHelper.WriteWithColor("There is no any student in this Id!", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto StudentsIdDescription;

            }
            ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter new Name of the student=*=*=*=*=*=*=*=", ConsoleColor.Yellow);
            string name = Console.ReadLine();
            ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter new Surname of the student=*=*=*=*=*=*=*=", ConsoleColor.Yellow);
            string surname = Console.ReadLine();
        BirthDateDescription: ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter new Birth Date of the student=*=*=*=*=*=*=*=", ConsoleColor.DarkMagenta);
            DateTime birthDate;
            isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Birth date is not correct format!", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto BirthDateDescription;
            }

            NewGroupIdDescription: _groupService.GetAll(admin);
            ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter new Group's Id=*=*=*=*=*=*=*=", ConsoleColor.Yellow);
            int groupId;
            isSucceeded = int.TryParse(Console.ReadLine(), out groupId);
            if (!isSucceeded)
            {

                ConsoleHelper.WriteWithColor("Group's Id is not correct format!", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto NewGroupIdDescription;
            }

            var group = _groupRepository.Get(groupId);
            if (group is null)
            {

                ConsoleHelper.WriteWithColor("There is no any group in this Id!", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto NewGroupIdDescription;

            }
            student.Name = name;
            student.Surname = surname;
            student.BirthDate = birthDate;
            student.Group = group;
            student.GroupId = group.Id;
            student.ModifiedBy = admin.Username; 

            _studentRepository.Update(student);
            ConsoleHelper.WriteWithColor($" Fullname: {student.Name} {student.Surname} \n Group: {student.Group.Name} \n - successfully updated", ConsoleColor.Green);

        }

        public void Delete(Admin admin)
        {
            GetAll(admin);
           IdDescription: ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter Id=*=*=*=*=*=*=*=", ConsoleColor.DarkYellow);
            int id;
            bool isSucceded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceded)
            {
                ConsoleHelper.WriteWithColor("Id is not correct format!", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto IdDescription;
            }
            var student = _studentRepository.Get(id);
            if (student is null)
            {
                ConsoleHelper.WriteWithColor("There is no any student in this ID!", ConsoleColor.Red);

            }
            _studentRepository.Delete(student);
            Console.WriteLine($"{student.Name} {student.Surname} - is successfully deleted", ConsoleColor.Green);

        }
	}
}

