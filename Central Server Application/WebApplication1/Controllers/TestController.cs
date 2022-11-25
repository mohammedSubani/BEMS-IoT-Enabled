/**
Project: BEMS IoT Enabled System V 1.0.0
File Name: TestController.cs
Development Environment: Microsoft Visual Studio v16.8.4 Community Version
Code contributors: Mohammed Ahmed Subani
ASU ID: 1218278997

File Description:
This is the controller for the TestSuite component, this implementation
determines the testing sequence for each test.

Old Functions:None

New Functions:
    Index, WeatherSvcTest, DashboardsTest , RegistrationTest, DatabaseTest, AuthorizeTest,
   ,AuthorizeTest ,FindElevation, EncDecTest, EncTest, DecTest, GetSolarIndexTest, ControlTest
   ,Execute.

Modified Functions:None
*/

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using Microsoft.AspNetCore.Authorization;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using EncryptDecrypt;

namespace WebApplication1.Controllers
{
    public class TestController : Controller
    {
        /*
         Models classes used to test services within each class
         there is a service client instance and functions to call
         the services functions 
        */
        public CurrentWeather WeatherSvc = new CurrentWeather();
        public AccountMgmt AccountMgmtSvc = new AccountMgmt();      
        public DatabaseMgmt DatabaseMgmtSvc = new DatabaseMgmt();


        /**
         Function Description:
            The following function returns the homepage view.

            @Parameters: void
            @Returns: IActionResult: View of homepage
        */
        public IActionResult Index()
        {
            return View(); // return view
        }

        /**
         Function Description:
            The following function tests the weather service by calling the weather service
            by getting the current temprature using the CurrentWeather class in Models.
            
            @Parameters: void
            @Returns: IActionResult: View of WeatherSvcTest 
        */
        [Authorize(Roles = "Admin")]
        public IActionResult WeatherSvcTest()
        {
            WeatherSvc.CallService(); // Use CurrentWeather function to call service
            ViewBag.WeatherMessage = WeatherSvc.temp; // Get temprature

            return View();
        }

        /**
         Function Description:
            The following function tests Cyclotron Dashboards by getting an iframe element
            to Cyclotron-site editing page at port 8080.
            
            @Parameters: void
            @Returns: IActionResult: View of DashboardsTest 
        */

        [Authorize(Roles = "Admin")]
        public IActionResult DashboardsTest()
        {
            return View(); // return view
        }

        /**
        Function Description:
            Return registration test page where tester will enter an email as input, in case 
            of sucessful registration test an email is sent to the provided email.
    
            @Parameters: void
            @Returns: IActionResult: View of DashboardsTest 
        */

        [Authorize(Roles = "Admin")]
        public IActionResult RegistrationTest()
        {
            return View(); // return view
        }


        /**
            Function Description:
                Return registration test page after submitting the email with the test results.

                @Parameters: string email
                @Returns: IActionResult: View of DashboardsTest 
        */
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult RegistrationTest(string email)
        {
            /*Use the account management service register function with fake credintials*/
            if (AccountMgmtSvc.Register("fooUser", "fooPwd", email, "fooPhoneNumber"))
            {
                /*Send an email with registration result*/
                Execute(email).Wait();

                /*Successful registration message*/
                ViewBag.Message = "Please check your email for registration confirmation";
            }
            else 
            {
                /*Failed registration message*/
                ViewBag.Message = "Registration failed !";
            }

            return View();
        }

        /**
        Function Description:
            Return database test view with test results, test happens on page load
            this test uses the DatabaseMgmtSvc class testInstace string that is written
            on calling DatabaseMgmtSvc.

            @Parameters: void
            @Returns: IActionResult: DatabaseTest View with test results messages
        */

        [Authorize(Roles = "Admin")]
        public IActionResult DatabaseTest()
        {
            /*Calling database management service using Model class DatabaseMgmtSvc*/
            DatabaseMgmtSvc.callService(); // Calling the service 
            ViewBag.DatabaseResult = DatabaseMgmtSvc.testInstance;// Getting the test result
            return View();
        }

        /**
            Function Description:
                 Return authorize test view with test results, test happens on page load.
                 this test uses the AccountMgmtSvc class to call Account Management Service.

                 @Parameters: void
                 @Returns: IActionResult: AuthorizeTest View with test results messages
        */
        [Authorize(Roles = "Admin")]
        public IActionResult AuthorizeTest()
        {
            ViewBag.AuthorizeMsg = AccountMgmtSvc.Authorize("Admin");
            return View();
        }


        /**
            Function Description:
                Return Annual Climate Data service test view with test results, 
                this test makes a RESTful call to the service to get results
                , test happens on page load.

         @Parameters: void
         @Returns: IActionResult: AnnualClimateDataTest View with test results messages
        */
        [Authorize(Roles = "Admin")]
        public IActionResult AnnualClimateDataTest()
        {
            using (WebClient client = new WebClient())
            {
                /*
                  RESTful call to Annual Climate Data service on the system's location for relative 
                  humidity environmental parameter (RH2M)
                */

                /*RESTful URL string with parameters*/
                string urlString =
                   "http://localhost:25253/AnnualClimateData/Service.svc/GetPointMonthlyClimateData?latitude=31.9702593&longitude=35.9568498&parameter=RH2M";

                string data = null; //String with call result

                try { data = client.DownloadString(urlString); } // RESTful call

                catch (WebException) { data = "Error in retrieving data !"; } // Failed call

                ViewBag.AnnaulDataMessage = data; // Test result message
            }
            return View();
        }

        /**
            Function Description:
                 Return authenitcate test view with input forms to test authentication
                 functionality for a registered user.

                 @Parameters: void
                 @Returns: IActionResult: AuthorizeTest View with test results messages
        */
        [Authorize(Roles = "Admin")]
        public IActionResult AuthenticateTest()
        {
            // Message of test results
            ViewBag.AuthenticateMsg = "Authentication message will appear here !";
            return View();
        }


        /**
            Function Description:
                Return authenitcate test view with input data posted to test authentication
                functionality for a registered user.

                @Parameters: string userName,string password
                @Returns: IActionResult: AuthenticateTest View with test results messages
        */
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AuthenticateTest(string userName,string password)
        {
            // Use local encryption assembly
            password = EncryptDecryprt.EncryptString(password); 

            // Call authetnication function using AccountMgmtSvc Model class
            ViewBag.AuthenticateMsg
                = "Authentication result :"+ AccountMgmtSvc.Authenticate(userName, password);
            return View();
        }

        /**
            Function Description:
                Return Point Elevation service test view with test results, 
                this test makes a RESTful call to the service to get results
                , test happens on page load.

                @Parameters: void
                @Returns: IActionResult: FindElevation View with test results messages
        */
        [Authorize(Roles = "Admin")]
        public IActionResult FindElevation()
        {
            /*
                  RESTful call to Point Elevation service on the system's location
            */

            using (WebClient client = new WebClient())
            {
                /*RESTful URL string with parameters*/
                string urlString =
                   "http://localhost:25253/pointElevation/Service.svc/findElevation?latitude=31.9702593&longitude=35.9568498";

                string data = null;//String with call result

                try { data = client.DownloadString(urlString); } // RESTful call

                catch (WebException) 
                { 
                    data = "Error in retrieving data !"; // failed call
                    return View();
                }

                ViewBag.pointElevation = data;
            }

            return View();
        }

        /**
            Function Description:
                 Return Encryption/Decryption test view with input forms.

                @Parameters: void
                @Returns: IActionResult: EncDecTest View with input forms
        */
        [Authorize(Roles = "Admin")]
        public IActionResult EncDecTest()
        {
            return View();
        }

        /**
            Function Description:
                Return Encryption test view with input forms using the internal 
                encrpytion/decryption test.

                @Parameters: string EncText
                @Returns: IActionResult: EncDecTest View with encrypt test results
        */
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult EncTest(string EncText)
        {
            /*Use internal encryption assembly*/
            ViewBag.EncResult = EncryptDecryprt.EncryptString(EncText); 
            return View("EncDecTest");
        }

        /**
        Function Description:
                Return decryption test view with input forms using the internal 
                encrpytion/decryption assembly.

                @Parameters: string DecText
                @Returns: IActionResult: EncDecTest View with decrypt test results
        */
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DecTest(string DecText)
        {
            ViewBag.DecResult = EncryptDecryprt.DecryptString(DecText);
            return View("EncDecTest");
        }


        /**
            Function Description:
                 Return Solar Energy Service test view with test results, 
                 this test makes a RESTful call to the service to get results
                 , test happens on page load.

                @Parameters: void
                @Returns: IActionResult: GetSolarIndexTest View with test results messages
        */
        [Authorize(Roles = "Admin")]
        public IActionResult GetSolarIndexTest()
        {
            /*
              Making a RESTful call to the Soalr Energy Service on the system's location
              as the test parameters
            */

            using (WebClient client = new WebClient())
            {
                /*RESTful URL with paramters*/
                string urlString =
                   "http://localhost:25253/SolarEnergy/Service.svc/GetSolarIndex?" +
                   "latitude=31.9704752" +
                   "&longitude=35.9568352";

                try
                {
                    /*RESTful call*/
                    string SolarIrradiance = client.DownloadString(urlString); 

                    /*proces the test result*/
                    ViewBag.SolarIrradianceMsg = SolarIrradiance.Trim(new Char[] { '\"' });
                }

                catch (WebException) 
                { 
                    /*Failed RESTful call*/
                    ViewBag.SolarIrradianceMsg = "Failed to get data";
                    return View();
                }
            }
            return View();
        }
        /**
            Function Description:
                 Return Controlling relay module RESTfull call test on its root directory,
                 , test happens on page load.

                @Parameters: void
                @Returns: IActionResult: ControlTest View with test results messages
        */
        [Authorize(Roles = "Admin")]
        public IActionResult ControlTest()
        {
            /*
              Calling the control module on its local IP and its assigned port,
              sucessful call will return a message sayin it is root directory
            */
            using (WebClient client = new WebClient())
            {
                string urlString ="http://192.168.1.103:5006";

                try
                {
                    /*Successful RESTful call*/
                    ViewBag.ControlMsg = client.DownloadString(urlString);
                }

                catch (WebException)
                {
                    /*Failed RESTful call*/
                    ViewBag.ControlMsg = "Failed to connect to relay module";
                    return View();
                }
            }
            return View();
        }

        /**
            Function Description:
                 The following function is an implementation for Microsoft SendGrid API
                 this function implementation is orginally found on Microsoft SendAPI 
                 platform at: https://app.sendgrid.com/guide/integrate/langs/csharp
                    
                 The implementation is modified to meet BEMS IoT Enabled System requirements.

                @Parameters: void
                @Returns: Task completed object for sending email via SendGrid API
        */
        private static async Task Execute(string email)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("deltan1ms@live.com", "BEMS_IoT Admin");
            var subject = "BEMS_IoT_Enabled Registeration";
            var to = new EmailAddress(email, "user");
            var plainTextContent = "Thank you for regisetring in BEMS_IoT_Enabled project";
            var htmlContent = "<strong>Thank you for regisetring in BEMS_IoT_Enabled project</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }


    }
}
