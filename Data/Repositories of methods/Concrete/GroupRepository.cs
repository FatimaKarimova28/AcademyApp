using Core.Entities;
using Data.Contexts;
using Data.Repositories_of_methods.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories_of_methods.Concrete
{
    public class GroupRepository : IGroupRepository
    {
        public Group GetByName(string name)
        {
           return DbContext.Groups.FirstOrDefault(g => g.Name.ToLower() == name.ToLower());
        }
        public List<Group> GetAll()
        {
            return DbContext.Groups;
        }
        public Group Get(int id)
        {
            return DbContext.Groups.FirstOrDefault(g => g.Id == id);
        }
        static int id;
        public void Add(Group group)
        {
            id++;
            group.Id = id;  
            group.CreatedAt = DateTime.Now; 
            DbContext.Groups.Add(group);
        }

        public void Delete(Group group)
        {
            DbContext.Groups.Remove(group); 
        }



        public void Update(Group group)
        {
            var dbGroup = DbContext.Groups.FirstOrDefault(g => g.Id == group.Id);
            if(dbGroup!= null)
            {
                dbGroup.Name= group.Name;
                dbGroup.MaxSize= group.MaxSize;
                dbGroup.StartDate= group.StartDate;
                dbGroup.EndDate= group.EndDate;
                dbGroup.ModifiedAt= DateTime.Now;   
            }
        }

    }
}
