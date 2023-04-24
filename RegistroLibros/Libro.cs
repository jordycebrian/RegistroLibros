using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroLibros
{
    class Libro
    {


        #region ATRIBUTOS DE LIBRO


        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Autor { get; set; }

        public string Isbn { get; set; }

        public string FechaPublicacion { get; set; }

        public string Genero { get; set; }

        public DateTime FechaCreacion { get; set; }


        #endregion


    }
}
