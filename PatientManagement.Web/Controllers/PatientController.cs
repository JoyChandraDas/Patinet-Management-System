using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using PatinetManagement.DataAccess.Domain;
using PatinetManagement.DataAccess.Enums;
using PatinetManagement.Infrastructure.Repositories;
using PatinetManagement.Utility.ViewModels;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PatientManagement.Web.Controllers
{
    public class PatientController : Controller
    {
        Uri baseURL = new Uri("https://localhost:7155/api");
        private readonly HttpClient _httpClient;
        private readonly INotyfService _notyf;
        public PatientController(INotyfService notyf)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseURL;
            _notyf = notyf;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<PatientViewModel> patientList = new List<PatientViewModel>();
            HttpResponseMessage responseMessage = _httpClient
                .GetAsync(_httpClient.BaseAddress + "/PatientInformation/PatientInformationList").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                patientList = JsonConvert.DeserializeObject<List<PatientViewModel>>(data);
            }
            return View(patientList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<Allergies> allergiesList = new List<Allergies>();
            HttpResponseMessage responseMessage = _httpClient
                .GetAsync(_httpClient.BaseAddress + "/PatientInformation/GetAllergiesList").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                allergiesList = JsonConvert.DeserializeObject<List<Allergies>>(data);
            }

            ViewBag.AllergiesList = allergiesList;

            List <NCD> ncdsList = new List<NCD>();
            HttpResponseMessage responseMessage1 = _httpClient
                .GetAsync(_httpClient.BaseAddress + "/PatientInformation/GetNDCsList").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage1.Content.ReadAsStringAsync().Result;
                ncdsList = JsonConvert.DeserializeObject<List<NCD>>(data);
            }
            ViewBag.NCDList = ncdsList;

            List <DiseaseInformation> diseassList = new List<DiseaseInformation>();
            HttpResponseMessage responseMessage2 = _httpClient
                .GetAsync(_httpClient.BaseAddress + "/PatientInformation/GetDiseaseList").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage2.Content.ReadAsStringAsync().Result;
                diseassList = JsonConvert.DeserializeObject<List<DiseaseInformation>>(data);
            }
            ViewBag.DiseaseList = diseassList;

            var epilepsyOptions = Enum.GetValues(typeof(Epilepsy))
                                  .Cast<Epilepsy>()
                                  .ToDictionary(e => (int)e, e => e.ToString());
            ViewBag.Epilepsy = epilepsyOptions;
            return View();
        }
        [IgnoreAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(PatientViewModel patient)
        {
            try
            {
                string data = JsonConvert.SerializeObject(patient);
                
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                
                HttpResponseMessage responseMessage = await _httpClient
                    .PostAsync(_httpClient.BaseAddress + "/PatientInformation/SavePatient", content);
                
                if (responseMessage.IsSuccessStatusCode)
                {
                    _notyf.Success("Patient Information Save Successfully");
                    return RedirectToAction("Index");
                }
                else
                {
                    _notyf.Error("Failed to save patient information.");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                _notyf.Error(ex.Message);
                return View();
            }
        }

    }
}
