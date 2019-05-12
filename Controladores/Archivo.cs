using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Modelos;
using Newtonsoft.Json;

namespace Controladores
{
    public class Archivo
    {
        #region InstanciasGlobales
        Resultados Resultados = new Resultados();
        List<Animal> Animales = new List<Animal>();
        #endregion

        ///<summary>
        ///Valida el archivo adjunto
        ///</summary>
        ///<return>
        ///Devuelve un JSON con el resultado si fue exitoso o fallido y el mensaje de respuesta.
        ///</return>
        ///<param name="Archivo">
        ///Archivo Adjunto.
        ///</param>
        ///<param name="NombreArchivo">
        ///Nombre del Archivo.
        ///</param>
        public string Validaciones(byte[] Archivo,string NombreArchivo)
        {
            string Respuesta = string.Empty;
            if (Archivo.Length > 0)
            {
                Stream File = new MemoryStream(Archivo);
                string Extension = System.IO.Path.GetExtension(NombreArchivo).ToLower();
                if(Extension == ".dat" || Extension == ".txt")
                {
                    int LineasArchivo = 0;
                    using(StreamReader sr = new StreamReader(File))
                    {
                        string ln;
                        while ((ln = sr.ReadLine()) != null)
                        {
                            LineasArchivo++;
                        }
                        sr.Close();
                    }

                    if(LineasArchivo > 0)
                    {
                        Resultados.Estado = true;
                        Resultados.Respuesta = "El Archivo fue validado exitosamente";
                        Respuesta = JsonConvert.SerializeObject(Resultados);
                    }
                    else
                    {
                        Resultados.Estado = false;
                        Resultados.Respuesta = "El archivo ingresado no tiene contenido, porfavor verificar e intentar nuevamente.";
                        Respuesta = JsonConvert.SerializeObject(Resultados);
                    }
                }
                else
                {
                    Resultados.Estado = false;
                    Resultados.Respuesta = "Solo se permiten Archivos .txt o .dat";
                    Respuesta = JsonConvert.SerializeObject(Resultados);
                }
            }
            else
            {
                Resultados.Estado = false;
                Resultados.Respuesta = "Debe ingrear un archivo para poder continuar.";
                Respuesta = JsonConvert.SerializeObject(Resultados);
            }
            return Respuesta;
        }

        ///<summary>
        ///Genera la separación de bovinos y equipos y genera sus respectivos archivos
        ///</summary>
        ///<param name="Archivo">
        ///Archivo Adjunto.
        ///</param>
        ///<param name="Delimitador">
        ///Delimitar de los Bovinos
        ///</param>
        public List<Descargar> GenerarArchivos(byte[] Archivo, string Delimitador)
        {
            List<Descargar> archivos = new List<Descargar>();
            Stream File = new MemoryStream(Archivo);
            using (StreamReader sr = new StreamReader(File))
            {
                string ln;
                while ((ln = sr.ReadLine()) != null)
                {
                    if (ln.ToUpper().StartsWith(Delimitador.ToUpper()) == true)
                    {
                        Animales.Add(new Animal
                        {
                            Tipo = Animal.TipoAnimal.Bovinos.ToString(),
                            NombreAnimal = ln
                        });
                    }
                    else
                    {
                        Animales.Add(new Animal
                        {
                            Tipo = Animal.TipoAnimal.Equinos.ToString(),
                            NombreAnimal = ln
                        });
                    }
                }
                sr.Close();
            }

            string Bovinos = "Bovinos.txt";
            byte[] ContenidoBovinos = GenerarArchivo(Bovinos, Animales, Animal.TipoAnimal.Bovinos.ToString());
            string Equinos = "Equinos.txt";
            byte[] ContenidoEquinos = GenerarArchivo(Equinos, Animales, Animal.TipoAnimal.Equinos.ToString());

            archivos.Add(new Descargar { NombreArchivo = Bovinos, Contenidos = ContenidoBovinos });
            archivos.Add(new Descargar { NombreArchivo = Equinos, Contenidos = ContenidoEquinos });

            return archivos;
        }

        private byte[] GenerarArchivo(string NombreArchivo, List<Animal> Animales, string Tipo)
        {
            byte[] file = null;
            using (FileStream fs = new FileStream(NombreArchivo, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    foreach (var info in Animales.Where(x => x.Tipo == Tipo))
                    {
                        sw.WriteLine(info.NombreAnimal);
                    }
                    sw.Close();
                }
                fs.Close();
                file = System.IO.File.ReadAllBytes(fs.Name);
            }
            return file;
        }

        public byte[] ConvertFiletoBytes(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
