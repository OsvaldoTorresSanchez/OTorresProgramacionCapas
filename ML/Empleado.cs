using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ML
{
    public class Empleado
    {

        public int IdEmpleado {  get; set; }

        [Display(Name = "Nombre Empleado")]
        public string NombreEmpleado {  get; set; }
        public string RFC {  get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre {  get; set; }

        [Required(ErrorMessage = "El Apellido es obligatorio")]
        [Display(Name = "Apellido Paterno")]
        public string ApellidoPaterno {  get; set; }

        [Display(Name = "ApellidoMaterno")]
        public string ApellidoMaterno {  get; set; }

        [Required(ErrorMessage = "El Email es obligatorio")]
        public string Email {  get; set; }

        [Required(ErrorMessage = "El Telefono es obligatorio")]
        public string Telefono {  get; set; }

        [Display(Name = "Fecha Nacimiento")]
        public string FechaNacimiento {  get; set; }
        public string NSS {  get; set; }

        [Display(Name = "Fecha Ingreso")]
        public string FechaIngreso {  get; set; } 
        public byte[] Foto {  get; set; }
        public ML.Empresa Empresa { get; set; }

        public List<object> Empleados { get;set; }

        public string Archivo {  get; set; } 

        public string Error {  get; set; } 


    }
}
