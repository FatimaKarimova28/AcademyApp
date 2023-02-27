using System;
using System.Globalization;
using Core.Entities;
using Core.Helpers;
using Data.Repositoriesofmethods.Abstract;
using Data.Repositoriesofmethods.Concrete;

namespace Presentation.Services
{
	public class TeacherService
	{
        private readonly TeacherRepository _teacherRepository;

        public TeacherService()
		{
            _teacherRepository = new TeacherRepository();
        }

        public void GetAll()
        {
            var teachers = _teacherRepository.GetAll();
            if (teachers.Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no any teacher!", ConsoleColor.Red);


            }
            foreach (var teacher in teachers)
            {
                ConsoleHelper.WriteWithColor($"Id: {teacher.Id} \n Fullname: {teacher.Name} {teacher.Surname} \n Teacher's speciality: {teacher.Speciality}", ConsoleColor.DarkMagenta);

                Console.WriteLine(" ");
                Thread.Sleep(1000);
                if (teacher.Groups.Count == 0)
                {

                    ConsoleHelper.WriteWithColor("There is no any group in this teacher!", ConsoleColor.Red);

                }




                foreach (var group in teacher.Groups)
                {
                    ConsoleHelper.WriteWithColor($"Id: {group.Id} \n Name; {group.Name}", ConsoleColor.DarkMagenta);

                }
            }
            

        }

        public void Create()
		{
			ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter Teacher's Name:=*=*=*=*=*=*=*=", ConsoleColor.DarkYellow);
			string name = Console.ReadLine();

			ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter Teacher's Surname:=*=*=*=*=*=*=*=", ConsoleColor.DarkYellow);
			string surname = Console.ReadLine();

        BirthDateDescription: ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter Teacher's Birth date:=*=*=*=*=*=*=*=", ConsoleColor.DarkYellow);
        
            DateTime birthDate;
            bool isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Birth date is not correct format!", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto BirthDateDescription;
            }

            ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter Teacher's Speciality:=*=*=*=*=*=*=*=", ConsoleColor.DarkYellow);
            string speciality = Console.ReadLine();
            var teacher = new Teacher
            {
                Name = name,
                Surname = surname,
                BirthDate = birthDate,
                Speciality = speciality,
                CreatedAt = DateTime.Now

            };

            _teacherRepository.Add(teacher);
            
            ConsoleHelper.WriteWithColor($"Name: {teacher.Name}, Surname: {teacher.Surname} \n BirthDate: {teacher.BirthDate.ToString()} \n Speciality: {teacher.Speciality}", ConsoleColor.Magenta);
        }

        public void Delete()
        {
            GetAll();

            if (_teacherRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no teacher!", ConsoleColor.Red);

            }
            else
            {

            TeacherIdDescripyion: ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter Teacher's Id:=*=*=*=*=*=*=*=", ConsoleColor.DarkCyan);
                int id;
                bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
                if (!isSucceeded)
                {

                    ConsoleHelper.WriteWithColor("Id is not correct format!", ConsoleColor.Red);
                    Thread.Sleep(1000);
                    goto TeacherIdDescripyion;

                }


                var teacher = _teacherRepository.Get(id);
                if (teacher is null)
                {
                    ConsoleHelper.WriteWithColor("There is no any teacher!", ConsoleColor.Red);
                }

                _teacherRepository.Delete(teacher);
                ConsoleHelper.WriteWithColor($" {teacher.Name} {teacher.Surname} - successfully deleted", ConsoleColor.Green);
            }

            



        }

        public void Update()
        {
            GetAll();

            TeacherIdDescription: ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter Id:=*=*=*=*=*=*=*=", ConsoleColor.DarkCyan);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Inputed ID is not correct format!", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto TeacherIdDescription;
            }
            var teacher = _teacherRepository.Get(id);
            if (teacher is null)
            {
                ConsoleHelper.WriteWithColor("There is no any teacher in this Id!", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto TeacherIdDescription;

            }
            ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter new Name of the Teacher:=*=*=*=*=*=*=*=", ConsoleColor.Yellow);
            string name = Console.ReadLine();
            ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter new Surname of the Teacher:=*=*=*=*=*=*=*=", ConsoleColor.Yellow);
            string surname = Console.ReadLine();
        BirthDateDescription: ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter new Birth Date of the Teacher:=*=*=*=*=*=*=*=", ConsoleColor.DarkMagenta);
            DateTime birthDate;
            isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Birth date is not correct format!", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto BirthDateDescription;
            }

            ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter new Spesiality:=*=*=*=*=*=*=*=", ConsoleColor.DarkCyan);
            string spesiality = Console.ReadLine();



            teacher.Name = name;
            teacher.Surname = surname;
            teacher.BirthDate = birthDate;
            teacher.Speciality = spesiality;

            _teacherRepository.Update(teacher);
            ConsoleHelper.WriteWithColor($"{teacher.Name} {teacher.Surname} - successfully updated", ConsoleColor.Green);

        }

       
	}
}

