using System;
using Core.Entities;
using Core.Helpers;
using Data.Repositoriesofmethods.Concrete;

namespace Presentation.Services
{
	public class AdminService
	{
        private readonly AdminRepository _adminRepository;
        public AdminService()
        {
            _adminRepository = new AdminRepository();
        }
		public Admin Authorize()
		{
           LoginDescription: ConsoleHelper.WriteWithColor("<.><.><.>Login<.><.><.>", ConsoleColor.Magenta);
            Thread.Sleep(1000);
            ConsoleHelper.WriteWithColor("***************Enter Username:***************", ConsoleColor.Magenta);
            string username = Console.ReadLine();
            Thread.Sleep(1000);
            ConsoleHelper.WriteWithColor("***************Enter Password:***************", ConsoleColor.Magenta);
            string password = Console.ReadLine();

            var admin = _adminRepository.GetByUsernameAndPassword(username, password);
            if (admin is null)
            {
                ConsoleHelper.WriteWithColor("Username or password are incorrect!", ConsoleColor.Red);
                Thread.Sleep(1000);
                goto LoginDescription;

            }
            return admin;

        }
	}
}

