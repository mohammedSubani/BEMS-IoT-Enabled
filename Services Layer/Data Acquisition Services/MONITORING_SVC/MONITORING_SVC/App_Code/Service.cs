/**
Project: BEMS IoT Enabled System V 1.0.0
File Name: Service.cs
Development Environment: Microsoft Visual Studio v16.8.4 Community Version
Code contributors: Mohammed Ahmed Subani
ASU ID: 1218278997

File Description:
 
The following code implements the monitoring functionality for the BEMS IoT Enabled system
this code  connect to the data acquisition ip addresses retrieves the data and use the 
database management service to write the readings back to the database.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;


public class Service : IService
{
    public static bool running = false; //Monitoring loop running flag initally set to false
	
	/**
	  Class to define a run method for starting a sepearte thread than the one running the
	  service, when startLoop opeartion is called a new service loop instance is called.
	*/
    public class MonitoringHW 
    {
	/**
	  Function Description:
		This is the run method for the monitoring thread, this function  connect  to
		the database service, read data from the acquisition units and write data back
		to BEMS database of readings.
	  
	  @parameters: void
	  @returns: void
	*/
        public void runMonitor() 
        {
			// defining a client to the database of BEMS of readings.
            DB_Usage_Svc.ServiceClient DB_Client = new DB_Usage_Svc.ServiceClient();

            string Data_Unit_0 = ""; // Data acquisition unit 1 reading
            string Data_Unit_1 = ""; // Data acquisition unit 2 reading

            running = true; // Setting the monitoring service state to true

			/*Monitoring loop to get data readings every assigned period of time
			  and writing the data back to the database of BEMS*/
            while (true) 
            {
                if (running == false) // If flag is set to false terminate the loop
                    break;

                Thread.Sleep(300000); // Monitoring time .... 5 minutes
                Data_Unit_0 = getDataString("192.168.1.101",  "5007"); //Local IP address for Data acquisition unit 1
                Data_Unit_1 = getDataString("192.168.1.102", "5004"); //Local IP address for the Data acquisition unit 2 

                if (Data_Unit_0 != "" && Data_Unit_1 != "")// Checking for null value 
                {
                    try
                    {
                        JObject room_0 = JObject.Parse(Data_Unit_0); // trying to parse readings into JObject
                        JObject room_1 = JObject.Parse(Data_Unit_1); // trying to parse readings into JObject

						// Saving data to be written to the BEMS database of readings(room0)
                        string temp = room_0["Data Reading"][0]["temp"].ToString();
                        string hum = room_0["Data Reading"][0]["hum"].ToString();
                        string lux = room_0["Data Reading"][0]["lux"].ToString();
                        string co2 = room_0["Data Reading"][0]["co2"].ToString();
                        string db = room_0["Data Reading"][0]["db"].ToString();
                        string mt = room_0["Data Reading"][0]["mt"].ToString();

						// Using the database management service to write the data
                        DB_Client.setNew(
                            Double.Parse(temp),
                            Double.Parse(hum),
                            0,
                            Double.Parse(lux),
                            Double.Parse(db),
                            Double.Parse(mt), "Room_0");

						// Saving data to be written to the BEMS database of readings(room1)
                        temp = room_1["Data Reading"][0]["temp"].ToString();
                        hum = room_1["Data Reading"][0]["hum"].ToString();
                        lux = room_1["Data Reading"][0]["lux"].ToString();
                        co2 = room_1["Data Reading"][0]["co2"].ToString();
                        db = room_1["Data Reading"][0]["db"].ToString();
                        mt = room_1["Data Reading"][0]["mt"].ToString();
						
						// Using the database management service to write the data
                        DB_Client.setNew(
                                Double.Parse(temp),
                                Double.Parse(hum),
                                Double.Parse(co2),
                                Double.Parse(lux),
                                Double.Parse(db),
                                Double.Parse(mt), "Room_1");

                    }
					// This exception guards against failing to get the readings
                    catch (Exception) { continue; } 
                }
                else 
                {
                    running = false; // If data readings return null set the monitor to false
                }
            }
        }
    }

	/**
	  Function Description:
		This function  make RESTful call to a local data acquisition unit 
		on a DHCP assigned IP address and a specific port decided in the hardware
		embedded code.
	  
	  @parameters: string IP,string port
	  @returns: string returned by a RESTful call to data acquisition unit
	*/
    public static string getDataString(string IP,string port) 
    {
        using (WebClient client = new WebClient())
        {
            string urlString = "http://"+ IP + ":" + port + "/";
            try
            {
				// Try to get the reading
                return client.DownloadString(urlString);
            }
            catch (WebException e)
            {
				// In case reading is not running (offline)
                return e.Message;
            }
        }
    }

	/**
	  Function Description:
		This function returns the state of the monitoring service.
	  
	  @parameters: void
	  @returns: boolean flag for the monitoring state
	  
	*/
    public bool IsRunning() { return running; }

	/**
	  Function Description:
		This function creates a new monitoring instance and starts the monitoring.
	  
	  @parameters: void
	  @returns: void
	  
	*/
    public void startLoop() 
    {
        if (running == false) 
        {
            MonitoringHW MonitorInstance = new MonitoringHW();
            Thread t1 = new Thread(new ThreadStart(MonitorInstance.runMonitor));
            t1.Start();
        }
    }
	
	/**
	  Function Description:
		This function sets the monitor state to false stopping the monitoring loop.
	  
	  @parameters: void
	  @returns: void
	  
	*/
    public void stopLoop() {if(running == true) running = false;}
}
