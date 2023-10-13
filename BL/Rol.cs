using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {

        public static ML.Result GetAllLinq()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {
                    var resultQuery = (from RolLinq in context.Rols
                                       select RolLinq).ToList();
                    if (resultQuery != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var objRol in resultQuery)
                        {
                            ML.Rol rol = new ML.Rol();
                            rol.IdRol = objRol.IdRol;
                            rol.Nombre = objRol.Nombre;

                            result.Objects.Add(rol);

                        }
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
    }
}
