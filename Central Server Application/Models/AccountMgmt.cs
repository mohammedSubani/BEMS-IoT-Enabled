/**
Project: BEMS IoT Enabled System V 1.0.0
File Name: AccountMgmt.cs
Development Environment: Microsoft Visual Studio v16.8.4 Community Version
Code contributors: Mohammed Ahmed Subani
ASU ID: 1218278997

File Description:
This is the model for account managment service instance creation,call and data processing.
The instance for this class creates service client to be called by the controllers in the
MVC web application.

Old Functions:None
New Functions:Register, Authenticate, Authorize.
Modified Functions:None
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class AccountMgmt
    {
        /*Account Management Service client instance creation*/
        AccountMgmtSvc.ServiceClient mgmtClient = new AccountMgmtSvc.ServiceClient();


        /**
            Function Description:
                The following function uses the account managment service registration function.

            @Parameters: string userName,string password,string email,string phoneNumber
            @Returns: boolean with registration result (successful/unsuccessful)
        */
        public bool Register(string userName,string password,string email,string phoneNumber)
        {
            return mgmtClient.Register(userName,password,email,phoneNumber);
        }


        /**
            Function Description:
                The following function returns the authentication result for username and password
                using the account management service.

            @Parameters: string userName,string pwd
            @Returns: boolean with authentication result (Authenticated, non-authenticated)
        */
        public bool Authenticate(string userName,string pwd) 
        {
            return mgmtClient.Authenticate(userName,pwd);
        }

        /**
            Function Description:
                The following function returns the role for a user using account managment service.

            @Parameters: string userName
            @Returns: string with user's role
        */
        public string Authorize(string userName) 
        {
            return mgmtClient.Authorize(userName); 
        }
    }
}
