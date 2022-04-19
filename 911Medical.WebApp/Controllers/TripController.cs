using _911Medical.Application.Features.TripFeatures.Queries;
using _911Medical.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using _911Medical.WebApp.Models;
using _911Medical.Application.Features.TripFeatures.Commands;
using _911Medical.Application.Features.VehicleFeatures.Queries;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace _911Medical.WebApp.Controllers
{
    [Authorize]
    public class TripController : Controller
    {
        private readonly IMediator _mediator;

        public TripController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        // GET: TripController
        public async Task<ActionResult> Index(TripStatus? status)
        {
            if (Request.Query.ContainsKey("test"))
            {
                await CreateTestTrips();
            }

            // Build query and dispatch it
            var trips = await _mediator.Send(new GetTripsByStatusQuery() { TripStatus = status });

            // Map results as view models
            var results = trips.Select(x => new TripViewModel()
            {
                Id = x.Id,
                DateCreated = x.DateCreated,
                Status = x.Status,
                TripStatusName = x.Status.ToString(),
                PatientId = x.Patient.Id,
                PatientFullName = $"{x.Patient.FirstName} {x.Patient.LastName}",
                VehicleId = x.Vehicle?.Id,
                VehicleRegNumber = x.Vehicle?.RegNumber,
                StartAddress = $"{x.StartAddress.AddressLine1}, {x.StartAddress.ZipPostalCode} {x.StartAddress.City}"
            });

            return View(results);
        }

        // GET: TripController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TripController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TripController/Create
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

        // GET: TripController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TripController/Edit/5
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

        // GET: TripController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TripController/Delete/5
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

        // GET: TripController/Assign/5
        public async Task<ActionResult> Assign(int id)
        {
            // load the trip instance
            var trip = await _mediator.Send(new GetTripByIdQuery() { Id = id });

            if (trip.Status != TripStatus.Queued)
            {
                ModelState.AddModelError("Trip", "TripStatus should be Queued.");
                return BadRequest(ModelState);
            }

            //load available vehicles
            var vehicles = await _mediator.Send(new GetAllVehiclesQuery());

            var selectListVehicles = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(vehicles, nameof(Vehicle.Id), nameof(Vehicle.RegNumber), trip.Vehicle?.Id);

            var model = new TripAssignViewModel()
            {
                Id = trip.Id,
                DateCreated = trip.DateCreated,
                Status = trip.Status,
                TripStatusName = trip.Status.ToString(),
                PatientId = trip.Patient.Id,
                PatientFullName = $"{trip.Patient.FirstName} {trip.Patient.LastName}",
                VehicleId = trip.Vehicle?.Id,
                VehicleRegNumber = trip.Vehicle?.RegNumber,
                AvailableVehicles = selectListVehicles,
                StartAddress = $"{trip.StartAddress.AddressLine1}, {trip.StartAddress.ZipPostalCode} {trip.StartAddress.City}"
            };
            
            return View(model);

        }

        public async Task<ActionResult> Dispatch(int id)
        {
            // load the trip instance
            var trip = await _mediator.Send(new GetTripByIdQuery() { Id = id });

            if (trip.Status != TripStatus.Queued)
            {
                ModelState.AddModelError("Trip", "TripStatus must be in status Queued to allow Dispatch.");
                return BadRequest(ModelState);
            }

            //load available vehicles
            var vehicles = await _mediator.Send(new GetAllVehiclesQuery());

            var selectListVehicles = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(vehicles, nameof(Vehicle.Id), nameof(Vehicle.RegNumber), trip.Vehicle?.Id);

            var model = new TripAssignViewModel()
            {
                Id = trip.Id,
                DateCreated = trip.DateCreated,
                Status = trip.Status,
                TripStatusName = trip.Status.ToString(),
                PatientId = trip.Patient.Id,
                PatientFullName = $"{trip.Patient.FirstName} {trip.Patient.LastName}",
                VehicleId = trip.Vehicle?.Id,
                VehicleRegNumber = trip.Vehicle?.RegNumber,
                AvailableVehicles = selectListVehicles,
                StartAddress = $"{trip.StartAddress.AddressLine1}, {trip.StartAddress.ZipPostalCode} {trip.StartAddress.City}"
            };

            return View(model);

        }

        // POST: TripController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Dispatch(int id, IFormCollection collection)
        {
            try
            {
                var dispatchCommand = new DispatchTripCommand() { TripId = id };

                var trip = await _mediator.Send(dispatchCommand);

                

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private async Task CreateTestTrips()
        {
            for (int i = 1; i < 5; i++)
            {
                var cmd = new EnqueueNewTripCommand()
                {
                    PatientId = i,
                };

                await _mediator.Send(cmd);
            }
        }
    }
}
