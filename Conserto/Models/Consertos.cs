using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Conserto.Models
{
    public class Consertos
    {
        [Key]
        public int ConsertoId { get; set; }


        public string Defeito { get; set; }

        public string Solucao { get; set; }


        public int MecanicoId { get; set; }

        public int ClienteId { get; set; }

        public DateTime DataCriacao { get; set; }


        [ForeignKey("MecanicoId")]
        public virtual Usuario Pessoa { get; set; }

        [ForeignKey("ClienteId")]
        public virtual Usuario Cliente { get; set; }


        public virtual ICollection<ConsertoDetalhes> ConsertoDetalhes { get; set; }
    }
}