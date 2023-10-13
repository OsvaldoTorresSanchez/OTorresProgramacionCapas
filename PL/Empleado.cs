using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Row = DocumentFormat.OpenXml.Spreadsheet.Row;
using DocumentFormat.OpenXml;
using SpreadsheetLight;
using DocumentFormat.OpenXml.Vml;

namespace PL
{
    public class Empleado
    {
        public static void FileTxt()
        {
            string filePath = "C:\\Users\\digis\\Documents\\Osvaldo Yair Torres Sanchez\\Notas\\Empleado.txt"; // Ruta del archivo .txt
            try
            {
                using (StreamReader file = new StreamReader(filePath))
                {
                    string line;
                    line = file.ReadLine();
                    List<string> resultLines = new List<string>();

                    while ((line = file.ReadLine()) != null)
                    {

                        var campos = line.Split('|'); // Dividir la línea en campos usando '|'

                        Console.WriteLine(line);
                        ML.Empleado empleado = new ML.Empleado();
                        empleado.NombreEmpleado = campos[0];
                        empleado.RFC = campos[1];
                        empleado.Nombre = campos[2];
                        empleado.ApellidoPaterno = campos[3];
                        empleado.ApellidoMaterno = campos[4];
                        empleado.Email = campos[5];
                        empleado.Telefono = campos[6];
                        empleado.FechaNacimiento = campos[7].ToString();
                        empleado.NSS = campos[8];
                        empleado.FechaIngreso = campos[9].ToString();
                        //empleado.Foto = campos[10].ToString();
                        empleado.Empresa = new ML.Empresa();
                        empleado.Empresa.IdEmpresa = int.Parse(campos[10]);

                        ML.Result result = BL.Empleado.AddEF(empleado);
                        if (result.Correct)
                        {
                            Console.WriteLine("Se agrego correctamente el empelado");
                            result.Correct = true;
                            resultLines.Add("INSERT de Empleado" + empleado.NombreEmpleado + "Fue exitoso");
                        }
                        else
                        {
                            Console.WriteLine("No se puedo agregar el empleado ");
                            result.Correct = false;
                            //result.ErrorMessage = "Ocurrio un error la tabla no contiene informacion ";
                            resultLines.Add("Hubo un erro al actualizar de Empleado" + empleado.NombreEmpleado + "Error" + result.ErrorMessage);

                        }

                    }
                    file.Close();
                    WriteResult(resultLines);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error al leer el archivo: " + ex.Message);
            }
        }


        public static void WriteResult(List<string> resultiltLine)
        {
            using (StreamWriter outputFile = new StreamWriter("C:\\Users\\digis\\Documents\\Osvaldo Yair Torres Sanchez\\Notas\\ReporteMasivaErorres.txt"))
            {

                foreach (string line in resultiltLine)
                {
                    outputFile.WriteLine(line);
                }
            }
        }

        /*
        public static void FileExel()
        {
            string filePath = "C:\\Users\\digis\\Documents\\Osvaldo Yair Torres Sanchez\\Notas\\Empleado.xlsx"; // Ruta del archivo .txt
            try
            {
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    { // Supongamos que estamos leyendo la primera columna

                        reader.Read();
                        List<string> resultLines = new List<string>();

                        string line;
                        while (reader.Read())
                        {

                            var campos = line.Split('|'); // Dividir la línea en campos usando '|'

                            ML.Empleado empleado = new ML.Empleado();
                            empleado.NombreEmpleado = campos[0];
                            empleado.RFC = campos[1];
                            empleado.Nombre = campos[2];
                            empleado.ApellidoPaterno = campos[3];
                            empleado.ApellidoMaterno = campos[4];
                            empleado.Email = campos[5];
                            empleado.Telefono = campos[6];
                            empleado.FechaNacimiento = campos[7].ToString();
                            empleado.NSS = campos[8];
                            empleado.FechaIngreso = campos[9].ToString();
                            //empleado.Foto = campos[10].ToString();
                            empleado.Empresa = new ML.Empresa();
                            empleado.Empresa.IdEmpresa = int.Parse(campos[10]);

                            ML.Result result = BL.Empleado.AddEF(empleado);

                            if (result.Correct)
                            {
                                Console.WriteLine("Se agrego correctamente el empelado");
                                result.Correct = true;
                                //  resultLines.Add("INSERT de Empleado" + empleado.NombreEmpleado + "Fue exitoso");
                            }
                            else
                            {
                                Console.WriteLine("No se puedo agregar el empleado ");
                                result.Correct = false;
                                //result.ErrorMessage = "Ocurrio un error la tabla no contiene informacion ";
                                //  resultLines.Add("Hubo un erro al actualizar de Empleado" + empleado.NombreEmpleado + "Error" + result.ErrorMessage);

                            }
                        }

                    }

                }
                //WriteResult(resultLines);
            }

            catch (IOException ex)
            {
                Console.WriteLine("Error al leer el archivo: " + ex.Message);
            }
        }*/


        public static void FileExell()
        {
            string filePath = "C:\\Users\\digis\\Documents\\Osvaldo Yair Torres Sanchez\\Notas\\Empleado.xlsx"; // Ruta del archivo .txt
            try
            {
                using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(filePath, false))
                {
                    WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                    WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                    SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
                    OpenXmlReader campos = OpenXmlReader.Create(worksheetPart);
                    while (campos.Read())
                    {
                        if (campos.ElementType == typeof(CellValue))
                        {

                            ML.Empleado empleado = new ML.Empleado();
                            empleado.NombreEmpleado = campos.GetText();
                            empleado.RFC = campos.GetText();
                            empleado.Nombre = campos.GetText();
                            empleado.ApellidoPaterno = campos.GetText();
                            empleado.ApellidoMaterno = campos.GetText();
                            empleado.Email = campos.GetText();
                            empleado.Telefono = campos.GetText();
                            empleado.FechaNacimiento = campos.GetText().ToString();
                            empleado.NSS = campos.GetText();
                            empleado.FechaIngreso = campos.GetText().ToString();
                            empleado.Empresa = new ML.Empresa();
                            empleado.Empresa.IdEmpresa = int.Parse(campos.GetText());

                            ML.Result result = BL.Empleado.AddEF(empleado);

                            if (result.Correct)
                            {
                                Console.WriteLine("Se agrego correctamente el empelado");
                                result.Correct = true;
                                //  resultLines.Add("INSERT de Empleado" + empleado.NombreEmpleado + "Fue exitoso");
                            }
                            else
                            {
                                Console.WriteLine("No se puedo agregar el empleado ");
                                result.Correct = false;
                                //result.ErrorMessage = "Ocurrio un error la tabla no contiene informacion ";
                                //  resultLines.Add("Hubo un erro al actualizar de Empleado" + empleado.NombreEmpleado + "Error" + result.ErrorMessage);

                            }
                        }

                    }

                }
                //WriteResult(resultLines);
            }

            catch (IOException ex)
            {
                Console.WriteLine("Error al leer el archivo: " + ex.Message);
            }
        }

        public static void FileExel()
        {
            string filePath = "C:\\Users\\digis\\Documents\\Osvaldo Yair Torres Sanchez\\Notas\\Empleado.xlsx"; // Ruta del archivo .txt
            try
            {
                SLDocument s1 = new SLDocument(filePath);
                int valor = 2;
                while (!string.IsNullOrEmpty(s1.GetCellValueAsString(valor, 1)))
                {


                    ML.Empleado empleado = new ML.Empleado();
                    empleado.NombreEmpleado = s1.GetCellValueAsString(valor,1);
                    empleado.RFC = s1.GetCellValueAsString(valor, 2);
                    empleado.Nombre = s1.GetCellValueAsString(valor, 3);
                    empleado.ApellidoPaterno = s1.GetCellValueAsString(valor, 4);
                    empleado.ApellidoMaterno = s1.GetCellValueAsString(valor, 5);
                    empleado.Email = s1.GetCellValueAsString(valor, 6);
                    empleado.Telefono = s1.GetCellValueAsString(valor, 7);
                    empleado.FechaNacimiento = (s1.GetCellValueAsString(valor, 8));
                    empleado.NSS = s1.GetCellValueAsString(valor, 9);
                    empleado.FechaIngreso = s1.GetCellValueAsString(valor, 10);
                    empleado.Empresa = new ML.Empresa();
                    empleado.Empresa.IdEmpresa = int.Parse(s1.GetCellValueAsString(valor, 11));

                    ML.Result result = BL.Empleado.AddEF(empleado);

                    if (result.Correct)
                    {
                        Console.WriteLine("Se agrego correctamente el empelado");
                        result.Correct = true;
                        //  resultLines.Add("INSERT de Empleado" + empleado.NombreEmpleado + "Fue exitoso");
                    }
                    else
                    {
                        Console.WriteLine("No se puedo agregar el empleado ");
                        result.Correct = false;
                        //result.ErrorMessage = "Ocurrio un error la tabla no contiene informacion ";
                        //  resultLines.Add("Hubo un erro al actualizar de Empleado" + empleado.NombreEmpleado + "Error" + result.ErrorMessage);

                    }

                    valor++;

                }
                //WriteResult(resultLines);
                Console.Read();
            }

            catch (IOException ex)
            {
                Console.WriteLine("Error al leer el archivo: " + ex.Message);
            }
        }
    }

}
