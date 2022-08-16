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
    /// <para>Resumo: Classe para texte unitario de contexto de tema</para>
    /// <para>Criado por: Henrique</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 08/08/2022</para>
    /// </summary>
    
    [TestClass]
    public class TemaContextoTeste
    {
        #region Atributos

        private BlogPessoalContexto _contexto;

        #endregion

        #region Métodos

        [TestMethod]
        public void InserirNovoTemaRetornaTemaInserido()
        {
            // Ambiente
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
            .UseInMemoryDatabase(databaseName: "IMD_blog_gen_TCT1")
            .Options;

            _contexto = new BlogPessoalContexto(opt);

            // DADO - Dado que adiciono um tema no sistema
            _contexto.Temas.Add(new Tema
            {
                Descricao = "CSHARP"
            });
            _contexto.SaveChanges();

            // QUANDO - Quando eu pesquiso pela descrição do tema adicionado
            var resultado = _contexto.Temas.FirstOrDefault(t => t.Descricao ==
            "CSHARP");

            // ENTÃO - Então deve retornar resultado nao nulo
            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public void LerListaDeTemasRetornaTresTemas()
        {
            // Ambiente
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
            .UseInMemoryDatabase(databaseName: "IMD_blog_gen_TCT2")
            .Options;

            _contexto = new BlogPessoalContexto(opt);

            // DADO - Dado que adiciono 3 temas novos no sistema
            _contexto.Temas.Add(new Tema
            {
                Descricao = "CSHARP"
            });
            _contexto.Temas.Add(new Tema
            {
                Descricao = "Java"
            });
            _contexto.Temas.Add(new Tema
            {
                Descricao = "Go"
            });
            _contexto.SaveChanges();

            // QUANDO - Quando eu pesquisar por todos os temas
            var resultado = _contexto.Temas.ToList();

            // ENTÃO - Então deve retornar uma lista com 3 temas
            Assert.AreEqual(3, resultado.Count);
        }

        [TestMethod]
        public void AtualizarTemaRetornaTemaAtualizado()
        {
            // Ambiente
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
            .UseInMemoryDatabase(databaseName: "IMD_blog_gen_TCT3")
            .Options;

            _contexto = new BlogPessoalContexto(opt);

            // DADO - Dado que adiciono um tema no sistema
            _contexto.Temas.Add(new Tema
            {
                Descricao = "COBOL"
            });
            _contexto.SaveChanges();

            // E - E altero seu nome para Python
            var auxiliar = _contexto.Temas.FirstOrDefault(t => t.Descricao == "COBOL");
            auxiliar.Descricao = "Python";
            _contexto.Temas.Update(auxiliar);
            _contexto.SaveChanges();

            // QUANDO - Quando pesquiso pelo tema Python
            var resultado = _contexto.Temas.FirstOrDefault(t => t.Descricao ==
            "Python");

            // ENTÃO - Então deve retornar resultado nao nulo
            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public void DeletaTemaRetornaTemaInesistente()
        {
            // Ambiente
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
            .UseInMemoryDatabase(databaseName: "IMD_blog_gen_TCT4")
            .Options;

            _contexto = new BlogPessoalContexto(opt);

            // DADO - Dado que adiciono um tema no sistema
            _contexto.Temas.Add(new Tema
            {
                Descricao = "Typescript"
            });
            _contexto.SaveChanges();

            // QUANDO - Quando deleto o tema inserido
            var auxiliar = _contexto.Temas.FirstOrDefault(t => t.Descricao ==
            "Typescript");
            _contexto.Temas.Remove(auxiliar);
            _contexto.SaveChanges();

            // E - E pesquiso pelo tema Typescript
            var resultado = _contexto.Temas.FirstOrDefault(t => t.Descricao ==
            "Typescript");

            // ENTÃO - Então deve retornar resultado nulo
            Assert.IsNull(resultado);
        }

        #endregion
    }
}
