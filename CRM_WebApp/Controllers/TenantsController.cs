using System.Text;
using CRM_WebApp.Service.IService;
using CRMWeb_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static CRM_WebApp.Utility.CrmConstants;

namespace CRM_WebApp.Controllers
{
    public class TenantsController : Controller
    {
        HttpClient client = new HttpClient();
        string url = "https://localhost:7294/api/Tenant/";
        private readonly ITokenProvider _tokenProvider;
        public TenantsController(ITokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        // GET: Tenants
        public async Task<IActionResult> Index()
        {
            try {
                var message = GetResponse(url, ApiType.GET);
                var response = await client.SendAsync(message);
                var apiContent = await response.Content.ReadAsStringAsync();
                var tenants = JsonConvert.DeserializeObject<List<Tenant>>(apiContent).ToList();
                return View(tenants);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        // GET: Tenants/Details/5
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                var tenant = await GetTenant(id);
                return View(tenant);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // GET: Tenants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tenants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantName,EmailId,TenantId")] Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                try {

                    var message = GetResponse(url, ApiType.POST);
                    message.Content = new StringContent(JsonConvert.SerializeObject(tenant), Encoding.UTF8, "application/json");
                    var response = await client.SendAsync(message);
                    var apiContent = await response.Content.ReadAsStringAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            return View(tenant);
        }

        // GET: Tenants/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            try { 
                var tenant = await GetTenant(id);
                return View(tenant);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        // POST: Tenants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TenantId,TenantName,EmailId")] Tenant tenant)
        { 
            if (ModelState.IsValid)
            {
                try
                {
                    var message = GetResponse(url+id, ApiType.PUT);
                    message.Content = new StringContent(JsonConvert.SerializeObject(tenant), Encoding.UTF8, "application/json");
                    var response = await client.SendAsync(message);
                    var apiContent = await response.Content.ReadAsStringAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw ex;
                }
                
            }
            return View(tenant);
        }

        // GET: Tenants/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            try { 
            var tenant = await GetTenant(id);
            return View(tenant);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST: Tenants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try {
                var message = GetResponse(url+id, ApiType.DELETE);
                var response = await client.SendAsync(message);
                var apiContent = await response.Content.ReadAsStringAsync();
            return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<Tenant> GetTenant(string? id)
        {
            var message = GetResponse(url+id, ApiType.GET);
            var response = await client.SendAsync(message);
            var apiContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Tenant>(apiContent);
        }

        private HttpRequestMessage GetResponse(string url, ApiType method)
        {
            
            HttpRequestMessage message = new();
            switch (method)
            {
                case ApiType.POST:
                    message.Method = HttpMethod.Post;
                    break;
                case ApiType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;
                case ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;
                default:
                    message.Method = HttpMethod.Get;
                    break;
            }
            var token = _tokenProvider.GetToken();
            message.Headers.Add("Authorization", $"Bearer {token}");
            message.RequestUri = new Uri(url);
            message.Headers.Add("Accept", "application/json");
            return message;
        }
    }
}
