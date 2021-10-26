using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityRegistrar.Models.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public int Credits { get; set; }
        public int Year { get; set; }
        public int FacultyId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public Faculty Faculty { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
