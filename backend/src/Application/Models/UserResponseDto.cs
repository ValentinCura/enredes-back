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
        public string FullName { get; set; } = string.Empty;
        public string ClientNumber { get; set; } = string.Empty; 
        public string Type { get; set; } = "Client";
        public string Phonenumber { get; set; } = string.Empty;
    }
}
