using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ML;

namespace PL_MVC.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Usuarios = new List<object>();
            ML.Result result = BL.Usuario.GetAllEFP();

            ML.Result resultRols = BL.Rol.GetAllLinq();
            usuario.Rol = new ML.Rol();
            usuario.Rol.Rols = resultRols.Objects;


            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
            else
            {
                ViewBag.Message = result.ErrorMessage; //Mandar datos a Controller hacia la vista
            }
            return View(usuario);
        }

        // Forms Mostrar Formulario
        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();

            //Relacion DropDownList
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

            ML.Result resultPais = BL.Pais.GetAllEF();
            usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

            //get de Rol
            ML.Result resultRols = BL.Rol.GetAllLinq();
            usuario.Rol = new ML.Rol();
            usuario.Rol.Rols = resultRols.Objects;

            if (IdUsuario == null) // Add
            {
                //return View(usuario); // Vacio
                //usuario.Rol = new ML.Rol();
                return View(usuario);
            }
            else //Update
            {
                //Get by Id
                /* ML.Result result = BL.Usuario.GetByIdLinq(IdUsuario.Value);
                 usuario.IdUsuario = ((ML.Usuario)result.Object).IdUsuario;
                 usuario.UserName = ((ML.Usuario)result.Object).UserName;
                 usuario.Nombre = ((ML.Usuario)result.Object).Nombre;
                 usuario.ApellidoPaterno = ((ML.Usuario)result.Object).ApellidoPaterno;
                 usuario.ApellidoPaterno = ((ML.Usuario)result.Object).ApellidoPaterno;
                 usuario.Email = ((ML.Usuario)result.Object).Email;
                 usuario.Password = ((ML.Usuario)result.Object).Password;
                 usuario.Sexo = ((ML.Usuario)result.Object).Sexo;
                 usuario.Telefono = ((ML.Usuario)result.Object).Telefono;
                 usuario.Celular = ((ML.Usuario)result.Object).Celular;
                 usuario.FechaNacimiento = ((ML.Usuario)result.Object).FechaNacimiento;
                 usuario.CURP = ((ML.Usuario)result.Object).CURP;
                 usuario.Rol.IdRol = ((ML.Usuario)result.Object).Rol.IdRol;*/

                ML.Result result = BL.Usuario.GetByIdEFP((int)IdUsuario);
                if (result.Correct)
                {
                    usuario = (ML.Usuario)result.Object;
                    usuario.Rol.Rols = resultRols.Objects;

                }
                usuario = FillDropDownList(usuario);
                return View(usuario); // Vacio
            }
            //return View(usuario); // Vacio
        }

        //DRopDownList Esatdo Pais
        public JsonResult EstadoGetByIdPais(int IdPais)
        {
            ML.Result result = BL.Estado.GetbyIdPais(IdPais);

            return Json(result.Objects);
        }
        //DRopDownList Municipio Estado
        public JsonResult MunicipioGetByIdEstado(int IdEstado)
        {
            ML.Result result = BL.Municipio.GetbyIdEstado(IdEstado);

            return Json(result.Objects);
        }

        //DRopDownList Colonia Municipio
        public JsonResult ColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = BL.Colonia.GetbyIdMunicipio(IdMunicipio);

            return Json(result.Objects);
        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario, HttpPostedFileBase fuimgUsuario)
        {
            if (ModelState.IsValid)
            {
                if (fuimgUsuario != null)
                {
                    usuario.Imagen = this.ConvertTobytes(fuimgUsuario);
                }
                if (usuario.IdUsuario == 0)
                {
                    ML.Result result = BL.Usuario.AddEFSP(usuario);

                    if (result.Correct)
                    {
                        ML.Result resultDireccion = BL.Direccion.AddEF(usuario.Direccion, (int)result.Object);

                        if (resultDireccion.Correct)
                        {
                            ViewBag.Message = "Se ha ingresado correctamente el usuario";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "no se ingresado correctemnte el usuario , Error: " + result.ErrorMessage;
                    }
                }
                else
                {
                    ML.Result result = BL.Usuario.UpdateEFP(usuario);
                    if (result.Correct)
                    {
                        ML.Result resultDireccion = BL.Direccion.UpdateEF(usuario.Direccion, usuario.IdUsuario);

                        if (resultDireccion.Correct)
                        {
                            ViewBag.Message = "Se ha actualizado correctamente el usuario";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "no se actualizado correctemnte el usuario , Error: " + result.ErrorMessage;
                    }
                }
                return PartialView("Modal");
            }
            else
            {
                usuario = FillDropDownList(usuario);
                return View(usuario);
            }
        }

        //Status
        /*
        public ActionResult UpdateStatu(int IdUsuario, bool Statu)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML. Result result = BL.Usuario.GetByIdEFP(IdUsuario);

            if (result.Correct)
            {
                usuario =((ML.Usuario)result.Object);
                usuario.Statu = (Statu) ? false : true;
                ML.Result resultUpdateStatu = BL.Usuario.UpdateEFP(usuario);

                ViewBag.Message = "Se actualizo el statu del usuario";

            }
            else
            {
                ViewBag.Message = "No se actualizo el statu del usuario";


            }
            return View(usuario);
        }

        */
        public JsonResult UpdateStatu(int IdUsuario, bool Statu)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetByIdEFP(IdUsuario);

            if (result.Correct)
            {
                usuario = ((ML.Usuario)result.Object);
                usuario.Statu = (usuario.Statu) ? false : true;

                ML.Result resultUpdateStatu = BL.Usuario.UpdateEFP(usuario);

                ViewBag.Message = "Se actualizo el statu del usuario";

            }
            else
            {
                ViewBag.Message = "No se actualizo el statu del usuario";


            }
            return Json(result.Objects);
        }


        //Este metodo llena las lista (pais, Estados, Municipio, Colonia y Rol)
        private ML.Usuario FillDropDownList(ML.Usuario usuario)
        {
            ML.Result resultPaises = new ML.Result();
            resultPaises = BL.Pais.GetAllEF();

            ML.Result resultEsatdos = BL.Estado.GetbyIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
            ML.Result resultMunicipios = BL.Municipio.GetbyIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
            ML.Result resultColonias = BL.Colonia.GetbyIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);

            usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;
            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEsatdos.Objects;
            usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Objects;
            usuario.Direccion.Colonia.Colonias = resultColonias.Objects;

            ML.Result resultroles = BL.Rol.GetAllLinq();
            usuario.Rol.Rols = resultroles.Objects;
            return usuario;

        }

        public byte[] ConvertTobytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        // [HttpDelete] 
        public ActionResult Delete(ML.Usuario usuario)
        {
            ML.Result resultDireccion = BL.Direccion.DeleteEF(usuario.Direccion, usuario.IdUsuario);

            if (resultDireccion.Correct)
            {
                ML.Result result = BL.Usuario.DeleteEFP(usuario);

                if (result.Correct)
                {
                    ViewBag.Message = "Se ha eliminado correctamente el usuario";
                }
                ViewBag.Message = "Se ha eliminado correctamente la  direccion ";
            }
            else
            {
                ViewBag.Message = "no se puede eliminar correctemnte el usuario , Error: " + resultDireccion.ErrorMessage;
            }

            return PartialView("Modal");
        }


    }


}