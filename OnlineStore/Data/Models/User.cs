using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.Models
{
    public class User : IdentityUser
    {
        public User(DateTime birthday, string address, string firstName, string lastName, string phoneNumber, string email)
        {
            Birthday = birthday;
            Address = address;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            UserName = email;
        }

        public User(string userName)
        {
            Email = userName;
            UserName = userName;
        }

        public DateTime Birthday { get; set; }

        public string Address { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }



    }
}
