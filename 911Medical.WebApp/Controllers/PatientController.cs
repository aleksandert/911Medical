using _911Medical.Application.Features.PatientFeatures.Commands;
using _911Medical.Application.Features.PatientFeatures.Queries;
using _911Medical.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using _911Medical.WebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace _911Medical.WebApp.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        private readonly IMediator _mediator;

        public PatientController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        // GET: PatientController
        public async Task<ActionResult> Index()
        {
            if (Request.Query.ContainsKey("test"))
            {
                await CreateTestPatients();
            }

            var patients = await this._mediator.Send(new GetAllPatientsQuery());

            var results = patients.Select(x => new PatientViewModel()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                HomeAddress = $"{x.HomeAddress.AddressLine1}, {x.HomeAddress.ZipPostalCode} {x.HomeAddress.City}"
            });

            return View(results);
        }

        // GET: PatientController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PatientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PatientController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PatientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PatientController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PatientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private async Task CreateTestPatients()
        {
            var firstNames = new[] { "Bojan", "Aleš", "Eva", "Ana", "Ivana" };
            var lastNames = new[] { "Novak", "Bedrač", "Potočnik", "Kac", "Pernat" };
            var addresses = new[] {
                 new Address("Mariborska cesta 33", null, "2250", "Ptuj", null, "SI"),
                 new Address("Ptujska cesta 112", null, "2000", "Maribor", null, "SI"),
                 new Address("Ljubljanska cesta 45", null, "4000", "Kranj", null, "SI"),
                 new Address("Ljubljanska cesta 2", null, "6000", "Koper", null, "SI"),
                 new Address("Zg. Hajdina 44a", null, "2288", "Hajdina", null, "SI"),
            };


            for (int i = 0; i < 5; i++)
            {
                var cmd = new CreatePatientCommand()
                {
                    FirstName = firstNames[i],
                    LastName = lastNames[i],
                    HomeAddress = addresses[i],
                };

                await _mediator.Send(cmd);
            }
        }
    }
}
