using AULA14_Async_Model.Model;
using AULA14_Async_Model.Model.Dto;
using AULA14_Async_Model.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AULA14_Async_Model.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase

    {
        private readonly IUsuarioRepository _repository;
        public UsuarioController(IUsuarioRepository repository)
        {
            _repository = repository;
        }
       
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usuarios = await _repository.BuscaUsuarios();

            List<UsuarioDto> usuariosRetorno = new List<UsuarioDto>();

            foreach (var usuario in usuarios) 
            {
                usuariosRetorno.Add(new UsuarioDto {Id = usuario.Id, Nome = usuario.Nome});
            }

            return usuariosRetorno.Any()
                ? Ok(usuariosRetorno)
                : BadRequest("Usuário não encontrado.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _repository.BuscaUsuario(id);
            return usuario != null
                ? Ok(usuario)
                : NotFound("Usuário não encontrado.");
        }

        [HttpPost]
        public async Task<IActionResult> Post(Usuario usuario)
        {
           _repository.AdicionaUsuario(usuario);
            return await _repository.SaveChangesAsync()
                    ? Ok("Usuário adicionado com sucesso.")
                    : BadRequest("Erro ao salvar usuário");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Usuario usuario)
        {
            var usuarioBanco = await _repository.BuscaUsuario(id);
            if(usuarioBanco == null) return NotFound("Usuário não encontrado.");

            usuarioBanco.Nome = usuario.Nome ?? usuarioBanco.Nome;
            usuarioBanco.DataNascimento = usuario.DataNascimento != new DateTime()
                ? usuario.DataNascimento 
                : usuarioBanco.DataNascimento;
            
            _repository.AtualizaUsuario(usuarioBanco);

            return await _repository.SaveChangesAsync()
                ? Ok("Usuário atualizado com sucesso.")
                : BadRequest("Erro ao atualizar usuário");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var usuarioBanco = await _repository.BuscaUsuario(id);
            if(usuarioBanco == null) return NotFound("Usuário não encontrado.");

            _repository.DeletaUsuario(usuarioBanco);

            return await _repository.SaveChangesAsync()
                ? Ok("Usuário deletado com sucesso.")
                : BadRequest("Erro ao deletar usuário");
        }

    }

}