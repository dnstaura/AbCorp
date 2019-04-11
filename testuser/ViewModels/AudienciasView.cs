using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testuser.Models.SysModel;

namespace testuser.ViewModels
{
    public class AudienciasView
    {
        public string nombreCLiente { get; set; }
        public string juzgado { get; set; }
        public int dias { get; set; }

        public static List<AudienciasView> Audiencias()
        {
            List<AudienciasView> NotiAudiencias = new List<AudienciasView>();

            using (dbModel db = new dbModel())
            {
                var clientesAudiencia = (from n in db.tblCasos
                                         select n).ToList();

                foreach (var item in clientesAudiencia)
                {
                    int diaAudi = item.Fecha_Audiencia.Day;
                    int mesAUdi = item.Fecha_Audiencia.Month;

                    DateTime proximaAudi;

                    if (DateTime.Now.Month > mesAUdi && DateTime.Now.Day > diaAudi)
                    {
                        proximaAudi = new DateTime(DateTime.Now.AddYears(1).Year, mesAUdi, diaAudi);
                    }
                    else
                    {
                        proximaAudi = new DateTime(DateTime.Now.Year, mesAUdi, diaAudi);
                    }

                    TimeSpan faltan = proximaAudi.Subtract(DateTime.Now);

                    if (faltan.Days >= 0 && faltan.Days <= 7)
                    {
                        var clienteAB = new AudienciasView
                        {
                            nombreCLiente = item.tblClientes.Nombres + " " + item.tblClientes.Apellidos,
                            juzgado = item.tblJuzgados.Juzgado,
                            dias = DateTime.Now.Day == diaAudi ? 0 : faltan.Days + 1
                        };

                        NotiAudiencias.Add(clienteAB);

                    }
                }

                return NotiAudiencias; 
            }
        }

    }
}
    