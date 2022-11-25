/**
Project: BEMS IoT Enabled System V 1.0.0
File Name: DatabaseMgmt.cs
Development Environment: Microsoft Visual Studio v16.8.4 Community Version
Code contributors: Mohammed Ahmed Subani
ASU ID: 1218278997

File Description:
This is the model for Database Management Service instance creation,call and data processing.
The instance for this class creates service client to be called by the controllers in the
MVC web application.

Old Functions:None
New Functions:callService.
Modified Functions:None
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class DatabaseMgmt
    {
        /*The service client instance creation for the database management service*/
        DatabaseMgmtSvc.ServiceClient client = new DatabaseMgmtSvc.ServiceClient();

        /*Calling to database management service test*/
        public string testInstance = null;


        /**
            Function Description:
                The following function call the database managment service and checks for the
                health of database management service by calling its functions. This function
                is only used by the TestSuite database component on a sample test database file.

            @Parameters: void
            @Returns: void
        */
        public void callService() 
        
        {
            testInstance=client.getDataRow(0, "Database2");
        }
    }
}
