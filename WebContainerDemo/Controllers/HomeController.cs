using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebContainerDemo.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration config;

        public HomeController(IConfiguration iConfig)
        {
            config = iConfig;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var registryURL = config["DOCKER_REGISTRY"];
                ViewData["REGISTRYURL"] = registryURL;
                if (registryURL != "<acrName>.azurecr.io")
                {
                    var hostEntry = await System.Net.Dns.GetHostEntryAsync(registryURL);
                    ViewData["HOSTENTRY"] = hostEntry.HostName;

                    string region = hostEntry.HostName.Split('.')[1];
                    ViewData["REGION"] = region;

                    var registryIp = System.Net.Dns.GetHostAddresses(registryURL)[0].ToString();
                    ViewData["REGISTRYIP"] = registryIp;
                }

                var osNameAndVersion = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
                ViewData["OS"] = osNameAndVersion;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return View();
        }
    }
}
