using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string Type { get; set; } = "Client";
        public string Phonenumber { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
        public bool WebMail { get; set; } = true;
    }
}
