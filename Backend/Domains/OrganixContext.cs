using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Backend.Domains
{
    public partial class OrganixContext : DbContext
    {
        public OrganixContext()
        {
        }

        public OrganixContext(DbContextOptions<OrganixContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CategoriaReceita> CategoriaReceita { get; set; }
        public virtual DbSet<Endereco> Endereco { get; set; }
        public virtual DbSet<ItemPedido> ItemPedido { get; set; }
        public virtual DbSet<Oferta> Oferta { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<Receita> Receita { get; set; }
        public virtual DbSet<Telefone> Telefone { get; set; }
        public virtual DbSet<Tipo> Tipo { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LAB103401\\SQLEXPRESS; Database=Organix; User Id=sa; Password=132");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoriaReceita>(entity =>
            {
                entity.HasKey(e => e.IdCategoriaReceita)
                    .HasName("PK__categori__122FE13C0BC880D8");

                entity.Property(e => e.NomeCategoria).IsUnicode(false);
            });

            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.HasKey(e => e.IdEndereco)
                    .HasName("PK__endereco__324B959E351D003A");

                entity.Property(e => e.Bairro).IsUnicode(false);

                entity.Property(e => e.Cep).IsUnicode(false);

                entity.Property(e => e.Cidade).IsUnicode(false);

                entity.Property(e => e.Estado).IsUnicode(false);

                entity.Property(e => e.Regiao).IsUnicode(false);

                entity.Property(e => e.Rua).IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Endereco)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__endereco__id_usu__4316F928");
            });

            modelBuilder.Entity<ItemPedido>(entity =>
            {
                entity.HasKey(e => e.IdItemPedido)
                    .HasName("PK__item_ped__D6E2225073F2B239");

                entity.Property(e => e.Quantidade).IsUnicode(false);

                entity.HasOne(d => d.IdOfertaNavigation)
                    .WithMany(p => p.ItemPedido)
                    .HasForeignKey(d => d.IdOferta)
                    .HasConstraintName("FK__item_pedi__id_of__4D94879B");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.ItemPedido)
                    .HasForeignKey(d => d.IdPedido)
                    .HasConstraintName("FK__item_pedi__id_pe__4CA06362");
            });

            modelBuilder.Entity<Oferta>(entity =>
            {
                entity.HasKey(e => e.IdOferta)
                    .HasName("PK__oferta__2B7BF92F885A2F96");

                entity.Property(e => e.EstadoProduto).IsUnicode(false);

                entity.HasOne(d => d.IdProdutoNavigation)
                    .WithMany(p => p.Oferta)
                    .HasForeignKey(d => d.IdProduto)
                    .HasConstraintName("FK__oferta__id_produ__49C3F6B7");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Oferta)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__oferta__id_usuar__48CFD27E");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.IdPedido)
                    .HasName("PK__pedido__6FF01489430FFAD8");

                entity.Property(e => e.StatusPedido).IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Pedido)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__pedido__id_usuar__45F365D3");
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(e => e.IdProduto)
                    .HasName("PK__produto__BA38A6B889824077");

                entity.Property(e => e.Imagem).IsUnicode(false);

                entity.Property(e => e.NomeProduto).IsUnicode(false);
            });

            modelBuilder.Entity<Receita>(entity =>
            {
                entity.HasKey(e => e.IdReceita)
                    .HasName("PK__receita__342E73421592C4FF");

                entity.Property(e => e.Imagem).IsUnicode(false);

                entity.Property(e => e.NomeReceita).IsUnicode(false);

                entity.Property(e => e.Porcoes).IsUnicode(false);

                entity.Property(e => e.TempoPreparo).IsUnicode(false);

                entity.HasOne(d => d.IdCategoriaReceitaNavigation)
                    .WithMany(p => p.Receita)
                    .HasForeignKey(d => d.IdCategoriaReceita)
                    .HasConstraintName("FK__receita__id_cate__5165187F");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Receita)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__receita__id_usua__5070F446");
            });

            modelBuilder.Entity<Telefone>(entity =>
            {
                entity.HasKey(e => e.IdTelefone)
                    .HasName("PK__telefone__28CD68347138D82F");

                entity.Property(e => e.Celular).IsUnicode(false);

                entity.Property(e => e.Telefone1).IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Telefone)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__telefone__id_usu__403A8C7D");
            });

            modelBuilder.Entity<Tipo>(entity =>
            {
                entity.HasKey(e => e.IdTipo)
                    .HasName("PK__tipo__CF9010892187F712");

                entity.Property(e => e.Perfil).IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__usuario__4E3E04AD9755656A");

                entity.Property(e => e.CpfCnpj).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Nome).IsUnicode(false);

                entity.Property(e => e.Senha).IsUnicode(false);

                entity.HasOne(d => d.IdTipoNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdTipo)
                    .HasConstraintName("FK__usuario__id_tipo__3D5E1FD2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
