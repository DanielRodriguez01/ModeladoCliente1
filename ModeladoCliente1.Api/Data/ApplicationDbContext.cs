using Microsoft.EntityFrameworkCore;
using ModeladoCliente1.Api.Entidades;

namespace ModeladoCliente1.Api.Data
{
    public class ApplicationDbContext : DbContext //DbContext es la clase central de EF que representa una sesión con la base de datos: permite consultar y guardar entidades
    {
        //Constructor que se ejecuta al crear una instancia...
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<RegistroCliente> RegistroClientes { get; set; }


        
        protected override void OnModelCreating(ModelBuilder modelBuilder) // Este es el método que EF llama durante el arranque para construir el modelo (la representación en memoria de tablas, columnas, relaciones)
        {

            // Aqui estan las claves primarias...

            modelBuilder.Entity<Cliente>().HasKey(c => c.ID); //es una expresión lambda (función corta): toma una instancia c de Cliente y devuelve la propiedad ID. EF lo usa para saber qué columna es la PK
            modelBuilder.Entity<RegistroCliente>().HasKey(r => r.IDRegistro);



            // Aqui esta la relación 1:N (o sea uno a muchos) Cliente -> RegistroCliente...

            modelBuilder.Entity<RegistroCliente>()
                .HasOne(r => r.Cliente)
                .WithMany(c => c.Registros)
                .HasForeignKey(r => r.IDCliente)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
