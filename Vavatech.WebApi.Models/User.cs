using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Vavatech.WebApi.Models
{
    [KnownType(typeof(Employee))]
    [KnownType(typeof(Guest))]
    [XmlInclude(typeof(Employee))]
    [XmlInclude(typeof(Guest))]
    public abstract class User : EntityBase
    {
        // public string FullName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public class Employee : User
    {
        public decimal Salary { get; set; }
        public string Department { get; set; }
    }

    public class Guest : User
    {
        public string Token { get; set; }
    }


}
