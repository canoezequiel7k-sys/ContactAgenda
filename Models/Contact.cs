using System;
using System.Linq;

namespace ContactAgenda.Models
{
    public class Contact
    {
        public string Name { get; set; }
        public string Phone { get; set; }

        public Contact(string name, string phone)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre no puede estar vacío.");
            if (string.IsNullOrWhiteSpace(phone) || !phone.All(char.IsDigit))
                throw new ArgumentException("El teléfono debe contener solo números.");

            Name = name;
            Phone = phone;
        }

        public override string ToString()
        {
            return $"{Name} - {Phone}";
        }
    }
}