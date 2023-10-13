using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmpleado" in both code and config file together.
    [ServiceContract]
    public interface IEmpleado
    {
        [OperationContract]
        [ServiceKnownType(typeof(ML.Empleado))]
        SL_WCF.Result GetAll(ML.Empleado empleado);

        [OperationContract]
        [ServiceKnownType(typeof(ML.Empleado))]
        SL_WCF.Result GetById(int Idempleado);

        [OperationContract]
        SL_WCF.Result AddEF(ML.Empleado empleado);

        [OperationContract]
        SL_WCF.Result UpdateEF(ML.Empleado empleado);

        [OperationContract]
        SL_WCF.Result DeleteEF(ML.Empleado empleado);
    }
}
