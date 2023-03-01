using System;
using Core.Entities;

namespace Data.Repositoriesofmethods.Abstract
{
	public interface IGroupFieldRepository: IRepository<GroupField>
	{
		GroupField GetByName(string name);

	}
}

