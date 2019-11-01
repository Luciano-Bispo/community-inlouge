using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace api_comil.Models
{
    public partial class communityInLoungeContext : DbContext
    {
        public communityInLoungeContext()
        {
        }

        public communityInLoungeContext(DbContextOptions<communityInLoungeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Comunidade> Comunidade { get; set; }
        public virtual DbSet<Evento> Evento { get; set; }
        public virtual DbSet<ResponsavelEventoTw> ResponsavelEventoTw { get; set; }
        public virtual DbSet<Sala> Sala { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=communityInLounge;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.Property(e => e.Nome).IsUnicode(false);
            });

            modelBuilder.Entity<Comunidade>(entity =>
            {
                entity.Property(e => e.Descricao).IsUnicode(false);

                entity.Property(e => e.EmailContato).IsUnicode(false);

                entity.Property(e => e.Foto).IsUnicode(false);

                entity.Property(e => e.Nome).IsUnicode(false);

                entity.Property(e => e.TelefoneContato).IsUnicode(false);

                entity.HasOne(d => d.ResponsavelUsuario)
                    .WithMany(p => p.Comunidade)
                    .HasForeignKey(d => d.ResponsavelUsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comunidad__Respo__3E52440B");
            });

            modelBuilder.Entity<Evento>(entity =>
            {
                entity.Property(e => e.Coffe).IsUnicode(false);

                entity.Property(e => e.Descricao).IsUnicode(false);

                entity.Property(e => e.Diversidade).IsUnicode(false);

                entity.Property(e => e.EmailContato).IsUnicode(false);

                entity.Property(e => e.Foto).IsUnicode(false);

                entity.Property(e => e.Nome).IsUnicode(false);

                entity.Property(e => e.Publico).IsUnicode(false);

                entity.Property(e => e.StatusEvento).IsUnicode(false);

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Evento)
                    .HasForeignKey(d => d.CategoriaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Evento__Categori__48CFD27E");

                entity.HasOne(d => d.Comunidade)
                    .WithMany(p => p.Evento)
                    .HasForeignKey(d => d.ComunidadeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Evento__Comunida__4AB81AF0");

                entity.HasOne(d => d.Sala)
                    .WithMany(p => p.Evento)
                    .HasForeignKey(d => d.SalaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Evento__Sala_id__49C3F6B7");
            });

            modelBuilder.Entity<ResponsavelEventoTw>(entity =>
            {
                entity.HasOne(d => d.EventoNavigation)
                    .WithMany(p => p.ResponsavelEventoTw)
                    .HasForeignKey(d => d.Evento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Responsav__Event__4D94879B");

                entity.HasOne(d => d.ResponsavelEventoNavigation)
                    .WithMany(p => p.ResponsavelEventoTw)
                    .HasForeignKey(d => d.ResponsavelEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Responsav__Respo__4E88ABD4");
            });

            modelBuilder.Entity<Sala>(entity =>
            {
                entity.Property(e => e.Descricao).IsUnicode(false);

                entity.Property(e => e.Nome).IsUnicode(false);

                entity.Property(e => e.QntdPessoas).IsUnicode(false);
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.HasIndex(e => e.Titulo)
                    .HasName("UQ__Tipo_usu__7B406B568DFB5A3B")
                    .IsUnique();

                entity.Property(e => e.Titulo).IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Usuario__A9D10534E7FD7C30")
                    .IsUnique();

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Foto).IsUnicode(false);

                entity.Property(e => e.Genero).IsUnicode(false);

                entity.Property(e => e.Nome).IsUnicode(false);

                entity.Property(e => e.Senha).IsUnicode(false);

                entity.Property(e => e.Telefone).IsUnicode(false);

                entity.HasOne(d => d.TipoUsuario)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.TipoUsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Usuario__Tipo_us__3B75D760");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
