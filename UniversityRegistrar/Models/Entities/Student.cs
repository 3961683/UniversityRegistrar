using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
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

        [JsonIgnore]
        public virtual User User { get; set; }
        [JsonIgnore]
        public virtual Faculty Faculty { get; set; }
        [JsonIgnore]
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
