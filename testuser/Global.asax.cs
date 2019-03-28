using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using testuser.Models;
using System.Data.Entity;
using System.Net;
using testuser.Controllers;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Entity.Migrations;
using testuser.Models.SysModel;

namespace testuser
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<
                    Models.ApplicationDbContext,
                    Migrations.Configuration>());


            //Crear super usuario al iniciar la app

            //ApplicationDbContext db = new ApplicationDbContext();
            //CreateRoles(db);
            //CreateSuperuser(db);
            //AddPermisionsToSuperuser(db);
            //db.Dispose();

            //FIN

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            SqlDependency.Start(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        protected void Application_End()
        {
            SqlDependency.Stop(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }



        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            var httpContext = ((HttpApplication)sender).Context;
            httpContext.Response.Clear();
            httpContext.ClearError();

            if (new HttpRequestWrapper(httpContext.Request).IsAjaxRequest())
            {
                return;
            }

            ExecuteErrorController(httpContext, exception as HttpException);
        }

        private void ExecuteErrorController(HttpContext httpContext, HttpException exception)
        {
            var routeData = new RouteData();
            routeData.Values["controller"] = "Error";

            if (exception != null && exception.GetHttpCode() == (int)HttpStatusCode.NotFound)
            {
                routeData.Values["action"] = "NotFound";
            }
            else
            {
                routeData.Values["action"] = "InternalServerError";
            }

            using (Controller controller = new ErrorController())
            {
                ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
            }
        }

        //private void AddPermisionsToSuperuser(ApplicationDbContext db)
        //{
        //    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

        //    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

        //    var user = userManager.FindByName("dnzoxyd@hotmail.com");

        //    if (!userManager.IsInRole(user.Id, "Administrador"))
        //    {
        //        userManager.AddToRole(user.Id, "Administrador");
        //    }
        //    if (!userManager.IsInRole(user.Id, "Notario"))
        //    {
        //        userManager.AddToRole(user.Id, "Notario");
        //    }
        //    if (!userManager.IsInRole(user.Id, "Abogacia"))
        //    {
        //        userManager.AddToRole(user.Id, "Abogacia");
        //    }
        //    if (!userManager.IsInRole(user.Id, "Secretaria"))
        //    {
        //        userManager.AddToRole(user.Id, "Secretaria");
        //    }

        //}

        //private void CreateSuperuser(ApplicationDbContext db)
        //{
        //    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

        //    var user = userManager.FindByName("dnzoxyd@hotmail.com");

        //    if (user == null)
        //    {
        //        user = new ApplicationUser
        //        {
        //            UserName = "dnzoxyd@hotmail.com",
        //            Email = "dnzoxyd@hotmail.com",
        //            PhoneNumber ="7700-0000"                  
        //        };
        //        userManager.Create(user, "+Dennis1");
        //    }
        //}

        //private void CreateRoles(ApplicationDbContext db)
        //{
        //    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
        //    if (!roleManager.RoleExists("Administrador"))
        //    {
        //        roleManager.Create(new IdentityRole("Administrador"));
        //    }

        //    if (!roleManager.RoleExists("Notario"))
        //    {
        //        roleManager.Create(new IdentityRole("Notario"));
        //    }

        //    if (!roleManager.RoleExists("Abogacia"))
        //    {
        //        roleManager.Create(new IdentityRole("Abogacia"));
        //    }

        //    if (!roleManager.RoleExists("Secretaria"))
        //    {
        //        roleManager.Create(new IdentityRole("Secretaria"));
        //    }
        //}
    }
}
