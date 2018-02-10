using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Conserto.Models.VM
{
    public class UsuarioVM
    {

        public Usuario Usuario { get; set; }

        public HttpPostedFileBase Fotos { get; set; }

    }
}