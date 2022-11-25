/**
Project: BEMS IoT Enabled System V 1.0.0
File Name: TestController.cs
Development Environment: Microsoft Visual Studio v16.8.4 Community Version
Code contributors: Mohammed Ahmed Subani
ASU ID: 1218278997

File Description:
This is the controller for the Account management component, this implementation
determines the logic for creating authorization and authentication functionality
this solution uses a cookie-based authentication and authorization using AspNetCore
libraries for authentication and authorization.

Old Functions:None

New Functions:

Modified Functions:None
*/

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EncryptDecrypt;
using WebApplication1.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Sockets;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        /*Model class to call Account Management Service to apply users database logic*/
        public AccountMgmt AccountMgmtSvc = new AccountMgmt();

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
                 The following function returns the Login view. This view contains 
                 input form with username and password fields. non-authenticated
                 users will be re-directed to this page when they try to access pages
                 with authentication restrictions.

             @Parameters: void
             @Returns: IActionResult: View of Login
        */
        public IActionResult Login()
        {
            return View();
        }

        /**
              Function Description:
                  The following function returns the AccessDenied view. This view 
                  is used to redirect to when the user is not authorized.

              @Parameters: void
              @Returns: IActionResult: View of AccessDenied
         */
        public IActionResult AccessDenied()
        {
            return View();
        }

        /**
             Function Description:
                 The following function returns the Homepage view. after successful logging in.
                 in case of unsucessful login, login page view is returned with messages 
                 notifying the user for the reason of login failuare.

                 ** Cookies are non-presistant after a period of time of inactivity users will 
                 *  need to login again.
                
             @Parameters: string userName, string password
             @Returns: IActionResult: View of Login / View of Homepage
        */
        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            /*If username or password fileds submit is empty*/
            if (userName == null || password == null || userName == "" || password == "")
            {
                ViewBag.ErrorMessage = "*Empty fields";
                return View();
            }

            /*Encrypting password to be used later with Account Management Service 
              Authentication function*/
            try { password = EncryptDecryprt.EncryptString(password); }
            catch (Exception) { ViewBag.ErrorMessage = "Encryption/decryption error"; }

            /*Creating Identity object for MVC authentication and authorization*/
            ClaimsIdentity identity = null;
            string role = null;

            /*If Account Management Service returned true result for authentication
              set up Idenity role by calling Auhtenticate function in the 
              Account Management Service*/
            if (AccountMgmtSvc.Authenticate(userName, password))
                role = AccountMgmtSvc.Authorize(userName);
            else
            {
                /* In case of failed authentication notify user*/
                ViewBag.ErrorMessage = "Invalid username or password";
                return View();
            }

            /*At this point authentication has happened sucessfully configur Identity object
              for cookie-based authentication(setting username,role and type of authentication)*/

            identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, role)
                }, CookieAuthenticationDefaults.AuthenticationScheme);

            /*Applying the Identity object settings*/
            var principal = new ClaimsPrincipal(identity);

            /*Starting the authenticated cookie-based session*/
            var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            /*Return to homepage*/
            return RedirectToAction("Index", "Home");
        }

        /**
             Function Description:
                 The following function is called upon loggin out using the logout button
                 that appears on main navigation bar after sucessful login. Calling this
                 function ends 
                
             @Parameters: string userName, string password
             @Returns: IActionResult: View of Login / View of Homepage
        */
        [HttpPost]
        public IActionResult Logout()
        {
            /*Logging out and redirect to homepage*/
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        /**
            Function Description:
                The following function returns the Register view. the register view contains
                a form for registration with the required fields.

                @Parameters: void
                @Returns: IActionResult: View of Register with registration form
        */
        public IActionResult Register()
        {
            return View();
        }

        /**
             Function Description:
                 The following function is called after submitting the Register forms. If the registration
                 is sucessful user will recieve a notifying email, user will be logged in and re-directed to
                 homepage. In case of failed registration Register view is returned with notifying message
                 for the reason of failing to register.

                 ** Cookies are non-presistant after a period of time of inactivity users will 
                 *  need to login again.
                
             @Parameters: string userName, string password, string re_password, string email, string phoneNumber
             @Returns: IActionResult: View of Register / View of Homepage
        */
        [HttpPost]
        public IActionResult Register(string userName, string password, string re_password, string email, string phoneNumber)
        {
            /*Guard against empty fields*/
            if (phoneNumber == "" || phoneNumber == null)
            {
                ViewBag.ErrorMessage2 = "* No phone number entered";
                return View();
            }

            /*Remove white spaces*/
            phoneNumber = phoneNumber.Trim();

            /*Check for phone number format*/
            if (!Int32.TryParse(phoneNumber, out int num))
            {
                ViewBag.ErrorMessage2 = "* Bad phone number format";
                return View();
            }

            /*Check for matching password fields*/
            if (password != re_password)
            {
                ViewBag.ErrorMessage2 = "* Passwords didn't match !";
                return View();
            }

            /*
              If passwords matched encrypt the password for registration and loggin in.
              By this logic users database only registers encrypted passwords.
            */

            try { password = EncryptDecryprt.EncryptString(password); }
            catch (Exception)
            {
                ViewBag.ErrorMessage2 = "* REGISTER IS UNSCUCESSFUL ! Encryption/decryption error";
                return View();
            }

            /* 
             Registering the new user , if registration is successful send an email to the
             user, user is logged in and redirected to homepage. Otherwise the user is notified 
             for the reason of failing to register.
            */
            if (AccountMgmtSvc.Register(userName, password, email, phoneNumber))
            {
                /*Trying to send email */
                try { Execute(email).Wait(); }
                catch (Exception)
                {/*Failing to send email catch the exception*/}

                /*Creating Identity object for MVC authentication and authorization*/
                ClaimsIdentity identity = null;

                /*Setting the role to user, admins registration is done manually editing the 
                 role part in the users database file*/
                string role = "user";

                /*
                 At this point registration and authentication has happened sucessfully configure 
                 Identity object for cookie-based authentication
                 (setting username,role and type of authentication)
                */

                identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, role)
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                /*Applying the Identity object settings*/
                var principal = new ClaimsPrincipal(identity);

                /*Starting the authenticated cookie-based session*/
                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                /*Redirect to homepage*/
                return RedirectToAction("Index", "Home");
            }
            else
                ViewBag.ErrorMessage2 = "* REGISTER IS UNSCUCESSFUL! , user name or email already exists !";

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
            try
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

            catch (Exception) {/*Do Nothing*/}
        }


    }
}
