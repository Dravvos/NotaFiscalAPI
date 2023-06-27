using AutoMapper;
using CRUD_nota_fiscal.Data;
using CRUD_nota_fiscal.DTO;
using CRUD_nota_fiscal.Interfaces;
using CRUD_nota_fiscal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CRUD_nota_fiscal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotaFiscalController : Controller
    {
        private readonly INotaFiscalRepositoy _repository;
        private readonly IMapper _map;

        public NotaFiscalController(INotaFiscalRepositoy repositoy, IMapper map)
        {
            _repository = repositoy;
            _map = map;
        }

        [HttpGet]
        [ProducesResponseType(200, Type=typeof(IEnumerable<NotaFiscal>))]
        public IActionResult GetNotasFiscais() 
        {
            var notasFiscais =_map.Map<List<NotaFiscalDTO>>(_repository.GetNotasFiscais());
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            return Ok(notasFiscais);
        }

        [HttpGet("numero")]
        [ProducesResponseType(200, Type = typeof(NotaFiscal))]
        [ProducesResponseType(400)]
        public IActionResult GetNotaFiscalPorNumero(int numero)
        {
            if (_repository.NotaFiscalExiste(numero) == false)
                return NotFound();

            var notaFiscal =_map.Map<NotaFiscalDTO>(_repository.GetNotaFiscalPorNumero(numero));

            if(ModelState.IsValid==false)
                return BadRequest(ModelState);

            return Ok(notaFiscal);
        }

        [HttpGet("cnpjEmissor")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<NotaFiscal>))]
        [ProducesResponseType(400)]
        public IActionResult GetNotasFiscaisPorEmissor(string cnpjEmissor)
        {
            if (_repository.CnpjEmissorExiste(cnpjEmissor) == false)
                return NotFound();

            var notasFiscais =_map.Map<List<NotaFiscalDTO>>(_repository.GetNotasFiscaisPorEmissor(cnpjEmissor));
            
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            return Ok(notasFiscais);
        }

        [HttpGet("cnpjDestinatario")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<NotaFiscal>))]
        [ProducesResponseType(400)]
        public IActionResult GetNotasFiscaisPorDestinatario(string cnpjDestinatario)
        {
            if (_repository.CnpjDestinatarioExiste(cnpjDestinatario) == false)
                return NotFound();

            var notasFiscais =_map.Map<List<NotaFiscalDTO>>(_repository.GetNotasFiscaisPorDestinatario(cnpjDestinatario));
            
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            return Ok(notasFiscais);
        }

        [HttpGet("data")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<NotaFiscal>))]
        [ProducesResponseType(400)]
        public IActionResult GetNotasFiscaisPorData(DateTime data)
        {
            if (_repository.NotasFiscaisExistem(data) == false)
                return NotFound();

            var notasFiscais = _map.Map<List<NotaFiscalDTO>>(_repository.GetNotasFiscaisPorData(data));
            
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            return Ok(notasFiscais);
        }

        [HttpPost("createNotaFiscal")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateNotaFiscal([FromBody]NotaFiscalDTO notaFiscalCreate)
        {
            if(notaFiscalCreate.CnpjDestinatarioNf.Length!=14)
            {
                ModelState.AddModelError("", "O CNPJ do destinatário deve ter 14 dígitos");
                return BadRequest(ModelState);
            }

            if (notaFiscalCreate.CnpjEmissorNf.Length != 14)
            {
                ModelState.AddModelError("", "O CNPJ do emissor deve ter 14 dígitos");
                return BadRequest(ModelState);
            }

            if (notaFiscalCreate == null)
                return BadRequest(ModelState);

            var notaFiscal = _repository.GetNotaFiscalPorNumero(notaFiscalCreate.NumeroNf);

            if(notaFiscal!=null)
            {
                ModelState.AddModelError("", "Nota Fiscal já existe");
                return StatusCode(422, ModelState);
            }
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var notaFiscalMap = _map.Map<NotaFiscal>(notaFiscalCreate);
            notaFiscalMap.NotaFiscalId = Guid.NewGuid();
            if(_repository.CreateNotaFiscal(notaFiscalMap)==false)
            {
                ModelState.AddModelError("", "Não foi possível criar nota fiscal");
                return StatusCode(500, ModelState);
            }

            return Ok("Nota fiscal criada com sucesso");
        }


        [HttpPut("updateNotaFiscal")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateNotaFiscal(int numero, [FromBody] NotaFiscalDTO notaFiscalAlterada)
        {
            if (notaFiscalAlterada.CnpjDestinatarioNf.Length != 14)
            {
                ModelState.AddModelError("", "O CNPJ do destinatário deve ter 14 dígitos");
                return BadRequest(ModelState);
            }

            if (notaFiscalAlterada.CnpjEmissorNf.Length != 14)
            {
                ModelState.AddModelError("", "O CNPJ do emissor deve ter 14 dígitos");
                return BadRequest(ModelState);
            }

            if (notaFiscalAlterada == null)
                return BadRequest(ModelState);

            if (_repository.NotaFiscalExiste(numero) == false)
                return NotFound();

            if (numero <= 0)
            {
                ModelState.AddModelError("", "Número de NF não pode ser menor ou igual 0");
                return BadRequest(ModelState);
            }

            if(ModelState.IsValid==false)
                return BadRequest(ModelState);

            var nota = _repository.GetNotaFiscalPorNumero(numero);
            
            var notaFiscal = _map.Map<NotaFiscal>(notaFiscalAlterada);
            notaFiscal.NotaFiscalId = nota.NotaFiscalId;
            if(_repository.UpdateNotaFiscal(notaFiscal)==false)
            {
                ModelState.AddModelError("", "Não foi possível alterar a nota fiscal");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("deleteNotaFiscal")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteNotaFiscal(int numero)
        {
            if (numero <= 0)
            {
                ModelState.AddModelError("", "Número de NF não pode ser menor ou igual a 0");
                return BadRequest(ModelState); 
            }

            var notaFiscal = _repository.GetNotaFiscalPorNumero(numero);

            if (notaFiscal == null)
                return NotFound();

            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            if (_repository.DeleteNotaFiscal(numero)==false)
            {
                ModelState.AddModelError("", "Não foi possível deletar nota fiscal");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
