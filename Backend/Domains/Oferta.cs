using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domains
{
    [Table("oferta")]
    public partial class Oferta
    {
        public Oferta()
        {
            ItemPedido = new HashSet<ItemPedido>();
        }

        [Key]
        [Column("id_oferta")]
        public int IdOferta { get; set; }
        [Required]
        [Column("estado_produto")]
        [StringLength(255)]
        public string EstadoProduto { get; set; }
        [Column("preco", TypeName = "money")]
        public decimal Preco { get; set; }
        [Column("data_fabricacao", TypeName = "date")]
        public DateTime DataFabricacao { get; set; }
        [Column("data_vencimento", TypeName = "date")]
        public DateTime DataVencimento { get; set; }
        [Column("id_usuario")]
        public int? IdUsuario { get; set; }
        [Column("id_produto")]
        public int? IdProduto { get; set; }

        [ForeignKey(nameof(IdProduto))]
        [InverseProperty(nameof(Produto.Oferta))]
        public virtual Produto IdProdutoNavigation { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuario.Oferta))]
        public virtual Usuario IdUsuarioNavigation { get; set; }
        [InverseProperty("IdOfertaNavigation")]
        public virtual ICollection<ItemPedido> ItemPedido { get; set; }
    }
}
