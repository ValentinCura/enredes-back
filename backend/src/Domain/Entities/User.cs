using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        // Id [int] IDENTITY(1,1)
        public int Id { get; set; }

        [Required]
        public string Firstname { get; set; } = string.Empty;

        public string? Lastname { get; set; }
        public string? Phonenumber { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Type { get; set; } = "Client";

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool Status { get; set; } = true;
        public string? PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpiry { get; set; }

    }
}
