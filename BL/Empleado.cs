using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empleado
    {

        public static ML.Result GetAllEFView(ML.Empleado empleadoBusqueda)
        {
            ML.Result result = new ML.Result();
            //empleadoBusqueda.Nombre= (empleadoBusqueda.Nombre == null) ? "" : empleadoBusqueda.Nombre;
            //empleadoBusqueda.ApellidoPaterno = (empleadoBusqueda.ApellidoPaterno == null) ? "" : empleadoBusqueda.ApellidoPaterno;
            //empleadoBusqueda.ApellidoMaterno = (empleadoBusqueda.ApellidoMaterno == null) ? "" : empleadoBusqueda.ApellidoMaterno;
            //empleadoBusqueda.Empresa.IdEmpresa = (empleadoBusqueda.Empresa.IdEmpresa != 0) ? int.Parse("0") : empleadoBusqueda.Empresa.IdEmpresa ;

            empleadoBusqueda.Nombre = (empleadoBusqueda.Nombre != null) ? empleadoBusqueda.Nombre :  "" ;
            empleadoBusqueda.ApellidoPaterno = (empleadoBusqueda.ApellidoPaterno != null) ? empleadoBusqueda.ApellidoPaterno : "" ;
            empleadoBusqueda.ApellidoMaterno = (empleadoBusqueda.ApellidoMaterno != null) ? empleadoBusqueda.ApellidoMaterno : "";
            empleadoBusqueda.Empresa.IdEmpresa = (empleadoBusqueda.Empresa.IdEmpresa != 0) ? empleadoBusqueda.Empresa.IdEmpresa : int.Parse("0");

            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {
                    var resultQuery = context.EmpleadoGetAllViews(empleadoBusqueda.Nombre, empleadoBusqueda.ApellidoPaterno, empleadoBusqueda.ApellidoMaterno, 
                        empleadoBusqueda.Empresa.IdEmpresa).ToList();


                    if (resultQuery != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in resultQuery)
                        {
                            ML.Empleado empleado = new ML.Empleado();
                            empleado.IdEmpleado = obj.IdEmpleado;
                            empleado.NombreEmpleado = obj.NumeroEmpleado;
                            empleado.RFC = obj.RFC;
                            empleado.Nombre = obj.Nombre;
                            empleado.ApellidoPaterno = obj.ApellidoPaterno;
                            empleado.ApellidoMaterno = obj.ApellidoMaterno;
                            empleado.Email = obj.Email;
                            empleado.Telefono = obj.Telefono;
                            empleado.FechaNacimiento = obj.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                            empleado.NSS = obj.NSS;
                            empleado.FechaIngreso = obj.FechaIngreso.Value.ToString("dd-MM-yyyy");
                            empleado.Empresa = new ML.Empresa();
                            empleado.Empresa.IdEmpresa = obj.IdEmpresa.Value;
                            empleado.Empresa.Nombre = obj.Empresa;
                            empleado.Foto = obj.Foto;

                            result.Objects.Add(empleado);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error la tabla no contiene informacion ";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result GetAllEF(ML.Empleado empleadoBusqueda)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {
                    var resultQuery = context.EmpeladoGetAll(empleadoBusqueda.Nombre, empleadoBusqueda.ApellidoPaterno, empleadoBusqueda.ApellidoMaterno).ToList();


                    if (resultQuery != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in resultQuery)
                        {
                            ML.Empleado empleado = new ML.Empleado();
                            empleado.IdEmpleado = obj.IdEmpleado;
                            empleado.NombreEmpleado = obj.NumeroEmpleado;
                            empleado.RFC = obj.RFC;
                            empleado.Nombre = obj.Nombre;
                            empleado.ApellidoPaterno = obj.ApellidoPaterno;
                            empleado.ApellidoMaterno = obj.ApellidoMaterno;
                            empleado.Email = obj.Email;
                            empleado.Telefono = obj.Telefono;
                            empleado.FechaNacimiento = obj.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                            empleado.NSS = obj.NSS;
                            empleado.FechaIngreso = obj.FechaIngreso.Value.ToString("dd-MM-yyyy");
                            empleado.Empresa = new ML.Empresa();
                            empleado.Empresa.IdEmpresa = obj.IdEmpresa.Value;
                            empleado.Foto = obj.Foto;

                            result.Objects.Add(empleado);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error la tabla no contiene informacion ";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result GetByIdEF(int Idempleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {
                    var objEmpleado = context.EmpleadoGetById(Idempleado).FirstOrDefault();

                    result.Objects = new List<object>();

                    if (objEmpleado != null)
                    {
                        ML.Empleado empleado = new ML.Empleado();
                        empleado.IdEmpleado = objEmpleado.IdEmpleado;
                        empleado.NombreEmpleado = objEmpleado.NumeroEmpleado;
                        empleado.RFC = objEmpleado.RFC;
                        empleado.Nombre = objEmpleado.Nombre;
                        empleado.ApellidoPaterno = objEmpleado.ApellidoPaterno;
                        empleado.ApellidoMaterno = objEmpleado.ApellidoMaterno;
                        empleado.Email = objEmpleado.Email;
                        empleado.Telefono = objEmpleado.Telefono;
                        empleado.FechaNacimiento = objEmpleado.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                        empleado.NSS = objEmpleado.NSS;
                        empleado.FechaIngreso = objEmpleado.FechaIngreso.Value.ToString("dd-MM-yyyy");
                        empleado.Empresa = new ML.Empresa();
                        empleado.Empresa.IdEmpresa = objEmpleado.IdEmpresa.Value;
                        empleado.Foto = objEmpleado.Foto;


                        result.Object = empleado;
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error la tabla no contiene informacion ";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result AddEF(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {
                    var resultQuery = context.EmpleadoAdd(empleado.NombreEmpleado, empleado.RFC, empleado.Nombre, empleado.ApellidoPaterno,
                        empleado.ApellidoMaterno, empleado.Email, empleado.Telefono, empleado.FechaNacimiento, empleado.NSS, empleado.FechaIngreso,
                        empleado.Empresa.IdEmpresa, empleado.Foto);

                    if (resultQuery > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se puede añadir la direccion " + empleado.NombreEmpleado;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;

        }


        public static ML.Result UpdateEF(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {
                    var updateResult = context.EmpleadoUpdate(empleado.IdEmpleado, empleado.NombreEmpleado, empleado.RFC, empleado.Nombre, empleado.ApellidoPaterno,
                        empleado.ApellidoMaterno, empleado.Email, empleado.Telefono, empleado.FechaNacimiento, empleado.NSS, empleado.FechaIngreso,
                        empleado.Empresa.IdEmpresa, empleado.Foto);

                    if (updateResult >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se actualizó el empleado" + empleado.NombreEmpleado;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;

        }


        public static ML.Result DeleteEF(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {
                    var updateResult = context.EmpleadoDelete(empleado.IdEmpleado);
                    if (updateResult >= 1)
                    {
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al elimino el empleado " + empleado.NombreEmpleado;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
    }
}
