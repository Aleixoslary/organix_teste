using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domains
{
    [Table("usuario")]
    public partial class Usuario
    {
        public Usuario()
        {
            Endereco = new HashSet<Endereco>();
            Oferta = new HashSet<Oferta>();
            Pedido = new HashSet<Pedido>();
            Receita = new HashSet<Receita>();
            Telefone = new HashSet<Telefone>();
        }

        [Key]
        [Column("id_usuario")]
        public int IdUsuario { get; set; }
        [Required]
        [Column("nome")]
        [StringLength(255)]
        public string Nome { get; set; }
        [Required]
        [Column("CPF_CNPJ")]
        [StringLength(14)]
        public string CpfCnpj { get; set; }
        [Required]
        [Column("email")]
        [StringLength(255)]
        public string Email { get; set; }
        [Required]
        [Column("senha")]
        [StringLength(255)]
        public string Senha { get; set; }
        [Column("id_tipo")]
        public int? IdTipo { get; set; }

        [ForeignKey(nameof(IdTipo))]
        [InverseProperty(nameof(Tipo.Usuario))]
        public virtual Tipo IdTipoNavigation { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Endereco> Endereco { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Oferta> Oferta { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Pedido> Pedido { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Receita> Receita { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Telefone> Telefone { get; set; }
    }
}
