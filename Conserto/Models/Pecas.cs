using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Conserto.Models
{
    public class Pecas
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Imagem")]
        [DataType(DataType.ImageUrl)]
        public string Photo { get; set; }

        [Display(Name = "Valor")]
        public decimal Valor { get; set; }


        public virtual ICollection<Consertos> Conserto { get; set; }

        public virtual ICollection<ConsertoDetalhes> ConsertoDetalhes { get; set; }
    }
}