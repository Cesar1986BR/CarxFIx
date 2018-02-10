using Conserto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using Conserto.Models.VM;

namespace Conserto.Controllers
{
    public class ConsertosController : Controller
    {

        public ActionResult Index()
        {
            Db db = new Db();
    
           var conserto = db.Conserto.Include(c => c.Pessoa).Include(c => c.Cliente);
            
            return View(conserto.ToList());

        }



        public ActionResult Criar()
        {

            Db db = new Db();
            ViewBag.MecanicoId = new SelectList(db.Usuario.Where(u => u.Mecanico).OrderBy(u => u.Nome), "UserId", "Nome");
            ViewBag.ClienteId = new SelectList(db.Usuario.Where(u => u.Cliente).OrderBy(u => u.Nome), "UserId", "Nome");

            return View();
        }

        [HttpPost]
        public ActionResult Criar(Consertos conserto)
        {
            Db db = new Db();

            ViewBag.MecanicoId = new SelectList(db.Usuario.Where(u => u.Mecanico).OrderBy(u => u.Nome), "UserId", "Nome");
            ViewBag.ClienteId = new SelectList(db.Usuario.Where(u => u.Cliente).OrderBy(u => u.Nome), "UserId", "Nome");


            if (ModelState.IsValid)
            {

                conserto.DataCriacao = DateTime.Now;
                db.Conserto.Add(conserto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(conserto);
        }


        public ActionResult Editar(int? id)
        {

            Db db = new Db();

            Consertos conserto = db.Conserto.Find(id);
            ViewBag.MecanicoId = new SelectList(db.Usuario.Where(u => u.Mecanico).OrderBy(u => u.Nome), "UserId", "Nome", conserto.ConsertoId);
            ViewBag.ClienteId = new SelectList(db.Usuario.Where(u => u.Cliente).OrderBy(u => u.Nome), "UserId", "Nome", conserto.ConsertoId);


            return View(conserto);

        }



        [HttpPost]
        public ActionResult Editar(Consertos conserto)
        {
            Db db = new Db();

            if (ModelState.IsValid)
            {
                var db2 = new Db();
                var dadoAtual = db2.Conserto.Find(conserto.ConsertoId);

                ViewBag.ClienteId = new SelectList(db.Usuario.Where(u => u.Cliente).OrderBy(u => u.Nome), "UserId", "Nome", conserto.ConsertoId);
                ViewBag.MecanicoId = new SelectList(db.Usuario.Where(u => u.Mecanico).OrderBy(u => u.Nome), "UserId", "Nome", conserto.ConsertoId);


                conserto.DataCriacao = dadoAtual.DataCriacao;
                db.Entry(conserto).State = EntityState.Modified;
                db.SaveChanges();

                TempData["MG"] = "Tarefa atualziada com sucesso";
                return RedirectToAction("Editar");

            }

            return View(conserto);
        }


        public ActionResult Detalhes(int? id)
        {

            Db db = new Db();
            Consertos consertos = db.Conserto.Find(id);

            return View(consertos);
        }

        public ActionResult AddPecas(int? id)
        {
            Db db = new Db();

            Consertos conserto = db.Conserto.Find(id);

            var consertoDetalhes = new ConsertoDetalhes
            {
                ConsertoId = id.Value
            };
            ViewBag.PecaId = new SelectList(db.Pecas.ToList(), "Id", "Nome");
            return View(consertoDetalhes);
        }

        [HttpPost]
        public ActionResult AddPecas(ConsertoDetalhes consertodetalhes)
        {
            Db db = new Db();
            if (ModelState.IsValid)
            {

                //verifica se peça já foi adicionada
                var existe = db.ConsertoDetalhes.Where(gd => gd.ConsertoId == consertodetalhes.ConsertoId && gd.PecaId == consertodetalhes.PecaId).FirstOrDefault();
                if (existe == null)
                {
                    db.ConsertoDetalhes.Add(consertodetalhes);
                    db.SaveChanges();
                    return RedirectToAction(string.Format("Detalhes/{0}", consertodetalhes.ConsertoId));
                }

                ModelState.AddModelError(string.Empty, "Essa peça já adicionada");
            }

            ViewBag.PecaId = new SelectList(db.Pecas.ToList(), "Id", "Nome", consertodetalhes.PecaId);

            return View(consertodetalhes);
        }

        public ActionResult MostraDetalhes()
        {
            List<ConsertoDetalhes> listaConsertoDetalhes;
            using (Db db = new Db())
            {
                var conserto = db.ConsertoDetalhes.Include(c => c.Pecas);

                listaConsertoDetalhes = conserto.ToList();

            }
            return View(listaConsertoDetalhes);
        }

        protected override void Dispose(bool disposing)
        {
            Db db = new Db();
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}