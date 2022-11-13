
using LogicaNegocio.Controllers;
using Model.Entidades;
using Model.Infraestrutura.Conection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(ClientConection.ListClients());
        }
        [HttpPost]
        public ActionResult getclient(int id)
        {
            return Json(ClientConection.GetClient(id));
        }

        public ActionResult getclientComplet(int id)
        {
            return Json(ClientConection.GetClientComplet(id));
        }

        [HttpPost]
        public ActionResult Deleteclient(int id)
        {
            return Json(ClientConection.DeleteClient(id));
        }

        [HttpPost]
        public ActionResult Newclient(ClientComplet model)
        {
            string status_response = "", message = "";
            Client client = null;
            ClientComplet clientComplet = null;
            bool status = true;

            var validation = ValidationCpfCnpj.Validation(model.client.cpf_cnpj, model.client.id, model.client.type_client, model.address.type_address);
            if (!validation.status)
            {
                status_response = validation.status_response;
                status = validation.status;
                message = validation.message;
            }
            if (status)
            {
                status_response = "Sucesso";
                message = "Sucesso";
                client = ClientConection.SaveClient(model);
                clientComplet = ClientConection.GetClientComplet(client.id);
            }
            return Json(new { status = status_response, message= message, data = clientComplet });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            
            return View();
        }
    }
}