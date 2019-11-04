using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domains
{
    [Table("telefone")]
    public partial class Telefone
    {
        [Key]
        [Column("id_telefone")]
        public int IdTelefone { get; set; }
        [Required]
        [Column("telefone")]
        [StringLength(255)]
        public string Telefone1 { get; set; }
        [Required]
        [Column("celular")]
        [StringLength(255)]
        public string Celular { get; set; }
        [Column("id_usuario")]
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuario.Telefone))]
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
