using Core.Entities;
using Core.Helpers;
using Data.Repositories_of_methods.Concrete;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Presentation.Services
{
    public class GroupService
    {
        private readonly GroupRepository _groupRepository;
        public GroupService()
        {
            _groupRepository= new GroupRepository();
        }
        public void Create()
        {
            ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter Name=*=*=*=*=*=*=*=", ConsoleColor.DarkCyan);
            string name = Console.ReadLine();

        MaxSizeDescription: ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter Max Size Of Group=*=*=*=*=*=*=*=", ConsoleColor.DarkCyan);
            int maxSize;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out maxSize);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Max size is not correct format", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto MaxSizeDescription;
            }
            if (maxSize > 18)
            {
                ConsoleHelper.WriteWithColor("Max size should be less than or equals to 18", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto MaxSizeDescription;
            }
        StartDateDescription: ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter Start Date=*=*=*=*=*=*=*=", ConsoleColor.DarkCyan);
            DateTime startDate;
            isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Start date is not correct format!", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto StartDateDescription;
            }
            DateTime boundryDate = new DateTime(2015, 1, 1);
            if (startDate < boundryDate)
            {
                ConsoleHelper.WriteWithColor("Start date is not chosen right", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto StartDateDescription;
            }
        EndDateDescription: ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter End Date=*=*=*=*=*=*=*=", ConsoleColor.DarkCyan);
            DateTime endDate;
            isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("End date is not correct format!", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto EndDateDescription;
            }
            if (startDate > endDate)
            {
                ConsoleHelper.WriteWithColor("End date must be bigger than start date", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto EndDateDescription;
            }

            var group = new Group
            {
                Name = name,
                MaxSize = maxSize,
                StartDate = startDate,
                EndDate = endDate,
            };
            _groupRepository.Add(group);
            ConsoleHelper.WriteWithColor($"Group successfully created with: \n Name: {group.Name} \n Id: {group.Id} \n Max Size: {group.MaxSize} \n Start date: {group.StartDate.ToShortDateString()} \n End date: {group.EndDate.ToShortDateString()} \n", ConsoleColor.Magenta);


        }
        public void GetAll()
        {
            var groups = _groupRepository.GetAll();
            ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=ALL GROUPS=*=*=*=*=*=*=*=", ConsoleColor.DarkCyan);
            foreach (var group in groups)
            {
                ConsoleHelper.WriteWithColor($"Name: {group.Name} \n  Id: {group.Id} \n Max Size: {group.MaxSize} \n Start date: {group.StartDate.ToShortDateString()} \n End date: {group.EndDate.ToShortDateString()} \n", ConsoleColor.Magenta);
            }


        }

        public void GetGroupById()
        {
            var groups = _groupRepository.GetAll();
            if(groups.Count == 0)
            {
            AreYouSureDescription: ConsoleHelper.WriteWithColor("There is no any group", ConsoleColor.Red);
                Console.WriteLine(" "); 
                Thread.Sleep(1500);
                ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Do you want to create new group?=*=*=*=*=*=*=*= \n =*=*=*=*=*=*=*=<<<<Y>>>> or <<<<N>>>>=*=*=*=*=*=*=*=", ConsoleColor.DarkCyan);
                char decision;
                bool isSucceededResult = char.TryParse(Console.ReadLine(), out decision);
                if(!isSucceededResult)
                {
                    ConsoleHelper.WriteWithColor("Your choice is not correct format", ConsoleColor.Red);
                    Thread.Sleep(1000);
                    goto AreYouSureDescription;

                }
                if(!(decision == 'Y' || decision == 'N'))
                {
                    ConsoleHelper.WriteWithColor("Your choice is not correct", ConsoleColor.Red);
                    Thread.Sleep(1000);
                    goto AreYouSureDescription;
                }
                if(decision == 'Y')
                {
                    Create();
                }
            }
            else
            {
            EnterIdDescription: ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter Id=*=*=*=*=*=*=*=", ConsoleColor.DarkCyan);
                int id;
                bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Inputed id is not correct format", ConsoleColor.Red);
                    Thread.Sleep(1000);
                    goto EnterIdDescription;
                }
                var group = _groupRepository.Get(id);
                if (group == null)
                {
                    ConsoleHelper.WriteWithColor("There is no any group in this Id", ConsoleColor.Red);
                    Thread.Sleep(1000);
                    goto EnterIdDescription;
                }
                ConsoleHelper.WriteWithColor($" Id: {group.Id} \n Name: {group.Name} \n Max Size: {group.MaxSize} \n Start Date: {group.StartDate} \n End Date: {group.EndDate} \n ", ConsoleColor.DarkMagenta);


            }







        }

        public void GetGroupByName()
        {
            var groups = _groupRepository.GetAll();
            if (groups.Count == 0)
            {
            AreYouSureDescription: ConsoleHelper.WriteWithColor("There is no any group", ConsoleColor.Red);
                Console.WriteLine(" ");
                Thread.Sleep(1500);
                ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Do you want to create new group?=*=*=*=*=*=*=*= \n =*=*=*=*=*=*=*=<<<<Y>>>> or <<<<N>>>>=*=*=*=*=*=*=*=", ConsoleColor.DarkCyan);
                char decision;
                bool isSucceededResult = char.TryParse(Console.ReadLine(), out decision);
                if (!isSucceededResult)
                {
                    ConsoleHelper.WriteWithColor("Your choice is not correct format", ConsoleColor.Red);
                    Thread.Sleep(1000);
                    goto AreYouSureDescription;

                }
                if (!(decision == 'Y' || decision == 'N'))
                {
                    ConsoleHelper.WriteWithColor("Your choice is not correct", ConsoleColor.Red);
                    Thread.Sleep(1000);
                    goto AreYouSureDescription;
                }
                if (decision == 'Y')
                {
                    Create();
                }
            }
            else
            {
            EnterNameOfGroup: ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter Name:=*=*=*=*=*=*=*=", ConsoleColor.DarkCyan);
                string name = Console.ReadLine();
                var group = _groupRepository.GetByName(name);
                if (group == null)
                {
                    ConsoleHelper.WriteWithColor("There is no any group in this Name", ConsoleColor.Red);
                    Thread.Sleep(1000);
                    goto EnterNameOfGroup;
                }
                
                else
                {
                    ConsoleHelper.WriteWithColor($" Id: {group.Id} \n Name: {group.Name} \n Max Size: {group.MaxSize} \n Start Date: {group.StartDate} \n End Date: {group.EndDate} \n ", ConsoleColor.DarkMagenta);
                }
                
              
               
            }

        }

        public void Delete()
        {
            var groupss = _groupRepository.GetAll();
            ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=ALL GROUPS=*=*=*=*=*=*=*=", ConsoleColor.DarkCyan);
            foreach (var group in groupss)
            {
                ConsoleHelper.WriteWithColor($"Name: {group.Name} \n  Id: {group.Id} \n Max Size: {group.MaxSize} \n Start date: {group.StartDate.ToShortDateString()} \n End date: {group.EndDate.ToShortDateString()} \n", ConsoleColor.Magenta);
            }
        IdDescription: ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter Id*=*=*=*=*=*=*=", ConsoleColor.DarkCyan);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Id is not correct format!", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto IdDescription;
            }
            var dbGroup = _groupRepository.Get(id);
            if (dbGroup == null)
            {
                ConsoleHelper.WriteWithColor("There is no any group in this id", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto IdDescription;
            }
            else
            {
                _groupRepository.Delete(dbGroup);
                ConsoleHelper.WriteWithColor("Group successfully deleted", ConsoleColor.Green);
            }

        }
        public void Exit()
        {
        AreYouSureDescription: ConsoleHelper.WriteWithColor("Are you sure? <<<<Y>>>> or <<<<N>>>>", ConsoleColor.DarkCyan);
            char decision; 
            bool isSucceeded = char.TryParse(Console.ReadLine(), out decision);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Your choice is not correct format", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto AreYouSureDescription;
            }
            if(!(decision == 'Y' || decision == 'N'))
            {
                ConsoleHelper.WriteWithColor("Your choice is not correct", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto AreYouSureDescription;
            }
            if (decision == 'Y')
            {
                return;
            }

        }
        public void Update()
        {
            GetAll();
        EnterGroupDescription: ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter group:=*=*=*=*=*=*=*= \n 1. ID \n or \n 2. Name", ConsoleColor.DarkCyan);
            int number;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out number);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Inputed number is not correct format!", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto EnterGroupDescription;
            }
            if(!(number == 1 || number== 2))
            {
                ConsoleHelper.WriteWithColor("Inputed number is not correct!", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto EnterGroupDescription;

            }
            if(number== 1)
            {
            EnterGroupIdDescription: ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter group Id=*=*=*=*=*=*=*=", ConsoleColor.DarkCyan);
                int id;
                isSucceeded = int.TryParse(Console.ReadLine(), out id);
                if(!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Inputed Id is not correct format!", ConsoleColor.Red);
                    Thread.Sleep(1000);
                    goto EnterGroupIdDescription;
                }
                var group = _groupRepository.Get(id);
                if (group == null)
                {
                    ConsoleHelper.WriteWithColor("There is no any group in this Id!", ConsoleColor.Red);
                    Thread.Sleep(1000);
                    goto EnterGroupIdDescription;
                }
                InternalUpdate(group);
            }
            else
            {
            EnterGroupNameDescription:  ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter group Name=*=*=*=*=*=*=*=", ConsoleColor.DarkCyan);
                string name = Console.ReadLine();
                var group = _groupRepository.GetByName(name);
                if (group == null)
                {
                    ConsoleHelper.WriteWithColor("There is no any group in this Name!", ConsoleColor.Red);
                    Thread.Sleep(1000);
                    goto EnterGroupNameDescription;
                }
                InternalUpdate(group);


            }
        }

        private void InternalUpdate(Group group)
        {
           
          
           
                ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter new Name:=*=*=*=*=*=*=*=", ConsoleColor.DarkCyan);
                string name = Console.ReadLine();

            MaxSizeDescription: ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter new Max size=*=*=*=*=*=*=*=", ConsoleColor.DarkCyan);
                int maxSize;
                bool isSucceeded = int.TryParse(Console.ReadLine(), out maxSize);

                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Max size is not correct format!", ConsoleColor.Red);
                    goto MaxSizeDescription;
                }
                if (maxSize > 18)
                {
                    ConsoleHelper.WriteWithColor("Max size must be less or equal then 18!", ConsoleColor.Red);
                    Thread.Sleep(1000);
                    goto MaxSizeDescription;

                }
            StartDateDescription: ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter new Start date:=*=*=*=*=*=*=*=", ConsoleColor.DarkCyan);
                DateTime startDate;
                isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Start date is not correct format!", ConsoleColor.Red);
                    Thread.Sleep(1000);
                    goto StartDateDescription;
                }
            EndDateDescription: ConsoleHelper.WriteWithColor("=*=*=*=*=*=*=*=Enter new End date:=*=*=*=*=*=*=*=", ConsoleColor.DarkCyan);
                DateTime endDate;
                isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("End date is not correct format!", ConsoleColor.Red);
                    Thread.Sleep(1000);
                    goto EndDateDescription;
                }
                group.Name = name;
                group.StartDate = startDate;
                group.EndDate = endDate;
                group.MaxSize = maxSize;
                _groupRepository.Update(group);
            
        }
    }
}
