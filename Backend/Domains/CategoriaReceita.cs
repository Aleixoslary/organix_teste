using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domains
{
    [Table("categoria_receita")]
    public partial class CategoriaReceita
    {
        public CategoriaReceita()
        {
            Receita = new HashSet<Receita>();
        }

        [Key]
        [Column("id_categoria_receita")]
        public int IdCategoriaReceita { get; set; }
        [Column("nome_categoria")]
        [StringLength(255)]
        public string NomeCategoria { get; set; }

        [InverseProperty("IdCategoriaReceitaNavigation")]
        public virtual ICollection<Receita> Receita { get; set; }
    }
}
