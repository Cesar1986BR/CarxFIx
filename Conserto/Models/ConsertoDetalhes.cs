using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Conserto.Models
{
    public class ConsertoDetalhes
    {
        [Key]
        public int consertoDetalhesId { get; set; }

        public int ConsertoId { get; set; }

        public int PecaId { get; set; }

        [ForeignKey("ConsertoId")]
        public virtual Consertos Conserto { get; set; }

        [ForeignKey("PecaId")]
        public virtual Pecas Pecas { get; set; }

    }
}