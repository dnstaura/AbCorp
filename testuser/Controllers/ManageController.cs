using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using testuser.Hubs;
using testuser.Models;

namespace testuser.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //NOTIFICACIONES
        public ActionResult VerNotificaciones()
       {
            ApplicationDbContext db = new ApplicationDbContext();
            //var userId = User.Identity.GetUserId();
            var notifiaciones = (from n in db.Notification
                                 //where n.Usuario_Id == userId
                                 orderby n.Id descending
                                 select n).ToList();

            foreach (var item in notifiaciones)
            {
                var diferencia = DateTime.Now - item.Date;
                int dias = diferencia.Days;

                if (dias > 30)
                {
                    db.Notification.Remove(item);
                    db.SaveChanges();
                }
            }

            return View(notifiaciones);
        }

        public ActionResult ViewedAllNotifications()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            //var userId = User.Identity.GetUserId();
            var notifiaciones = (from n in db.Notification
                                 //where n.Usuario_Id == userId
                                 select n).ToList();

            foreach (var item in notifiaciones)
            {
                if (item.Viewed == false)
                {
                    item.Viewed = true;
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return RedirectToAction("VerNotificaciones");
        }

        public ActionResult DeleteAllNotifications()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var elnot = (from n in db.Notification
                         select n).ToList();

            foreach (var item in elnot)
            {
                if (item.Viewed == true)
                {
                    db.Notification.Remove(item);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("VerNotificaciones");
        }

        public ActionResult MarcarLeido(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var notificacion = db.Notification.Find(id);

            notificacion.Viewed = true;
            db.Entry(notificacion).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("VerNotificaciones");
        }

        public ActionResult NotificacionesLeidas()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var notificaciones = (from n in db.Notification
                                  where n.Viewed == true
                                  orderby n.Id descending
                                  select n).ToList();

            return View("VerNotificaciones", notificaciones);
        }

        public ActionResult NotificacionesSinLeer()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var notificaciones = (from n in db.Notification
                                  where n.Viewed == false
                                  orderby n.Id descending
                                  select n).ToList();

            return View("VerNotificaciones", notificaciones);
        }

        public ActionResult EliminarNotificacion(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var notificacion = db.Notification.Find(id);

                db.Notification.Remove(notificacion);
                db.SaveChanges();
            }

            return RedirectToAction("VerNotificaciones");
        }
        //FIN NOTIFICACIONES

        /*Tiempo real notificaciones*/
        public JsonResult NotificacionesPendientes()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(string.Format(@"SELECT [Message] FROM [dbo].[Notifications] WHERE [Viewed] <> 1"), connection))
                {
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == System.Data.ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    SqlDataReader reader = command.ExecuteReader();

                    var listEmp = reader.Cast<IDataRecord>()
                        .Select(x => new
                        {
                            Message = (string)x["Message"]
                        }).ToList();

                    return Json(new { listEmp = listEmp }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult ContarNotificaciones()
        {

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(string.Format(@"SELECT COUNT(*) FROM [dbo].[Notifications] WHERE Viewed <> 1"), connection))
                {
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == System.Data.ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    SqlDataReader reader = command.ExecuteReader();


                    var cantNot = reader.Cast<IDataRecord>()
                        .Select(x => new
                        {
                            Cantidad = (int)x[0]
                        }).First();

                    return Json(new { cantNot = cantNot }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            NotificationHub.Show();
        }
        /*Fin Tiempo real notificaciones*/
        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Su contraseña se ha cambiado."
                : message == ManageMessageId.SetPasswordSuccess ? "Su contraseña se ha establecido."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Su proveedor de autenticación de dos factores se ha establecido."                
                : message == ManageMessageId.AddPhoneSuccess ? "Se ha agregado su número de teléfono."
                : message == ManageMessageId.RemovePhoneSuccess ? "Se ha quitado su número de teléfono."
                : message == ManageMessageId.EmptyImage ? "No se ha seleccionado ninguna imagen"
                : message == ManageMessageId.SuccessImage ? "La imagen se cambió correctamente"
                : message == ManageMessageId.ChangeNameSuccess ? "El nombre se cambió correctamente"
                : "";

            ViewBag.Error =
                 message == ManageMessageId.ErrorPassword ? "Algo salió mal al cambiar la contraseña"
                 : message == ManageMessageId.Error ? "Se ha producido un error."
                 : message == ManageMessageId.EmptyPassword ? "No se introdujeron todas las contraseñas"
                 : message == ManageMessageId.EmptyName ? "El campo Nombres está vacío"
                 : message == ManageMessageId.ErrorChangeName ? "Algo salió mal al cambiar el nombre. Intente nuevamente"
                 : "";

            var userId = User.Identity.GetUserId();
            var usuario = UserManager.FindById(userId);
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId),
                Nombres = usuario.Nombres,
                Id = usuario.Id,
                Email = usuario.Email,
                Imagen = usuario.Imagen
            };
            return View(model);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generar el token y enviarlo
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Su código de seguridad es: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Enviar un SMS a través del proveedor de SMS para verificar el número de teléfono
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // Si llegamos a este punto, es que se ha producido un error, volvemos a mostrar el formulario
            ModelState.AddModelError("", "No se ha podido comprobar el teléfono");
            return View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.EmptyPassword });
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.PasswordChange.OldPassword, model.PasswordChange.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return RedirectToAction("Index", new { Message = ManageMessageId.ErrorPassword });
        }

        [HttpPost]
        public async Task<ActionResult> CambiarEstado(string id)
        {
            var usuario = await UserManager.FindByIdAsync(id);

            bool estadoAnterior = usuario.Estado;

            if (estadoAnterior)
            {
                usuario.Estado = !estadoAnterior;
            }
            else
            {
                usuario.Estado = !estadoAnterior;
            }

            IdentityResult result = await UserManager.UpdateAsync(usuario);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Users");
            }
            else
            {
                return RedirectToAction("Index", "Users");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CambiarImagen(HttpPostedFileBase Imagen)
        {
            if (Imagen == null)
            {
                var userId = User.Identity.GetUserId();
                TempData["UserId"] = userId;
                return RedirectToAction("Index", new { Message = ManageMessageId.EmptyImage });
            }

            var user = User.Identity.IsAuthenticated;
            if (user)
            {

                var email = User.Identity.GetUserName();
                var usuario = await UserManager.FindByEmailAsync(email);


                var userId = User.Identity.GetUserId();
                string imagen = (userId + "-" + Imagen.FileName).ToLower();

                Imagen.SaveAs(Server.MapPath("~/Content/Perfiles/" + imagen));

                

                usuario.Imagen = "~/Content/Perfiles/" + imagen;

                IdentityResult result = await UserManager.UpdateAsync(usuario);

                if (result.Succeeded)
                {
                    var userID = User.Identity.GetUserId();
                    TempData["UserId"] = userID;
                    Session["ImagenPerfil"] = usuario.Imagen;
                    return RedirectToAction("Index", new { Message = ManageMessageId.SuccessImage });
                }
            }
            var usuarioId = User.Identity.GetUserId();
            TempData["UserId"] = usuarioId;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> CambiarNombre(string Nombres)
        {
            if (Nombres != "")
            {
                var user = User.Identity.IsAuthenticated;
                if (user)
                {
                    var email = User.Identity.GetUserName();

                    var usuario = await UserManager.FindByEmailAsync(email);

                    usuario.Nombres = Nombres;

                    IdentityResult result = await UserManager.UpdateAsync(usuario);

                    if (result.Succeeded)
                    {
                        ApplicationDbContext db = new ApplicationDbContext();
                        Notifications notificacion = new Notifications();
                        notificacion.Module = "Usuarios";
                        notificacion.Message = string.Format("Cambió su nombre correctamente");
                        notificacion.Date = DateTime.Now;
                        notificacion.Viewed = false;
                        notificacion.Usuario_Id = User.Identity.GetUserId();

                        db.Notification.Add(notificacion);
                        db.SaveChanges();

                        return RedirectToAction("Index", new { Message = ManageMessageId.ChangeNameSuccess });
                    }

                    return RedirectToAction("Index", new { Message = ManageMessageId.ErrorChangeName });

                }

                return RedirectToAction("Index", new { Message = ManageMessageId.ChangeNameSuccess });

            }
            else
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.EmptyName });

            }
        }
        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // Si llegamos a este punto, es que se ha producido un error, volvemos a mostrar el formulario
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "Se ha quitado el inicio de sesión externo."
                : message == ManageMessageId.Error ? "Se ha producido un error."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Solicitar la redirección al proveedor de inicio de sesión externo para vincular un inicio de sesión para el usuario actual
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

#region Aplicaciones auxiliares
        // Se usan para protección XSRF al agregar inicios de sesión externos
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error,
            EmptyImage,
            ErrorImage, 
            SuccessImage,
            ErrorPassword, 
            EmptyPassword, 
            ChangeNameSuccess,
            EmptyName,
            ErrorChangeName
        }

#endregion
    }
}