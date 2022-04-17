using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Models
{
    public class UsuarioService
    {
        public List<Usuario> Listar()
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.ToList();
            }
            // essa se comunicara com a view listagem 
        }
        public Usuario Listar(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.Find(id);
            }
            // essa se comunicara com a view edição 
        }
        public void Inserir(Usuario user)

        // Preciso receber um usuario, a função, para inserir as informações 

        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Usuarios.Add(user);
                bc.SaveChanges();
            }
        }
        
        public void Editar(Usuario userEditado)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                Usuario u = bc.Usuarios.Find(userEditado.Id);
                u.Nome = userEditado.Nome;
                u.Login = userEditado.Login;
                u.Senha = userEditado.Senha;
                u.Tipo = userEditado.Tipo;
                // buscando a informação no banco de dados e trocando pela nova
                bc.SaveChanges();
            }
        }
        public void Excluir(int id) 
        // int id esta puxando a id do usuario que sera excluido 
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            // esta abrindo a interação com o banco de dados 
            {
                bc.Usuarios.Remove(bc.Usuarios.Find(id));
                //buscando o usuario que tem essa id e excluindo ele
                bc.SaveChanges();
            }
        }
        
    }


    
}