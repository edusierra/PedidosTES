using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ventas.Model;

#nullable disable

namespace Ventas.Data
{
    public partial class TesDbContext : DbContext
    {
        public TesDbContext()
        {
        }

        public TesDbContext(DbContextOptions<TesDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ciudad> Ciudads { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Departamento> Departamentos { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Pedido> Pedidos { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Vendedor> Vendedors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Database=TesAmerica;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Ciudad>(entity =>
            {
                entity.HasKey(e => e.Codciu);

                entity.ToTable("CIUDAD");

                entity.Property(e => e.Codciu)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CODCIU")
                    .IsFixedLength(true);

                entity.Property(e => e.Departamento)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("DEPARTAMENTO")
                    .IsFixedLength(true);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.HasOne(d => d.DepartamentoNavigation)
                    .WithMany(p => p.Ciudads)
                    .HasForeignKey(d => d.Departamento)
                    .HasConstraintName("FK_CIUDAD_DEPARTAMENTO");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Codcli);

                entity.ToTable("CLIENTE");

                entity.Property(e => e.Codcli)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CODCLI")
                    .IsFixedLength(true);

                entity.Property(e => e.Canal)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CANAL");

                entity.Property(e => e.Ciudad)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CIUDAD")
                    .IsFixedLength(true);

                entity.Property(e => e.Cupo).HasColumnName("CUPO");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("DIRECCION");

                entity.Property(e => e.Fechacreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHACREACION");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.Padre)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PADRE")
                    .IsFixedLength(true);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TELEFONO");

                entity.Property(e => e.Vendedor)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("VENDEDOR")
                    .IsFixedLength(true);

                entity.HasOne(d => d.CiudadNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.Ciudad)
                    .HasConstraintName("FK_CLIENTE_CIUDAD");

                entity.HasOne(d => d.PadreNavigation)
                    .WithMany(p => p.InversePadreNavigation)
                    .HasForeignKey(d => d.Padre)
                    .HasConstraintName("FK_CLIENTE_CLIENTE");

                entity.HasOne(d => d.VendedorNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.Vendedor)
                    .HasConstraintName("FK_CLIENTE_VENDEDOR");
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.HasKey(e => e.Coddep);

                entity.ToTable("DEPARTAMENTO");

                entity.Property(e => e.Coddep)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CODDEP")
                    .IsFixedLength(true);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => new { e.Numpedido, e.Producto });

                entity.ToTable("ITEMS");

                entity.Property(e => e.Numpedido)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("NUMPEDIDO")
                    .IsFixedLength(true);

                entity.Property(e => e.Producto)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCTO")
                    .IsFixedLength(true);

                entity.Property(e => e.Cantidad)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("CANTIDAD");

                entity.Property(e => e.Precio)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("PRECIO");

                entity.Property(e => e.Subtotal)
                    .HasColumnType("numeric(37, 0)")
                    .HasColumnName("SUBTOTAL")
                    .HasComputedColumnSql("([PRECIO]*[CANTIDAD])", false);

                entity.HasOne(d => d.NumpedidoNavigation)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.Numpedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ITEMS_PEDIDO");

                entity.HasOne(d => d.ProductoNavigation)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.Producto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ITEMS_PRODUCTO");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.Numpedido);

                entity.ToTable("PEDIDO");

                entity.Property(e => e.Numpedido)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("NUMPEDIDO")
                    .IsFixedLength(true);

                entity.Property(e => e.Cliente)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CLIENTE")
                    .IsFixedLength(true);

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA");

                entity.Property(e => e.Vendedor)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("VENDEDOR")
                    .IsFixedLength(true);

                entity.HasOne(d => d.ClienteNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.Cliente)
                    .HasConstraintName("FK_PEDIDO_CLIENTE");

                entity.HasOne(d => d.VendedorNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.Vendedor)
                    .HasConstraintName("FK_PEDIDO_VENDEDOR");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.Codprod);

                entity.ToTable("PRODUCTO");

                entity.Property(e => e.Codprod)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CODPROD")
                    .IsFixedLength(true);

                entity.Property(e => e.Familia)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FAMILIA");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.Precio)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("PRECIO");
            });

            modelBuilder.Entity<Vendedor>(entity =>
            {
                entity.HasKey(e => e.Codvend);

                entity.ToTable("VENDEDOR");

                entity.Property(e => e.Codvend)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CODVEND")
                    .IsFixedLength(true);

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ESTADO");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        public virtual DbSet<TotalDepto> TotalDepartamento { get; set; }
        public virtual DbSet<ComisionVendedor> TotalComision { get; set; }
        public virtual DbSet<NextOrder> SiguientePedido { get; set; }
    }
}
