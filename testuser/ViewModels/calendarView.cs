using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testuser.Models.SysModel;

namespace testuser.ViewModels
{
    public class calendarView
    {
        public string TituloEvento { get; set; }
        public int dias { get; set; }

        public static List<calendarView> Calendar()
        {
            List<calendarView> Calendario = new List<calendarView>();
            using (dbModel db = new dbModel())
            {
                var eventos = (from n in db.tblEventCalendar
                              select n).ToList();

                foreach (var item in eventos)
                {
                    int diaEvent = item.start.Day;
                    int mesEvent = item.start.Month;

                    DateTime proximoEvent;

                    if (DateTime.Now.Month > mesEvent && DateTime.Now.Day > diaEvent)
                    {
                        proximoEvent = new DateTime(DateTime.Now.AddYears(1).Year, mesEvent, diaEvent);
                    }
                    else
                    {
                        proximoEvent = new DateTime(DateTime.Now.Year, mesEvent, diaEvent);
                    }

                    TimeSpan faltan = proximoEvent.Subtract(DateTime.Now);

                    if (faltan.Days >= 0 && faltan.Days <=4)
                    {
                        var EventosCalendar = new calendarView
                        {
                            TituloEvento = item.title,
                            dias = DateTime.Now.Day == diaEvent ? 0 : faltan.Days + 1
                        };

                        Calendario.Add(EventosCalendar);

                    }
                }
                return Calendario;
            }
        }
    }
}