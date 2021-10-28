using System.Text.Json.Serialization;

namespace UniversityRegistrar.Models.Entities
{
    public class StudentCourse
    {
        public int Grade { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        [JsonIgnore]
        public virtual Student Student { get; set; }
        [JsonIgnore]
        public virtual Course Course { get; set; }
    }
}
