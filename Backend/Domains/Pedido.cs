using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domains
{
    [Table("pedido")]
    public partial class Pedido
    {
        public Pedido()
        {
            ItemPedido = new HashSet<ItemPedido>();
        }

        [Key]
        [Column("id_pedido")]
        public int IdPedido { get; set; }
        [Column("data_pedido", TypeName = "date")]
        public DateTime DataPedido { get; set; }
        [Required]
        [Column("status_pedido")]
        [StringLength(255)]
        public string StatusPedido { get; set; }
        [Column("id_usuario")]
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuario.Pedido))]
        public virtual Usuario IdUsuarioNavigation { get; set; }
        [InverseProperty("IdPedidoNavigation")]
        public virtual ICollection<ItemPedido> ItemPedido { get; set; }
    }
}
