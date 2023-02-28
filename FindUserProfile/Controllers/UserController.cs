using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FindUserProfile.Models;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace FindUserProfile.Controllers
{
    public class UserController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        Uri baseAddress = new Uri("https://localhost:44332/api/UserDetails");

        public UserController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            using (var client = new HttpClient())
            {
                List<UserViewModel> customers = new List<UserViewModel>();
                HttpResponseMessage response = client.GetAsync(baseAddress).Result;
                if (response.IsSuccessStatusCode)
                {
                    customers = JsonConvert.DeserializeObject<List<UserViewModel>>(response.Content.ReadAsStringAsync().Result);
                }
                return View(customers);
            }
        }
        public IActionResult Create()
        {
            UserViewModel user = new UserViewModel();
            user.ImageName = "noimage.png";
            var ListOfqua = GetAllQualifications();
            ViewBag.QualDetails = ListOfqua;
            return View(user);
        }
        [HttpPost]
        public IActionResult Create(UserViewModel model)
        {
            using (var client = new HttpClient())
            {
                var filename = UploadedFile(model);
                model.ImageName = filename;
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(baseAddress, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int Id)
        {
            using (var client = new HttpClient())
            {
                var ListOfqua = GetAllQualifications();
                ViewBag.QualDetails = ListOfqua;
                UserViewModel customers = new UserViewModel();
                string apiUrl = baseAddress + "/" + Id;
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    customers = JsonConvert.DeserializeObject<UserViewModel>(response.Content.ReadAsStringAsync().Result);
                }
                customers.QualificationId = ListOfqua.Where(x => x.Qualif == customers.Qualification.ToString()).FirstOrDefault().Id;
                return View(customers);
            }
        }
        [HttpPost]
        public IActionResult Edit(UserViewModel user)
        {
            using (var client = new HttpClient())
            {              
                if(user.Image!=null)
                {
                    var filename = UploadedFile(user);
                    user.ImageName = filename;
                }
                var ApiUrl = baseAddress + "/" + user.Id;
                string data = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(ApiUrl, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Details(int Id)
        {
            var result = GetUserInformation(Id);
            return View(result);
        }
        public IActionResult Delete(int Id)
        {
            var result = GetUserInformation(Id);
            return View(result);
        }
        [HttpPost]
        public IActionResult Delete(int Id, IFormCollection collection)
        {
            using (var client = new HttpClient())
            {
                var ApiUrl = baseAddress + "/" + Id;
                HttpResponseMessage response = client.DeleteAsync(ApiUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        private string UploadedFile(UserViewModel model)
        {
            string uniqueFileName = null;

            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        public List<Qualifications> GetAllQualifications()
        {
            using (var client = new HttpClient())
            {
                List<Qualifications> ListOfqua = new List<Qualifications>();
                string apiUrl = "https://localhost:44332/api/Qualifications";
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    ListOfqua = JsonConvert.DeserializeObject<List<Qualifications>>(response.Content.ReadAsStringAsync().Result);
                }
                return ListOfqua;
            }
        }

        public UserViewModel GetUserInformation(int? Id)
        {
            string apiUrl = baseAddress.ToString();
            if (Id != null)
            {
                apiUrl = baseAddress + "/"+Id;
            }
            using (var client = new HttpClient())
            {
                UserViewModel customers =new UserViewModel();
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    customers = JsonConvert.DeserializeObject<UserViewModel>(response.Content.ReadAsStringAsync().Result);
                }
                return customers;
            }
        }
    }
}
