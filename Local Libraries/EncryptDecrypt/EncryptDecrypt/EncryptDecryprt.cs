/**
Project: BEMS IoT Enabled System V 1.0.0
File Name: EncryptDecryprt.cs
Development Environment: Microsoft Visual Studio v16.8.4 Community Version
Code contributors: Mohammed Al Jammal


File Description:
 
The following code implements the Encrypt/Decrypt functionality for the BEMS account 
management functionality, this code was implemented for CSE598 : Distributed Software Development
course with no modification to its functionality.

This code is build as an assembly and is added to the BEMS central server solution.

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptDecrypt
{
    public class EncryptDecryprt
    {
        public static string EncryptString(string Text)
        {
            int TextLength; // Used to store the byte array length
            byte[] ArrText; // Represents an array of bytes
            string FinalText; // Represents the final encoded text

            try
            {
                TextLength = Text.Length; // Get the text length
                ArrText = new byte[TextLength]; // Create the encode byte array
                ArrText = System.Text.Encoding.UTF8.GetBytes(Text); // Encode the text and store it in the array
                FinalText = Convert.ToBase64String(ArrText); // Convert the encoded string and store it in a string

                return FinalText;
            }
            catch (Exception) // If an error occurs while encrypting the text throw an exception 
            {
                throw new Exception("An error occurred while ecnrypting the text!");
            }
        }
        public static string DecryptString(string Text)
        {

            UTF8Encoding TextEncode = new UTF8Encoding(); // Create an instance of UTF8Encoding class used to encode the string
            Decoder TextDecode = TextEncode.GetDecoder(); // Create an instance of the Decoder class

            byte[] ArrByte;
            char[] ArrChar;

            int ArrLength; // Used to store the length of the byte array
            int ArrCounter; // Used to store the number of characters produced by decoding the sequence of bytes

            string FinalText; // Represents the final decoded text 

            try
            {
                ArrByte = Convert.FromBase64String(Text); // Encodes the data to the equivalent 8-bit unsigned array
                ArrLength = ArrByte.Length; // Get the length of the bytes array
                ArrCounter = TextDecode.GetCharCount(ArrByte, 0, ArrLength); // Gets the number of characters produced by decoding the sequence of bytes

                ArrChar = new char[ArrCounter]; // Create an instance from char class and pass ArrCounter variable
                TextDecode.GetChars(ArrByte, 0, ArrLength, ArrChar, 0); // Gets the actual number of characters
                FinalText = new String(ArrChar); // Represets the final decoded text, based on value of characters array

                return FinalText;
            }
            catch (Exception) // If an error occurs while decrypting the text throw an exception 
            {
                throw new Exception("An error occurred while decrypting the text!");
            }
        }
    }
}
