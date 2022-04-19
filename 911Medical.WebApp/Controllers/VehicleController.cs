using _911Medical.Application.Features.VehicleFeatures.Commands;
using _911Medical.Application.Features.VehicleFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace _911Medical.WebApp.Controllers
{
    [Authorize]
    public class VehicleController : Controller
    {
        private readonly IMediator _mediator;

        public VehicleController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        // GET: VehicleController
        public async Task<ActionResult> Index()
        {
            if (Request.Query.ContainsKey("test"))
            {
                await CreateTestVehicles();
            }

            var vehicles = await _mediator.Send(new GetAllVehiclesQuery());

            return View(vehicles);
        }

        // GET: VehicleController/Details/5
        public async Task<ActionResult> Details(GetVehicleByIdQuery query)
        {
            var vehicle = await _mediator.Send(query);

            return View(vehicle);
        }

        // GET: VehicleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateVehicleCommand command)
        {
            try
            {
                var vehicleId = await _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                
                return View();
            }
        }

        // GET: VehicleController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var vehicle = await _mediator.Send(new GetVehicleByIdQuery() { Id = id });

            var command = new UpdateVehicleCommand()
            {
                Id = vehicle.Id,
                RegNumber = vehicle.RegNumber,
                VehicleType = vehicle.VehicleType,
                Description = vehicle.Description
            };

            return View(command);
        }

        // POST: VehicleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateVehicleCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VehicleController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var vehicle = await _mediator.Send(new GetVehicleByIdQuery() { Id = id });

            return View(vehicle);
        }

        // POST: VehicleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteVehicleCommand command)
        {
            try
            {
                _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private async Task CreateTestVehicles()
        {
            for (int i = 1; i < 10; i++)
            {
                var cmd = new CreateVehicleCommand()
                {
                    RegNumber = $"MBZU{589 + 10 * 1}",
                    VehicleType = Domain.Entities.VehicleType.Van,
                    Description = $"Avto_{i}"
                };

                await _mediator.Send(cmd);
            }
        }
    }
}
