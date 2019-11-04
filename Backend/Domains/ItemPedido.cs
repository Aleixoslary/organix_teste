using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domains
{
    [Table("item_pedido")]
    public partial class ItemPedido
    {
        [Key]
        [Column("id_item_pedido")]
        public int IdItemPedido { get; set; }
        [Required]
        [Column("quantidade")]
        [StringLength(255)]
        public string Quantidade { get; set; }
        [Column("id_pedido")]
        public int? IdPedido { get; set; }
        [Column("id_oferta")]
        public int? IdOferta { get; set; }

        [ForeignKey(nameof(IdOferta))]
        [InverseProperty(nameof(Oferta.ItemPedido))]
        public virtual Oferta IdOfertaNavigation { get; set; }
        [ForeignKey(nameof(IdPedido))]
        [InverseProperty(nameof(Pedido.ItemPedido))]
        public virtual Pedido IdPedidoNavigation { get; set; }
    }
}
