using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaBusquedaBiblioteca
{
    // crear una caja para guardar la info de cada libro
    class Libro
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int Anio { get; set; }
        public string Descripcion { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Crear la base de datos
            List<Libro> biblioteca = new List<Libro>
            {
                new Libro { Titulo = "Cien Años de Soledad", Autor = "Gabriel Garcia Marquez", Anio = 1967, Descripcion = "Historia de la familia Buendía." },
                new Libro { Titulo = "Don Quijote", Autor = "Miguel de Cervantes", Anio = 1605, Descripcion = "Un caballero que lucha contra molinos." },
                new Libro { Titulo = "El Principito", Autor = "Antoine de Saint-Exupery", Anio = 1943, Descripcion = "Un niño viaja por planetas." },
                new Libro { Titulo = "Harry Potter", Autor = "J.K. Rowling", Anio = 1997, Descripcion = "Un niño mago va a la escuela." },
                new Libro { Titulo = "1984", Autor = "George Orwell", Anio = 1949, Descripcion = "Distopía sobre el control totalitario." }
            };

            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("=== BIBLIOTECA DIGITAL ===");
                Console.WriteLine("1. Buscar libro por Título (Lineal)");
                Console.WriteLine("2. Buscar por Autor (Binaria)");
                Console.WriteLine("3. Ver libro más nuevo y más viejo");
                Console.WriteLine("4. Buscar en descripciones");
                Console.WriteLine("5. Salir");
                Console.Write("Elige una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        BusquedaLineal(biblioteca);
                        break;
                    case "2":
                        BusquedaBinariaAutores(biblioteca);
                        break;
                    case "3":
                        BuscarMasNuevoYViejo(biblioteca);
                        break;
                    case "4":
                        BuscarEnDescripcion(biblioteca);
                        break;
                    case "5":
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
                Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }

        // BuSQUEDA LINEAL 
        // Recorrer uno por uno hasta encontrarlo.
        static void BusquedaLineal(List<Libro> libros)
        {
            Console.Write("Ingresa el título del libro: ");
            string busqueda = Console.ReadLine().ToLower();

            bool encontrado = false;

            foreach (var libro in libros)
            {
                if (libro.Titulo.ToLower() == busqueda)
                {
                    Console.WriteLine($"encontrado Libro: {libro.Titulo}, Autor: {libro.Autor}");
                    encontrado = true;
                    break; 
                }
            }

            if (!encontrado) Console.WriteLine("Libro no encontrado.");
        }

        //BuSQUEDA BINARIA ---
        // Dividir la lista a la mitad repetidamente. 
        static void BusquedaBinariaAutores(List<Libro> libros)
        {
            // ordeno la lista por autor
            var listaOrdenada = libros.OrderBy(x => x.Autor).ToList();

            Console.WriteLine("Lista de autores disponibles (Ordenada):");
            foreach (var l in listaOrdenada) Console.WriteLine("- " + l.Autor);

            Console.Write("\nIngresa el nombre exacto del autor a buscar: ");
            string buscado = Console.ReadLine();

            int inicio = 0;
            int fin = listaOrdenada.Count - 1;
            bool encontrado = false;

            while (inicio <= fin)
            {
                int medio = (inicio + fin) / 2;
                // Compara strings. 0 significa que son iguales.
                int comparacion = String.Compare(listaOrdenada[medio].Autor, buscado, StringComparison.OrdinalIgnoreCase);

                if (comparacion == 0)
                {
                    Console.WriteLine($"autor encontrado escribio: {listaOrdenada[medio].Titulo}");
                    encontrado = true;
                    break;
                }
                else if (comparacion < 0) // El buscado esta en la mitad derecha
                {
                    inicio = medio + 1;
                }
                else // El buscado esta en la mitad izquierda
                {
                    fin = medio - 1;
                }
            }

            if (!encontrado) Console.WriteLine("Autor no encontrado.");
        }

        // min y max
        static void BuscarMasNuevoYViejo(List<Libro> libros)
        {
            // asumimos que el primero es el mas viejo y el mas nuevo y comparamos
            Libro masViejo = libros[0];
            Libro masNuevo = libros[0];

            foreach (var libro in libros)
            {
                if (libro.Anio < masViejo.Anio) masViejo = libro;
                if (libro.Anio > masNuevo.Anio) masNuevo = libro;
            }

            Console.WriteLine($"El libro mas antiguo es: {masViejo.Titulo} ({masViejo.Anio})");
            Console.WriteLine($"El libro mas reciente es: {masNuevo.Titulo} ({masNuevo.Anio})");
        }

        // coincidencias
        static void BuscarEnDescripcion(List<Libro> libros)
        {
            Console.Write("Ingresa una palabra clave (ej: mago, nino, historia): ");
            string palabra = Console.ReadLine().ToLower();

            Console.WriteLine("Resultados:");
            foreach (var libro in libros)
            {
                if (libro.Descripcion.ToLower().Contains(palabra))
                {
                    Console.WriteLine($"- {libro.Titulo}: {libro.Descripcion}");
                }
            }
        }
    }
}