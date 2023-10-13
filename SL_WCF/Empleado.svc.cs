using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Empleado" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Empleado.svc or Empleado.svc.cs at the Solution Explorer and start debugging.
    public class Empleado : IEmpleado
    {
        public SL_WCF.Result GetAll(ML.Empleado empleado)
        {
            
            ML.Result result = BL.Empleado.GetAllEF(empleado);

            SL_WCF.Result resultSL = new SL_WCF.Result();
            resultSL.Correct = result.Correct;
            resultSL.ErrorMessage = result.ErrorMessage;
            resultSL.ex = result.Ex;
            resultSL.Object = result.Object;
            resultSL.Objects = result.Objects;

            return resultSL;
        }

        public SL_WCF.Result GetById(int empleado)
        {
            ML.Result result = BL.Empleado.GetByIdEF(empleado);

            SL_WCF.Result resultSL = new SL_WCF.Result();
            resultSL.Correct = result.Correct;
            resultSL.ErrorMessage = result.ErrorMessage;
            resultSL.ex = result.Ex;
            resultSL.Object = result.Object;
            resultSL.Objects = result.Objects;

            return resultSL;
        }

        public SL_WCF.Result AddEF(ML.Empleado empleado)
        {
            ML.Result result = BL.Empleado.AddEF(empleado);

            SL_WCF.Result resultSL = new SL_WCF.Result();
            resultSL.Correct = result.Correct;
            resultSL.ErrorMessage = result.ErrorMessage;
            resultSL.ex = result.Ex;
            resultSL.Object = result.Object;
            resultSL.Objects = result.Objects;

            return resultSL;
        }

        public SL_WCF.Result UpdateEF(ML.Empleado empleado)
        {
            ML.Result result = BL.Empleado.UpdateEF(empleado);

            SL_WCF.Result resultSL = new SL_WCF.Result();
            resultSL.Correct = result.Correct;
            resultSL.ErrorMessage = result.ErrorMessage;
            resultSL.ex = result.Ex;
            resultSL.Object = result.Object;
            resultSL.Objects = result.Objects;

            return resultSL;
        }

        public SL_WCF.Result DeleteEF(ML.Empleado empleado)
        {
            ML.Result result = BL.Empleado.DeleteEF(empleado);

            SL_WCF.Result resultSL = new SL_WCF.Result();
            resultSL.Correct = result.Correct;
            resultSL.ErrorMessage = result.ErrorMessage;
            resultSL.ex = result.Ex;
            resultSL.Object = result.Object;
            resultSL.Objects = result.Objects;

            return resultSL;
        }
    }
}
