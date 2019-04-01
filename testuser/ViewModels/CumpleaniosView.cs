using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testuser.Models.SysModel;

namespace testuser.ViewModels
{
    public class CumpleaniosView
    {
        public string nombreCliente { get; set; }
        public int dias { get; set; }


        public static List<CumpleaniosView> cumpleanosAF()
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

                    if (DateTime.Now.Month > mesCumple && DateTime.Now.Day > diaCumple)
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
                            nombreCliente = item.nombres + " " + item.apellidos,
                            //dias = faltan.Days
                            //dias = faltan.Days == 0 ? faltan.Days + 1 : faltan.Days
                            dias = DateTime.Now.Day == diaCumple ? 0 : faltan.Days + 1
                        };
                        cumpleanieros.Add(clienteAF);
                    }

                }
                return cumpleanieros;
            }
        }

        public static List<CumpleaniosView> cumpleanosOT()
        {
            List<CumpleaniosView> cumpleanieros = new List<CumpleaniosView>();
            using (dbModel db = new dbModel())
            {
                var clientes = (from n in db.tblOtorgante
                                select n).ToList();

                foreach (var item in clientes)
                {
                    int diaCumple = item.fechanacimiento.Day;
                    int mesCumple = item.fechanacimiento.Month;
                    //int anioCumple = 1994;

                    DateTime proximoCumple;

                    if (DateTime.Now.Month > mesCumple && DateTime.Now.Day > diaCumple)
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
                        var clienteOT = new CumpleaniosView
                        {
                            nombreCliente = item.nombres + " " + item.apellidos,
                            //dias = faltan.Days
                            //dias = faltan.Days == 0 ? faltan.Days + 1 : faltan.Days
                            dias = DateTime.Now.Day == diaCumple ? 0 : faltan.Days + 1
                        };
                        cumpleanieros.Add(clienteOT);
                    }

                }
                return cumpleanieros;
            }
        }

        public static List<CumpleaniosView> cumpleanosEMP()
        {
            List<CumpleaniosView> cumpleanieros = new List<CumpleaniosView>();
            using (dbModel db = new dbModel())
            {
                var clientes = (from n in db.tblPersonal
                                select n).ToList();

                foreach (var item in clientes)
                {
                    int diaCumple = item.Fecha_Nacimiento.Day;
                    int mesCumple = item.Fecha_Nacimiento.Month;
                    //int anioCumple = 1994;

                    DateTime proximoCumple;

                    if (DateTime.Now.Month > mesCumple && DateTime.Now.Day > diaCumple)
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
                        var clienteOT = new CumpleaniosView
                        {
                            nombreCliente = item.Nombres + " " + item.Apellidos,
                            //dias = faltan.Days
                            //dias = faltan.Days == 0 ? faltan.Days + 1 : faltan.Days
                            dias = DateTime.Now.Day == diaCumple ? 0 : faltan.Days + 1
                        };
                        cumpleanieros.Add(clienteOT);
                    }

                }
                return cumpleanieros;
            }
        }
        public static List<CumpleaniosView> cumpleanosCLI()
        {
            List<CumpleaniosView> cumpleanieros = new List<CumpleaniosView>();
            using (dbModel db = new dbModel())
            {
                var clientes = (from n in db.tblClientes
                                select n).ToList();

                foreach (var item in clientes)
                {
                    int diaCumple = item.Fecha_Nacimiento.Day;
                    int mesCumple = item.Fecha_Nacimiento.Month;
                    //int anioCumple = 1994;

                    DateTime proximoCumple;

                    if (DateTime.Now.Month > mesCumple && DateTime.Now.Day > diaCumple)
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
                        var clienteOT = new CumpleaniosView
                        {
                            nombreCliente = item.Nombres + " " + item.Apellidos,
                            //dias = faltan.Days
                            //dias = faltan.Days == 0 ? faltan.Days + 1 : faltan.Days
                            dias = DateTime.Now.Day == diaCumple ? 0 : faltan.Days + 1
                        };
                        cumpleanieros.Add(clienteOT);
                    }

                }
                return cumpleanieros;
            }
        }

    }
}