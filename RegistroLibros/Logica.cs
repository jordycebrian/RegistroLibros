using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace RegistroLibros
{
    class Logica
    {
        #region CONEXION BASE DE DATOS

        private static string connectionString = "Data Source=DESKTOP-H2RJN7F;Initial Catalog=ConsoleProyectsDatabase;User=sa;Password=cero41";

        #endregion


        #region METODO PARA MANIPULAR OBJETO LIBRO

        public static List<Libro> Get()
        {
            List<Libro> libros = new List<Libro>();

            string query = "SELECT * FROM Libro";

            using (var connection = new SqlConnection(connectionString))
            {

                try
                {
                    connection.Open();
                    var command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var libro = new Libro();
                        libro.Id = reader.GetInt32(0);
                        libro.Titulo = reader.GetString(1);
                        libro.Autor = reader.GetString(2);
                        libro.Isbn = reader.GetString(3);
                        libro.FechaPublicacion = reader.GetString(4);
                        libro.Genero = reader.GetString(5);
                        libro.FechaCreacion = reader.GetDateTime(6);

                        libros.Add(libro);
                    }
                    return libros;


                }
                catch (Exception ex)
                {

                    Console.WriteLine("ERROR : " + ex.Message);
                    return null;
                }
                finally
                {
                    connection.Close();
                }

            }
        }

        #endregion


        #region METODO INSERTAR LIBRO NUEVO
        public static Libro SolicitarDatosLibro(Libro libro)
        {

            Console.WriteLine("Deseas continuar con la opcion? (S/N): ");
            string respuesta = Console.ReadLine();

            if(respuesta.ToLower() == "n")
            {
                return null;
            }

            Console.Write("Ingrese el titulo del libro: ");
            string titulo = Console.ReadLine();

            Console.Write("Ingrese el nombre del autor del libro: ");
            string autor = Console.ReadLine();

            Console.Write("Ingrese el Isbn del libro: ");
            string isbn = Console.ReadLine();

            Console.Write("Ingrese fecha de publicacion del libro: ");
            string fechaPublicacion = Console.ReadLine();

            Console.Write("Ingresar genero del libro: ");
            string genero = Console.ReadLine();

            
            return new Libro()
            {

                Titulo = titulo,
                Autor = autor,
                Isbn = isbn,
                FechaPublicacion = fechaPublicacion,
                Genero = genero,
                
            };
        }
        #endregion


        #region METODO MOSTRAR LIBROS

        public static void MostrarLibros()
        {
            List<Libro> libros = Logica.Get();

            var table = new ConsoleTable("Clave", "Titulo del libro", "Nombre del autor", "Isbn", " Fecha de Publicación", "Genero", "Fecha de registro");

            foreach (Libro libro in libros)
            {
                table.AddRow(libro.Id, libro.Titulo, libro.Autor, libro.Isbn, libro.FechaPublicacion, libro.Genero, libro.FechaCreacion.ToShortDateString());
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            table.Write(Format.Alternative);
        }


        #endregion


        #region METODO BUSCAR LIBRO 

        public static Libro BuscarLibro(int id)
        {
            string query = "SELECT * FROM Libro WHERE id=@id";

            using(var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);

                    var reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        var libro = new Libro();
                        libro.Id = reader.GetInt32(0);
                        libro.Titulo = reader.GetString(1);
                        libro.Autor = reader.GetString(2);
                        libro.Isbn = reader.GetString(3);
                        libro.FechaPublicacion = reader.GetString(4);
                        libro.Genero = reader.GetString(5);

                        return libro;
                    }
                    else
                    {
                        Console.WriteLine("El producto no existe");
                        return null;
                    }

                }
                catch (Exception ex)
                {

                    Console.WriteLine("ERROR : " + ex);
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        #endregion


        #region METODO AGREGAR LIBRO

        public static bool AgregarLibro(Libro libro)
        {
            if (libro == null)
            {
                return false;
            }

            string query = "INSERT INTO Libro(titulo,autor,isbn,fechaPublicacion,genero)" +
                "VALUES(@titulo,@autor,@isbn,@fechaPublicacion,@genero)";

            using(var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand(query,connection);
                    command.Parameters.AddWithValue("@titulo", libro.Titulo);
                    command.Parameters.AddWithValue("@autor", libro.Autor);
                    command.Parameters.AddWithValue("@isbn", libro.Isbn);
                    command.Parameters.AddWithValue("@fechaPublicacion", libro.FechaPublicacion);
                    command.Parameters.AddWithValue("@genero", libro.Genero);

                    Console.WriteLine($"Seguro que quieres agregar el libro {libro.Titulo}  si/no ");
                    string respuesta = Console.ReadLine();
                    if (respuesta.ToLower() == "si")
                    {
                        int result = command.ExecuteNonQuery();

                        return result > 0;
                    }
                    else
                    {
                        Console.WriteLine("Operacion cancelada :/");
                        return false;
                    }
                    
                }
                catch (Exception ex)
                {

                    Console.WriteLine("ERROR : " + ex.Message);
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        #endregion


        #region METODO EDITAR LIBRO

        public static bool EditarLibro(int id)
        {
            Libro libro = Logica.BuscarLibro(id);
            if (libro == null)
            {
                return false;
            }
            

            libro = SolicitarDatosLibro(libro);

            if (libro == null)
            {
                return false;
            }

            using (var connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Libro SET " +
                "titulo=@titulo,autor=@autor,isbn=@isbn,fechaPublicacion=@fechaPublicacion,genero=@genero WHERE id=@id";
                
                try
                {
                    
                    var command = new SqlCommand(query, connection);

                    connection.Open();

                    command.Parameters.AddWithValue("@titulo",libro.Titulo);
                    command.Parameters.AddWithValue("@autor", libro.Autor);
                    command.Parameters.AddWithValue("@isbn", libro.Isbn);
                    command.Parameters.AddWithValue("@fechaPublicacion", libro.FechaPublicacion);
                    command.Parameters.AddWithValue("@genero", libro.Genero);
                    command.Parameters.AddWithValue("@id", id);

                    int result = command.ExecuteNonQuery();

                    return result > 0;
                }
                catch (Exception ex)
                {

                    Console.WriteLine("ERROR : " + ex.Message);
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }


        }

        #endregion


        #region METODO BORRAR LIBRO

        public static bool BorrarLibro(int id)
        {
            Libro libro = Logica.BuscarLibro(id);
            if (libro == null)
            {
                return false;
            }

            string query = "DELETE FROM Libro WHERE id=@id";

            using(var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);

                    Console.WriteLine($"Seguro que quieres borrar el libro {libro.Titulo} (si/no)");
                    string respuesta = Console.ReadLine();

                    if (respuesta.ToLower() == "si")
                    {
                        int result = command.ExecuteNonQuery();
                        return result > 0;

                    }
                    else
                    {
                        Console.WriteLine("Operacion cancelada con exito!!");
                        return false;
                    }
                    
                }
                catch (Exception ex)
                {

                    Console.WriteLine("ERROR : " + ex.Message);
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        #endregion
    }
}
