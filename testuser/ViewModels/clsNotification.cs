using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testuser.Models;

namespace testuser.ViewModels
{
    public class clsNotification
    {
        public static List<Notifications> notificacionesPendientes(/*string userId*/)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var notifications = (from n in db.Notification
                                     where n.Viewed == false /*&& n.Usuario.Id == userId*/
                                     orderby n.Id ascending
                                     select n).ToList();

                return notifications;
            }
        }
        public static List<Notifications> allNotifications(/*string userId*/)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var notifications = (from n in db.Notification
                                     //where n.Usuario.Id == userId
                                     orderby n.Id ascending
                                     select n).ToList();

                return notifications;
            }
        }
        public static int cantidadNotificacionesSinLeer(/*string userId*/)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

            var notifications = (from n in db.Notification
                                 where n.Viewed == false /*&& n.Usuario.Id == userId*/
                                 select n).Count();

            return notifications;
            }
        }

        public static int cantidadNotificacionesLeidas(/*string userId*/)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

            var notifications = (from n in db.Notification
                                 where n.Viewed == true /*&& n.Usuario.Id == userId*/
                                 select n).Count();

            return notifications;
            }
        }
    }
}