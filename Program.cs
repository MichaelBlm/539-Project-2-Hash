using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;
namespace HashProject;
class Program
{

    public static byte[] addByteToArray(byte[] byteArray, byte newByte)
    {
        byte[] newArray = new byte[byteArray.Length + 1];
        byteArray.CopyTo(newArray, 1);
        newArray[0] = newByte;
        return newArray;
    }
    public static byte[] MD5Hash(String text, byte salt)
    {
    // Use input string to calculate MD5 hash
    using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
    {
        byte[] byteArray = Encoding.UTF8.GetBytes(text);
        byteArray = addByteToArray(byteArray, salt);
        byte[] hashBytes = md5.ComputeHash(byteArray);
        return hashBytes;


    }
    }

    static void Main(string[] args)
    { 
        var salt = Convert.ToByte(args[0]);
        var text = "Hello World!";

        Console.WriteLine(MD5Hash(text,salt));
    }
}