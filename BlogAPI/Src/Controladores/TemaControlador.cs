using BlogAPI.Src.Modelos;
using BlogAPI.Src.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogAPI.Src.Controladores
{
    [ApiController]
    [Route("api/Temas")]
    [Produces("application/json")]
    public class TemaControlador : ControllerBase
    {
        #region Atributos

        private readonly ITema _repositorio;

        #endregion

        #region Construtores
        public TemaControlador(ITema repositorio)
        {
            _repositorio = repositorio;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Pegar Todos os Temas
        /// </summary>
        /// <param name="Pegar Todos Temas">Todos os Temas</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna todos os temas</response>
        /// <response code="404">não existente</response>
        [HttpGet]
        public async Task<ActionResult> PegarTodosTemasAsync()
        {
            var lista = await _repositorio.PegarTodosTemasAsync();

            if (lista.Count < 1) return NoContent();

            return Ok(lista);
        }

        /// <summary>
        /// Pegar Tema Pelo Id
        /// </summary>
        /// <param name="idTema">Tema pelo Id</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna Tema pelo Id</response>
        /// <response code="404">Id não existente</response>
        [HttpGet("id/{idTema}")]
        public async Task<ActionResult> PegarTemaPeloIdAsync([FromRoute] int idTema)
        {
            try
            {
                return Ok(await _repositorio.PegarTemaPeloIdAsync(idTema));
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Criar Novo Tema
        /// </summary>
        /// <param name="tema">Criar Tema </param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// POST /api/Temas
        /// {
        /// "Descrição": "Resultado de criação de novo tema",
        /// }
        /// 
        /// </remarks>
        /// <response code="201">Retorna tema criado</response>
        /// <response code="401">Descrição ja cadastrada</response>
        [HttpPost]
        public async Task<ActionResult> NovoTemaAsync([FromBody] Tema tema)
        {
            await _repositorio.NovoTemaAsync(tema);

            return Created($"api/Temas", tema);
        }

        /// <summary>
        /// Atualizar Tema
        /// </summary>
        /// <param name="tema">Atuaçizar Tema </param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// PUT /api/Temas
        /// {
        /// "Id": "valor do Id",
        /// "Descrição": "Resultado de criação de novo tema",
        /// }
        /// 
        /// </remarks>
        /// <response code="201">Retorna tema atualizado</response>
        /// <response code="401">Id não encontrado</response>
        [HttpPut]
        public async Task<ActionResult> AtualizarTema([FromBody] Tema tema)
        {
            try
            {
                await _repositorio.AtualizarTemaAsync(tema);
                return Ok(tema);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Deletar Tema
        /// </summary>
        /// <param name="idTema">Deletar tema pelo Id</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// DELETE /api/Usuarios
        /// {
        /// "Id": "valor do Id",
        /// }
        ///
        /// </remarks>
        /// <response code="201">Retorna confirmação de tema deletado</response>
        /// <response code="401">Id não encontrado</response>
        [HttpDelete("deletar/{idTema}")]
        public async Task<ActionResult> DeletarTema([FromRoute] int idTema)
        {
            try
            {
                await _repositorio.DeletarTemaAsync(idTema);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        #endregion

    }
}
