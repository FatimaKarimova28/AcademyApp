using System;
using Core.Entities;

namespace Data.Repositoriesofmethods.Abstract
{
	public interface IAdminRepository
	{
		Admin GetByUsernameAndPassword(string username, string password);

	}
}

