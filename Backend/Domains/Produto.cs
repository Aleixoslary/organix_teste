using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domains
{
    [Table("produto")]
    public partial class Produto
    {
        public Produto()
        {
            Oferta = new HashSet<Oferta>();
        }

        [Key]
        [Column("id_produto")]
        public int IdProduto { get; set; }
        [Column("nome_produto")]
        [StringLength(255)]
        public string NomeProduto { get; set; }
        [Column("imagem")]
        [StringLength(255)]
        public string Imagem { get; set; }

        [InverseProperty("IdProdutoNavigation")]
        public virtual ICollection<Oferta> Oferta { get; set; }
    }
}
