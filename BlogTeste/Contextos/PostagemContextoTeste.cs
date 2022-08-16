using BlogAPI.Src.Contextos;
using BlogAPI.Src.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTeste.Contextos
{
    /// <summary>
    /// <para>Resumo: Classe para texte unitario de contexto de postagem</para>
    /// <para>Criado por: Henrique</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 08/08/2022</para>
    /// </summary>
    [TestClass]
    public class PostagemContextoTeste
    {
        #region Atributos

        private BlogPessoalContexto _contexto;

        #endregion

        #region Métodos

        [TestMethod]
        public void InserirNovaPostagemRetornaPostagemInserida()
        {
            // Ambiente
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
            .UseInMemoryDatabase(databaseName: "IMD_blog_gen_PCT1")
            .Options;

            _contexto = new BlogPessoalContexto(opt);

            // DADO - Dado que adiciono um usuario no sistema
            _contexto.Usuarios.Add(new Usuario
            {
                Nome = "Henrique Melo",
                Email = "henrique@email.com",
                Senha = "134652",
                Foto = "URLFOTOHENRIQUE",
            });
            _contexto.SaveChanges();

            // E - E adciono um tema
            _contexto.Temas.Add(new Tema
            {
                Descricao = "CSHARP"
            });
            _contexto.SaveChanges();

            // E - E adciono uma novapostagem com o usuario e o tema acima
            _contexto.Postagens.Add(new Postagem
            {
                Titulo = "ASP.NET",
                Descricao = "Um framework muito importante para WEB apps",
                Foto = "URLDAFOTO",
                Criador = _contexto.Usuarios.FirstOrDefault(u => u.Email ==
                "Henrique@email.com"),
                Tema = _contexto.Temas.FirstOrDefault(t => t.Descricao == "CSHARP")
            });
            _contexto.SaveChanges();

            // QUANDO - Quando eu pesquiso pelo titulo da postagem adicionada
            var resultado = _contexto.Postagens.FirstOrDefault(p => p.Titulo ==
            "ASP.NET");

            // ENTÃO - Então deve retornar resultado nao nulo
            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public void LerListaDePostagensRetornaTresPostagens()
        {
            // Ambiente
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
            .UseInMemoryDatabase(databaseName: "IMD_blog_gen_PCT2")
            .Options;
            _contexto = new BlogPessoalContexto(opt);

            // DADO - Dado que adiciono um usuario no sistema
            _contexto.Usuarios.Add(new Usuario
            {
                Nome = "Henrique Melo",
                Email = "henrique@email.com",
                Senha = "134652",
                Foto = "URLFOTOHENRIQUEMELO",
            });
            _contexto.SaveChanges();

            // E - E adciono um tema
            _contexto.Temas.Add(new Tema
            {
                Descricao = "CSHARP"
            });
            _contexto.SaveChanges();

            // E - E adciono 3 postagens com o usuario e o tema acima
            _contexto.Postagens.Add(new Postagem
            {
                Titulo = "ASP.NET",
                Descricao = "Um framework muito importante para WEB apps",
                Foto = "URLDAFOTO",
                Criador = _contexto.Usuarios.FirstOrDefault(u => u.Email ==
                "henrique@email.com"),
                Tema = _contexto.Temas.FirstOrDefault(t => t.Descricao == "CSHARP")
            });
            _contexto.Postagens.Add(new Postagem
            {
                Titulo = "Linq",
                Descricao = "Linq é a ferramenta que utilizamos para pesquisa avançada",
                Foto = "URLDAFOTO",
                Criador = _contexto.Usuarios.FirstOrDefault(u => u.Email ==
                "henrique@email.com"),
                Tema = _contexto.Temas.FirstOrDefault(t => t.Descricao == "CSHARP")
            });
            _contexto.Postagens.Add(new Postagem
            {
                Titulo = "POO",
                Descricao = "POO é muito utilizado em CSHARP",
                Foto = "URLDAFOTO",
                Criador = _contexto.Usuarios.FirstOrDefault(u => u.Email ==
                "henrique@email.com"),
                Tema = _contexto.Temas.FirstOrDefault(t => t.Descricao == "CSHARP")
            });
            _contexto.SaveChanges();

            // QUANDO - Quando eu pesquisar por todas as postagens
            var resultado = _contexto.Postagens.ToList();

            // ENTÃO - Então deve retornar uma lista com 3 postagens
            Assert.AreEqual(3, resultado.Count);
        }

        [TestMethod]
        public void AtualizarPostagenRetornaPostagenAtualizado()
        {
            // Ambiente
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
            .UseInMemoryDatabase(databaseName: "IMD_blog_gen_PCT3")
            .Options;

            _contexto = new BlogPessoalContexto(opt);

            // DADO - Dado que adiciono um usuario no sistema
            _contexto.Usuarios.Add(new Usuario
            {
                Nome = "Henrique Melo",
                Email = "henrique@email.com",
                Senha = "134652",
                Foto = "URLFOTOGUSTAVOBOAZ",
            });
            _contexto.SaveChanges();

            // E - E adciono um tema
            _contexto.Temas.Add(new Tema
            {
                Descricao = "CSHARP"
            });
            _contexto.SaveChanges();

            // E - E adciono uma postagen com o usuario e o tema acima
            _contexto.Postagens.Add(new Postagem
            {
                Titulo = "ASP.NET",
                Descricao = "É um concorrente de peso para o Spring",
                Foto = "URLDAFOTO",

                Criador = _contexto.Usuarios.FirstOrDefault(u => u.Email ==
                "henrique@email.com"),
                Tema = _contexto.Temas.FirstOrDefault(t => t.Descricao == "CSHARP")
            });
            _contexto.SaveChanges();

            // E - E altero sua descrição
            var auxiliar = _contexto.Postagens.FirstOrDefault(p => p.Id == 1);
            auxiliar.Descricao = "É um concorrente de peso para o Spring WEB";
            _contexto.Postagens.Update(auxiliar);
            _contexto.SaveChanges();

            // QUANDO - Quando pesquiso pela postegem alterada
            var resultado = _contexto.Postagens.FirstOrDefault(p => p.Id == 1);

            // ENTÃO - Então deve retornar alteração
            Assert.AreEqual("É um concorrente de peso para o Spring WEB",
            resultado.Descricao);
        }

        [TestMethod]
        public void DeletaPostagemRetornaPostagemInesistente()
        {
            // Ambiente
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
            .UseInMemoryDatabase(databaseName: "IMD_blog_gen_PCT4")
            .Options;

            _contexto = new BlogPessoalContexto(opt);

            // DADO - Dado que adiciono um usuario no sistema
            _contexto.Usuarios.Add(new Usuario
            {
                Nome = "Henrique Melo",
                Email = "hernique@email.com",
                Senha = "134652",
                Foto = "URLFOTOHENRIQUEMELO",
            });
            _contexto.SaveChanges();

            // E - E adiciono um tema
            _contexto.Temas.Add(new Tema
            {
                Descricao = "CSHARP"
            });
            _contexto.SaveChanges();

            // E - E adiciono uma postagen com o usuario e o tema acima
            _contexto.Postagens.Add(new Postagem
            {
                Titulo = "ASP.NET",
                Descricao = "Blablablaa escrito ERRADO",
                Foto = "URLDAFOTO",
                Criador = _contexto.Usuarios.FirstOrDefault(u => u.Email ==
                "henrique@email.com"),
                Tema = _contexto.Temas.FirstOrDefault(t => t.Descricao == "CSHARP")
            });
            _contexto.SaveChanges();

            // QUANDO - Quando deleto a postagem inserida
            var auxiliar = _contexto.Postagens.FirstOrDefault(p => p.Titulo ==
            "ASP.NET");
            _contexto.Postagens.Remove(auxiliar);
            _contexto.SaveChanges();

            // E - E pesquiso pelo ttitulo da postagem ASP.NET
            var resultado = _contexto.Postagens.FirstOrDefault(p => p.Titulo ==
            "ASP.NET");

            // ENTÃO - Então deve retornar resultado nulo
            Assert.IsNull(resultado);
        }

        #endregion

    }
}
