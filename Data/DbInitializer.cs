using System;
using Core.Entities;
using Core.Helpers;
using Data.Contexts;

namespace Data
{
	public static class DbInitializer
	{
		static int id;

		public static void SeedAdmins()
		{
			var admins = new List<Admin>
			{

				new Admin
				{
					Id = ++id,
					Username = "AdminFatima",
					Password  = PasswordHasher.Encrypt("28081997"),
					CreatedBy = "System"

				},

				new Admin
				{

					Id = ++id,
					Username = "AdminAzer",
					Password= PasswordHasher.Encrypt("20071997"),
					CreatedBy = "System"

				}


			};

			DbContext.Admins.AddRange(admins);


		}
	}
}

