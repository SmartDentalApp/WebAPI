using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Interfaces;
using smart_dental_webapi.Entity;
using smart_dental_webapi.Helper;
using smart_dental_webapi.Models.Customer;
using smart_dental_webapi.Models.Pagination;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace smart_dental_webapi.Controllers
{
    [Route("v1/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult CreateCustomer([FromBody] CustomerPostRequestModel customerCreate)
        {
            if (customerCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = _customerRepository.GetCustomers()
                .Where(c => c.Email.Trim().ToUpper() == customerCreate.Email.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (customer != null)
            {
                ModelState.AddModelError("", "Cliente já cadastrado");
                return StatusCode(422, ModelState);
            }

            var customerMap = _mapper.Map<CustomerEntity>(customerCreate);

            if (!_customerRepository.CreateCustomer(customerMap))
            {
                ModelState.AddModelError("", "Algo ocorreu errado ao salvar");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<CustomerEntity>> GetCategories([FromQuery] Pagination paginationParameters)
        {
            List<CustomerEntity> customers = _mapper.Map<List<CustomerEntity>>(_customerRepository.GetCustomers());

            var paginatedData = PaginationHelper.Paginate(customers.AsQueryable(), paginationParameters.Page, paginationParameters.PageSize);

            PaginationHelper.SetPaginationHeaders(Response.Headers, paginationParameters, customers);

            return Ok(paginatedData);
        }

        [HttpGet("{customerId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<CustomerEntity> GetCategory(int customerId)
        {
            CustomerEntity customer = _mapper.Map<CustomerEntity>(_customerRepository.GetCustomer(customerId));

            return Ok(customer);
        }
    }
}
