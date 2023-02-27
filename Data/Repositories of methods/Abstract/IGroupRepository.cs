using Core.Entities;
using Data.Repositoriesofmethods.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories_of_methods.Abstract
{
    internal interface IGroupRepository: IRepository<Group>
    {


        Group GetByName(string name);

    }
}
