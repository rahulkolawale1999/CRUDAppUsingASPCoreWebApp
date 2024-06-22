using Microsoft.AspNetCore.Mvc;
using CRUDAppUsingASPCoreWebApp.Models;

using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Text;
namespace CRUDAppUsingASPCoreWebApp.Controllers
{
    public class EmployeeController : Controller
    {

        private string url = "https://localhost:7164/api/Employees/";

        private HttpClient client=new HttpClient();

        [HttpGet]
        public IActionResult Index()
        {

            List<Employee> employee = new List<Employee>();
            HttpResponseMessage response=client.GetAsync(url).Result;
            if(response.IsSuccessStatusCode)
            {
                string result=response.Content.ReadAsStringAsync().Result;
                var data=JsonConvert.DeserializeObject<List<Employee>>(result);
                if(data != null )
                {
                    employee = data;
                }
            }
            return View(employee);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            string data=JsonConvert.SerializeObject(employee);
            StringContent content=new StringContent(data,Encoding.UTF8,"application/Json");
            HttpResponseMessage response=client.PostAsync(url,content).Result;
            if(response.IsSuccessStatusCode )
            {
                TempData["insert_message"] = "Employee added successfully..";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee employee = new Employee();
            HttpResponseMessage response = client.GetAsync(url+id).Result;
            if(response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data=JsonConvert.DeserializeObject<Employee>(result);
                if(data!=null)
                {
                    employee = data;
                }
            }
            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            string data = JsonConvert.SerializeObject(employee);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/Json");
            HttpResponseMessage response = client.PutAsync(url+employee.id,content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["update_message"] = "Employee updated successfully..";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]

        public IActionResult Delete(int id)
        {
            Employee employee = new Employee();
            HttpResponseMessage response=client.GetAsync(url+id).Result;
            if(response.IsSuccessStatusCode )
            {
                string result=response.Content.ReadAsStringAsync().Result;
                var data=JsonConvert.DeserializeObject<Employee>(result);
                if(data!=null)
                {
                    employee = data;
                }
            }
            return View(employee);
        }
        public IActionResult Details(int id)
        {
            Employee employee = new Employee();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Employee>(result);
                if (data != null)
                {
                    employee = data;
                }
            }
            return View(employee);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            
            HttpResponseMessage response = client.DeleteAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["delete_message"] = "Employee deleted";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
