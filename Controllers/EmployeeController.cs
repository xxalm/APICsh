using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Infraestrutura;
using PrimeiraAPI.Model;
using PrimeiraAPI.ViewModel;

namespace PrimeiraAPI.Controllers {
    [ApiController]
    [Route("api/v1/employee")]
    public class EmployeeController : ControllerBase {

        private readonly IEmplyeeRepository _employeeRepository;

        public EmployeeController(IEmplyeeRepository employeeRepository) {

           _employeeRepository = employeeRepository ?? throw new ArgumentException(nameof(employeeRepository));
        }

        [HttpPost]
        public IActionResult Add(EmployeeViewModel employeeView) {
            var employee = new Employee(employeeView.Name, employeeView.Age, null);
            _employeeRepository.Add(employee);

            return Ok();
        }

        [HttpGet]
        public IActionResult Get() {
            var employees = _employeeRepository.Get();
            return Ok(employees);
        }
    }
}
