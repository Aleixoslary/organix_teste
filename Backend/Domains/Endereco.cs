using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domains
{
    [Table("endereco")]
    public partial class Endereco
    {
        [Key]
        [Column("id_endereco")]
        public int IdEndereco { get; set; }
        [Required]
        [Column("CEP")]
        [StringLength(9)]
        public string Cep { get; set; }
        [Required]
        [Column("rua")]
        [StringLength(255)]
        public string Rua { get; set; }
        [Required]
        [Column("bairro")]
        [StringLength(255)]
        public string Bairro { get; set; }
        [Required]
        [Column("cidade")]
        [StringLength(255)]
        public string Cidade { get; set; }
        [Required]
        [Column("estado")]
        [StringLength(255)]
        public string Estado { get; set; }
        [Required]
        [Column("regiao")]
        [StringLength(255)]
        public string Regiao { get; set; }
        [Column("id_usuario")]
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuario.Endereco))]
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
