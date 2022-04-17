using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Listagem()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdim(this);
            return View(new UsuarioService().Listar());
            //  estamos criando a classe de forma direta e não precissamos pegar um objeto
        }
        public IActionResult Cadastro()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdim(this);
            return View();
        }
        public IActionResult ExcluirUsuario(int id)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdim(this);
            return View(new UsuarioService().Listar(id));
            // quando eu clicar em excluirusuairo na view ele se comunicara com o contrller que se comunicara com o metodo listar em models buscado apenas o usuario correspondente 

        }
        [HttpPost]
        public IActionResult ExcluirUsuario(string decisao, int id)
        {
            if(decisao == "EXCLUIR")
            {
                new UsuarioService().Excluir(id);
                
            }
            return View("Listagem", new UsuarioService().Listar());
        }
        [HttpPost]
        public IActionResult Cadastro( Usuario u)
        {
            u.Senha = Criptografo.TextoCriptografado(u.Senha);
            new UsuarioService().Inserir(u);
            return RedirectToAction("CadastroRealizado");
        }
        public IActionResult EditarUsuario(int id)
        // vamos receber uma id
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdim(this);
            Usuario u = new UsuarioService().Listar(id);
            // quando se cria um novo usuario é porque ele deve aparecer na view
            return View(u);
        }
        [HttpPost]
        public IActionResult EditarUsuario(Usuario u)
        {
            u.Senha = Criptografo.TextoCriptografado(u.Senha);
            new UsuarioService().Editar(u);
            return RedirectToAction("Listagem");
        }
        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult NeedAdmin()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }
        public IActionResult CadastroRealizado()
        {
            return View();
        }
    }
}