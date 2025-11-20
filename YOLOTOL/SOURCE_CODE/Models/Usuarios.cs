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
    public class Usuarios
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }

        //[Required(ErrorMessage = "El campo es obligatorio")]
        //[StringLength(50, ErrorMessage = "Máximo de carácteres permitidos")]
        //[Display(Name = "Nombre")]
        public String nombre { get; set; }

        //[Required(ErrorMessage = "El campo es obligatorio")]
        //[StringLength(50, ErrorMessage = "Máximo de carácteres permitidos")]
        //[Display(Name = "Apellido paterno")]
        public String apellidoP { get; set; }

        //[Required(ErrorMessage = "El campo es obligatorio")]
        //[StringLength(50, ErrorMessage = "Máximo de carácteres permitidos")]
        //[Display(Name = "Apellido materno")]
        public String apellidoM { get; set; }

        //[Required(ErrorMessage = "El campo es obligatorio")]
        //[StringLength(50, ErrorMessage = "Máximo de carácteres permitidos")]
        //[Display(Name = "Correo")]
        public String correo { get; set; }

        //[Required(ErrorMessage = "El campo es obligatorio")]
        //[StringLength(50, ErrorMessage = "Máximo de carácteres permitidos")]
        //[Display(Name = "Contraseña")]
        public String contrasenia { get; set; }

        //[Required(ErrorMessage = "El campo es obligatorio")]
        //[Display(Name = "Fecha de Nacimiento")]
        public DateTime fechaNacimiento { get; set; }

        //[Required(ErrorMessage = "El campo es obligatorio")]
        //[Display(Name = "FotoPerfil")]
        public String fotoPerfil { get; set; }

        [NotMapped]
        //[DisplayName("Archivo de foto de perfil")]
        public IFormFile archivo { get; set; }

        //[StringLength(12, ErrorMessage = "Máximo de carácteres permitidos")]
        //[Display(Name = "Cedula")]
        public String cedula { get; set; }

        //[StringLength(50, ErrorMessage = "Máximo de carácteres permitidos")]
        //[Display(Name = "Telefono")]
        public String telefono { get; set; }

   
         public string tipoUsuario { get; set; }

    }
}
