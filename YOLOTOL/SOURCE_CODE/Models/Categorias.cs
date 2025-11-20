using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace YOLOTOL.Models
{
    public class Categorias
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCategoria { get; set; }

        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        [Remote(action: "ExisteCategorias", controller: "Categorias")]
        [StringLength(100, ErrorMessage = "Máximo de carácteres permitidos")]
        public String Nombre { get; set; }


    }
}
