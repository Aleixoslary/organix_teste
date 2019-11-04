using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domains
{
    [Table("tipo")]
    public partial class Tipo
    {
        public Tipo()
        {
            Usuario = new HashSet<Usuario>();
        }

        [Key]
        [Column("id_tipo")]
        public int IdTipo { get; set; }
        [Required]
        [Column("perfil")]
        [StringLength(255)]
        public string Perfil { get; set; }

        [InverseProperty("IdTipoNavigation")]
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
