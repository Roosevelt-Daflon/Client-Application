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
            {
                return NotFound();
            }

            return View(cliente);
        }

		// GET: Clientes/Create
		[Route("/Create")]
        public IActionResult Create()
        {
			ViewData["Title"] = "Create";

			return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Route("Create")]
        public async Task<IActionResult> Create([Bind("Nome, Cep")] ClienteDTO clienteDTO)
        {
			if(clienteDTO.Nome == null|| clienteDTO.Cep == null)
				return RedirectToAction(nameof(Index));

			var cliente = new Cliente();
			var endereco = await _cepService.SearchCep(clienteDTO.Cep);

			if(endereco == null)
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
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind(include:"Nome, Endereco")] Cliente cliente)
        {
			await _repository.Update(id, cliente);

			return RedirectToAction(nameof(Index));
		}

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

			var cliente = await _repository.GetBy<Cliente>(c => c.Id == id, e => e.Endereco);
			if (cliente == null)
            {
                return NotFound();
            }

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
