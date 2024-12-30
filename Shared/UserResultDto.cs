using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record UserResultDto
    {
        private string v;

        public UserResultDto(string displayName, string? email, string token)
        {
            this.DisplayName = displayName;
            this.Email = email; 
            this.Token = token;
        }

        public string DisplayName { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
    }
}
