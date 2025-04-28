using System.Collections.Generic;
using System.Threading.Tasks;
using FinalExam.Backend.Application.Entities;

namespace FinalExam.Backend.Application.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company> GetByIdAsync(int id);
        Task CreateAsync(Company company);
        Task UpdateAsync(Company company);
        Task DeleteAsync(int id);
    }
}