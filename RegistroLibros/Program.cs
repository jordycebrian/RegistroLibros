using System;
using System.Collections.Generic;
using ConsoleTables;


namespace RegistroLibros
{
    class Program
    {
        static void Main()
        {
            int opcion = 0;

            #region OPCIONES DE MENÚ

            string menu = @"
                [1] Ver libros guardados
                [2] Buscar libro por clave
                [3] Agregar libro
                [4] Editar informacion del libro
                [5] Borrar libro
                [6] Para regresar al menu
                [7] Salir de la aplicación
                ";

            #endregion


            #region MARCO MENÚ
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("--------------- MENÚ PRINCIPAL REGISTRO LIBROS -------------\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(menu);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n------------------------------------------------------------");
            Console.ResetColor();
            #endregion


            while (opcion != 7)
            {
                #region Manejo de entrada

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\nIngrese el número de la opción deseada: ");
                string entrada = Console.ReadLine();

                #endregion

                if (int.TryParse(entrada, out opcion))
                {

                    #region MOSTRAR LIBROS

                    if (opcion == 1)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;

                        Console.WriteLine("Libros Almacenados\n");

                        Logica.MostrarLibros();
                    }

                    #endregion


                    #region BUSCAR LIBRO POR CLAVE

                    if (opcion == 2)
                    {
                        Console.Clear();
                        Console.WriteLine("Ingrese la clave del libro a buscar");
                        int id = int.Parse(Console.ReadLine());

                        Libro libro = Logica.BuscarLibro(id);

                        if (libro != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"Clave: {libro.Id}");
                            Console.WriteLine($"Titulo: {libro.Titulo}");
                            Console.WriteLine($"Autor: {libro.Autor}");
                            Console.WriteLine($"Isbn: {libro.Isbn}");
                            Console.WriteLine($"Fecha de Publicación: {libro.FechaPublicacion}");
                            Console.WriteLine($"Genero: {libro.Genero}");
                        }
                    }
                    Console.WriteLine("\nPresiona 6 para regresar al menú");
                    #endregion


                    #region AGREGAR LIBRO

                    if (opcion == 3)
                    {

                        Console.Clear();
                        Console.WriteLine("Opcion agregar tu libro seleccionada\n");
                        Libro libro = new Libro();
                        libro = Logica.SolicitarDatosLibro(libro);

                        bool result = Logica.AgregarLibro(libro);

                        if (result)
                        {
                            Console.WriteLine("Libro agregado con exito");
                        }

                        Console.WriteLine("\nOpcion 6 para regresar al menú");
                    }

                    #endregion


                    #region EDITAR LIBRO

                    if (opcion == 4)
                    {
                        Console.Clear();
                        Console.WriteLine("Selecciona el libro a editar\n");
                        Console.WriteLine("\nPreciona 6 para regresar al menú");

                        Logica.MostrarLibros();

                        Console.WriteLine("Escribe la clave del libro a editar");

                        int id = int.Parse(Console.ReadLine());

                        bool result = Logica.EditarLibro(id);

                        if (result)
                        {
                            Console.WriteLine("\nLibro editado con exito !!!");
                            Console.WriteLine("\nPreciona 6 para regresar al menú");
                        }


                    }

                    #endregion


                    #region BORRAR LIBRO

                    if (opcion == 5)
                    {
                        Console.Clear();
                        Console.WriteLine("\npresiona 6 para regresar al menú");
                        Console.WriteLine("OPCION BORRAR LIBRO SELECCIONADA\n");

                        Logica.MostrarLibros();

                        Console.Write("\nIngresa la clave del libro para borrarlo: ");
                        int id = int.Parse(Console.ReadLine());

                        bool result = Logica.BorrarLibro(id);

                        if (result)
                        {
                            Console.WriteLine("\nLibro borrado con exito!!!!");
                            Console.WriteLine("\nPresiona 6 para regresar al menú");
                        }


                    }

                    #endregion


                    #region EFECTO RETORNO AL MENÚ

                    if (opcion == 6)
                    {
                        Console.Clear();
                        Console.WriteLine("--------------- MENÚ PRINCIPAL REGISTRO LIBROS -------------\n");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(menu);
                        Console.WriteLine("\n------------------------------------------------------------");
                    }

                    #endregion


                }
                else
                {
                    Console.WriteLine("Opcion no disponible");
                    continue;
                }
            }
        }
    }
}
