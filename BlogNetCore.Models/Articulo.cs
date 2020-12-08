using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogNetCore.Models
{
    public class Articulo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingresa un nombre para el artículo")]
        [Display(Name = "Nombre artículo")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingresa la descripción para el artículo")]
        [Display(Name = "Descripción artículo")]
        public string Descripcion { get; set; }


        [Display(Name = "Fecha de creación")]
        public string FechaCreacion { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string UrlImagen { get; set; }

        //Relaciones
        #region ARTICULO_CATEGORIA
        [Required]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; } 
        #endregion
    }
}
