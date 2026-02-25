using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        // Id [int] IDENTITY(1,1)
        public int Id { get; set; }

        // [ClientNumber] [nvarchar](50) - Unique
        public string ClientNumber { get; set; } = string.Empty;

        // [Firstname] [nvarchar](50)
        public string Firstname { get; set; } = string.Empty;

        // [Lastname] [nvarchar](50)
        public string Lastname { get; set; } = string.Empty;

        // [Email] [nvarchar](255) - Unique
        public string Email { get; set; } = string.Empty;

        // [Phonenumber] [varchar](20)
        public string Phonenumber { get; set; } = string.Empty;

        // [Password] [varchar](255)
        public string Password { get; set; } = string.Empty;

        // [Type] [varchar](20) - Default 'Client' (Check: Client o Admin)
        public string Type { get; set; } = "Client";

        // [CreatedAt] [datetime] - Default getdate()
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
