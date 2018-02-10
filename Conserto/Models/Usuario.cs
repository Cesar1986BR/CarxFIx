using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Conserto.Models
{
    public class Usuario
    {
        [Key]
        public int UserId { get; set; }



        [Display(Name = "Nome")]
        [Required(ErrorMessage = " O Campo {0} é Obrigatório!")]
        [StringLength(50, ErrorMessage = " O Campo {0} pode ter no máximo {1} e minimo {2} caracteres ", MinimumLength = 2)]
        public string Nome { get; set; }


        [Required(ErrorMessage = " O Campo {0} é Obrigatório!")]
        [StringLength(20, ErrorMessage = " O Campo {0} pode ter no máximo {1} e minimo {2} caracteres ", MinimumLength = 7)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = " O Campo {0} é Obrigatório!")]
        [StringLength(100, ErrorMessage = " O Campo {0} pode ter no máximo {1} e minimo {2} caracteres ", MinimumLength = 10)]
        public string Endereco { get; set; }


        [Display(Name = "Imagem")]
        [DataType(DataType.ImageUrl)]
        public string Photo { get; set; }

        [Display(Name = "Cliente")]
        public bool Cliente { get; set; }

        [Display(Name = "Mecanico")]
        public bool Mecanico { get; set; }


        public virtual ICollection<Consertos> Conserto { get; set; }
        public virtual ICollection<ConsertoDetalhes> ConsertoDetalhes { get; set; }
    }
}