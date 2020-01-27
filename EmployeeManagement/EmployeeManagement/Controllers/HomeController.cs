using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace EmployeeManagement.Controllers
{
    public class HomeController:Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public HomeController(IEmployeeRepository employeeRepository, IHostingEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        public ViewResult Index()
        {
           var model= _employeeRepository.GetAllEmployee();

            return View(model);
        }

        public ViewResult details(int id)
        {
            //viewdata uses string keys. errors occur at run time only., no intellisense.
            //Viewbag uses dynamic properties. errors occur at run time only., no intellisense. Its just a wrapper around viewdata

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmployee(id),
                PageTitle = "employee details"
            };    
            
            return View(homeDetailsViewModel);
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var newEmployee = _employeeRepository.Add(employee);
               // return RedirectToAction("details", new { id = newEmployee.Id });
            }

            return View();
           
        }
    }
}
