using Controladores;
using Ionic.Zip;
using Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace BovinosTest
{
    public partial class Default : System.Web.UI.Page
    {
        #region Instancias Globales
        Archivo C_Archivo = new Archivo();
        Log C_Log = new Log();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnCargarArchivo_Click(object sender, EventArgs e)
        {
            byte[] Archivo = null;
            string Resultado = string.Empty;
            List<Descargar> ListadoArchivo = new List<Descargar>();

            try
            {
                Archivo = C_Archivo.ConvertFiletoBytes(FUArchivoPlano.PostedFile.InputStream);
                Resultado = C_Archivo.Validaciones(Archivo, FUArchivoPlano.PostedFile.FileName);
            }
            catch (Exception ex)
            {
                C_Log.GenerarArchivoLog(ex);
                ModalGeneral("Ocurrio un error inesperado, porfavor intentelo nuevamente.");
            }

            if (Resultado != string.Empty)
            {
                var info = JsonConvert.DeserializeObject<Resultados>(Resultado);
                if(info != null)
                {
                    switch (info.Estado)
                    {
                        case true:
                            try
                            {
                                ListadoArchivo = C_Archivo.GenerarArchivos(Archivo, TxtFiltroBovino.Text);
                            }
                            catch (Exception ex)
                            {
                                C_Log.GenerarArchivoLog(ex);
                                ModalGeneral("Ocurrio un error inesperado, porfavor intentelo nuevamente.");
                            }
                                    
                            ModalGeneral("Se ha completado la tarea con exito");
                            GenerarDescargas(ListadoArchivo);
                        break;
                        default:
                            ModalGeneral(info.Respuesta);
                            break;
                    }
                }
            }
        }

        private void GenerarDescargas(List<Descargar> listadoArchivo)
        {
            using (ZipFile zip = new ZipFile())
            {
                foreach (var item in listadoArchivo)
                {
                    Stream stream = new MemoryStream(item.Contenidos);
                    zip.AddEntry(item.NombreArchivo, stream);
                }
                Response.ClearContent();
                Response.ClearHeaders();
                Response.AppendHeader("content-disposition", "attachment; filename=Archivos.zip");

                Response.BufferOutput = true;
                //Set zip file name  
                zip.CompressionMethod = CompressionMethod.BZip2;
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                //Save the zip content in output stream
                zip.Save(Response.OutputStream);
                Response.End();
            }
        }

        private void ModalGeneral(string Mensaje)
        {
            Page.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "ModalGeneral",
                 string.Format("<script type='text/javascript'>ModalGeneral('{0}');</script>",
                 Mensaje));
        }
    }
}