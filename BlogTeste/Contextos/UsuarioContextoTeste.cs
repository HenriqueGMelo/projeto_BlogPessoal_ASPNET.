using BlogAPI.Src.Contextos;
using BlogAPI.Src.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BlogTeste.Contextos
{
    /// <summary>
    /// <para>Resumo: Classe para texte unitario de contexto de usuario</para>
    /// <para>Criado por: Henrique </para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 08/08/2022</para>
    /// </summary>
    [TestClass]
    public class UsuarioContextoTeste
    {
        #region Atributos

        private BlogPessoalContexto _contexto;

        #endregion

        #region Métodos

        [TestMethod]
        public void InserirNovoUsuarioRetornaUsuarioInserido()
        {
            // Ambiente
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
            .UseInMemoryDatabase(databaseName: "IMD_blog_gen_UCT1")
            .Options;

            _contexto = new BlogPessoalContexto(opt);

            // DADO - Dados que adiciono um usuario no sistema
            _contexto.Usuarios.Add(new Usuario
            {
                Nome = "Henrique Melo",
                Email = "henriqueM@hotmail.com",
                Senha = "134652",
                Foto = "URLFOTOHENRIQUEMELO",
            });
            _contexto.SaveChanges();

            // QUANDO - Quando eu pesquiso pelo e-mail do usuario adicionado
            var resultado = _contexto.Usuarios.FirstOrDefault(u => u.Email == "henrique@hotmail.com");

            // ENTÃO - Então deve retornar resultado nao nulo
            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public void LerListaDeUsuariosRetornaTresUsuarios()
        {
            // Ambiente
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
            .UseInMemoryDatabase(databaseName: "IMD_blog_gen_UCT2")
            .Options;

            _contexto = new BlogPessoalContexto(opt);

            // DADO - Dado que adiciono 3 usuarios novos no sistema
            _contexto.Usuarios.Add(new Usuario
            {
                Nome = "Henrique",
                Email = "Henrique@email.com",
                Senha = "54321",
                Foto = "URLFOTOHENRIQUE",
            });
            _contexto.Usuarios.Add(new Usuario
            {
                Nome = "Marcos",
                Email = "marcos@email.com",
                Senha = "12345",
                Foto = "URLFOTOMARCOS",
            });
            _contexto.Usuarios.Add(new Usuario
            {
                Nome = "Jessica",
                Email = "Jéssica@email.com",
                Senha = "134652",
                Foto = "URLFOTOJESSICA",
            });
            _contexto.SaveChanges();

            // QUANDO - Quando eu pesquisar por todos os usuarios
            var resultado = _contexto.Usuarios.ToList();

            // ENTÃO - Então deve retornar uma lista com 3 usuarios
            Assert.AreEqual(3, resultado.Count);
        }

        [TestMethod]
        public void AtualizarUsuarioRetornaUsuarioAtualizado()
        {
            // Ambiente
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
            .UseInMemoryDatabase(databaseName: "IMD_blog_gen_UCT3")
            .Options;

            _contexto = new BlogPessoalContexto(opt);

            // DADO - Dado que adiciona um usuario no sistema
            _contexto.Usuarios.Add(new Usuario
            {
                Nome = "Marcos Name",
                Email = "Marcos@email.com",
                Senha = "12345",
                Foto = "URLFOTOMARCOSNOME",
            });
            _contexto.SaveChanges();

            // E - E altero seu nome para Marcos Nome
            var auxiliar = _contexto.Usuarios.FirstOrDefault(u => u.Email ==
            "Marcos@email.com");
            auxiliar.Nome = "Marcos Nome";
            _contexto.Usuarios.Update(auxiliar);
            _contexto.SaveChanges();

            // QUANDO - Quando pesquiso pelo nome Marcos Nome
            var resultado = _contexto.Usuarios.FirstOrDefault(u => u.Nome == "Marcos Nome");

            // ENTÃO - Então deve retornar resultado nao nulo
            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public void DeletaUsuarioRetornaUsuarioInesistente()
        {
            // Ambiente
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
            .UseInMemoryDatabase(databaseName: "IMD_blog_gen_UCT4")
            .Options;

            _contexto = new BlogPessoalContexto(opt);

            // DADO - Dado que adiciono um usuario no sistema
            _contexto.Usuarios.Add(new Usuario
            {
                Nome = "Henrique G",
                Email = "henriqueg@email.com",
                Senha = "134652",
                Foto = "URLFOTOHENRIQUE",
            });
            _contexto.SaveChanges();

            // QUANDO - Quando deleto o usuario inserido
            var auxiliar = _contexto.Usuarios.FirstOrDefault(u => u.Email ==
            "henriqueg@email.com");
            _contexto.Usuarios.Remove(auxiliar);
            _contexto.SaveChanges();

            // E - E pesquiso pelo nome Henrique G
            var resultado = _contexto.Usuarios.FirstOrDefault(u => u.Nome == "Henrique G");

            // ENTÃO - Então deve retornar resultado nulo
            Assert.IsNull(resultado);
        }

        #endregion

    }
}
