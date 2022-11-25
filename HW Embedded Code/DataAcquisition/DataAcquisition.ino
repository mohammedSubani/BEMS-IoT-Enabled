/**
Project: BEMS IoT Enabled System V 1.0.0
File Name: DataAcquisition.ino
Development Environment: Arduino IDE 1.8.19
Code contributors: Mohammed Ahmed Subani
ASU ID: 1218278997


File Description:
 
This is the implementation of the embedded code for ESP8266 data acquisition
circuitry,The code will initiate the sensing modules and collect their data 
and broadcast the readings in JSON format to an assigned local access point
*/

/*Libraries*/
#include <ESP8266WiFi.h>  		// Provides WiFi basic functionality
#include <ESP8266WebServer.h> 	// For setting up response to the assigned IP address
#include <Wire.h>				// Allow I2C (Inter-Integrated Circuit, eye-squared-C) communication
#include <Adafruit_Sensor.h>	// General library for Adafruit sensors
#include <Adafruit_TSL2561_U.h> // Library for light intensity sensing functionality
#include "Adafruit_SGP30.h" 	// Library for CO2 sensing functionality
#include "DHT.h"				// Temp. and humidity sensing functionality

/*DHT sensor type definition*/
#define DHTTYPE DHT22

/* ESP8266 Pins Definition*/
#define MIC_PIN A0 				// Sound sensing analog pin
#define DHT_PIN D5 				// DHT input pin definition
#define PIR_PIN D7				// Motion sensor input pin definition

/*Motion sensing status will stay 1 or 0 for the number of these counts*/
#define MOTION_LOOP_COUNTS 1000

/* DHT22 Sensor Variables: Temp (centigrades) and Humidity (relative) */
float temp;
float hum;

/* SGP3 Sensor Variables: TVOC in (ppb)units and eco2 (ppm)units*/
int tvoc;
int eco2;

/* TSL2561 Sensor Variables: light intensity (lux) units */
float lux;

/* LM393 Sensor Variables: sound levels (db) units */

unsigned int db; 				   // Sound level in Decibels
const int    sampling_period = 50; // Frequency = 50 Hz for collecting the analog input
unsigned int analog_reading;	   // Raw analog reading from A0 pin

/* PIR HC-SR501 Sensor Variables: motion detection (boolean)*/

unsigned char current_motion_state;		// The momentary motion state
unsigned int  motion_counter = 0;		// motion_counter for the presistant state
unsigned char motion_loop_state = LOW;  // Initially motion state set to LOW

/*Wireless access point credentials*/

const char* ssid = "Umniah evo router"; // SSID of the acess point to connect to
const char* pwd = "B1AC73D0";			// Password of the access	point

/*Defining web server object like struct using port 5004*/
ESP8266WebServer server(5004);

/*Defining adafruit sensors object like structs (TSL2561 and SGP30 sensors)*/

// For light intensity sensor
Adafruit_TSL2561_Unified tsl = Adafruit_TSL2561_Unified(TSL2561_ADDR_FLOAT, 12345); 

// For CO2 sensor
Adafruit_SGP30 sgp; 

/*Defining a DHT object like struct for DHT sensor with pin and DHT type defined*/
DHT dht(DHT_PIN, DHTTYPE);


/**
The following function is the initiating function for the settings of ESP8266 
and the associated sensing moduels

@Parameters: void
@Returns: void
*/
void setup() {
  
  Serial.begin(115200); //COM connection port 
  delay(100); // Delay to complete connection 

  pinMode(DHT_PIN, INPUT);   //Setting DHT pin to input state to get data from DHT22
  pinMode(PIR_PIN,INPUT) ;    //Setting PIR pin to input state to get data from PIR HC-SR501
  pinMode(MIC_PIN, INPUT); //Setting LM393 pin to input state to get data from LM393

/* In case of failing connection DHT readings will read not a number value (nan)*/

  //starting DHT sensor
  dht.begin();

  //starting I2C conncetions
  Wire.begin();

/*For Adafruit sensors if they failed to connect sucessfly the will notify developer
  and send ESP8266 in an infinite loop this is useful for debugging*/
  
  //starting SGP30 sensor
  if (! sgp.begin()){
    Serial.println("SGP30 Sensor not found ......");
    Serial.println("Sending the device in an infinite loop .......");
    while (1);
  }
  
  //starting TSL2561 sensor
  if(!tsl.begin())
  {
    Serial.println("TSL2561 Sensor not found ......");
    Serial.println("Sending the device in an infinite loop .......");
    while(1);
  }

  // Setting the light gain automatically for TSL2561
  tsl.enableAutoRange(true); 
  
  //Setting the sensor detection resolution, the longer the time the better
  tsl.setIntegrationTime(TSL2561_INTEGRATIONTIME_13MS); 
  
  // Sending notifying message for sucessful connection.
  Serial.println("All sensors initialized sucessfully (^_^)");

 // Connecting to access point network using wifi
  Serial.print("Starting a WIFI connection with (AP): "); Serial.println(ssid);
  
  // Connecting to access point
  WiFi.begin(ssid, pwd);

  //Busy wait to gain IP from AP DHCP server .....
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  
  /* Sucessfully connected to AP and printing the assigned IP */
  Serial.println(""); Serial.println("");
  Serial.println("Connection to AP is established sucessfully");
  Serial.print("Your provided IP is: ");  Serial.println(WiFi.localIP());
  Serial.println(""); Serial.println("");

  //Defining the 200 OK response function for RESTful call on the IP locally
  server.on("/", call_handle); 

  //Defining the 400 BAD REQUEST response for RESTful call on the IP locally
  server.onNotFound(not_found_handle);
 
  // Starting ESP8266 server for calls over the network on the assigned IP and port
  server.begin();
  Serial.println("Server is up and running .......");
}


/**
The following function is the main control loop, in this loop the ESP8266 will
collect readings from  sensors, complete any further processing on the collected
data and broadcast the readings in JSON format to the local network on the assigned 
IP address and port

@Parameters: void
@Returns: void
*/

void loop() {

/*==========================================================*/
        /*Temp. and humidity sesnor data acquisition */
/*==========================================================*/
  temp = dht.readTemperature();
  hum = dht.readHumidity();
  
/*==========================================================*/
              /*Motion sesnor data acquisition */
/*==========================================================*/

  current_motion_state = digitalRead(PIR_PIN); //Getting momentary reading
  motion_counter++; // Incrementing presistant state counter
  
  /* When motion is detected set the state to HIGH state this state will be 
	 presistant for the time of looping number of MOTION_LOOP_COUNTS*/
  if(current_motion_state == HIGH)
  {
    //Serial.println("Motion captured!"); //Uncomment this line for debugging
    motion_loop_state = HIGH;
    delay(500);
  }
  else
  {
    //Serial.println("There was no motion so far ...."); //Uncomment this line for debugging
    delay(500);
  }

  /* Motion loop state is detected every MOTION_LOOP_COUNTS to make the reading presistant
     for the assigned number of loop counts*/
  if (motion_counter == MOTION_LOOP_COUNTS)
  {
    motion_loop_state = LOW ;
    motion_counter = 0;
  }

/*==========================================================*/
              /*Sound sesnor data acquisition */
/*==========================================================*/
           
/**

Algorithm Reference:

The following code excerept implements the algorithm for processing 
the analog readings for LM393 sound sensing module at the following 
blog: https://how2electronics.com/iot-decibelmeter-sound-sensor-esp8266/

*/

  unsigned long start_time= millis(); // Starting time of taking analog samples
  
  float PPVoltage = 0; // Defining the peak-to-peak voltage 

  /*Calibrating max and min signal peaks */
  unsigned int MAX_VOLTAGE = 0;
  unsigned int MIN_VOLTAGE = 1024;

  /* while the time spent collecting analog input is not equal to the sampling_period 
	 after this period the maximum and minimum voltages (wave peaks) are defined */
  
     while (millis() - start_time < sampling_period)
   {
      analog_reading = analogRead(MIC_PIN); // Collect analog input
	  
	  // Analog reading range is 0-1024 (ADC readings) otherwise the reading is false
      if (analog_reading < 1024) 
      {
         if (analog_reading > MAX_VOLTAGE) // Setting the max votlage 
         {
            MAX_VOLTAGE = analog_reading;
         }
         else if (analog_reading < MIN_VOLTAGE)
         {
            MIN_VOLTAGE = analog_reading; // Setting the min votlage 
         }
      }
   }
   
   // Getting the analog wave amplitude 
   PPVoltage = MAX_VOLTAGE - MIN_VOLTAGE;
   
   //Mapping sensor amplitudes value to get the decibels
   db = map(PPVoltage,20,900,49.5,90);

  //Serial.print("The sound level is:"); Serial.println(db); // Uncomment for debugging
  
/*==========================================================*/
              /*Light sesnor data acquisition */
/*==========================================================*/

  sensors_event_t light_event; // Defining a light event
  tsl.getEvent(&light_event);  // Getting the light event
 
  if (light_event.light)
  {
    lux = light_event.light;
    //Serial.print("Light Intensity is: "); // Uncomment for debugging
    //Serial.println(lux); // Uncomment for debugging
    
  }
  else
  {
	// Uncomment for debugging
    //Serial.println("Didn't recieve a light event due overloadding .... completing the loop");
  }
  delay(250);

/*==========================================================*/
              /*CO2 sesnor data acquisition */
/*==========================================================*/

  // Checking if SGP30 sensor is functional if not the loop is ended
  if (!sgp.IAQmeasure()) {
    //Serial.println("Failed to capture SGP30 readings .... ending the loop"); // Uncomment for debugging
    return;
  }
  
  // Saving SGP30 values
  tvoc = sgp.TVOC;
  eco2 = sgp.eCO2;
  
  
  //Serial.print("TVOC "); // Uncomment for debugging
  //Serial.print(tvoc);    // Uncomment for debugging
  //Serial.print(" ppb\t");// Uncomment for debugging
  
  //Serial.print("eCO2 "); // Uncomment for debugging
  //Serial.print(eco2);    // Uncomment for debugging
  //Serial.println(" ppm");// Uncomment for debugging

/*==========================================================*/
              /*Handling RESTful calls */
/*==========================================================*/
  server.handleClient();
}


/**
The following function is HTTP 200 OK status handling method, upon sucessful calling to
the IP address of ESP8266 this function will get the string of JSON readings from
string_toSend() and return them as plain text

@Parameters: void
@Returns: void
*/
void call_handle()     
{
	server.send(200, "text/plain", string_toSend());
}


/**
The following function is HTTP 404 NOT FOUND status, this function returns a message
notifying a not-found message for users when IP address is called for non-appropriate
requests for example: <IP address>:port/non-defined-page

@Parameters: void
@Returns: void
*/
void not_found_handle()
{
	server.send(404, "text/plain", "This page is not found !");
}


/**
The following function returns the JSON format of the sesnor readings to the 
HTTP handling function.

@Parameters: void
@Returns: String
*/
String string_toSend()
{
  String ptr= "{\n  \"Data Reading\":";
  ptr+= "[{";
  
  ptr+= "\n \"temp\":"; //temp reading
  ptr+= temp;
  ptr+= " ,";
  
  ptr+= "\n \"hum\":"; //humidity reading
  ptr+= hum;
  ptr+= " ,";
  
  ptr+= "\n \"lux\":"; //light intensity reading
  ptr+= lux;
  ptr+= " ,";
  
  ptr+= "\n \"co2\":"; //CO2 concentration reading
  ptr+= eco2;
  ptr+= " ,";
  
  ptr+= "\n \"db\":"; //sound descible reading
  ptr+= db;
  ptr+= " ,";
  
  ptr+= "\n \"mt\":"; 
  ptr+= motion_loop_state; //motion state reading
  ptr+= " ,";
  
  ptr+= "\n \"date\": \"1995-10-19T05:00:00\"";
  ptr+= "\n }]";
  ptr+= "\n}";

  return ptr;
}
