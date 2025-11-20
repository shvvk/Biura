using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biura.Model
{
    public abstract class Person
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Client : Person
    {
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }

        public Client()
        { 
            DateOfBirth = DateTime.MinValue;
            Email = string.Empty;
        }
        public Client(string fn, string ln, DateTime dob, string email)
        {
            FirstName = fn;
            LastName = ln;
            DateOfBirth = dob;
            Email = email;
        }
        public override string ToString()
        {
            return $"klient nazywa sie {FirstName} {LastName} ma {DateTime.Now.Year - DateOfBirth.Year} lat";
        }
    }

    public class Owner : Person
    {
        public Owner() 
        { 
            FirstName = string.Empty;
            LastName = string.Empty;
        }
        public Owner(string fn, string ln)
        {
            FirstName = fn;
            LastName = ln;
        }
        public override string ToString()
        {
            return $"Właściciel nazywa sie {FirstName} {LastName}";
        }
    }
}
