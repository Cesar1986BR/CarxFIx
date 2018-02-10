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
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            List<Usuario> listaUser;
            using (Db db = new Db())
            {
                listaUser = db.Usuario.ToList();
            }
            return View(listaUser);
        }

        public ActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Criar(UsuarioVM model)
        {
            if (ModelState.IsValid)
            {
                using (Db db = new Db())
                {
                    db.Usuario.Add(model.Usuario);
                    try
                    {

                        if (model.Fotos != null)
                        {
                            var pic = Ultilidade.UploadPhoto(model.Fotos);

                            if (!string.IsNullOrEmpty(pic))
                            {
                                model.Usuario.Photo = string.Format("~/Content/Fotos/{0}", pic);//cria pra nova foto
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

            UsuarioVM model;
            using (Db db = new Db())
            {
                Usuario user = db.Usuario.Find(id);

                model = new UsuarioVM
                {
                    Usuario = user
                };

            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(UsuarioVM model)
        {

            if (ModelState.IsValid)
            {

                using (Db db = new Db())// cria instancia do contexto banco
                {

                    var db2 = new Db();// cria uma instancia do contexto banco
                    var usuarioAtual = db2.Usuario.Find(model.Usuario.UserId);// pega o usuario pelo Id

                    if (model.Fotos != null)// verifica se valor da foto esta nulo
                    {
                        var pic = Ultilidade.UploadPhoto(model.Fotos);//chama metodo para adicionar foto

                        if (!string.IsNullOrEmpty(pic))// verifica se nome da foto esta vazio
                        {
                            model.Usuario.Photo = string.Format("~/Content/Fotos/{0}", pic);//altera pra nova foto
                        }
                    }
                    else
                    {
                        model.Usuario.Photo = usuarioAtual.Photo;// caso contrario continua com a foto ja cadastrada
                    }

                    db.Entry(model.Usuario).State = EntityState.Modified;
                    db.SaveChanges();

                    TempData["MG"] = "Usuario  alterada com sucesso";
                    return RedirectToAction("Editar");
                }
            }
            return View(model);
        }

        public ActionResult Detalhes(int id)
        {


            UsuarioVM model;
            using (Db db = new Db())
            {

                Usuario user = db.Usuario.Find(id);

                model = new UsuarioVM
                {
                    Usuario = user
                };
            }
            return View(model);

        }


        public ActionResult Excluir(int id)
        {
            UsuarioVM model;
            using (Db db = new Db())
            {

                Usuario user = db.Usuario.Find(id);

                model = new UsuarioVM
                {
                    Usuario = user
                };
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Excluir(int id,UsuarioVM model)
        {
        
            using (Db db = new Db())
            {

                Usuario user = db.Usuario.Find(id);

                model = new UsuarioVM
                {
                    Usuario = user
                };

                db.Usuario.Remove(user);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }



    }
}