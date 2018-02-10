using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Conserto.Models.VM
{
    public class PecaVM
    {

        public  Pecas  Pecas { get; set; }

        public HttpPostedFileBase Fotos { get; set; }

    }
}