﻿using Microsoft.AspNetCore.Authorization;
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

        [Authorize] 
        [HttpGet]
        public IActionResult Get() {
            var employees = _employeeRepository.Get();
            return Ok(employees);
        }
    }
}
