using System;
using Core.Entities;
using Data.Contexts;
using Data.Repositoriesofmethods.Abstract;

namespace Data.Repositoriesofmethods.Concrete
{
    public class TeacherRepository : ITeacherRepository
    {
        static int id;
        public List<Teacher> GetAll()
        {
            return DbContext.Teachers;
        }

        public Teacher Get(int id)
        {
            return DbContext.Teachers.FirstOrDefault(t => t.Id == id);
        }

       
        public void Add(Teacher teacher)
        {
            id++;
            teacher.Id = id;
            DbContext.Teachers.Add(teacher);
        }

        public void Update(Teacher teacher)
        {
            var dbTeacher = DbContext.Teachers.FirstOrDefault(t => t.Id == teacher.Id);
            if (dbTeacher is not null)
            {
                dbTeacher.Name = teacher.Name;
                dbTeacher.Surname = teacher.Surname;
                dbTeacher.BirthDate = teacher.BirthDate;
                dbTeacher.Speciality = teacher.Speciality;

            }
        }

        public void Delete(Teacher teacher)
        {
            DbContext.Teachers.Remove(teacher);
        }

        

        
    }
}

