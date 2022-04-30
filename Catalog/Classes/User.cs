//using ReactiveValidation;
//using ReactiveValidation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Classes
{
    public class User //: ValidatableObject
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

        //public User()
        //{
        //    Validator = GetValidator();
        //}

        //private IObjectValidator GetValidator()
        //{
        //    var builder = new ValidationBuilder<User>();

        //    builder.RuleFor(u => u.Login).NotEmpty();
        //    builder.RuleFor(u => u.Password).NotEmpty().Length(8, 20);
        //    builder.RuleFor(u => u.Name).NotEmpty();
        //    builder.RuleFor(u => u.Surname).NotEmpty();
        //    builder.RuleFor(u => u.Patronymic).NotEmpty();
        //    builder.RuleFor(u => u.PhoneNumber).NotEmpty().Length(12);
        //    builder.RuleFor(u => u.Address).NotEmpty();

        //    return builder.Build(this);
        //}

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
