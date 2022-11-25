/**
Project: BEMS IoT Enabled System V 1.0.0
File Name: HomeController.cs
Development Environment: Microsoft Visual Studio v16.8.4 Community Version
Code contributors: Mohammed Ahmed Subani
ASU ID: 1218278997

File Description:
This is the controller for the BEMS IoT components, this implementation
determines how the website main pages views processing is done.

Old Functions:None

New Functions:
    Index, Dashboards, RawData, IndoorParam, Analytics, OutdoorParam, AggregateData, 
    SensingUnits, Controlling, Contact, TestSuite, About.

Modified Functions:None
*/

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Models;
using System.Net;
using System.Xml;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /*CurrentWeather model class instance for calling weather service*/
        public CurrentWeather OutdoorWeather = new CurrentWeather();
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        /**
            Function Description:
                The following function returns the homepage view.

            @Parameters: void
            @Returns: IActionResult: View of homepage
        */
        public IActionResult Index()
        {
            return View();
        }

        /**
         Function Description:
            The following function returns the Dashboards view.

            @Parameters: void
            @Returns: IActionResult: View of Dashboards
        */
        [Authorize]
        public IActionResult Dashboards()
        {
            return View();
        }

        /**
         Function Description:
            The following function returns the RawData view.

            @Parameters: void
            @Returns: IActionResult: View of RawData
        */
        [Authorize(Roles ="Admin")]
        public IActionResult RawData()
        {
            return View();
        }

        /**
         Function Description:
            The following function returns the IndoorParam view.

            @Parameters: void
            @Returns: IActionResult: View of IndoorParam
        */
        [Authorize]
        public IActionResult IndoorParam()
        {
            return View();
        }

        /**
         Function Description:
            The following function returns the Analytics view.

            @Parameters: void
            @Returns: IActionResult: View of Analytics
        */
        [Authorize(Roles = "Admin")]
        public IActionResult Analytics()
        {
            return View();
        }

        /**
         Function Description:
            The following function returns the OutdoorParam view.
            Upon page loading this function will use the CurrentWeather class
            instance to create Weather Service Instance and make RESTful calls
            to other climate related services using RESTful calls.

            @Parameters: void
            @Returns: IActionResult: View of OutdoorParam with calling to weather services
        */
        [Authorize]
        public IActionResult OutdoorParam()
        {
            /*Use the CurrentWeather instace to get results from weather service*/
            OutdoorWeather.CallService();

            /*Get the messages to the view*/
            ViewBag.WindDirection = OutdoorWeather.WindrDir;
            ViewBag.WindSpeed = OutdoorWeather.WindSpeed;
            ViewBag.Humidity = OutdoorWeather.Humidity;
            ViewBag.Temp = OutdoorWeather.temp;
            ViewBag.CloudAmount = OutdoorWeather.CloudAmount;

            /*Calling a RESTful service and getting the elevation data related.*/
            XmlDocument elevationDoc = new XmlDocument();

            using (WebClient client = new WebClient())
            {
                /*RESTful URL with latitude/longitude parameters for the system's location*/
                string urlString =
                   "http://localhost:25253/pointElevation/Service.svc/findElevation?" +
                   "latitude=31.9704752" +
                   "&longitude=35.9568352";

                /*RESTful call to the point elevation RESTful service*/
                try { elevationDoc.LoadXml(client.DownloadString(urlString)); }

                catch (WebException) {/*Failing to make a call*/ }
            }

            XmlNodeList elemList = elevationDoc.GetElementsByTagName("string");
            string elevation = elemList[0].InnerXml;

            ViewBag.PointElevation = elevation; /*returning the results*/

            /*Calling a RESTful service SolarEnergy and getting the solar irradiance data.*/
            using (WebClient client = new WebClient())
            {
                /*RESTful URL with latitude/longitude parameters for the system's location*/
                string urlString =
                   "http://localhost:25253/SolarEnergy/Service.svc/GetSolarIndex?" +
                   "latitude=31.9704752" +
                   "&longitude=35.9568352";

                try 
                {
                    /*RESTful call to the service*/
                    string SolarIrradiance = client.DownloadString(urlString);
                    ViewBag.SolarIrradiance= SolarIrradiance.Trim(new Char[] {'\"'}); 
                }

                catch (WebException) {/*Failing to make a call*/  }
            }

            return View();
        }

        /**
          Function Description:
             The following function returns the AggregateData view.

             @Parameters: void
             @Returns: IActionResult: View of AggregateData
         */
        [Authorize]
        public IActionResult AggregateData()
        {
            return View();
        }

        /**
          Function Description:
             The following function returns the SensingUnits view. upon page load
             this controller will check for the health of system's attached hardware
             units on local network assigned IP's and their assigned ports.

             @Parameters: void
             @Returns: IActionResult: View of SensingUnits with their status
         */
        [Authorize(Roles = "Admin")]
        public IActionResult SensingUnits()
        {
            /*RESTful call to sesning unit 0 on port 5007*/
            using (WebClient client = new WebClient())
            {
                /*Sensing unit URL of IP address and the assigned port 5007*/
                string urlString = "http://192.168.1.101:5007/";

                try
                {
                    /*
                      RESTful call upon sucessful call set message sensingUnit_0 to be 
                      interpreted by View code to decide how to present results
                    */
                    client.DownloadString(urlString);
                    ViewBag.sensingUnit_0 = "true";
                }

                catch (WebException) 
                {
                    /*Failing to call the sensing unit*/
                    ViewBag.sensingUnit_0 = "false";
                    return View();
                }
            }

            /*RESTful call to sesning unit 1 on port 5004*/
            using (WebClient client = new WebClient())
            {
                /*Sensing unit URL of IP address and the assigned port 5004*/
                string urlString = "http://192.168.1.102:5004/";


                try
                {
                    /*
                      RESTful call upon sucessful call set message sensingUnit_1 to be 
                      interpreted by View code to decide how to present results
                    */
                    client.DownloadString(urlString);
                    ViewBag.sensingUnit_1 = "true";
                }

                catch (WebException)
                {
                    /*Failing to call the sensing unit*/
                    ViewBag.sensingUnit_1 = "false";
                    return View();
                }
            }

            /*RESTful call to control unit on port 5006*/
            using (WebClient client = new WebClient())
            {
                string urlString = "http://192.168.1.103:5006/";


                try
                {
                    /*
                       RESTful call upon sucessful call set message sensingUnit_2 to be 
                       interpreted by View code to decide how to present results
                    */
                    client.DownloadString(urlString);
                    ViewBag.sensingUnit_2 = "true";
                }

                catch (WebException)
                {
                    /*Failing to call the sensing unit*/
                    ViewBag.sensingUnit_2 = "false";
                    return View();
                }
            }

            return View();
        }


        /**
         Function Description:
            The following function returns the SensingUnits view. upon page submission
            this controller will check for the health of system's attached hardware
            units on local network assigned IP's and their assigned ports.

            @Parameters: string str
            @Returns: IActionResult: View of SensingUnits with their status
        */
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult SensingUnits(string str)
        {
            return View();
        }


        /**
         Function Description:
            The following function returns controlling dashboard view for the control
            module. The view contains a form to submit the switch to use.

            @Parameters: void
            @Returns: IActionResult: View of Controlling
        */
        [Authorize]
        public IActionResult Controlling()
        {
            return View();
        }


        /**
         Function Description:
            The following function returns controlling dashboard view for the control
            module. The view contains a form to submit the switch to use.

            @Parameters: string switchRelay
            @Returns: IActionResult: View of Controlling with status of assigned switch used
        */
        [Authorize]
        [HttpPost]
        public IActionResult Controlling(string switchRelay)
        {
            /*RESTful call to the control module service*/
            using (WebClient client = new WebClient())
            {
                /*RESTful URL with switching the control module parameters*/
                string urlString =
                   "http://192.168.1.103:5006/relay"+ switchRelay;

                /*RESTful call*/
                try  
                { ViewBag.Response += client.DownloadString(urlString); }

                /*Failing to make RESTful call*/
                catch (WebException) 
                { ViewBag.ErrMsg = "Failed to switch relay number: " + switchRelay; }
            }

            return View();
        }

        /**
          Function Description:
             The following function returns the Contact view.

             @Parameters: void
             @Returns: IActionResult: View of Contact
         */
        public IActionResult Contact()
        {
            return View();
        }

        /**
          Function Description:
             The following function returns the TestSuite component view. The links 
             at this page uses TestController instead of main controller(HomeController).

             @Parameters: void
             @Returns: IActionResult: View of TestSuite
         */
        [Authorize(Roles = "Admin")]
        public IActionResult TestSuite()
        {

            return View();
        }

        /**
          Function Description:
             The following function returns the About view.

             @Parameters: void
             @Returns: IActionResult: View of About
         */
        public IActionResult About()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
