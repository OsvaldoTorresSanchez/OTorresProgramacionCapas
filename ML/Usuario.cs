using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        
        [Required(ErrorMessage= "El Username es obligatorio") ]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre's")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido paterno es obligatorio")]
        [Display(Name = "Apellido Paterno")]
        public string ApellidoPaterno { get; set; }

        [Display(Name = "Apellido Materno")]
        public string ApellidoMaterno { get; set; }

       [EmailAddress(ErrorMessage = "Es necesario un correo")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
       // [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "El sexo es obligatorio")]
       // [StringLength(1, MinimumLength = 11)]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "El telefono es obligatorio de 10 digitos")]
       // [MaxLength(10), MinLength(10)]
        public string Telefono { get; set; }

        //[MaxLength(10), MinLength(10)]
        public string Celular { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd-MM-yyyy}")]
        [Display(Name = "Fecha de Nacimiento")]
        public string FechaNacimiento { get; set; }

       // [StringLength(18, MinimumLength = 18)]
        public string CURP { get; set; }

        public byte[] Imagen { get; set; }

        public bool Statu { get; set; }


        public ML.Rol Rol { get; set; }


        public ML.Direccion Direccion { get; set; }

        public List<Object> Usuarios { get; set; }


    }
}
