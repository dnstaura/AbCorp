using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testuser.Models.SysModel;

namespace testuser.ViewModels
{
    public class CumpleaniosView
    {
        public string nombreClienteAF { get; set; }
        public int dias { get; set; }


        public static List<CumpleaniosView> cumpleanosxdxd()
        {
            List<CumpleaniosView> cumpleanieros = new List<CumpleaniosView>();
            using (dbModel db = new dbModel())
            {
                var clientes = (from n in db.tblAfavorde
                                select n).ToList();

                foreach (var item in clientes)
                {
                    int diaCumple = item.fechanacimiento.Day;
                    int mesCumple = item.fechanacimiento.Month;
                    //int anioCumple = 1994;

                    DateTime proximoCumple;

                    if (DateTime.Now.Month >= mesCumple && DateTime.Now.Day >= diaCumple)
                    {
                        proximoCumple = new DateTime(DateTime.Now.AddYears(1).Year, mesCumple, diaCumple);
                    }
                    else
                    {
                        proximoCumple = new DateTime(DateTime.Now.Year, mesCumple, diaCumple);
                    }

                    TimeSpan faltan = proximoCumple.Subtract(DateTime.Now);

                    if (faltan.Days >= 0 && faltan.Days <= 200)
                    {
                        var clienteAF = new CumpleaniosView
                        {
                            nombreClienteAF = item.nombres + " " + item.apellidos,
                            //dias = faltan.Days
                            dias = faltan.Days == 0 ? faltan.Days + 1 : faltan.Days
                        };
                        cumpleanieros.Add(clienteAF);
                    }

                }

                return cumpleanieros;

            }
        }

    }
}