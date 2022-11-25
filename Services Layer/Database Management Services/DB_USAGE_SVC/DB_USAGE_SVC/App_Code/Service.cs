/**
Project: BEMS IoT Enabled System V 1.0.0
File Name: Service.cs
Development Environment: Microsoft Visual Studio v16.8.4 Community Version
Code contributors: Mohammed Ahmed Subani
ASU ID: 1218278997

File Description:
 
The following service implements the environmental paramters database management layer
this service controls the reading and writing to the readings database and is used by the 
monitoring service to write to the database periodically.

Old functions: None
New functions: setNew, getDataRow, getDataColumn, getDataCell, getDataString, ReadingsCount
Modified functions: None
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;

public class Service : IService
{
	/**
	  Function Description:
		  A method that will write the a new reading to a room readings database
		  where every room has a database file assoicated with it, the function
		  recieves the environmental readings temprature, humidity, co2 levels,light 
		  intensity, noise and motion detection state.
		  
	  @parameters: double temp, double hum, double CO2, double lux,double db, double motion
                 ,string room_ref
				 
	  @returns: boolean flag for the writing state to the database
	*/
	
    public bool setNew(double temp, double hum,
                double CO2, double lux,
                double db, double motion
                ,string room_ref) 

    {
        /*Finding the App_Data folder in the hosting environmen*/
        string appLocate = AppDomain.CurrentDomain.BaseDirectory;
        string path = appLocate + @"\App_Data\"+ room_ref +".json";
		
		/*Get the current readings count to assign an id number for the reading*/
        int id = ReadingsCount(room_ref);

		/*If counting has failed don't write to database and return false*/
        if (id == -1)
            return false;
		
		/*Reading the current database in a string variable, add the new reading 
			to the string then append the database file with the new reading*/
        try
        {
            var lines = File.ReadAllLines(path); //Get all the lines from file
			
			/*count lines to get the behind last line to write to
			the last line contains the closing of JSON array object ']' */
            File.WriteAllLines(path, lines.Take(lines.Length - 1).ToArray());
			
            string entry=""; // The new entry string
			
			/*Add comma only if this is not the first reading*/
            if (id > 0)
                entry = ",";

			/*Adding the new reading in JSON object formatting with the currnet date of the
				new reading*/
            entry = entry + "{\n    \"id\":" + id + ",\n" 
                          +"    \"temp\":"+ temp + ",\n"
                          +"    \"hum\":" + hum + ",\n"
                          +"    \"lux\":" + lux + ",\n"
                          +"    \"co2\":" + CO2 + ",\n"
                          +"    \"db\":" + db + ",\n"
                          +"    \"mt\":" + motion + ",\n"
                          +"    \"date\":" +"\"" +DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + "\"\n"
                          +" }\n]";
			
			/*Append the database file with the new entry*/
            File.AppendAllText(path,entry);

            return true; // At this point the writing is sucessful
        }
        catch (Exception) 
        {
           return false; // Failing to write to database 
        }

    }

	/**
	  Function Description:
		  A method that will read a row of database readings (a single reading)
		  the row number is the id number and will return the reading with 
		  that id (id's are ordered incrementally).
		  
	  @parameters: int row, string room_ref 
	  @returns: string of a data reading formatted in JSON object format
	*/
    public string getDataRow(int row, string room_ref) 
    {
		/*Finding the App_Data folder in the hosting environmen*/
        string appLocate = AppDomain.CurrentDomain.BaseDirectory;
        string path = appLocate + @"\App_Data\"+room_ref +".json";

		/*Reading the current database in a string variable, returning the 
			reading with assoicated row number (id number)*/
        try
        {
			/*Getting data readings into JSON_String to parse as JObject*/
            string JSON_String = "{ \"Data Readings\":";
			
			/*Get all the JSON array of readings as string*/
            JSON_String += File.ReadAllText(path); 
            JSON_String += "}";
			
			/*Parse the readings*/
            JObject o1 = JObject.Parse(JSON_String);
			
			/*return the targeted reading with its associated row*/
            string rowStr = o1["Data Readings"][row].ToString();

            return rowStr; // return result
        }
        catch (Exception e) 
        {
            return e.Message; // Failed to read database return the error cause
        }

    }

	/**
	  Function Description:
		  A method that will read a column of database readings (getting a parameter reading
		  for the entire readings)the function gets the parameter type: temp,hum,lux ..etc
		  and return the readings for that parameter.
		  
	  @parameters: string param, string room_ref
	  @returns: string of a data readings
	*/
    public string getDataColumn(string param, string room_ref) 
    {
		/*Getting data readings into JSON_String to parse as JObject*/
        string appLocate = AppDomain.CurrentDomain.BaseDirectory;
        string path = appLocate + @"\App_Data\" + room_ref + ".json";
		
		/*Reading the current database in a string variable, returning the 
			readings with assoicated parameter*/
        try
        {
			/*Getting data readings into JSON_String to parse as JObject*/
            string JSON_String = "{ \"Data Readings\":";
            JSON_String += File.ReadAllText(path);
            JSON_String += "}";
			
			/*Get all the JSON array of readings as string*/
            JObject o1 = JObject.Parse(JSON_String);
			
			/*Get all the JSON array readings count to iterate over*/
            int ReadingsCount = o1["Data Readings"].Count();
			
			/*Results of readings ordered with each reading on a new line*/
            string Result = "";
            for (int i = 0; i < ReadingsCount; i++)
            {
                Result += "    ";
				
				// Getting the data reading for a parameter
                Result += o1["Data Readings"][i][param].ToString(); 
                Result += "\n";
            }

            return Result; // Return the results 
        }

        catch (Exception e) 
        {
            return e.Message; // Failing to get the readings return the error message
        }

    }
	
	/**
	  Function Description:
		  A method that will read a cell reading of database readings 
		  (getting a parameter reading for a specific reading)the 
		  function gets the parameter type: temp,hum,lux ..etc
		  and the specific reading (row number/id) and returns the 
		  assoicated readings.
		  
	  @parameters: int row,string param,string room_ref
	  @returns: string of a parameter reading in a row
	*/
    public string getDataCell(int row,string param,string room_ref)
    {
		/*Getting data readings into JSON_String to parse as JObject*/
        string appLocate = AppDomain.CurrentDomain.BaseDirectory;
        string path = appLocate + @"\App_Data\"+ room_ref +".json";
		
		/*Reading the current database in a string variable, returning the 
			specific reading for a paramter in a specific row*/
        try
        {
			/*Getting data readings into JSON_String to parse as JObject*/
            string JSON_String = "{ \"Data Readings\":";
            JSON_String += File.ReadAllText(path);
            JSON_String += "}";
			
			/*parse readings in JObject*/
            JObject o1 = JObject.Parse(JSON_String);
			
			/*Get the reading and return it*/
            string Result = o1["Data Readings"][row][param].ToString();


            return Result; // Sucessful reading retrieval 
        }

        catch (Exception e) 
        {
            return e.Message; // In case of error, return error message
        }
    }
	
	/**
	  Function Description:
		  A method that will get the entire table of readings for a room
		  in a string that is JSON formatted.
		  
	  @parameters: string room_ref
	  @returns: string of readings for a room
	*/
    public string getDataString(string room_ref)
    {
		/*Getting data readings into JSON_String to parse as JObject*/
        string appLocate = AppDomain.CurrentDomain.BaseDirectory;
        string path = appLocate + @"\App_Data\" + room_ref + ".json";

		/*Reading the current database in a string variable, returning the 
			specific reading for a paramter in a specific row*/
        try
        {
			/*Getting data readings into JSON_String to parse as JObject*/
            string JSON_String = "{ \"Data Readings\":";
            JSON_String += File.ReadAllText(path);
            JSON_String += "}";
			
			/*Parse readings in JObject*/
            JObject o1 = JObject.Parse(JSON_String);


            return o1.ToString() ; // Sucessful reading retrieval 
        }

        catch (Exception e)
        {
            return e.Message; // In case of error, return error message
        }

    }
	
	/**
	  Function Description:
		  A method that will get the count for the database readings for a room.
		  
	  @parameters: string room_ref
	  @returns: integer number of readings, failing to read database will return -1
	*/
    public static int ReadingsCount(string room_ref) 
    {
		/*Getting data readings into JSON_String to parse as JObject*/
        string appLocate = AppDomain.CurrentDomain.BaseDirectory;
        string path = appLocate + @"\App_Data\" + room_ref + ".json";

        try
        {
			/*Reading the current database*/
            string JSON_String = "{ \"Data Readings\":";
            JSON_String += File.ReadAllText(path);
            JSON_String += "}";
			
			/*Get all the JSON array of readings as string*/
            JObject o1 = JObject.Parse(JSON_String);
			
			/*Get the counts*/
            return o1["Data Readings"].Count();
        }

        catch (Exception) { return -1;} // Error reading the database file
    }

}
