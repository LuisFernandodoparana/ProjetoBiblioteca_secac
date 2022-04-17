using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Biblioteca.Models;
using System.Linq;
using System.Collections.Generic;


namespace Biblioteca.Controllers
{
    public class Autenticacao
    {
        public static void CheckLogin(Controller controller)
        {   
            if(string.IsNullOrEmpty(controller.HttpContext.Session.GetString("Login")))
            {
                controller.Request.HttpContext.Response.Redirect("/Home/Login");
            }
        }
        public static bool verificaLoginSenha(string Login, string Senha, Controller controller)
        // Essa função vai checar se a um usuario no banco de dados 
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                verificaSeUsuarioAdimExiste(bc);
                Senha = Criptografo.TextoCriptografado(Senha);

                IQueryable<Usuario> UsuarioEncontrado = bc.Usuarios.Where(u => u.Login==Login && u.Senha == Senha);
                // vai pegar o usuario no banco de dados e vai ver se o login e senha digitados corresponte a algum cadastrado 
                List<Usuario> ListaUsuarioEncontrado = UsuarioEncontrado.ToList();
                // lista de usuarios encontrados

                if (ListaUsuarioEncontrado.Count == 0)
                {
                    return false;
                }
                else 
                {
                    controller.HttpContext.Session.SetString("Login", ListaUsuarioEncontrado[0].Login);
                    controller.HttpContext.Session.SetString("Nome", ListaUsuarioEncontrado[0].Nome);
                    controller.HttpContext.Session.SetInt32("Tipo", ListaUsuarioEncontrado[0].Tipo);
                    // ele vai pegar o usuario listado e vai abrir uma sessão com os dados passados
                    return true;
                }
            }
        }
        public static void verificaSeUsuarioAdimExiste(BibliotecaContext bc)
        {
            // vai criar um usuario admin para que estiver acessando pela primeira vez consiga acessar as outras paginas 
            IQueryable<Usuario> userEncontrado = bc.Usuarios.Where(u => u.Login =="admin");
            // esta fazrndo a busca por um login com a nome admin 
            if (userEncontrado.ToList().Count == 0)
            {
                Usuario admin = new Usuario();
                // esta contruindo o novo usuario 
                admin.Login = "admin";
                admin.Senha = Criptografo.TextoCriptografado("123");
                admin.Tipo = Usuario.Admin;
                admin.Nome ="administrador";

                bc.Usuarios.Add(admin);
                bc.SaveChanges();
            }
         }

         public static void verificaSeUsuarioEAdim(Controller controller)
         {
             // essa função limita o acesso a usuarios que não são admin
             if (!(controller.HttpContext.Session.GetInt32("Tipo") == Usuario.Admin))
             // pega as informações de sessão busca a variavel tipo e conpara para saber se o usuario logado é admin
             {
                controller.Request.HttpContext.Response.Redirect("/Usuario/NeedAdmin");
             }
         }
    }
}