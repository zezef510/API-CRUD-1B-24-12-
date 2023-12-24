using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QLHS_baitap3_.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace QLHS_baitap3_.Controllers
{
    [Route("students")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public StudentController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet("")]
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

            return View(new List<Student>());
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var client = _clientFactory.CreateClient("ApiClient");

            var response = await client.GetAsync($"api/students/{id}");
            if (response.IsSuccessStatusCode)
            {
                var studentDetail = await response.Content.ReadAsStringAsync();
                var studentViewModel = JsonConvert.DeserializeObject<Student>(studentDetail);

                return View(studentViewModel);
            }

            return NotFound();
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            var studentViewModel = new Student();
            return View(studentViewModel);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Student studentViewModel)
        {
            var client = _clientFactory.CreateClient("ApiClient");
            try
            {
                if (ModelState.IsValid)
                {
                    var jsonData = JsonConvert.SerializeObject(studentViewModel);
                    Console.WriteLine($"Data sent to the server: {jsonData}");

                    var jsonContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("api/students", jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var serverResponse = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Data returned from the server: {serverResponse}");

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var serverResponse = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Failed to send data to the server. Server response: {serverResponse}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return View(studentViewModel);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var client = _clientFactory.CreateClient("ApiClient");

            var response = await client.GetAsync($"api/students/{id}");
            if (response.IsSuccessStatusCode)
            {
                var studentDetail = await response.Content.ReadAsStringAsync();
                var studentViewModel = JsonConvert.DeserializeObject<Student>(studentDetail);

                return View(studentViewModel);
            }

            return NotFound();
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(int id, Student studentViewModel)
        {
            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient("ApiClient");

                var jsonContent = new StringContent(JsonConvert.SerializeObject(studentViewModel), System.Text.Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"api/students/{id}", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            return View(studentViewModel);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _clientFactory.CreateClient("ApiClient");

            var response = await client.DeleteAsync($"api/students/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}
