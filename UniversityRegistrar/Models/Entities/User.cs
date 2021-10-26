using Microsoft.AspNetCore.Identity;

namespace UniversityRegistrar.Models.Entities
{
    public class User: IdentityUser
    {
        public Student Student { get; set; }
        public Lecturer Lecturer { get; set; }
    }
}