using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contexts
{
    public static class DbContext
    {
        static DbContext()
        {
            Groups = new List<Group>();

            Students = new List<Student>();

            Admins = new List<Admin>();

            Teachers = new List<Teacher>();
        }
         
        public static List<Group> Groups { get; set; }

        public static List<Student> Students { get; set; }

        public static List<Admin> Admins { get; set; }

        public static List<Teacher> Teachers { get; set; }
    }
}
