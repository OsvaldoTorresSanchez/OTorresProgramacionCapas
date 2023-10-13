using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class Conexion
    {
        public static string Get()
        {
            string conexion = "Data Source=.;Initial Catalog=OTorresProgramacionCapas;User ID=sa;Password=pass@word1";
            return conexion;

        }

    }
}
