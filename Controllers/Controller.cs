using EasyPay.Models;
using EasyPay.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EasyPay.Controllers
{
    public abstract class Controller<T, R> : ControllerBase where T : class, IEntity where R : class, IEntity
    {
        private readonly Repository<T> _repository;

        public Controller(Repository<T> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _repository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var entity = await _repository.GetById(id);

            if (entity == null) return NotFound();

            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] R dto)
        {
            try
            {
                if (await _repository.Add(dto))
                {
                    return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
                }

                return BadRequest($"Não foi possível criar essa entidade ({typeof(T).Name}).");
            }
            catch (Exception)
            {
                return BadRequest($"Erro ao criar a entidade ({typeof(T).Name}).");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] T request)
        {
            try
            {
                if (await _repository.Update(request))
                {
                    return Ok($"Entidade ({typeof(T).Name}) atualizada com sucesso.");
                }
                return BadRequest($"Não foi possível atualizar essa entidade ({typeof(T).Name}).");
            }
            catch (Exception)
            {
                return BadRequest("Erro ao excluir a entidade.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                if (await _repository.Remove(id))
                {
                    return Ok($"Entidade ({typeof(T).Name}) excluída com sucesso.");
                }
                return NotFound($"Essa entidade ({typeof(T).Name}) não foi encontrada.");
            }
            catch (Exception)
            {
                return BadRequest($"Erro ao excluir a entidade ({typeof(T).Name}).");
            }
        }
    }
}
