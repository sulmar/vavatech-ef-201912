using System;
using System.Collections.Generic;
using System.Text;

namespace Vavatech.WebApi.Models
{
    public class User : EntityBase
    {
        // public string FullName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
