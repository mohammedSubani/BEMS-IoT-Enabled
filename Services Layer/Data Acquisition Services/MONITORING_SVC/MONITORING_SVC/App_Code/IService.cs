using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

[ServiceContract]
public interface IService
{
	// Starting the monitoring loop operation interface
	[OperationContract]
	void startLoop();
	
	// Stopping the monitoring loop operation interface
	[OperationContract]
	void stopLoop();
	
	// Checking if the monitoring loop is running or not
	[OperationContract]
	bool IsRunning();

}



