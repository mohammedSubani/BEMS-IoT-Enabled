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
	
	/*Registration function service interface saving data to local JSON database*/
	[OperationContract]
	bool Register(string userName,string password ,string email, string phoneNumber);

	/*Authenticate function interface checking if user is registered in local JSON db*/
	[OperationContract]
	bool Authenticate(string userName,string password);

	/*Authorize function interface returning the role of the user*/
	[OperationContract]
	string Authorize(string userName);
}


