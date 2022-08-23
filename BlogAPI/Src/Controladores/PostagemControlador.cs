using BlogAPI.Src.Modelos;
using BlogAPI.Src.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogAPI.Src.Controladores
{

    [ApiController]
    [Route("api/Postagens")]
    [Produces("application/json")]
    public class PostagemControlador : ControllerBase
    {
        #region Atributos

        private readonly IPostagem _repositorio;

        #endregion

        #region Construtores
        public PostagemControlador(IPostagem repositorio)
        {
            _repositorio = repositorio;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Pegar Todas Postagens
        /// </summary>
        /// <param name="Todas Postagens">Pegar todas Postagens</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna todas postagens</response>
        /// <response code="404">Postagens não encontradas</response>
        [HttpGet]
        public async Task<ActionResult> PegarTodasPostagensAsync()
        {
            var lista = await _repositorio.PegarTodasPostagensAsync();

            if (lista.Count < 1) return NoContent();

            return Ok(lista);
        }

        /// <summary>
        /// Pegar Postagem Pelo Id
        /// </summary>
        /// <param name="idPostagem">Pegar Postagens pelo Id</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna postagens pelo Id</response>
        /// <response code="404">Id não existente</response>
        [HttpGet("id/{idPostagem}")]
        public async Task<ActionResult> PegarPostagemPeloIdAsync([FromRoute] int idPostagem)
        {
            try
            {
                return Ok(await _repositorio.PegarPostagemPeloIdAsync(idPostagem));
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Criar Postagem
        /// </summary>
        /// <param name="postagem">Nova Postagem</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// POST /api/Postagens
        /// {
        /// "Titulo": "Documentação",
        /// "Descrição": "Resultado de criação de novo tema",
        /// "Foto": "URL_FOTO",
        /// "Criador": {
        ///     "Id": n°
        /// },
        /// "Tema": {
        ///     "Id": n°
        /// }
        /// 
        /// </remarks>
        /// <response code="201">Retorna postagem criada</response>
        /// <response code="401">Falha ao criar postagem</response>
        [HttpPost]
        public async Task<ActionResult> NovaPostagemAsync([FromBody] Postagem postagem)
        {
            try
            {
                await _repositorio.NovaPostagemAsync(postagem);
                return Created($"api/Postagens", postagem);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Atualizar Postagem
        /// </summary>
        /// <param name="postagem">Atualizar Postagem </param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// PUT /api/Postagens
        /// {
        /// "Id": "valor do Id",
        /// "Descrição": "Resultado de criação de novo tema",
        /// }
        /// 
        /// </remarks>
        /// <response code="201">Retorna tema atualizado</response>
        /// <response code="401">Id não encontrado</response>
        [HttpPut]
        public async Task<ActionResult> AtualizarPostagemAsync([FromBody] Postagem
        postagem)
        {
            try
            {
                await _repositorio.AtualizarPostagemAsync(postagem);
                return Ok(postagem);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });

            }
        }

        /// <summary>
        /// Deletar Postagem
        /// </summary>
        /// <param name="idPostagem">Deletar postagem pelo Id</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// DELETE /api/Postagens
        /// {
        /// "Id": "valor do Id",
        /// }
        ///
        /// </remarks>
        /// <response code="201">Retorna confirmação de tema deletado</response>
        /// <response code="401">Id não encontrado</response>
        [HttpDelete("deletar/{idPostagem}")]
        public async Task<ActionResult> DeletarPostagem([FromRoute] int idPostagem)
        {
            try
            {
                await _repositorio.DeletarPostagemAsync(idPostagem);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }

            #endregion

        }
    }
}


