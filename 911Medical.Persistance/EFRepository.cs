using _911Medical.Domain.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _911Medical.Persistance
{
    public class EFRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public EFRepository(MedicalDbContext dbContext) : base(dbContext)
        {
        }
    }
}
