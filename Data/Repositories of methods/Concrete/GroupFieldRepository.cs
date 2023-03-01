using System;
using Core.Entities;
using Data.Contexts;
using Data.Repositoriesofmethods.Abstract;

namespace Data.Repositoriesofmethods.Concrete
{
    public class GroupFieldRepository : IGroupFieldRepository
    {
        static int id;

        public List<GroupField> GetAll()
        {
            return DbContext.GroupFields;
        }

        public GroupField Get(int id)
        {
            return DbContext.GroupFields.FirstOrDefault(g => g.Id == id);
        }

     
        public void Add(GroupField groupField)
        {
            id++;
            groupField.Id = id;
            DbContext.GroupFields.Add(groupField);
        }

      

        public void Update(GroupField groupField)
        {
            var dbGroupField = DbContext.GroupFields.FirstOrDefault(g => g.Id == groupField.Id);
            if (groupField is not null)
            {
                dbGroupField.Name = groupField.Name;
                

            }
        }

        public void Delete(GroupField groupField)
        {
            DbContext.GroupFields.Remove(groupField);
        }

        public GroupField GetByName(string name)
        {
            return DbContext.GroupFields.FirstOrDefault(g => g.Name == name);



        }

    }
}

