using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace YOLOTOL.Models
{
    public class Productos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [StringLength(25, ErrorMessage = "Máximo de carácteres permitidos")]
        [Display(Name = "Nombre")]
        public String Nombre { get; set; }

        //[Required(ErrorMessage = "El campo es obligatorio")]
        //[StringLength(100, ErrorMessage = "Máximo de carácteres permitidos")]
        //[Display(Name = "Imagen")]
        public String Imagen { get; set; }

        [NotMapped]
        //[DisplayName("Imagen producto")]
        public IFormFile Archivo { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo precio es obligatorio")]
        public decimal Precio { get; set; }

        [Display(Name = "Stock")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [StringLength(250, ErrorMessage = "Máximo de carácteres permitidos")]
        [Display(Name = "Descripcion")]
        public String Descripcion { get; set; }

        [Display(Name = "CategoríaID")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public int IdCategoria { get; set; }
        public Categorias categoria { get; set; }

       

    }
}
