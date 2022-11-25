BEMS IoT Enabled System V1.0.0

Testing Instructions:

For testing instructions there are two options for testing. The first is to read the code from the submitted 
code and test the application on the local hosted IIS server using a provided URL. The second option is
to test the application locally. 

WARNING: The second option is extremly time consuming it is preferable to use option 1 if the testing time 
	   is limited.

-------------------------------------------------------------------------------------------------------------------------------------------------
Option 1: Testing application on IIS hosted server

Instructions:

1. Download BEMS IoT Source Code zip file and uncompress it. 

2. The folders are arranged in 5 folders.
______________________________________________________________________________
	A. Central Server Application: That is the main web application

	B. Cyclotron: This is SaaS implementation (Not developed by student)
			for more information: https://github.com/ExpediaGroup/cyclotron

	C. HW Embedded Code: Contains the code developed for hardware data acquisiton units

	D.Local Libraries: Contains a single solution, interntal EncryptDecrypt assemblies.

	E.Services Layer: Contains servics hosted on IIS a full directory for these services
			is attached in the final report.
			________________________________________________________________
			I. Account Management Service
		                   II. Data Acquisition Services: Annual Climate Data, Monitoring Service
						, Point Elevation, SolarEnergy and Weather Service
		                  III.Database Management Service.
__________________________________________________________________________________

3. To access the web application use the following URL: 

http://bems-iot.ddnsgeek.com:25253/bems-iot-enabled

4. Test the integration by using the site
5. Use TestSuite for making unit testing and component testing, 
     Each test has its own guiding on the test page.

NOTE: TestSuite component require Admin credentials please note that username is case sensitive

Username: Admin
Password: bems_iot_1995

6. Ad-hoc testing:
	A. Data acquisiton 0 unit testing: RESTful calling to unit in browser
	      Visit: http://bems-iot.ddnsgeek.com:5007/

	B. Data acquisiton 0 unit testing: RESTful calling to unit in browser
	      Visit: http://bems-iot.ddnsgeek.com:5004/

	C. Verify that server is running on database directory:
	      Visit: http://bems-iot.ddnsgeek.com:5003/
-------------------------------------------------------------------------------------------------------------------------------------------
Option 2: Testing application on local host.

NOTE: please keep in mind that testing the application locally is extremly time consuming and contains 
             a two external unresolved depednecies. It is preferable to stick with option 1. Where the student
             is responsible for keeping the IIS server available until the end of testing period. 

1. The hardware components are not available thus components calling to hardware
component will be affected however data visualizations are still functional for the saved database.

2. The other dependency is the need to install required application on local machine MongoDB, Git Windows
	and Node.js to run Cyclotron, The instruction for installing these applications is available at:
 
	https://github.com/ExpediaGroup/cyclotron

Test Instructions:

1. Setup a local IIS server

2. Download BEMS IoT Source Code zip file and uncompress it. 

3. Host the services on the local machine in folder with same name as the solution folder names in order 
 	for them to work for example: DB_USAGE_SVC is the directory name for Data Management Service
				  it should be named DB_USAGE_SVC otherwise it won't work.

4. Open central server application solution , create an application directory name BEMS-IoT-Enabled and publish 
     the web application with saved configurations to that directory. 
	
	A. Visiting each iframe element in the source code replace domain name
		'bems-iot.ddnsgeek.com' with 'localhost'

	B. EncryptDecrypt.dll files should be added as a reference for the cental server application and
	     for the account management service.

5. Install MongoDB, Git Windows and Node.js following installation instructions at:
	https://github.com/ExpediaGroup/cyclotron

6. After installing open a Bash window in Cyclotron-Svc directory by right click in the directory

7. Run this command: node app.js

8. Open a Bash window in Cyclotron-Site directory by right click in the directory

9. Run this command: gulp server

10. in Cyclotron-site component open file " \_public\js\conf\configService.js "
	change domain names 'bems-iot.ddnsgeek.com' to 'localhost'

11. Go to App_data directory in DB_USAGE_SVC in IIS and open a bash window in that directory

12. Run this command: python3 simple-server.py

Now cylcotron server is setup and local services are hosted and the central web application is available
neither monitoring service nor hardware components can be tested locally because there are no setup
devices at the local network. 

You can follow the previous steps in option 1 from this point.
