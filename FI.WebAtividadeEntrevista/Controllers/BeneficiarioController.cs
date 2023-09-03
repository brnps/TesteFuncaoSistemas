using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Web.Mvc;
using WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller

    {        
        public ActionResult Incluir()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Listar()
        {
            try
            {
                List<Beneficiario> beneficiarios = new BoBeneficiario().Listar();
                //Return result to jTable
                return Json(new { Result = "OK", Records = beneficiarios });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Incluir(BeneficiarioModel model)
        {
            BoBeneficiario bo = new BoBeneficiario();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                bool verificaCliente = bo.VerificarExistencia(model.CPF);
                if (verificaCliente)
                {
                    return Json("CPF já possui cadastro.");
                }

                model.Id = bo.Incluir(new Beneficiario()
                {
                    Id = model.Id,
                    CPF = model.CPF,
                    Nome = model.Nome,                    
                });


                return Json("Cadastro efetuado com sucesso");
            }
        }

        [HttpPost]
        public JsonResult Alterar(BeneficiarioModel model)
        {
            BoBeneficiario bo = new BoBeneficiario();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                bo.Alterar(new Beneficiario()
                {
                    Id = model.Id,
                    CPF = model.CPF,
                    Nome = model.Nome,                    
                });

                return Json("Cadastro alterado com sucesso");
            }
        }
    }
}