/**
Project: BEMS IoT Enabled System V 1.0.0
File Name: Service.cs
Development Environment: Microsoft Visual Studio v16.8.4 Community Version
Code contributors: Mohammed Ahmed Subani
ASU ID: 1218278997

File Description:
 
The following service implements the account management functionality for BEMS IoT Enabled system
this service will interface the register, authenticate and authorize functions.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

/*Service interface implementation for functions Regisetr,Authenticate and Authorize*/
public class Service : IService
{
	/**
		Function description:
		
		The following function recieves as input parameters account data for a user read 
		the current JSON database and tries to write a new user entry to the current library
		upon sucessful registration it will return true boolean value otherwise it will return false
		
		@parameters: string userName, string password, string email, string phoneNumber
		@returns: boolean
	*/
    public bool Register(string userName, string password, string email, string phoneNumber) 
    {
		/*Finding the path for the users database*/
        string appLocate = AppDomain.CurrentDomain.BaseDirectory; // Get base directory
        string path = appLocate + @"\App_Data\UsersDB.json";      // Locate data base at App_Data folder

        try
        {
            /*Read the database file into a JObject*/
            JObject usersDB = JObject.Parse(File.ReadAllText(path));
			
			/*Create new entry string and parse as JObject to add to database*/
            JObject user = JObject.Parse(
                "{" +

                "\"userName\":" +"\"" +userName+ "\"," +
                "\"password\":" +"\"" +password+ "\"," +
                "\"email\":" + "\"" + email + "\"," +
                "\"role\":" + "\"" + "user" + "\"," +
                "\"phoneNumber\":" + "\"" + phoneNumber + "\"," +

                "}");
			
			/*Get users database entries as array to iterate and check if the users already
				exists in the local database*/
            JArray users = (JArray)usersDB["users"];

			/*Iteration through entries, if username OR email exists return false flagging
			  the failure of registration*/
            foreach (var userEntry in users)
            {
                if (userEntry["userName"].ToString() == userName)
                    return false;

                if (userEntry["email"].ToString() == email)
                    return false;
            }
			
			/*Reaching this point the user data are unique and new (new username and new email)*/
            users.Add(user);

			/*Write JObject data back to UsersDB.json database file*/
            File.WriteAllText(path, usersDB.ToString());

            return true; // Succesful registration
        }
        catch (Exception)
        {
            return false; // Safety method for non-expected errors 
        }
    }
	
	
	/**
		Function description:
		
		The following function checks if the user name and password matches and entry 
		in the JSON users database, if either the user name or the passowrd are incorrect
		it will return a flase value
		
		@parameters: string userName, string password
		@returns: boolean
	*/
    public bool Authenticate(string userName, string password)
    {
		/*Finding the path for the users database*/
        string appLocate = AppDomain.CurrentDomain.BaseDirectory; //Get the base directory
        string path = appLocate + @"\App_Data\UsersDB.json"; // Locate data base at App_Data folder 

        try
        {
			/*Read the database file into a JObject*/	
            JObject usersDB = JObject.Parse(File.ReadAllText(path));
			
			/*Get users database entries as array to iterate and check if the users already
				exists in the local database*/
            JArray users = (JArray)usersDB["users"];

			/*Iteration through entries, if user name and password are found and are correct
				return true otherwise the loop termintates and function returns false*/
            foreach (var userEntry in users)
            {
                if (userEntry["userName"].ToString() == userName) 
                    if (userEntry["password"].ToString() == password)
                        return true;
                
            }

            return false;
        }
        catch (Exception)
        {
            return false; // Guard against unexpected errors
        }
    }

	/**
		Function description:
		
		The following function checks if the user name exists in the database
		if the username exists it will return the role for that user otherwise
		it will return a string with the error message
		
		@parameters: string userName
		@returns: string
	*/
    public string Authorize(string userName) 
    {
		/*Finding the path for the users database*/
        string appLocate = AppDomain.CurrentDomain.BaseDirectory; //Get the base directory
        string path = appLocate + @"\App_Data\UsersDB.json"; // Locate data base at App_Data folder 

        try
        {
			/*Read the database file into a JObject*/
            JObject usersDB = JObject.Parse(File.ReadAllText(path));
			
			/*Get users database entries as array to iterate and check if the user
				exists in the local database*/
            JArray users = (JArray)usersDB["users"];

			/*Iteration through entries, if user name is found return the role 
			  , otherwise the entries list are exhausted, loop is termintated and
			  the function returns a "user not found" string*/
            foreach (var userEntry in users)
            {
                if (userEntry["userName"].ToString() == userName)
                    return userEntry["role"].ToString();
            }

            return "User not found";
        }
        catch (Exception e)
        {
            return e.Message; // Guard against unexpected errors
        }
    }
}
