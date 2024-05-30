using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PatinetManagement.Utility.ViewModels;
using System.Drawing.Imaging;
using System.Text;

namespace PatientManagement.Web.Controllers
{
    public class DiseaseInformationController : Controller
    {
        Uri baseURL = new Uri("https://localhost:7155/api");
        private readonly HttpClient _httpClient;
        private readonly INotyfService _notyf;
        public DiseaseInformationController(INotyfService notyf)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseURL;
            _notyf = notyf;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<DiseasesViewModel> deasesList = new List<DiseasesViewModel>();
            HttpResponseMessage responseMessage = _httpClient
                .GetAsync(_httpClient.BaseAddress + "/Disease/GetAllDisease").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                deasesList = JsonConvert.DeserializeObject<List<DiseasesViewModel>>(data);
            }
            return View(deasesList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DiseasesViewModel diseases)
        {
            try
            {
                string data = JsonConvert.SerializeObject(diseases);
                StringContent content = new StringContent(data, Encoding.UTF8,"application/json");
                HttpResponseMessage responseMessage = _httpClient
               .PostAsync(_httpClient.BaseAddress + "/Disease/SaveDisease", content).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    _notyf.Success("Disease Information Save Successfully");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                _notyf.Error(ex.Message);
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                DiseasesViewModel diseases = new DiseasesViewModel();
                HttpResponseMessage responseMessage = _httpClient
                  .GetAsync(_httpClient.BaseAddress + "/Disease/GetDisease/" + id).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string data = responseMessage.Content.ReadAsStringAsync().Result;
                    diseases = JsonConvert.DeserializeObject<DiseasesViewModel>(data);
                }
                return View(diseases);
            }
            catch (Exception ex)
            {
                _notyf.Error(ex.Message);
                return View();
            }
        }
        [HttpPost]
        public IActionResult Edit(DiseasesViewModel diseases, int id)
        {
            try
            {
                string data = JsonConvert.SerializeObject(diseases);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = _httpClient
               .PutAsync(_httpClient.BaseAddress + "/Disease/UpdateDisease/" + id, content).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    _notyf.Warning("Disease Information Update Successfully");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                _notyf.Error(ex.Message);
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                DiseasesViewModel diseases = new DiseasesViewModel();
                HttpResponseMessage responseMessage = _httpClient
                  .GetAsync(_httpClient.BaseAddress + "/Disease/GetDisease/" + id).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string data = responseMessage.Content.ReadAsStringAsync().Result;
                    diseases = JsonConvert.DeserializeObject<DiseasesViewModel>(data);
                }
                return View(diseases);
            }
            catch (Exception ex)
            {
                _notyf.Error(ex.Message);
                return View();
            }
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            try
            {
                HttpResponseMessage responseMessage = _httpClient
               .DeleteAsync(_httpClient.BaseAddress + "/Disease/DeleteDisease/" + id).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    _notyf.Error("Disease Information Delete Successfully");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                _notyf.Error(ex.Message);
                return View();
            }
            return View();
        }
    }
   
}


//_notyf.Success("Success Notification");
//_notyf.Error("Some Error Message");
//_notyf.Warning("Some Error Message");
//_notyf.Information("Information Notification - closes in 4 seconds.", 4);