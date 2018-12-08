using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrabalhandoComAppMetrics.Api.Models;

namespace TrabalhandoComAppMetrics.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        public ProdutoController() { }

        // GET api/produtos
        [HttpGet("")]
        public ActionResult<IEnumerable<Produto>> Gets()
        {
            return new List<Produto>() { new Produto() { Nome = "Fabricio", SobreNome = "Veronez", Idade = 34 }, new Produto() { Nome = "Fulano", SobreNome = "da Silva", Idade = 29 } };
        }

        // GET api/produtos/5
        [HttpGet("{id}")]
        public ActionResult<Produto> GetById(int id)
        {
            return new Produto() { Nome = "Fabricio", SobreNome = "Veronez", Idade = 34 };
        }

        // POST api/produtos
        [HttpPost("")]
        public void Post([FromBody] Produto produto) { }

        // PUT api/produtos/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Produto produto) { }

        // DELETE api/produtos/5
        [HttpDelete("{id}")]
        public void DeleteById(int id) { }
    }
}