using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Biblioteca.Models
{
    public class BibliotecaContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {                   
            optionsBuilder.UseMySql("DataSource=localhost;DataBase=Biblioteca;User Id=root;");
        }

        public DbSet<Livro> Livros {get; set;}
        public DbSet<Emprestimo> Emprestimos {get; set;}
    }
}
