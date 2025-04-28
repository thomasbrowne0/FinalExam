using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinalExam.Backend.Application.Entities;
using FinalExam.Backend.Application.Interfaces;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Company>> GetAll()
        {
            return await _companyRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> Get(int id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            if (company == null)
                return NotFound();

            return company;
        }

        [HttpPost]
        public async Task<ActionResult> Create(Company company)
        {
            await _companyRepository.CreateAsync(company);
            return CreatedAtAction(nameof(Get), new { id = company.CompanyID }, company);
        }
    }
}
