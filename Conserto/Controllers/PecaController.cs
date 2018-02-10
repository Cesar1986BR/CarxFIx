using Conserto.Models;
using Conserto.Models.VM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Conserto.Controllers
{
    public class PecaController : Controller
    {
        // GET: Peca
        public ActionResult Index()
        {
            List<Pecas> listaPeca;
            using (Db db = new Db())
            {
                listaPeca = db.Pecas.ToList();
            }
            return View(listaPeca);
        }

        public ActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Criar(PecaVM model)
        {
            if (ModelState.IsValid)
            {
                using (Db db = new Db())
                {
                    db.Pecas.Add(model.Pecas);
                    try
                    {

                        if (model.Fotos != null)
                        {
                            var pic = Ultilidade.UploadPhoto(model.Fotos);

                            if (!string.IsNullOrEmpty(pic))
                            {
                                model.Pecas.Photo = string.Format("~/Content/Fotos/{0}", pic);//cria pra nova foto
                            }
                        }
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                }
            }


            return View(model);
        }



        public ActionResult Editar(int? id)
        {

            PecaVM model;
            using (Db db = new Db())
            {
                Pecas peca = db.Pecas.Find(id);

                model = new PecaVM
                {
                    Pecas = peca
                };

            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(PecaVM model)
        {

            if (ModelState.IsValid)
            {

                using (Db db = new Db())// cria instancia do contexto banco
                {

                    var db2 = new Db();// cria uma instancia do contexto banco
                    var pecaAtual = db2.Pecas.Find(model.Pecas.Id);// pega o PecaVM pelo Id

                    if (model.Fotos != null)// verifica se valor da foto esta nulo
                    {
                        var pic = Ultilidade.UploadPhoto(model.Fotos);//chama metodo para adicionar foto

                        if (!string.IsNullOrEmpty(pic))// verifica se nome da foto esta vazio
                        {
                            model.Pecas.Photo = string.Format("~/Content/Fotos/{0}", pic);//altera pra nova foto
                        }
                    }
                    else
                    {
                        model.Pecas.Photo = pecaAtual.Photo;// caso contrario continua com a foto ja cadastrada
                    }

                    db.Entry(model.Pecas).State = EntityState.Modified;
                    db.SaveChanges();

                    TempData["MG"] = "Peca  alterada com sucesso";
                    return RedirectToAction("Editar");
                }
            }
            return View(model);
        }

        public ActionResult Detalhes(int id)
        {


            PecaVM model;
            using (Db db = new Db())
            {

                Pecas peca = db.Pecas.Find(id);

                model = new PecaVM
                {
                    Pecas = peca
                };
            }
            return View(model);

        }


        public ActionResult Excluir(int id)
        {
            PecaVM model;
            using (Db db = new Db())
            {

                Pecas peca = db.Pecas.Find(id);

                model = new PecaVM
                {
                    Pecas = peca
                };
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Excluir(int id, PecaVM model)
        {

            using (Db db = new Db())
            {

                Pecas peca = db.Pecas.Find(id);

                model = new PecaVM
                {
                    Pecas = peca
                };

                db.Pecas.Remove(peca);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}