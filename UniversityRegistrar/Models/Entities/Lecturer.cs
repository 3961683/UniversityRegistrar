using System.Text.Json.Serialization;

namespace UniversityRegistrar.Models.Entities
{
    public class Lecturer
    {
        public int Id { get; set; }
        public int FacultyId { get; set; }
        public string UserId { get; set; }
        public int CourseId { get; set; }

        [JsonIgnore]
        public virtual Course Course { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
        [JsonIgnore]
        public virtual Faculty Faculty { get; set; }
    }
}
