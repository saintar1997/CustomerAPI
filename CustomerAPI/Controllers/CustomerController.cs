using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DataContext _context;
        public CustomerController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Customer>> Get()
        {
            return Ok(await _context.Customers.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Customer>>> Get(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return BadRequest("Customer not found.");
            return Ok(customer);
        }


        [HttpPost]
        public async Task<ActionResult<List<Customer>>> AddHero(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok(await _context.Customers.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Customer>>> Update(Customer request)
        {
            var dbCustomer = await _context.Customers.FindAsync(request.Id);
            if (dbCustomer == null)
                return BadRequest("Customer not found.");

            dbCustomer.FirstName = request.FirstName;
            dbCustomer.LastName = request.LastName;
            dbCustomer.NickName = request.NickName;
            dbCustomer.Age = request.Age;
            dbCustomer.Sex = request.Sex;
            dbCustomer.Position = request.Position;
            dbCustomer.Salary = request.Salary;

            await _context.SaveChangesAsync();

            return Ok(await _context.Customers.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Customer>>> Delete(int id)
        {
            var dbCustomer = await _context.Customers.FindAsync(id);
            if (dbCustomer == null)
                return BadRequest("Customer not found.");

            _context.Customers.Remove(dbCustomer);
            await _context.SaveChangesAsync();

            return Ok(await _context.Customers.ToListAsync());
        }

    }
}
