using System;
using Core.Entities;
using Core.Helpers;
using Data.Contexts;
using Data.Repositoriesofmethods.Concrete;

namespace Presentation.Services
{
	public class GroupFieldService
	{
		private readonly GroupFieldRepository _groupFieldRepository;
		public GroupFieldService()
		{
			_groupFieldRepository = new GroupFieldRepository();
		}
		public void Create()
		{
		

			GroupFieldDescription:  ConsoleHelper.WriteWithColor("|||||||||||||||Enter Name:|||||||||||||||", ConsoleColor.DarkMagenta);
			string name = Console.ReadLine();
			var groupField = _groupFieldRepository.GetByName(name);
			if (groupField is not null)
			{

				ConsoleHelper.WriteWithColor("This group field already exsit!", ConsoleColor.Red);
				Thread.Sleep(1000);
				goto GroupFieldDescription;

			}

			groupField = new GroupField
			{

				Name = name

			};

			_groupFieldRepository.Add(groupField);

			ConsoleHelper.WriteWithColor($"{groupField.Name} - is successfully added!", ConsoleColor.Green);

		}

		public void Update()
		{

			_groupFieldRepository.GetAll();
			EnterFieldIdDescription: ConsoleHelper.WriteWithColor("||||||||||Enter Field Id|||||||", ConsoleColor.DarkCyan);
			int id;
			bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
			if (!isSucceeded)
			{
				ConsoleHelper.WriteWithColor("Field's id is not correct format!", ConsoleColor.Red);
				Thread.Sleep(1000);
				goto EnterFieldIdDescription;
			}

			var field = _groupFieldRepository.Get(id);
			if (field is null)
			{

                ConsoleHelper.WriteWithColor("There is no any Field in this Id!", ConsoleColor.Red);

            }

            ConsoleHelper.WriteWithColor("**********Enter new Field Name:**********", ConsoleColor.Yellow);
			string name = Console.ReadLine();
			field.Name = name;
            ConsoleHelper.WriteWithColor($"{field.Name} - is successfully updated!", ConsoleColor.Green);
        }

		public void GetAll()
		{

			var groupFields = _groupFieldRepository.GetAll();
			if (groupFields.Count == 0)
			{

				ConsoleHelper.WriteWithColor("There is no any groud field!", ConsoleColor.Red);

			}
			else
			{

				foreach (var groupField in groupFields)
				{
					ConsoleHelper.WriteWithColor($" Name: {groupField.Name}", ConsoleColor.Yellow);
				}


			}
		}

		public void Remove()
		{
			GetAll();
			FieldIdDescription:  ConsoleHelper.WriteWithColor("*********Enter group field Id to remove", ConsoleColor.Yellow);
			int id;
			bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
			if (!isSucceeded)
			{


				ConsoleHelper.WriteWithColor("Wrong input!", ConsoleColor.Red);

				Thread.Sleep(1000);
				goto FieldIdDescription;
			}

			var groupField =  _groupFieldRepository.Get(id);
			if (groupField is null)
			{

				ConsoleHelper.WriteWithColor("There is no any Group Field!", ConsoleColor.Red);
			}

			_groupFieldRepository.Delete(groupField);

			ConsoleHelper.WriteWithColor($"Name; {groupField.Name} - is successfully deleted!", ConsoleColor.Green);








		}

	}
}

