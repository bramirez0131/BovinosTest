using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladores
{
    public class Log
    {
        public void GenerarArchivoLog(Exception ex)
        {
            string NombreArchivo = HttpContext.Current.Server.MapPath("~/Logs/"+ "Log_" +DateTime.Now.ToString("dd-MM-yyyy_H-mm-ss")+".log");
            using (FileStream fs = new FileStream(@NombreArchivo, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(string.Format("Exception Message: {0}", ex.Message));
                    sw.Close();
                }
                fs.Close();
            }
        }
    }
}
