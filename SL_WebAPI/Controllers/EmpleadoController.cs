using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebAPI.Controllers
{
    public class EmpleadoController : ApiController
    {
        // GET: api/Empleado
        
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("api/empleado/getall")]
        public IHttpActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();
            ML.Result result = BL.Empleado.GetAllEFView(empleado);

            if (result.Correct)
            {
                return Ok(result);

            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("api/empleado/getallview")]
        public IHttpActionResult GetAll([FromBody] ML.Empleado empleado)
        {
            
            //empleado.Empresa = new ML.Empresa();
            ML.Result result = BL.Empleado.GetAllEFView(empleado);

            if (result.Correct)
            {
                return Ok(result);

            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet]
        [Route("api/empleado/getbyid/{IdEmpleado}")]
        public IHttpActionResult GetById(int idEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();
            ML.Result result = BL.Empleado.GetByIdEF(idEmpleado);

            if (result.Correct)
            {
                return Ok(result);

            }
            else
            {
                return NotFound();
            }
        }

      
        // POST: api/Empleado
        [HttpPost]
        [Route("api/empleado/add")]
        public IHttpActionResult Add(ML.Empleado empleado)
        {
            //ML.Empleado empleados = new ML.Empleado();
            //empleado.Empresa = new ML.Empresa();
            ML.Result result = BL.Empleado.AddEF(empleado);

            if (result.Correct)
            {
                return Ok(result);

            }
            else
            {
                return NotFound();
            }
        }



        // PUT: api/Empleado/5
        [HttpPut]
        [Route("api/empleado/update")]
        public IHttpActionResult Update(ML.Empleado empleado)
        {
            ML.Result result = BL.Empleado.UpdateEF(empleado);

            if (result.Correct)
            {
                return Ok(result);

            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("api/empleado/delete/{IdEmpleado}")]
        public IHttpActionResult Delete(int idEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.IdEmpleado = idEmpleado;

            ML.Result result = BL.Empleado.DeleteEF(empleado);

            if (result.Correct)
            {
                return Ok(result);

            }
            else
            {
                return NotFound();
            }
        }
    }
}
