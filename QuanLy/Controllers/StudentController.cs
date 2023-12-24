using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuanLy.Models; // Make sure to adjust the namespace as per your front-end models
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Linq;

namespace QuanLy.Controllers
{
    [Route("students")]
    public class StudentsController : Controller
    {
        
            private readonly IHttpClientFactory _clientFactory;

            public StudentsController(IHttpClientFactory clientFactory)
            {
                _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            }

            [Route("")] // or [Route("Index")]
            public async Task<IActionResult> Index()
            {
                
                
                    var client = _clientFactory.CreateClient("ApiClient");

                    var response = await client.GetAsync("api/students");

                    if (response.IsSuccessStatusCode)
                    {
                        var students = await response.Content.ReadAsStringAsync();
                        var studentViewModels = JsonConvert.DeserializeObject<List<Student>>(students);

                        return View(studentViewModels);
                    }

                    // Handle error here if needed
                
                return View(new List<Student>());
            }

        [Route("details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var client = _clientFactory.CreateClient("ApiClient");

                var response = await client.GetAsync($"api/students/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var student = await response.Content.ReadAsStringAsync();
                    var studentViewModel = JsonConvert.DeserializeObject<Student>(student);

                    return View(studentViewModel);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return NotFound();
                }
                else
                {
                    // Log or handle the error if needed
                    ModelState.AddModelError(string.Empty, "Error retrieving data from the API.");
                    return View("Error");
                }
            }
            catch (Exception )
            {
                // Log or handle the exception if needed
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return View("Error");
            }
        }


        [Route("males")]
        public async Task<IActionResult> Males()
        {
            var client = _clientFactory.CreateClient("ApiClient");
            var response = await client.GetAsync("api/students");

            if (response.IsSuccessStatusCode)
            {
                var students = await response.Content.ReadAsStringAsync();
                var allStudents = JsonConvert.DeserializeObject<List<Student>>(students);

                var maleStudents = allStudents.Where(s => s.GioiTinh == true).ToList();
                return View("Index", maleStudents);
            }

            // Handle error here if needed
            return View("Index", new List<Student>());
        }

        [Route("females")]
        public async Task<IActionResult> Females()
        {
            var client = _clientFactory.CreateClient("ApiClient");
            var response = await client.GetAsync("api/students");

            if (response.IsSuccessStatusCode)
            {
                var students = await response.Content.ReadAsStringAsync();
                var allStudents = JsonConvert.DeserializeObject<List<Student>>(students);

                var femaleStudents = allStudents.Where(s => s.GioiTinh == false).ToList();
                return View("Index", femaleStudents);
            }

            // Handle error here if needed
            return View("Index", new List<Student>());
        }

        [Route("salary-10000")]
        public async Task<IActionResult> Salary10000()
        {
            var client = _clientFactory.CreateClient("ApiClient");
            var response = await client.GetAsync("api/students");

            if (response.IsSuccessStatusCode)
            {
                var students = await response.Content.ReadAsStringAsync();
                var allStudents = JsonConvert.DeserializeObject<List<Student>>(students);

                var highSalaryStudents = allStudents.Where(s => s.Luong == 10000).ToList();
                return View("Index", highSalaryStudents);
            }

            // Handle error here if needed
            return View("Index", new List<Student>());
        }

        [Route("birth-year-2003")]
        public async Task<IActionResult> BirthYear2003()
        {
            var client = _clientFactory.CreateClient("ApiClient");
            var response = await client.GetAsync("api/students");

            if (response.IsSuccessStatusCode)
            {
                var students = await response.Content.ReadAsStringAsync();
                var allStudents = JsonConvert.DeserializeObject<List<Student>>(students);

                var studentsBornIn2003 = allStudents.Where(s => s.NgaySinh.Year == 2003).ToList();
                return View("Index", studentsBornIn2003);
            }

            // Handle error here if needed
            return View("Index", new List<Student>());
        }




        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            Console.WriteLine("Dữ liệu gửi đi khi tạo mới: " + JsonConvert.SerializeObject(student));
            try
            {
                var client = _clientFactory.CreateClient("ApiClient");

                var response = await client.PostAsJsonAsync("api/students", student);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Log or handle the error if needed
                    ModelState.AddModelError(string.Empty, "Error creating data through the API.");
                }
            }
            catch (Exception)
            {
                // Log or handle the exception if needed
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
            }

            return View(student);
        }


        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var client = _clientFactory.CreateClient("ApiClient");

                var response = await client.GetAsync($"api/students/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var student = await response.Content.ReadAsStringAsync();
                    var studentViewModel = JsonConvert.DeserializeObject<Student>(student);

                    return View(studentViewModel);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return NotFound();
                }
                else
                {
                    // Log or handle the error if needed
                    ModelState.AddModelError(string.Empty, "Error retrieving data from the API.");
                    return View("Error");
                }
            }
            catch (Exception )
            {
                // Log or handle the exception if needed
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return View("Error");
            }
        }

        [HttpPost]
        [Route("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            
            Console.WriteLine("Dữ liệu gửi đi: " + JsonConvert.SerializeObject(student));
            Console.WriteLine("Dữ liệu gửi đi: " + JsonConvert.SerializeObject(id));

            if (id != student.Id)
            {
                return NotFound();
            }

            try
            {
                var client = _clientFactory.CreateClient("ApiClient");
               
                var response = await client.PutAsJsonAsync($"api/Students/{id}", student);
                Console.WriteLine("day la repon", response);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Log or handle the error if needed
                    ModelState.AddModelError(string.Empty, "Error updating data through the API.");
                }
            }
            catch (Exception )
            {
                // Log or handle the exception if needed
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
            }
            

            return View(student);
        }

        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var client = _clientFactory.CreateClient("ApiClient");

                var response = await client.GetAsync($"api/students/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var student = await response.Content.ReadAsStringAsync();
                    var studentViewModel = JsonConvert.DeserializeObject<Student>(student);

                    return View(studentViewModel);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return NotFound();
                }
                else
                {
                    // Log or handle the error if needed
                    ModelState.AddModelError(string.Empty, "Error retrieving data from the API.");
                    return View("Error");
                }
            }
            catch (Exception )
            {
                // Log or handle the exception if needed
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return View("Error");
            }
        }

        [HttpPost]
        [Route("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var client = _clientFactory.CreateClient("ApiClient");

                var response = await client.DeleteAsync($"api/students/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Log or handle the error if needed
                    ModelState.AddModelError(string.Empty, "Error deleting data through the API.");
                }
            }
            catch (Exception )
            {
                // Log or handle the exception if needed
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
            }

            return View("Error");
        }
    }


}
