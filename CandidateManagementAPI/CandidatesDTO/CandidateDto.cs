using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CandidateManagementAPI.NewFolder
{
    public class CandidateDto
    {
       
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            public string PhoneNumber { get; set; }
            [Required]
            public string Email { get; set; }
            public string CallInterval { get; set; }
            public string LinkedInProfile { get; set; }
            public string GitHubProfile { get; set; }
            [Required]
            public string Comments { get; set; }
            
        }
    }
