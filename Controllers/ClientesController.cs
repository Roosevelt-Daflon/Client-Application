using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Client_Application.Data;
using Client_Application.Model;
using Client_Application.DTO;
using Client_Application.Repository;
using Client_Application.Service;

namespace Client_Application.Controllers
{
    public class ClientesController : Controller
    {
		private readonly IRepository _repository;
		private readonly ICepService _cepService;

		public ClientesController(IRepository repository, ICepService cepService)
        {
			_repository = repository;
			_cepService = cepService;
		}

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
			return View(await _repository.GetAll<Cliente>(e => e.Endereco));
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int id)
        {
			var cliente = await _repository.GetBy<Cliente>(c => c.Id == id, e => e.Endereco);
			
			if (cliente == null)
                return NotFound();

            return View(cliente);
        }

		// GET: Clientes/Create
		[Route("/Create")]
        public IActionResult Create()
        {
			return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Route("Create")]
        public async Task<IActionResult> Create([Bind("Nome, Cep")] ClienteDTO clienteDTO)
        {
			//Verifica se o Cliente DTO é valido
			if(clienteDTO.Nome == null || clienteDTO.Cep == null)
				return RedirectToAction(nameof(Index));

			//padronização de formatação do cep
			if (clienteDTO.Cep[5] == '-')
				clienteDTO.Cep.Insert(5, "-");

			var cliente = new Cliente();
			var endereco = await _cepService.SearchCep(clienteDTO.Cep);

			//Verifica se o endereco existe
			if (endereco == null) 
				return RedirectToAction(nameof(Index));

			cliente.Nome = clienteDTO.Nome;
			cliente.Endereco = endereco;

			await _repository.Add(cliente);
            return RedirectToAction(nameof(Index));
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
			var cliente = await _repository.GetBy<Cliente>(c => c.Id == id, e => e.Endereco);
            
			if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind(include:"Nome, Endereco")] Cliente cliente)
        {
			cliente.Id = id;

			await _repository.Update(cliente);

			return RedirectToAction(nameof(Index));
		}

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
			var cliente = await _repository.GetBy<Cliente>(c => c.Id == id, e => e.Endereco);
			
			if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
			await _repository.DeleteById<Cliente>(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
