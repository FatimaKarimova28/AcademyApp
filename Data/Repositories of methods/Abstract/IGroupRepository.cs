﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories_of_methods.Abstract
{
    internal interface IGroupRepository
    {

        List<Group> GetAll();
        Group Get(int id);  
        void Add (Group group); 
        void Update (Group group);  
        void Delete (Group group);
        Group GetByName(string name);

    }
}
