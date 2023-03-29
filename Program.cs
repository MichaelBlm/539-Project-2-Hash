using System;
using System.Security.Cryptography;
using System.Collections.Generic;
namespace HashProject;
class Program
{

public static string CreateMD5(string input, string salt)
{
    // Use input string to calculate MD5 hash
    using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
    {
        byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
        byte saltByte = Convert.ToByte(salt,16);
        byte[] saltedInput = new byte[inputBytes.Length + 1];
        for(int i = 0; i < inputBytes.Length; i++)
        {
          saltedInput[i] = inputBytes[i];
        }
        saltedInput[saltedInput.Length-1] = saltByte;
        byte[] hashBytes = md5.ComputeHash(saltedInput);
        Console.WriteLine(BitConverter.ToString(saltedInput).Replace("-"," "));

        // return Convert.ToHexString(hashBytes); // .NET 5 +
        return BitConverter.ToString(hashBytes).Replace("-"," ");
    }
}
    static void Main(string[] args)
    { 
        var text = args[0];
        var salt = args[1];
        var output = CreateMD5(text, salt);
        Console.WriteLine(output);
    }
}