/**
Project: BEMS IoT Enabled System V 1.0.0
File Name: RelayControl.ino
Development Environment: Arduino IDE 1.8.19
Code contributors: Mohammed Ahmed Subani
ASU ID: 1218278997


File Description:
 
This is the implementation of the embedded code for ESP8266 realy control
circuitry,The code will define the settings for HTTP calls for ESP8266 which
in its turn will decide what signals should be sent to the ESP8266

*/

/*Libraries*/
#include <ESP8266WiFi.h> 	  // Provides WiFi basic functionality
#include <ESP8266WebServer.h> // For setting up response to the assigned IP address


/* ESP8266 Pins Definition*/
#define R_PIN_0 5
#define R_PIN_1 4
#define R_PIN_2 12
#define R_PIN_3 14

/*Wireless access point credentials*/

const char* ssid = "Umniah evo router"; // SSID of the acess point to connect to
const char* pwd = "B1AC73D0"; 			// Password of the access	point

/*Defining web server object like struct using port 5006*/
ESP8266WebServer server(5006);


void setup() {
  Serial.begin(115200); //COM connection port
  delay(100); // Delay to complete connection 
  
  pinMode(R_PIN_0, OUTPUT); // Setting GPIO pin to output state
  pinMode(R_PIN_1, OUTPUT);	// Setting GPIO pin to output state
  pinMode(R_PIN_2, OUTPUT);	// Setting GPIO pin to output state
  pinMode(R_PIN_3, OUTPUT); // Setting GPIO pin to output state

   // Connecting to access point network using wifi
  Serial.print("Starting a WIFI connection with (AP): "); Serial.println(ssid);
  
  // Connecting to AP
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
  server.on("/", main_dir); //root directory call definiton
  
  server.on("/relay0", relay0_call);  // relay0 directory call definiton
  server.on("/relay1", relay1_call);  // relay1 directory call definition
  server.on("/relay2", relay2_call);  // relay2 directory call definition 
  server.on("/relay3", relay3_call);  // relay3 directory call definition 
  
  //Defining the 400 BAD REQUEST response for RESTful call on the IP locally
  server.onNotFound(not_found_handle);

  // Starting ESP8266 server for calls over the network on the assigned IP and port
  server.begin();
  Serial.println("Server is up and running ....... Listening at port 5006");
}

/**
The following function is the main control loop, in this loop the ESP8266 will
be listening for RESTful calls and will switch the realy controls with respect 
to the calls

@Parameters: void
@Returns: void
*/

void loop() 
{
/*==========================================================*/
              /*Handling RESTful calls */
/*==========================================================*/
  server.handleClient();
}


/**
The following function is HTTP 200 OK status handling method, upon sucessful calling to
the IP address of ESP8266 to root directory it will return the main_dir_str() value

@Parameters: void
@Returns: void
*/
void main_dir()     {server.send(200, "text/plain", main_dir_str());}

/**
The following functions are HTTP 200 OK status handling method, upon sucessful calling to
the IP address with the respective realy number it will switch the current status of the 
associated realy and return its current status

@Parameters: void
@Returns: void
*/
void relay0_call()   {server.send(200, "text/plain", relay0_switch());}
void relay1_call()   {server.send(200, "text/plain", relay1_switch());}
void relay2_call()   {server.send(200, "text/plain", relay2_switch());}
void relay3_call()   {server.send(200, "text/plain", relay3_switch());}

/**
The following function is HTTP 404 NOT FOUND status, this function returns a message
notifying a not-found message for users when IP address is called for non-appropriate
requests for example: <IP address>:port/non-defined-page

@Parameters: void
@Returns: void
*/
void not_found_handle(){server.send(404, "text/plain", "This page is not found !");}


/**
The following function returns a String when ESP8266 server is called on its root directory.

@Parameters: void
@Returns: String
*/
String main_dir_str()
{
  String ptr="You are in the root directory";
  return ptr;
}

/**
The following function switches relay0 if it is ON it set to OFF and vice versa and will
return the current status of the switch to the handling function which in its turn return
the current state to the caller 

@Parameters: void
@Returns: String
*/
String relay0_switch()
{
  String ptr="";
  bool state = digitalRead(R_PIN_0); // Read current state 

/*Depending on the current state decide the next state for the relay*/
  if(state == LOW)
  {
    digitalWrite(R_PIN_0,HIGH);
    ptr+="Relay switch 0 is OFF";
  }
  else
  {
    digitalWrite(R_PIN_0,LOW);
    ptr+="Relay switch 0 is ON";
  }
  
  return ptr; // Return the current state of the switch
}

/**
The following function switches relay1 if it is ON it set to OFF and vice versa and will
return the current status of the switch to the handling function which in its turn return
the current state to the caller 

@Parameters: void
@Returns: String
*/
String relay1_switch()
{
  String ptr="";
  bool state = digitalRead(R_PIN_1);// Read current state 

/*Depending on the current state decide the next state for the relay*/
  if(state == LOW)
  {
    digitalWrite(R_PIN_1,HIGH);
    ptr+="Relay switch 1 is OFF";
  }
  else
  {
    digitalWrite(R_PIN_1,LOW);
    ptr+="Relay switch 1 is ON";
  }
  
  return ptr; // Return the current state of the switch
}

/**
The following function switches relay2 if it is ON it set to OFF and vice versa and will
return the current status of the switch to the handling function which in its turn return
the current state to the caller 

@Parameters: void
@Returns: String
*/
String relay2_switch()
{
  String ptr="";
  bool state = digitalRead(R_PIN_2); // Read current state 
  
/*Depending on the current state decide the next state for the relay*/
  if(state == LOW)
  {
    digitalWrite(R_PIN_2,HIGH);
    ptr+="Relay switch 2 is OFF";
  }
  else
  {
    digitalWrite(R_PIN_2,LOW);
    ptr+="Relay switch 2 is ON";
  }
  
  return ptr; // Return the current state of the switch
}

/**
The following function switches relay3 if it is ON it set to OFF and vice versa and will
return the current status of the switch to the handling function which in its turn return
the current state to the caller 

@Parameters: void
@Returns: String
*/
String relay3_switch()
{
  String ptr="";
  bool state = digitalRead(R_PIN_3); // Read current state 
  
/*Depending on the current state decide the next state for the relay*/
  if(state == LOW)
  {
    digitalWrite(R_PIN_3,HIGH);
    ptr+="Relay switch 3 is OFF";
  }
  else
  {
    digitalWrite(R_PIN_3,LOW);
    ptr+="Relay switch 3 is ON";
  }
  
  return ptr; // Return the current state of the switch
}
