using ML;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Usuario
    {
        public static void GetAll()
        {
            //ML.Result result = BL.Usuario.GetAll();
            //ML.Result result = BL.Usuario.GetAllSP();
            //ML.Result result = BL.Usuario.GetAllEF();
            ML.Result result = BL.Usuario.GetAllLinq();

            if (result.Correct)
            {
                Console.WriteLine("IdUsuario | UserName | | Nombre | ApellidoPaterno | ApellidoMaterno | Email | Password | Sexo | Telefono | Celular | FechaNacimiento | CURP | IdRol");
                foreach (ML.Usuario usuario in result.Objects)
                {

                    //Console.WriteLine(usuario.IdUsuario);
                    //Console.WriteLine(usuario.UserName);
                    //Console.WriteLine(usuario.Nombre);
                    //Console.WriteLine(usuario.ApellidoPaterno);
                    //Console.WriteLine(usuario.ApellidoMaterno);
                    //Console.WriteLine(usuario.Email);
                    //Console.WriteLine(usuario.Password);
                    //Console.WriteLine(usuario.Sexo);
                    //Console.WriteLine(usuario.Telefono);
                    //Console.WriteLine(usuario.Celular);
                    //Console.WriteLine(usuario.FechaNacimiento);
                    //Console.WriteLine(usuario.CURP);
                    //Console.WriteLine(usuario.Rol.IdRol);

                    // Console.WriteLine("\n");
                    Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine($"{usuario.IdUsuario} | {usuario.UserName}  | {usuario.Nombre}| {usuario.ApellidoPaterno} | {usuario.ApellidoMaterno} | {usuario.Email} |{usuario.Password} | {usuario.Sexo} | {usuario.Telefono} | {usuario.Celular} | {usuario.FechaNacimiento} | {usuario.CURP} | {usuario.Rol.IdRol}");
                    Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------");
                }

            }
            else
            {

                Console.WriteLine("No se encontraron registros en la tabla Usuario " + result.ErrorMessage);

            }
        }
        public static void GetById() 
        {
            ML.Usuario usuario = new ML.Usuario();
           // usuario.Rol = new Rol();
            Console.WriteLine("Ingrese el ID del usuario que deseas mostrar");
            usuario.IdUsuario = int.Parse(Console.ReadLine());

            //ML.Result result = BL.Usuario.GetById(usuario.IdUsuario);
            //ML.Result result = BL.Usuario.GetByIdSP(usuario.IdUsuario);
            //ML.Result result = BL.Usuario.GetByIdEF(usuario.IdUsuario);
            ML.Result result = BL.Usuario.GetByIdLinq(usuario.IdUsuario);

            if (result.Correct)
            {
                Console.WriteLine("IdUsuario | UserName | Nombre | ApellidoPaterno | ApellidoMaterno | Email | Password | Sexo | Telefono | Celular | FechaNacimiento | CURP | IdRol ");
                
                 usuario = (ML.Usuario)result.Object;
                //usuario.Rol = new ML.Rol();
                //Console.WriteLine(usuario.IdUsuario);
                //Console.WriteLine(usuario.UserName);
                //Console.WriteLine(usuario.Nombre);
                //Console.WriteLine(usuario.ApellidoPaterno);
                //Console.WriteLine(usuario.ApellidoMaterno);
                //Console.WriteLine(usuario.Email);
                //Console.WriteLine(usuario.Password);
                //Console.WriteLine(usuario.Sexo);
                //Console.WriteLine(usuario.Telefono);
                //Console.WriteLine(usuario.Celular);
                //Console.WriteLine(usuario.FechaNacimiento);
                //Console.WriteLine(usuario.CURP);
                //Console.WriteLine(usuario.Rol.IdRol);

                Console.WriteLine("\n");
               // Console.WriteLine("IdUsuario | UserName | ApellidoPaterno | ApellidoMaterno | Email | Password | Sexo | Telefono | Celular | FechaNacimiento | CURP | IdRol");
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"{usuario.IdUsuario} | {usuario.UserName} | {usuario.Nombre} |  {usuario.ApellidoPaterno} | {usuario.ApellidoMaterno} | {usuario.Email} |{usuario.Password} | {usuario.Sexo} | {usuario.Telefono} | {usuario.Celular} | {usuario.FechaNacimiento} | {usuario.CURP} | {usuario.Rol.IdRol}");

            }
            else
            {
                Console.WriteLine("No se encuentra los registros en la tabla Usuario " + result.ErrorMessage);
            }

        }
        public static void Add() 
        {

            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Ingrese tu UserName");
            usuario.UserName = Console.ReadLine();

            Console.WriteLine("Ingreso tu nombre");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Ingrese tu apellido paterno");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("Ingrese tu apellido materno");
            usuario.ApellidoMaterno = Console.ReadLine();

            Console.WriteLine("Ingrese tu correo electronico");
            usuario.Email = Console.ReadLine();

            Console.WriteLine("Ingreso tu contraseña");
            usuario.Password = Console.ReadLine();

            Console.WriteLine("Ingrese tu sexo, escribe (M) si eresMasculino o (F) Si eres Femenino");
            usuario.Sexo = Console.ReadLine();

            Console.WriteLine("Ingrese tu telefono de casa");
            usuario.Telefono = Console.ReadLine();

            Console.WriteLine("Ingrese tu numero celular");
            usuario.Celular = Console.ReadLine();

            Console.WriteLine("Ingrese tu fecha por Dia/Mes/Año");
            usuario.FechaNacimiento = Console.ReadLine();

            Console.WriteLine("Ingrese tu Curp");
            usuario.CURP = Console.ReadLine();

            Console.WriteLine("Ingrese Id Rol que perteneso (1)  Admin o (2) Usuario");
            usuario.Rol = new ML.Rol();
            usuario.Rol.IdRol = int.Parse(Console.ReadLine());

            //ML.Result result = BL.Usuario.Add(usuario);
            //ML.Result result = BL.Usuario.AddSP(usuario);
            //ML.Result result = BL.Usuario.AddEF(usuario);
            ML.Result result = BL.Usuario.AddLinq(usuario);

            if (result.Correct)
            {
                Console.WriteLine("El usuario se inserto correctamente");
            }
            else
            {
                Console.WriteLine("Ocurrio un error al insertar el usuriao" + result.ErrorMessage);
                Console.WriteLine(result.Ex);
            }
        }
         public static void Update()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Ingrese Los datos que quieras actualizar");

            Console.WriteLine("\n Ingrese tu UserName");
            usuario.UserName = Console.ReadLine();

            Console.WriteLine("Ingreso tu nombre");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Ingrese tu apellido paterno");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("Ingrese tu apellido materno");
            usuario.ApellidoMaterno = Console.ReadLine();

            Console.WriteLine("Ingrese tu correo electronico");
            usuario.Email = Console.ReadLine();

            Console.WriteLine("Ingreso tu contraseña");
            usuario.Password = Console.ReadLine();

            Console.WriteLine("Ingrese tu sexo, escribe (M) si eresMasculino o (F) Si eres Femenino");
            usuario.Sexo = Console.ReadLine();

            Console.WriteLine("Ingrese tu telefono de casa");
            usuario.Telefono = Console.ReadLine();

            Console.WriteLine("Ingrese tu numero celular");
            usuario.Celular = Console.ReadLine();

            Console.WriteLine("Ingrese tu fecha por Dia/Mes/Año");
            usuario.FechaNacimiento = Console.ReadLine();

            Console.WriteLine("Ingrese tu Curp");
            usuario.CURP = Console.ReadLine();

            Console.WriteLine("Ingrese Id Rol que perteneso (1)  Admin o (2) Usuario");
            usuario.Rol = new ML.Rol();
            usuario.Rol.IdRol = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese el Id donde se encuentra el usuario: ");
            usuario.IdUsuario = int.Parse(Console.ReadLine());


            //ML.Result result = BL.Usuario.Update(usuario);
            //ML.Result result = BL.Usuario.UpdateSP(usuario);
            //ML.Result result = BL.Usuario.UpdateEF(usuario);
            ML.Result result = BL.Usuario.UpdateLinq(usuario);

            if (result.Correct)
            {
                Console.WriteLine("El usuario se actualizo correctamente");
            }
            else
            {
                Console.WriteLine("Ocurrio un error al actualizar el usuario" + result.ErrorMessage);
                Console.WriteLine(result.Ex);
            }
            

        }
        
        public static void Delete()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Ingrese el Id donde se encuentra el usuario que quieres eliminar: ");
            usuario.IdUsuario = int.Parse(Console.ReadLine());

            //ML.Result result = BL.Usuario.Delete(usuario);
            //ML.Result result = BL.Usuario.DeleteSP(usuario);
            //ML.Result result = BL.Usuario.DeleteEF(usuario);
            ML.Result result = BL.Usuario.DeleteLinq(usuario);

            if (result.Correct)
            {
                Console.WriteLine("El usuario se elimino correctamente");
            }
            else
            {
                Console.WriteLine("Ocurrio un error al eliminar el usuario" + result.ErrorMessage);
                Console.WriteLine(result.Ex);
            }

        }

        public static void Suma(int numero1, int numero2, out int resultado)
        {
            resultado = numero1 + numero2;
        }
        public static void Resta(int numero1, int numero2, out int resultado)
        {
            resultado = numero1 - numero2;
        }
        public static void Multiplicacion(int numero1, int numero2, out int resultado)
        {
            resultado = numero1 * numero2;
        }
        public static void Division(int numero1, int numero2, out int resultado)
        {
            resultado = numero1 / numero2;
        }
        
        public static void Entero()
        {
            double resultado;
            Console.WriteLine("ingrese el radio de un circulo");
            string numero = Console.ReadLine();
            if (double.TryParse(numero, out resultado)) {

                double area = Math.PI * resultado * resultado;
                Console.WriteLine("El area del circulo de: " + resultado +" es: "+ area);
            
            
            }

        }


    }
}
