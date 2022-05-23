using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Classes
{
    [Serializable]
    public class User
    {
        public int ID { get; set; }
        public bool IsAdmin { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Patronymic { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        public User() { }

        public override string ToString()
        {
            return $"\tUser info\n\n" +
                   $"ID: {ID}\n" +
                   $"IsAdmin: {IsAdmin}\n" +
                   $"Login: {Login}\n" +
                   $"Password: {Password}\n\n" +
                   $"Name: {Name}\n" +
                   $"Surname: {Surname}\n" +
                   $"Patronymic: {Patronymic}\n" +
                   $"Phone Number: {PhoneNumber}\n" +
                   $"Address: {Address}";
        }
    }
}
