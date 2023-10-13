using BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class EmpleadoController : Controller
    {
        /*
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empleados = new List<object>();

            empleado.Empresa = new ML.Empresa();
            ML.Result resultEmpresa = BL.Empresa.GetAllEF();
            empleado.Empresa.Empresas = resultEmpresa.Objects;

            //empleado.Nombre = "";
            //empleado.ApellidoPaterno = "";
            //empleado.ApellidoMaterno = "";
            //empleado.Empresa.IdEmpresa = 0; 

           // ServiceEmpleado.EmpleadoClient empleadoClient = new ServiceEmpleado.EmpleadoClient();
           // var result = empleadoClient.GetAll(empleado);
            ML.Result result = BL.Empleado.GetAllEFView(empleado);

            if (result.Correct)
            {
                empleado.Empleados = result.Objects.ToList();
                //empleado.Empleados = result.Objects;
            }
            else
            {
                ViewBag.Message = result.ErrorMessage; //Mandar datos a Controller hacia la vista
            }
            return View(empleado);
        }*/

        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            ML.Result resultEmpleado = new ML.Result();

            empleado.Empresa = new ML.Empresa();
            ML.Result resultEmpresa = BL.Empresa.GetAllEF();
            empleado.Empresa.Empresas = resultEmpresa.Objects;

            resultEmpleado.Objects = new List<Object>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51276/api/empleado/");//Base de la ruta

                var responseTask = client.GetAsync("getall");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Empleado resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Empleado>(resultItem.ToString());
                        resultEmpleado.Objects.Add(resultItemList);
                        empleado.Empleados = resultEmpleado.Objects;
                    }
                }
            }
            return View(empleado);

        }

        [HttpPost]
        public ActionResult GetAll(ML.Empleado empleado)
        {
            empleado.Empleados = new List<object>();
            // empleado.Empresa = new ML.Empresa();
            ML.Result resultEmpresa = BL.Empresa.GetAllEF();
            empleado.Empresa.Empresas = resultEmpresa.Objects;

            //ServiceEmpleado.EmpleadoClient empleadoClient = new ServiceEmpleado.EmpleadoClient();
            //var result = empleadoClient.GetAll(empleado);

            ML.Result resultEmpleado = new ML.Result();
            resultEmpleado.Objects = new List<Object>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51276/api/empleado/");//Base de la ruta

                var responseTask = client.PostAsJsonAsync("getallview/" , empleado);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Empleado resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Empleado>(resultItem.ToString());
                        resultEmpleado.Objects.Add(resultItemList);
                        empleado.Empleados = resultEmpleado.Objects;
                    }
                }
                else
                {
                    ViewBag.Message = resultEmpleado.ErrorMessage;
                }
            }

            // ML.Result result = BL.Empleado.GetAllEFView(empleado);

           // GetAllView(empleado);

            //if (result.Correct)
            //{
            //    // empleado.Empleados.ToList();
            //    empleado.Empleados = result.Objects;
            //}
            //else
            //{
            //    ViewBag.Message = result.ErrorMessage; //Mandar datos a Controller hacia la vista
            //}
            return View(empleado);
        }

        [HttpGet]
        public ActionResult Form(int? IdEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();

            //get de Rol
            ML.Result resultEmpresas = BL.Empresa.GetAllEF();
            empleado.Empresa = new ML.Empresa();
            empleado.Empresa.Empresas = resultEmpresas.Objects;

            if (IdEmpleado == null) // Add
            {
                return View(empleado);
            }
            else //Update
            {
                //ServiceEmpleado.EmpleadoClient empleadoClient = new ServiceEmpleado.EmpleadoClient();
                //var result = empleadoClient.GetById((int)IdEmpleado);

                //ML.Result result = BL.Empleado.GetByIdEF((int)IdEmpleado);

                ML.Result result = new ML.Result();
                //GetById((int)IdEmpleado);

                try
                {
                    //string urlAPI = System.Configuration.ConfigurationManager.AppSettings["http://localhost:51276/api/empleado/"];
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:51276/api/empleado/");
                        var responseTask = client.GetAsync("getbyid/" + IdEmpleado);
                        responseTask.Wait();
                        var resultAPI = responseTask.Result;
                        if (resultAPI.IsSuccessStatusCode)
                        {
                            var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();
                            ML.Empleado resultItemList = new ML.Empleado();
                            resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Empleado>(readTask.Result.Object.ToString());
                            result.Object = resultItemList;

                            empleado = (ML.Empleado)result.Object;
                            empleado.Empresa.Empresas = resultEmpresas.Objects;
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en la tabla Departamento";
                           
                        }

                    }

                }

                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;

                }

                //if (results.Correct)
                //{
                //    empleado = (ML.Empleado)results.Object;
                //    empleado.Empresa.Empresas = resultEmpresas.Objects;

                //}
                return View(empleado); // Vacio
            }
        }



        [HttpPost]
        public ActionResult Form(ML.Empleado empleado, HttpPostedFileBase fuimgEmpleado)
        {
            //if (ModelState.IsValid)
            //{
            if (fuimgEmpleado != null)
            {
                empleado.Foto = this.ConvertTobytes(fuimgEmpleado);
            }
            if (empleado.IdEmpleado == 0) // ADD
            {
                // ServiceEmpleado.EmpleadoClient empleadoClient = new ServiceEmpleado.EmpleadoClient();
                //var result = empleadoClient.AddEF(empleado);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:51276/api/empleado/");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Empleado>("Add", empleado);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se ha ingresado correctamente el Empleado";
                    }
                    else
                    {
                        ViewBag.Message = "no se ingresado correctemnte el Empleado , Error: " + result.StatusCode;
                    }
                }
                // ML.Result result = BL.Empleado.AddEF(empleado);

                /* if (result.Correct)
                 {
                     ViewBag.Message = "Se ha ingresado correctamente el Empleado";
                 }
                 else
                 {
                     ViewBag.Message = "no se ingresado correctemnte el Empleado , Error: " + result.ErrorMessage;
                 }*/
            }
            else // UPDATE
            {
                //ServiceEmpleado.EmpleadoClient empleadoClient = new ServiceEmpleado.EmpleadoClient();
                //var result = empleadoClient.UpdateEF(empleado);

                //Update(empleado);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:51276/api/empleado/");

                    //HTTP POST
                    var postTask = client.PutAsJsonAsync<ML.Empleado>("update", empleado);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se ha actualizado correctamente el Empleado";
                    }
                    else
                    {
                        ViewBag.Message = "no se actualizado correctemnte el usuario , Error: " + result.StatusCode;
                    }
                }

                // ML.Result result = BL.Empleado.UpdateEF(empleado);
                /* if (result.Correct)
                 {
                     ViewBag.Message = "Se ha actualizado correctamente el Empleado";
                 }
                 else
                 {
                     ViewBag.Message = "no se actualizado correctemnte el usuario , Error: " + result.ErrorMessage;
                 }*/
            }
            return PartialView("Modal");
            //}
            //else
            //{

            // return View(empleado);
            //}
        }

        [HttpPost]
        public byte[] ConvertTobytes(HttpPostedFileBase Foto)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(Foto.InputStream);
            imageBytes = reader.ReadBytes((int)Foto.ContentLength);
            return imageBytes;
        }

        //Delete MVC 
        /*
        public ActionResult Delete(ML.Empleado empleado)
        {
            ServiceEmpleado.EmpleadoClient empleadoClient = new ServiceEmpleado.EmpleadoClient();
            var result = empleadoClient.DeleteEF(empleado);



            //ML.Result result = BL.Empleado.DeleteEF(empleado);

            if (result.Correct)
            {
                ViewBag.Message = "Se ha eliminado correctamente el usuario";
            }

            else
            {
                ViewBag.Message = "no se puede eliminar correctemnte el usuario , Error: " + result.ErrorMessage;
            }

            return PartialView("Modal");
        }*/

        public ActionResult Delete(ML.Empleado empleado)
        {

            int id = empleado.IdEmpleado;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51276/api/empleado/");

                //HTTP POST
                var postTask = client.DeleteAsync("delete/" + id);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Se ha eliminado correctamente el usuario";
                    return RedirectToAction("GetAll");
                }
                else
                {
                    ViewBag.Message = "no se puede eliminar correctemnte el usuario , Error: ";
                }
            }
            return View("Modal");
        }

        [HttpGet]
        public ActionResult UploadData()
        {
            ML.Empleado empleado = new ML.Empleado();

            return View(empleado);
        }

        [HttpPost]
        public ActionResult UploadData(ML.Empleado empleado, HttpPostedFileBase cargaMasiva)
        {
            // Obtén la extensión del archivo
            //if (cargaMasiva == null)
            ML.Result result = new ML.Result();

            if (cargaMasiva == null)
            {
                var lista = (List<Object>)Session["ListEmpleados"];
                empleado.Empleados = lista;
                CargaDatos(empleado);
                Session.Remove("ListEmpleados");
                ViewBag.Message = "Datos cargados ";
                return PartialView("Modal");
            }
            string fileExtension = Path.GetExtension(cargaMasiva.FileName);
            //{
            //if (fileExtension.Equals(".txt", StringComparison.OrdinalIgnoreCase) || fileExtension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            if (fileExtension == ".txt")
            {
                if (Session["ListEmpleados"] == null)
                {
                    string direccionExcel = Server.MapPath("~/CargaMasiva/TXT/") + Path.GetFileNameWithoutExtension(cargaMasiva.FileName) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";

                    cargaMasiva.SaveAs(direccionExcel);

                    empleado = PrevisualizarTxt(empleado, cargaMasiva);
                    //Lista de empleados
                    //Guardadr sesion
                    Session["ListEmpleados"] = empleado.Empleados;

                }
                else
                {
                    //Extraer la infromacion de la sesion lista de empleos

                }

            }
            else
            {
                if (fileExtension == ".xlsx")
                {
                    string conexionExel = System.Configuration.ConfigurationManager.AppSettings["ConexionExcel"];

                    string direccionExcel = Server.MapPath("~/CargaMasiva/Excel/") + Path.GetFileNameWithoutExtension(cargaMasiva.FileName) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                    try
                    {
                        cargaMasiva.SaveAs(direccionExcel);
                        empleado = PrevisualizarExcel(empleado, direccionExcel, conexionExel);
                        Session["ListEmpleados"] = empleado.Empleados;


                    }
                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                        result.Ex = ex;
                    }
                }
                else
                {

                }
                //Lista de empleados
                //Guardadr sesion

                //Extraer la infromacion de la sesion lista de empleos
                //var lista = (List<Object>)Session["ListEmpleados"];
                //empleado.Empleados = lista;
                //CargaDatos(empleado);
                //ViewBag.Message = "Datos cargados ";
                //return PartialView("Modal");
            }
            //}
            return View(empleado);
        }

        public static ML.Empleado PrevisualizarTxt(ML.Empleado empleado, HttpPostedFileBase cargaMasiva)
        {
            empleado.Empleados = new List<object>();
            using (StreamReader file = new StreamReader(cargaMasiva.InputStream))
            {
                try
                {
                    string line;
                    line = file.ReadLine();
                    while ((line = file.ReadLine()) != null)
                    {
                        var campos = line.Split('|'); // Dividir la línea en campos usando '|'

                        ML.Empleado empleadoItem = new ML.Empleado();
                        empleadoItem.NombreEmpleado = campos[0];
                        empleadoItem.RFC = campos[1];
                        empleadoItem.Nombre = campos[2];
                        empleadoItem.ApellidoPaterno = campos[3];
                        empleadoItem.ApellidoMaterno = campos[4];
                        empleadoItem.Email = campos[5];
                        empleadoItem.Telefono = campos[6];
                        empleadoItem.FechaNacimiento = campos[7].ToString();
                        empleadoItem.NSS = campos[8];
                        empleadoItem.FechaIngreso = campos[9].ToString();
                        empleadoItem.Empresa = new ML.Empresa();
                        empleadoItem.Empresa.IdEmpresa = int.Parse(campos[10]);

                        empleado.Empleados.Add(empleadoItem);

                    }
                }
                catch (IOException ex)
                {

                }
            }
            return empleado;

        }

        public static ML.Empleado PrevisualizarExcel(ML.Empleado empleado, string rutaExel, string conexion)
        {
            ML.Result result = new ML.Result();
            using (OleDbConnection file = new OleDbConnection(conexion + rutaExel))
            {
                try
                {
                    string query = "SELECT * FROM [Hoja 1$]";
                    using (OleDbCommand context = new OleDbCommand())
                    {
                        context.CommandText = query;
                        context.Connection = file;

                        OleDbDataAdapter data = new OleDbDataAdapter(context);
                        data.SelectCommand = context;

                        DataTable tableEmpleado = new DataTable();
                        data.Fill(tableEmpleado);

                        if (tableEmpleado.Rows.Count > 0)
                        {

                            empleado.Empleados = new List<object>();

                            foreach (DataRow row in tableEmpleado.Rows)
                            {

                                ML.Empleado empleadoItem = new ML.Empleado();
                                empleadoItem.NombreEmpleado = row[0].ToString();
                                empleadoItem.RFC = row[1].ToString();
                                empleadoItem.Nombre = row[2].ToString();
                                empleadoItem.ApellidoPaterno = row[3].ToString();
                                empleadoItem.ApellidoMaterno = row[4].ToString();
                                empleadoItem.Email = row[5].ToString();
                                empleadoItem.Telefono = row[6].ToString();
                                empleadoItem.FechaNacimiento = row[7].ToString();
                                empleadoItem.NSS = row[8].ToString();
                                empleadoItem.FechaIngreso = row[9].ToString();
                                empleadoItem.Empresa = new ML.Empresa();
                                empleadoItem.Empresa.IdEmpresa = int.Parse(row[10].ToString());

                                empleado.Empleados.Add(empleadoItem);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;
                    result.Ex = ex;

                }
            }
            return empleado;

        }

        public void CargaDatos(ML.Empleado empleado)
        {

            List<string> resultLines = new List<string>();

            foreach (ML.Empleado empleadoItem in empleado.Empleados)
            {
                ML.Result result = BL.Empleado.AddEF(empleadoItem);
                if (result.Correct)
                {
                    resultLines.Add("La inserccion del Empleado" + empleado.NombreEmpleado + "Fue exitoso");
                    result.Correct = true;
                }
                else
                {
                    resultLines.Add("Hubo un erro al actualizar de Empleado" + empleado.NombreEmpleado + "Error" + result.ErrorMessage);
                    result.Correct = false;

                }
            }

            var fecha = DateTime.Now.ToString("dd-MM-yyyy HHmmss");
            string path = Server.MapPath("~/CargaMasiva/TXT/" + "CargaMasivaTxt_" + fecha + ".txt");

            using (StreamWriter archvioError = System.IO.File.CreateText(path))
            {
                foreach (string line in resultLines)
                {
                    archvioError.WriteLine(line);
                }
            }
        }
        
        public void CargaDatosExcel(ML.Empleado empleado)
        {

            List<string> resultLines = new List<string>();

            foreach (ML.Empleado empleadoItem in empleado.Empleados)
            {
                ML.Result result = BL.Empleado.AddEF(empleadoItem);
                if (result.Correct)
                {
                    resultLines.Add("La inserccion del Empleado" + empleado.NombreEmpleado + "Fue exitoso");
                    result.Correct = true;
                }
                else
                {
                    resultLines.Add("Hubo un erro al actualizar de Empleado" + empleado.NombreEmpleado + "Error" + result.ErrorMessage);
                    result.Correct = false;

                }
            }

            var fecha = DateTime.Now.ToString("dd-MM-yyyy HHmmss");
            string path = Server.MapPath("~/CargaMasiva/EXEL/" + "CargaMasivaExcel_" + fecha + ".txt");

            using (StreamWriter archvioError = System.IO.File.CreateText(path))
            {
                foreach (string line in resultLines)
                {
                    archvioError.WriteLine(line);
                }
            }
        }

        // Pruebas de WEB_API
        public ActionResult Add(ML.Empleado empleado)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51276/api/empleado/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ML.Empleado>("Add", empleado);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAll");
                }
            }

            return View("GetAll");
        }


        public ActionResult Update(ML.Empleado empleado)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51276/api/empleado/");

                //HTTP POST
                var postTask = client.PutAsJsonAsync<ML.Empleado>("update", empleado);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAll");
                }
            }
            return View("Modal");

        }

        public static ML.Result GetById(int IdEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                //string urlAPI = System.Configuration.ConfigurationManager.AppSettings["http://localhost:51276/api/empleado/"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:51276/api/empleado/");
                    var responseTask = client.GetAsync("getbyid/" + IdEmpleado);
                    responseTask.Wait();
                    var resultAPI = responseTask.Result;
                    if (resultAPI.IsSuccessStatusCode)
                    {
                        var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        ML.Empleado resultItemList = new ML.Empleado();
                        resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Empleado>(readTask.Result.Object.ToString());
                        result.Object = resultItemList;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No existen registros en la tabla Departamento";
                    }

                }
               
            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
            
        }

        public ActionResult GetAllView(ML.Empleado empleado)
        {
            ML.Result resultEmpleado = new ML.Result();


            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51276/api/empleado/");//Base de la ruta

                var responseTask = client.GetAsync("getallview/"  + empleado);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Empleado resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Empleado>(resultItem.ToString());
                        resultEmpleado.Objects.Add(resultItemList);
                        empleado.Empleados = resultEmpleado.Objects;
                    }
                }
            }
            return View(empleado);

        }

    }

}

