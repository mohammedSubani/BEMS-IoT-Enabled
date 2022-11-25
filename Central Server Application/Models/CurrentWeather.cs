/**
Project: BEMS IoT Enabled System V 1.0.0
File Name: CurrentWeather.cs
Development Environment: Microsoft Visual Studio v16.8.4 Community Version
Code contributors: Mohammed Ahmed Subani
ASU ID: 1218278997

File Description:
This is the model for weather api service instance creation,call and data processing.
The instance for this class creates service client to be called by the controllers in the
MVC web application.

Old Functions:None
New Functions:CallService.
Modified Functions:None
*/

namespace WebApplication1.Models
{
    public class CurrentWeather
    {
        public string Humidity = "";    //Current humidity data field 
        public string CloudAmount = ""; //Current cloud amounts data field 
        public string temp = "";        //Current temprature data field 
        public string WindSpeed = "";   //Current wind speed data field 
        public string WindrDir = "";    //Current wind direction data field   

        public string condition = "";   //Current weather condition data field 
        public string icon = "";        //Current weather condition icon URL data field 

        /*The service client instance creation for the weather service*/
        public WeatherService.ServiceClient WeatherCaller = new WeatherService.ServiceClient();


        /**
            Function Description:
                The following function call the weather service at the system's location and 
                upon sucessful data retrieval assigns the data fileds of weather data in this
                class.

            @Parameters: void
            @Returns: void
        */
        public void CallService() 
        {
            /*Calling service at system's location (latitude,longitude)*/
            WeatherCaller.GetWeatherJSON(31.9702593, 35.9568498);

            /*Upon sucessful retrieval assign data fields */
            if (WeatherCaller.SucessRetrieval()) 
            {
                Humidity = WeatherCaller.GetHum();
                CloudAmount = WeatherCaller.GetCloudAmnt();
                WindSpeed = WeatherCaller.GetCloudAmnt();
                WindrDir = WeatherCaller.GetWndDir();
                temp = WeatherCaller.GetTemp();
            }
        }

    }
}
