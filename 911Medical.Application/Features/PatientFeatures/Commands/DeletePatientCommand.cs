using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _911Medical.Application.Features.PatientFeatures.Commands
{
    public class DeletePatientCommand : IRequest
    {
        public int Id { get; set; }
    }
}
