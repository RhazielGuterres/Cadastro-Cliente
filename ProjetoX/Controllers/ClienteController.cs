using CadastroDeCliente.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoX.Models;

namespace ProjetoX.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteDAO _clienteDAO;

        // Construtor com injeção de dependência
        public ClientesController(ClienteDAO clienteDAO)
        {
            _clienteDAO = clienteDAO;
        }

        // POST: api/clientes (ADICIONA O CLIENTE AO BANCO DE DADOS)
        [HttpPost]
        public ActionResult<Cliente> PostCliente([FromBody] Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest("Dados inválidos");
            }

            _clienteDAO.inserir(cliente);
            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, cliente);
        }

        // GET: api/clientes/{id}
        [HttpGet("{id}")]
        public ActionResult<Cliente> GetCliente(int id)
        {
            var cliente = _clienteDAO.buscarPorId(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        // PUT: api/clientes/{id}
        [HttpPut("{id}")]
        public IActionResult PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            _clienteDAO.atualizar(cliente);
            return NoContent();
        }

        // DELETE: api/clientes/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCliente(int id)
        {
            var cliente = _clienteDAO.buscarPorId(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _clienteDAO.DeletarPorId(id);
            return NoContent();
        }

        // GET: api/clientes (LISTA TODOS OS CLIENTES)
        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> ListarClientes()
        {
            var clientes = _clienteDAO.ListarTodos();
            if (clientes == null || clientes.Count == 0)
            {
                return NotFound("Nenhum cliente encontrado.");
            }

            return Ok(clientes);
        }
    }

}
