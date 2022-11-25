/**
Project: BEMS IoT Enabled System V 1.0.0
File Name: Account.cs
Development Environment: Microsoft Visual Studio v16.8.4 Community Version
Code contributors: Mohammed Ahmed Subani
ASU ID: 1218278997

File Description:
This is the model for accounts. This contains the information that user might have.
This implementation can be used by the AccountController.

Old Functions:None
New Functions:None
Modified Functions:None
*/

using EncryptDecrypt;

namespace WebApplication1.Models
{
    public class Account
    {
        /*Data fields for Account*/
        public string userName { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        public string zipCode { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}
