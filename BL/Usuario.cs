using DL_EF;
using ML;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BL
{
    public class Usuario
    {

        // Forma directa
        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "SELECT IdUsuario,UserName,Nombre,ApellidoPaterno,ApellidoMaterno,Email,Password,Sexo,Telefono,Celular,FechaNacimiento,CURP, IdRol FROM Usuario WHERE IdUsuario = @IdUsuario";
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    cmd.Connection = context;
                    context.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable tabla = new DataTable();
                    adapter.Fill(tabla);

                    if (tabla.Rows.Count > 0)
                    {
                        DataRow row = tabla.Rows[0];

                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = int.Parse(row[0].ToString());
                        usuario.UserName = row[1].ToString();
                        usuario.Nombre = row[2].ToString();
                        usuario.ApellidoPaterno = row[3].ToString();
                        usuario.ApellidoMaterno = row[4].ToString();
                        usuario.Email = row[5].ToString();
                        usuario.Password = row[6].ToString();
                        usuario.Sexo = row[7].ToString();
                        usuario.Telefono = row[8].ToString();
                        usuario.Celular = row[9].ToString();
                        usuario.FechaNacimiento = row[10].ToString();
                        usuario.CURP = row[11].ToString();
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = int.Parse(row[12].ToString());

                        result.Object = usuario;

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

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection conn = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand("SELECT IdUsuario,UserName,Nombre,ApellidoPaterno,ApellidoMaterno,Email,Password,Sexo,Telefono,Celular,FechaNacimiento,CURP, IdRol FROM USUARIO", conn);
                    cmd.Connection = conn;

                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable tabla = new DataTable();
                    adapter.Fill(tabla);

                    if (tabla.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();
                        // foreach (var lista in tabla.Rows) // En caso que no se conosca un valor 
                        foreach (DataRow row in tabla.Rows)
                        {

                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = int.Parse(row[0].ToString());
                            usuario.UserName = row[1].ToString();
                            usuario.Nombre = row[2].ToString();
                            usuario.ApellidoPaterno = row[3].ToString();
                            usuario.ApellidoMaterno = row[4].ToString();
                            usuario.Email = row[5].ToString();
                            usuario.Password = row[6].ToString();
                            usuario.Sexo = row[7].ToString();
                            usuario.Telefono = row[8].ToString();
                            usuario.Celular = row[9].ToString();
                            usuario.FechaNacimiento = row[10].ToString();
                            usuario.CURP = row[11].ToString();
                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = int.Parse(row[12].ToString());

                            result.Objects.Add(usuario);

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

        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection conn = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO [USUARIO] ([UserName], [Nombre], [ApellidoPaterno], [ApellidoMaterno], [Email], [Password], [Sexo],[Telefono], [Celular], [FechaNacimiento], [CURP], [IdRol]) VALUES (@UserName, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @Email, @Password, @Sexo, @Telefono, @Celular, @FechaNacimiento, @CURP, @IdRol)", conn);
                    //SqlCommand cmd = new SqlCommand("INSERT INTO [USUARIO]([Nombre],[Apellido],[Edad],[Genero],[Correo]) VALUES (@Nombre,@Apellido, @Edad,@Genero, @Correo)", conn);
                    //SqlCommand cmd1 = new SqlCommand();
                    //cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Password", usuario.Password);
                    cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@CURP", usuario.CURP);
                    //usuario.Rol = new ML.Rol();
                    cmd.Parameters.AddWithValue("@IdRol", usuario.Rol.IdRol);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al insertar el usuario " + usuario.Nombre;
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

        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection conn = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE [USUARIO] SET [UserName] = @UserName,[Nombre] = @Nombre,[ApellidoPaterno] = @ApellidoPaterno,[ApellidoMaterno] = @ApellidoMaterno ,[Email] = @Email ,[Password] = @Password ,[Sexo]= @Sexo,[Telefono] = @Telefono,[Celular] = @Celular,[FechaNacimiento] = @FechaNacimiento,[CURP] = @CURP, [IdRol]=@IdRol WHERE IdUsuario = @IdUsuario", conn);

                    cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Password", usuario.Password);
                    cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@CURP", usuario.CURP);
                    //usuario.Rol = new ML.Rol();
                    cmd.Parameters.AddWithValue("@IdRol", usuario.Rol.IdRol);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al actualizar el usuario " + usuario.Nombre;
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

        public static ML.Result Delete(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection conn = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM [USUARIO] WHERE IdUsuario=@IdUsuario", conn);
                    cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al elimino el usuario " + usuario.Nombre;
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

        /// Procedimiento almacenados 
        public static ML.Result GetByIdSP(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                   // SqlCommand cmd = new SqlCommand();
                    //cmd.CommandText = "EXEC UsuariosGetById @IdUsuario";

                    SqlCommand cmd = new SqlCommand("UsuariosGetById ", context);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    cmd.Connection = context;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable tabla = new DataTable();
                    adapter.Fill(tabla);

                    if (tabla.Rows.Count > 0)
                    {
                        DataRow row = tabla.Rows[0];

                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = int.Parse(row[0].ToString());
                        usuario.UserName = row[1].ToString();
                        usuario.Nombre = row[2].ToString();
                        usuario.ApellidoPaterno = row[3].ToString();
                        usuario.ApellidoMaterno = row[4].ToString();
                        usuario.Email = row[5].ToString();
                        usuario.Password = row[6].ToString();
                        usuario.Sexo = row[7].ToString();
                        usuario.Telefono = row[8].ToString();
                        usuario.Celular = row[9].ToString();
                        usuario.FechaNacimiento = row[10].ToString();
                        usuario.CURP = row[11].ToString();
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = int.Parse(row[12].ToString());

                        result.Object = usuario;

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
       
        public static ML.Result GetAllSP()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection conn = new SqlConnection(DL.Conexion.Get()))
                {
                   // SqlCommand cmd = new SqlCommand("EXEC UsuariosGetAll", conn);
                    //cmd.Connection = conn;

                    SqlCommand cmd = new SqlCommand("UsuariosGetAll ", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable tabla = new DataTable();
                    adapter.Fill(tabla);

                    if (tabla.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();
                        // foreach (var lista in tabla.Rows) // En caso que no se conosca un valor 
                        foreach (DataRow row in tabla.Rows)
                        {

                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = int.Parse(row[0].ToString());
                            usuario.UserName = row[1].ToString();
                            usuario.Nombre = row[2].ToString();
                            usuario.ApellidoPaterno = row[3].ToString();
                            usuario.ApellidoMaterno = row[4].ToString();
                            usuario.Email = row[5].ToString();
                            usuario.Password = row[6].ToString();
                            usuario.Sexo = row[7].ToString();
                            usuario.Telefono = row[8].ToString();
                            usuario.Celular = row[9].ToString();
                            //usuario.FechaNacimiento = DateTime.Parse(row[10].ToString());
                            usuario.FechaNacimiento = row[10].ToString();
                            usuario.CURP = row[11].ToString();
                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = int.Parse(row[12].ToString());

                            result.Objects.Add(usuario);

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

        public static ML.Result AddSP(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection conn = new SqlConnection(DL.Conexion.Get()))
                {
                    //Opcion 1
                    //SqlCommand cmd = new SqlCommand("EXEC UsuarioAdd @UserName,@Nombre,@ApellidoPaterno, @ApellidoMaterno,@Email,@Password,@Sexo,@Telefono,@Celular,@FechaNacimiento,@CURP", conn);

                    //Opcion 1 Recomendable
                    SqlCommand cmd = new SqlCommand("UsuarioAdd ", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Password", usuario.Password);
                    cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@CURP", usuario.CURP);
                    cmd.Parameters.AddWithValue("@IdRol", usuario.Rol.IdRol);
                    cmd.Connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al insertar el usuario " + usuario.Nombre;
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

        public static ML.Result UpdateSP(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection conn = new SqlConnection(DL.Conexion.Get()))
                {
                    //SqlCommand cmd = new SqlCommand("EXEC UsuarioUpdate @IdUsuario, @UserName,@Nombre,@ApellidoPaterno, @ApellidoMaterno,@Email,@Password,@Sexo,@Telefono,@Celular,@FechaNacimiento,@CURP", conn);

                    SqlCommand cmd = new SqlCommand("UsuarioUpdate ", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Password", usuario.Password);
                    cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@CURP", usuario.CURP);
                    cmd.Parameters.AddWithValue("@IdRol", usuario.Rol.IdRol);
                    cmd.Connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al acualizar el usuario " + usuario.Nombre;
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

        public static ML.Result DeleteSP(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection conn = new SqlConnection(DL.Conexion.Get()))
                {
                    //SqlCommand cmd = new SqlCommand("exec UsuarioDelete  @IdUsuario", conn)

                    SqlCommand cmd = new SqlCommand("UsuarioDelete ", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al elimino el usuario " + usuario.Nombre;
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

        // Entity Frameword
        /*
        public static ML.Result GetByIdEF(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            

            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {
                    var objUsuario = context.UsuariosGetById(IdUsuario).FirstOrDefault();

                    result.Objects = new List<object>();

                    if (objUsuario != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = objUsuario.IdUsuario;
                        usuario.UserName = objUsuario.UserName;
                        usuario.Nombre = objUsuario.Nombre;
                        usuario.ApellidoPaterno = objUsuario.ApellidoPaterno;
                        usuario.ApellidoMaterno = objUsuario.ApellidoMaterno;
                        usuario.Email = objUsuario.Email;
                        usuario.Password = objUsuario.Password;
                        usuario.Sexo = objUsuario.Sexo;
                        usuario.Telefono = objUsuario.Telefono;
                        usuario.Celular = objUsuario.Celular;
                        usuario.FechaNacimiento = objUsuario.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                        usuario.CURP = objUsuario.CURP;
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = objUsuario.IdRol.Value;

                        result.Object = usuario;
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

        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {
                    var resultQuery = context.UsuariosGetAll().ToList();


                    if (resultQuery != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in resultQuery)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.UserName = obj.UserName;
                            usuario.Nombre = obj.Nombre;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.Email = obj.Email;
                            usuario.Password = obj.Password;
                            usuario.Sexo = obj.Sexo;
                            usuario.Telefono = obj.Telefono;
                            usuario.Celular = obj.Celular;
                            usuario.FechaNacimiento = obj.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                            usuario.CURP = obj.CURP;
                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = obj.IdRol.Value;

                            result.Objects.Add(usuario);
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
       
        public static ML.Result AddEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {
                    var resultQuery = context.UsuarioAdd(usuario.UserName, usuario.Nombre,usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.Password, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.FechaNacimiento, usuario.CURP, usuario.Rol.IdRol);

                    if (resultQuery > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se puede añadir el registro de usuario" + usuario.Nombre;
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

        public static ML.Result UpdateEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {
                    var updateResult = context.UsuarioUpdate(usuario.IdUsuario, usuario.UserName, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.Password, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.FechaNacimiento, usuario.CURP, usuario.Rol.IdRol);
                    if (updateResult >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se actualizó el status de la credencial" + usuario.Nombre;
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

        public static ML.Result DeleteEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {
                    var updateResult = context.UsuarioDelete(usuario.IdUsuario);
                    if (updateResult >= 1)
                    {
                        result.Correct = true;
                   
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al elimino el usuario " + usuario.Nombre;
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
       

        */
        
        
        // LinQ

        public static ML.Result GetByIdLinq(int IdUsuario)
        {
            ML.Result result = new ML.Result();


            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {
                    var resultQuery = (from UsuarioLinq in context.USUARIOs
                                       where UsuarioLinq.IdUsuario == IdUsuario
                                       select UsuarioLinq).First();

                    result.Objects = new List<object>();

                    if (resultQuery != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = resultQuery.IdUsuario;
                        usuario.UserName = resultQuery.UserName;
                        usuario.Nombre = resultQuery.Nombre;
                        usuario.ApellidoPaterno = resultQuery.ApellidoPaterno;
                        usuario.ApellidoMaterno = resultQuery.ApellidoMaterno;
                        usuario.Email = resultQuery.Email;
                        usuario.Password = resultQuery.Password;
                        usuario.Sexo = resultQuery.Sexo;
                        usuario.Telefono = resultQuery.Telefono;
                        usuario.Celular = resultQuery.Celular;
                        usuario.FechaNacimiento = resultQuery.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                        usuario.CURP = resultQuery.CURP;
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = resultQuery.IdRol.Value;
                        usuario.Imagen = resultQuery.Imagen;

                        result.Objects.Add(usuario);
                        result.Object = usuario;

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

        public static ML.Result GetAllLinq()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {
                    var resultQuery = (from UsuarioLinq in context.USUARIOs
                                       select UsuarioLinq).ToList();


                    if (resultQuery != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var objUsuario in resultQuery)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = objUsuario.IdUsuario;
                            usuario.UserName = objUsuario.UserName;
                            usuario.Nombre = objUsuario.Nombre;
                            usuario.ApellidoPaterno = objUsuario.ApellidoPaterno;
                            usuario.ApellidoMaterno = objUsuario.ApellidoMaterno;
                            usuario.Email = objUsuario.Email;
                            usuario.Password = objUsuario.Password;
                            usuario.Sexo = objUsuario.Sexo;
                            usuario.Telefono = objUsuario.Telefono;
                            usuario.Celular = objUsuario.Celular;
                            usuario.FechaNacimiento = objUsuario.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                            usuario.CURP = objUsuario.CURP;
                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = objUsuario.IdRol.Value;

                            result.Objects.Add(usuario);
                            
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

        public static ML.Result AddLinq(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {
                      
                    DL_EF.USUARIO usuariolinq = new DL_EF.USUARIO();

                    usuariolinq.UserName = usuario.UserName;
                    usuariolinq.Nombre = usuario.Nombre;
                    usuariolinq.ApellidoPaterno = usuario.ApellidoPaterno;
                    usuariolinq.ApellidoMaterno = usuario.ApellidoMaterno;
                    usuariolinq.Email = usuario.Email;
                    usuariolinq.Password = usuario.Password;
                    usuariolinq.Sexo = usuario.Sexo;
                    usuariolinq.Telefono = usuario.Telefono;
                    usuariolinq.Celular = usuario.Celular;
                    //usuariolinq.FechaNacimiento = usuario.FechaNacimiento;
                    //usuariolinq.FechaNacimiento = usuario.FechaNacimiento.ToString("dd-MM-yyyy");
                    usuariolinq.CURP = usuario.CURP;
                    usuariolinq.IdRol = usuario.Rol.IdRol;
                    usuariolinq.Imagen = usuario.Imagen;

                   // var query = context.USUARIOs.Add(usuariolinq);
                    context.USUARIOs.Add(usuariolinq);
                    int RowAffected = context.SaveChanges();

                    if (RowAffected > 0)
                    {
                        result.Correct = true;
                    }

                   else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se insertar el usuario" + usuario.Nombre;
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

        public static ML.Result UpdateLinq(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {


                    var usuariolinq = (from UsuarioLinq in context.USUARIOs
                                 where UsuarioLinq.IdUsuario == usuario.IdUsuario
                                 select UsuarioLinq).SingleOrDefault();


                    if (usuariolinq != null)
                    {

                        usuariolinq.IdUsuario = usuario.IdUsuario;
                        usuariolinq.UserName = usuario.UserName;
                        usuariolinq.Nombre = usuario.Nombre;
                        usuariolinq.ApellidoPaterno = usuario.ApellidoPaterno;
                        usuariolinq.ApellidoMaterno = usuario.ApellidoMaterno;
                        usuariolinq.Email = usuario.Email;
                        usuariolinq.Password = usuario.Password;
                        usuariolinq.Sexo = usuario.Sexo;
                        usuariolinq.Telefono = usuario.Telefono;
                        usuariolinq.Celular = usuario.Celular;
                        //usuariolinq.FechaNacimiento = usuario.FechaNacimiento;
                        usuariolinq.CURP = usuario.CURP;
                        usuariolinq.IdRol = usuario.Rol.IdRol;
                        usuariolinq.Imagen = usuario.Imagen;
                        context.SaveChanges();

                        result.Correct = true;
                    }

                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se actualizar el usuario" + usuario.Nombre;
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
        
        public static ML.Result DeleteLinq(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {

                    var query = (from UsuarioLinq in context.USUARIOs
                                      where UsuarioLinq.IdUsuario == usuario.IdUsuario
                                      select UsuarioLinq).SingleOrDefault();


                    if (query !=null)
                    {

                        context.USUARIOs.Remove(query);
                        context.SaveChanges();

                        result.Correct = true;
                    }

                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se alimino el usuario" + usuario.Nombre;
                    }
                }
            }
            catch (Exception ex)
            {
                //Error.Add(ex);
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;

        }

       
        
        //Los nuevos con SP de Usuario y Direccion

        //Linq 

        public static ML.Result GetAllLinQ()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {
                    var resultQuery = (from UsuarioLinq in context.USUARIOs
                                       join rol in context.Rols on UsuarioLinq.IdRol equals rol.IdRol
                                       select new { UsuarioLinq, rol.Nombre }).ToList();


                    if (resultQuery != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var objUsuario in resultQuery)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = objUsuario.UsuarioLinq.IdUsuario;
                            usuario.UserName = objUsuario.UsuarioLinq.UserName;
                            usuario.Nombre = objUsuario.UsuarioLinq.Nombre;
                            usuario.ApellidoPaterno = objUsuario.UsuarioLinq.ApellidoPaterno;
                            usuario.ApellidoMaterno = objUsuario.UsuarioLinq.ApellidoMaterno;
                            usuario.Email = objUsuario.UsuarioLinq.Email;
                            usuario.Password = objUsuario.UsuarioLinq.Password;
                            usuario.Sexo = objUsuario.UsuarioLinq.Sexo;
                            usuario.Telefono = objUsuario.UsuarioLinq.Telefono;
                            usuario.Celular = objUsuario.UsuarioLinq.Celular;
                            usuario.FechaNacimiento = objUsuario.UsuarioLinq.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                            usuario.CURP = objUsuario.UsuarioLinq.CURP;
                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = objUsuario.UsuarioLinq.IdRol.Value;
                            usuario.Rol.Nombre = objUsuario.Nombre.ToString();
                            usuario.Imagen = objUsuario.UsuarioLinq.Imagen;

                            result.Objects.Add(usuario);

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
        
        public static ML.Result GetByIdLinqP(int IdUsuario)
        {
            ML.Result result = new ML.Result();


            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {
                    var resultQuery = (from UsuarioLinq in context.USUARIOs
                                       where UsuarioLinq.IdUsuario == IdUsuario
                                       select UsuarioLinq).First();

                    result.Objects = new List<object>();

                    if (resultQuery != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = resultQuery.IdUsuario;
                        usuario.UserName = resultQuery.UserName;
                        usuario.Nombre = resultQuery.Nombre;
                        usuario.ApellidoPaterno = resultQuery.ApellidoPaterno;
                        usuario.ApellidoMaterno = resultQuery.ApellidoMaterno;
                        usuario.Email = resultQuery.Email;
                        usuario.Password = resultQuery.Password;
                        usuario.Sexo = resultQuery.Sexo;
                        usuario.Telefono = resultQuery.Telefono;
                        usuario.Celular = resultQuery.Celular;
                        usuario.FechaNacimiento = resultQuery.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                        usuario.CURP = resultQuery.CURP;
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = resultQuery.IdRol.Value;
                        usuario.Imagen = resultQuery.Imagen;

                        result.Objects.Add(usuario);
                        result.Object = usuario;

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

        //EF SP

        public static ML.Result GetAllEFP()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {
                    var resultQuery = context.UsuariosGetAll().ToList();


                    if (resultQuery != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in resultQuery)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.UserName = obj.UserName;
                            usuario.Nombre = obj.Nombre;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.Email = obj.Email;
                            usuario.Password = obj.Password;
                            usuario.Sexo = obj.Sexo;
                            usuario.Telefono = obj.Telefono;
                            usuario.Celular = obj.Celular;
                            usuario.FechaNacimiento = obj.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                            usuario.CURP = obj.CURP;
                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = obj.IdRol.Value;
                            usuario.Rol.Nombre = obj.Nombre;
                            usuario.Imagen = obj.Imagen;
                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.Calle = obj.DireccionC;
                            usuario.Statu = obj.Statu.Value ? true : false;

                            result.Objects.Add(usuario);
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
        
        public static ML.Result GetByIdEFP( int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {
                    var objUsuario = context.UsuariosGetById(IdUsuario).FirstOrDefault();

                    result.Objects = new List<object>();

                    if (objUsuario != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = objUsuario.IdUsuario;
                        usuario.UserName = objUsuario.UserName;
                        usuario.Nombre = objUsuario.Nombre;
                        usuario.ApellidoPaterno = objUsuario.ApellidoPaterno;
                        usuario.ApellidoMaterno = objUsuario.ApellidoMaterno;
                        usuario.Email = objUsuario.Email;
                        usuario.Password = objUsuario.Password;
                        usuario.Sexo = objUsuario.Sexo;
                        usuario.Telefono = objUsuario.Telefono;
                        usuario.Celular = objUsuario.Celular;
                        usuario.FechaNacimiento = objUsuario.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                        usuario.CURP = objUsuario.CURP;
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = objUsuario.IdRol.Value;
                        //usuario.Rol.Nombre = objUsuario.Nombre;
                        usuario.Imagen = objUsuario.Imagen;
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = objUsuario.IdDireccion.Value;
                        usuario.Direccion.Calle = objUsuario.Calle;
                        usuario.Direccion.NumeroInterior = objUsuario.NumeroInterior;
                        usuario.Direccion.NumeroExterior = objUsuario.NumeroInterior;
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = objUsuario.IdColonia.Value;
                        usuario.Direccion.Colonia.Nombre = objUsuario.NombreColonia;
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = objUsuario.IdMunicipio.Value;
                        usuario.Direccion.Colonia.Municipio.Nombre = objUsuario.NombreMunicipio;
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = objUsuario.IdEstado.Value;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = objUsuario.NombreEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = objUsuario.IdPais.Value;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = objUsuario.NombrePais;
                        usuario.Statu = objUsuario.Statu.Value ? true : false;


                        result.Object = usuario;
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

        public static ML.Result AddEFSP(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {
                    ObjectParameter IdUsuario  = new ObjectParameter("IdUsuario", typeof(int));
                    var resultQuery = context.UsuarioAdd(usuario.UserName, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, 
                        usuario.Email, usuario.Password, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.FechaNacimiento, usuario.CURP, 
                        usuario.Rol.IdRol, usuario.Imagen, usuario.Statu=true, IdUsuario);

                    if (resultQuery > 0)
                    {
                        result.Correct = true;
                        result.Object = Convert.ToInt32(IdUsuario.Value);
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se puede añadir el registro de usuario" + usuario.Nombre;
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


        public static ML.Result UpdateEFP(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {
                    var updateResult = context.UsuarioUpdate(usuario.IdUsuario, usuario.UserName, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno,
                        usuario.Email, usuario.Password, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.FechaNacimiento, usuario.CURP, 
                        usuario.Rol.IdRol, usuario.Imagen, usuario.Statu);
                    if (updateResult >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se actualizó el status de la credencial" + usuario.Nombre;
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

        public static ML.Result DeleteEFP(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.OTorresProgramacionCapasEntities context = new DL_EF.OTorresProgramacionCapasEntities())
                {
                    var updateResult = context.UsuarioDelete(usuario.IdUsuario);
                    if (updateResult >= 1)
                    {
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al elimino el usuario " + usuario.Nombre;
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
