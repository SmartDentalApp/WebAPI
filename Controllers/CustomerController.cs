using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using smart_dental_webapi.Repositories.Customer;
using smart_dental_webapi.Entity;
using smart_dental_webapi.Helper;
using smart_dental_webapi.Models.Customer;
using smart_dental_webapi.Models.Pagination;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using smart_dental_webapi.Services.Customer;

namespace smart_dental_webapi.Controllers
{
    [Route("v1/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper, ICustomerService customerService)
        {
            _customerRepository = customerRepository;
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult CreateCustomer([FromBody] CustomerPostRequestModel customerCreate)
        {
            if (!_customerService.IsCustomerCreateValid(customerCreate, ModelState))
            {
                return StatusCode(400, ModelState);
            }

            var savedCustomer = _customerService.CreateCustomer(customerCreate);

            if (!savedCustomer)
            {
                ModelState.AddModelError("", "Ocorreu um erro ao salvar o cliente");
                return StatusCode(500, ModelState);
            }

            return Ok("Cliente criado com sucesso");
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
