using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Infraestrutura;
using PrimeiraAPI.Model;
using PrimeiraAPI.ViewModel;

namespace PrimeiraAPI.Controllers {
    [ApiController]
    [Route("api/v1/employee")]
    public class EmployeeController : ControllerBase {

        private readonly IEmplyeeRepository _employeeRepository;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmplyeeRepository employeeRepository, ILogger<EmployeeController> logger) {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add([FromForm]EmployeeViewModel employeeView) {

            var filePath = Path.Combine("Storage", employeeView.Photo.FileName);
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            employeeView.Photo.CopyTo(fileStream);
            var employee = new Employee(employeeView.Name, employeeView.Age, filePath);
            _employeeRepository.Add(employee);

            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("{id}/download")]
        public IActionResult DownloadPhoto(int id) {
            var employee = _employeeRepository.Get(id);
            var dataBytes = System.IO.File.ReadAllBytes(employee.photo);
            return File(dataBytes, "image/png");
        }

        [HttpGet]

        //Os log's nesse caso são somente para fins de aprendizado
        public IActionResult Get(int pageNumber, int pageQuantity) {
            _logger.Log(LogLevel.Error, "Erro");

            throw new Exception("Erro de teste");

            var employees = _employeeRepository.Get(pageNumber, pageQuantity);
            _logger.LogInformation("Teste");
            return Ok(employees);
        }
    }
}
