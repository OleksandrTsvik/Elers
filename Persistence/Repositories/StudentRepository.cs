using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories;

internal class StudentRepository : ApplicationDbRepository<Student>, IStudentRepository
{
    public StudentRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }
}
