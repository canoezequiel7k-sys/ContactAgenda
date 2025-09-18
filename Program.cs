using System;
using ContactAgenda.Models;
using ContactAgenda.Services;

namespace ContactAgenda
{
    class Program
    {
        static void Main(string[] args)
        {
            ContactManager manager = new ContactManager("contacts.json");
            int option = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("==== AGENDA DE CONTACTOS ====");
                Console.WriteLine("1. Agregar contacto");
                Console.WriteLine("2. Listar contactos");
                Console.WriteLine("3. Buscar contacto");
                Console.WriteLine("4. Editar contacto");
                Console.WriteLine("5. Eliminar contacto");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Opción inválida. Presione una tecla para continuar...");
                    Console.ReadKey();
                    continue;
                }

                Console.Clear();
                switch (option)
                {
                    case 1:
                        manager.AddContact();
                        break;
                    case 2:
                        manager.ListContacts();
                        break;
                    case 3:
                        manager.SearchContact();
                        break;
                    case 4:
                        manager.EditContact();
                        break;
                    case 5:
                        manager.DeleteContact();
                        break;
                    case 0:
                        Console.WriteLine("Saliendo...");
                        break;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }

                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();
            } while (option != 0);
        }
    }
}