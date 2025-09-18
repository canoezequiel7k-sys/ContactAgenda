using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ContactAgenda.Models;

namespace ContactAgenda.Services
{
    public class ContactManager
    {
        private readonly string _filePath;
        private List<Contact> contacts;

        public ContactManager(string filePath)
        {
            _filePath = filePath;
            contacts = LoadContacts();
        }

        public void AddContact()
        {
            Console.Write("Nombre: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Teléfono: ");
            string phone = Console.ReadLine() ?? "";

            try
            {
                Contact contact = new Contact(name, phone);
                contacts.Add(contact);
                SaveContacts();
                Console.WriteLine("Contacto agregado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void ListContacts()
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine("No hay contactos guardados.");
                return;
            }

            Console.WriteLine("==== CONTACTOS ====");
            foreach (var c in contacts)
                Console.WriteLine(c);
        }

        public void SearchContact()
        {
            Console.Write("Ingrese nombre a buscar: ");
            string name = Console.ReadLine()?.ToLower() ?? "";

            var found = contacts.Where(c => c.Name.ToLower().Contains(name)).ToList();
            if (found.Count == 0)
            {
                Console.WriteLine("No se encontraron coincidencias.");
                return;
            }

            Console.WriteLine("Resultados:");
            foreach (var c in found)
                Console.WriteLine(c);
        }

        public void EditContact()
        {
            Console.Write("Ingrese nombre del contacto a editar: ");
            string name = Console.ReadLine()?.ToLower() ?? "";

            var contact = contacts.FirstOrDefault(c => c.Name.ToLower().Equals(name));
            if (contact == null)
            {
                Console.WriteLine("Contacto no encontrado.");
                return;
            }

            Console.Write("Nuevo nombre (dejar vacío para no cambiar): ");
            string newName = Console.ReadLine();
            Console.Write("Nuevo teléfono (dejar vacío para no cambiar): ");
            string newPhone = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(newName)) contact.Name = newName;
            if (!string.IsNullOrWhiteSpace(newPhone) && newPhone.All(char.IsDigit))
                contact.Phone = newPhone;

            SaveContacts();
            Console.WriteLine("Contacto actualizado correctamente.");
        }

        public void DeleteContact()
        {
            Console.Write("Ingrese nombre del contacto a eliminar: ");
            string name = Console.ReadLine()?.ToLower() ?? "";

            var contact = contacts.FirstOrDefault(c => c.Name.ToLower().Equals(name));
            if (contact == null)
            {
                Console.WriteLine("Contacto no encontrado.");
                return;
            }

            contacts.Remove(contact);
            SaveContacts();
            Console.WriteLine("Contacto eliminado correctamente.");
        }

        private void SaveContacts()
        {
            var json = JsonSerializer.Serialize(contacts, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        private List<Contact> LoadContacts()
        {
            if (!File.Exists(_filePath))
                return new List<Contact>();

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Contact>>(json) ?? new List<Contact>();
        }
    }
}