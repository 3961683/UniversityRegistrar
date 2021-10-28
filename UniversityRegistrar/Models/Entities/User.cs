using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace UniversityRegistrar.Models.Entities
{
    public class User: IdentityUser
    {
        [JsonIgnore]
        public virtual Student Student { get; set; }
        [JsonIgnore]
        public virtual Lecturer Lecturer { get; set; }
    }
}