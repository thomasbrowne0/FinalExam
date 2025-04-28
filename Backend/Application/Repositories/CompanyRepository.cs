using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalExam.Backend.Application.Entities;
using FinalExam.Backend.Application.Interfaces;

namespace FinalExam.Backend.Application.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly List<Company> _companies = new List<Company>();

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await Task.FromResult(_companies);
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            var company = _companies.FirstOrDefault(c => c.CompanyID == id);
            return await Task.FromResult(company);
        }

        public async Task CreateAsync(Company company)
        {
            company.CompanyID = _companies.Count + 1;
            _companies.Add(company);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Company company)
        {
            var existing = _companies.FirstOrDefault(c => c.CompanyID == company.CompanyID);
            if (existing != null)
            {
                existing.CompanyName = company.CompanyName;
                existing.Address = company.Address;
                existing.CompanyAdmin = company.CompanyAdmin;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var company = _companies.FirstOrDefault(c => c.CompanyID == id);
            if (company != null)
            {
                _companies.Remove(company);
            }
            await Task.CompletedTask;
        }
    }
}
