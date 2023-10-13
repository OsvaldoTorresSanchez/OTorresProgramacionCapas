using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Direccion
    {
        /*
        public static ML.Result GetAllEF(int IdUsuario)
        {
            ML.Result result = new ML.Result();


            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {
                    var objDireccion = context.DireccionGetByIdUsuario(IdUsuario).FirstOrDefault();

                    result.Objects = new List<object>();

                    if (objDireccion != null)
                    {
                        ML.Direccion direccion = new ML.Direccion();
                        direccion.IdDireccion = objDireccion.IdDireccion;
                        direccion.Calle = objDireccion.Calle;
                        direccion.NumeroInterior = objDireccion.NumeroInterior;
                        direccion.Colonia = new ML.Colonia();
                        direccion.Colonia.IdColonia = objDireccion.IdColonia.Value;
                        direccion.Usuario = new ML.Usuario();
                        direccion.Usuario.IdUsuario = objDireccion.IdUsuario.Value;

                        result.Object = direccion;
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
        }*/

        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {
                    var resultQuery = context.DireccionGetByIdUsuario().ToList();


                    if (resultQuery != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var objDireccion in resultQuery)
                        {
                            ML.Direccion direccion = new ML.Direccion();
                            direccion.IdDireccion = objDireccion.IdDireccion;
                            direccion.Calle = objDireccion.Calle;
                            direccion.NumeroInterior = objDireccion.NumeroInterior;
                            direccion.Colonia = new ML.Colonia();
                            direccion.Colonia.IdColonia = objDireccion.IdColonia.Value;
                            direccion.Usuario = new ML.Usuario();
                            direccion.Usuario.IdUsuario = objDireccion.IdUsuario.Value;

                            result.Objects.Add(direccion);
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
        public static ML.Result AddEF(ML.Direccion direccion, int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {
                    var resultQuery = context.DireccionAdd(direccion.Calle, direccion.NumeroInterior, direccion.NumeroExterior, direccion.Colonia.IdColonia, IdUsuario);

                    if (resultQuery > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se puede añadir la direccion " + direccion.Calle;
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
        
        public static ML.Result UpdateEF(ML.Direccion direccion, int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {
                    var updateResult = context.DireccionUpdate( direccion.Calle, direccion.NumeroInterior, direccion.NumeroExterior, direccion.Colonia.IdColonia, IdUsuario);
                    if (updateResult >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se actualizó el status de la direccion" + direccion.Calle;
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

        public static ML.Result DeleteEF(ML.Direccion direccion, int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {
                    var updateResult = context.DireccionDelete(IdUsuario);
                    if (updateResult >= 1)
                    {

                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al elimino el usuario " + direccion.Calle;
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
