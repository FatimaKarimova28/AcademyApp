using System;
using Core.Entities;

namespace Data.Repositoriesofmethods.Abstract
{
	public interface IStudentRepository: IRepository<Student>
	{


        bool IsDuplicateEmail(string email);

    }
}

