using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domains
{
    [Table("receita")]
    public partial class Receita
    {
        [Key]
        [Column("id_receita")]
        public int IdReceita { get; set; }
        [Required]
        [Column("nome_receita")]
        [StringLength(255)]
        public string NomeReceita { get; set; }
        [Required]
        [Column("ingredientes", TypeName = "text")]
        public string Ingredientes { get; set; }
        [Required]
        [Column("tempo_preparo")]
        [StringLength(255)]
        public string TempoPreparo { get; set; }
        [Required]
        [Column("porcoes")]
        [StringLength(255)]
        public string Porcoes { get; set; }
        [Required]
        [Column("modo_preparo", TypeName = "text")]
        public string ModoPreparo { get; set; }
        [Column("id_usuario")]
        public int? IdUsuario { get; set; }
        [Column("id_categoria_receita")]
        public int? IdCategoriaReceita { get; set; }
        [Column("imagem")]
        [StringLength(255)]
        public string Imagem { get; set; }

        [ForeignKey(nameof(IdCategoriaReceita))]
        [InverseProperty(nameof(CategoriaReceita.Receita))]
        public virtual CategoriaReceita IdCategoriaReceitaNavigation { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuario.Receita))]
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
