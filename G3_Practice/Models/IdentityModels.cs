using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace G3_Practice.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {
        public string Location { get; set; }

        public virtual ICollection<UsuariosPreferencia> UsuariosPreferencias { get; set; }
        public ICollection<Pedidos> Pedidos { get; set; }
        public ICollection<CarritoDetalle> CarritoDetalle { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que authenticationType debe coincidir con el valor definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar reclamaciones de usuario personalizadas aquí
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
              : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<PreferenciaAlimenticia> PreferenciasAlimenticias { get; set; }
        public DbSet<UsuariosPreferencia> UsuariosPreferencias { get; set; }
        public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<PedidoDetalles> PedidoDetalles { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Ventas> Ventas { get; set; }
        public DbSet<CategoriaxProducto> CategoriaxProducto { get; set; }
        public DbSet<CarritoDetalle> CarritoDetalle { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación entre ApplicationUser y PreferenciaAlimenticia a través de UsuariosPreferencia
            modelBuilder.Entity<UsuariosPreferencia>()
                .HasKey(up => new { up.ApplicationUserId, up.PreferenciaAlimenticiaId }); // Clave primaria compuesta

            modelBuilder.Entity<UsuariosPreferencia>()
                .HasRequired(up => up.ApplicationUser) // Relación con ApplicationUser
                .WithMany(u => u.UsuariosPreferencias) // Un usuario puede tener varias preferencias
                .HasForeignKey(up => up.ApplicationUserId);

            modelBuilder.Entity<UsuariosPreferencia>()
                .HasRequired(up => up.PreferenciaAlimenticia) // Relación con PreferenciaAlimenticia
                .WithMany() // Una preferencia alimenticia puede estar asociada a muchos usuarios
                .HasForeignKey(up => up.PreferenciaAlimenticiaId);

            modelBuilder.Entity<CategoriaxProducto>()
                .HasKey(cp => new { cp.ProductoId, cp.CategoriaId });

            modelBuilder.Entity<CategoriaxProducto>()
                .HasRequired(cp => cp.Producto)
                .WithMany(p => p.CategoriaxProductos)
                .HasForeignKey(cp => cp.ProductoId);

            modelBuilder.Entity<CategoriaxProducto>()
                .HasRequired(cp => cp.Categoria)
                .WithMany(c => c.CategoriaxProductos)
                .HasForeignKey(cp => cp.CategoriaId);

            modelBuilder.Entity<Pedidos>()
                .HasRequired(p => p.ApplicationUser)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.UsuarioId);

            modelBuilder.Entity<Ventas>()
                .HasRequired(v => v.Pedido)
                .WithMany(p => p.Venta)
                .HasForeignKey(v => v.PedidoId);

            modelBuilder.Entity<PedidoDetalles>()
               .HasRequired(pd => pd.Pedido)
               .WithMany(p => p.PedidoDetalle)
               .HasForeignKey(pd => pd.PedidoId);

            modelBuilder.Entity<PedidoDetalles>()
                .HasRequired(pd => pd.Producto)
                .WithMany()
                .HasForeignKey(pd => pd.ProductoId);

            modelBuilder.Entity<CarritoDetalle>()
            .HasRequired(cd => cd.ApplicationUser)
            .WithMany(u => u.CarritoDetalle)
            .HasForeignKey(cd => cd.UsuarioId);

            modelBuilder.Entity<CarritoDetalle>()
                .HasRequired(cd => cd.Producto)
                .WithMany()
                .HasForeignKey(cd => cd.ProuctoId);
        }

    }
}