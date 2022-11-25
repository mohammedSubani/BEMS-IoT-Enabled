using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Newtonsoft.Json.Linq;

[ServiceContract]
public interface IService
{
	/* This operation is used to write to database of readings, it recieves the readings
	   in double and the database room reference for the targeted file*/
    [OperationContract]
    bool setNew(double temp, double hum, 
                double CO2, double lux, 
                double db, double motion,
                string room_ref);
	
	/* Returns a data row from the database readings associated with room reference*/
    [OperationContract]
    string getDataRow(int row, string room_ref);

	/* Returns a data column from the database readings associated with room reference*/
    [OperationContract]
    string getDataColumn(string param , string room_ref);

	/* Returns a data cell from the database readings associated with room reference*/
    [OperationContract]
    string getDataCell(int row,string param,string room_ref);
	
	/* returns the entir data table for a room*/
    [OperationContract]
    string getDataString(string room_ref);

}


