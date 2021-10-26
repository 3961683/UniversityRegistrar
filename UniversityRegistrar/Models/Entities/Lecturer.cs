using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityRegistrar.Models.Entities
{
    public class Lecturer
    {
        public int Id { get; set; }
        public int FacultyId { get; set; }
        public string UserId { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public User User { get; set; }
        public Faculty Faculty { get; set; }
    }
}
