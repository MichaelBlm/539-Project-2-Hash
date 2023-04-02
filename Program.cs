using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace HashProject;
class Program
{   
//   public static string GetInput(string[] args)
//   {
//     string input = "";

//     if( args.Length == 1)
//     {
//         input = args[0];
//     }
//     else
//     {
//         Console.WriteLine("Not enough inputs provided.");
//     }
//     return input;
//   }
 
  //https://stackoverflow.com/a/24031467
  public static string CreateMD5(string input, string salt, int bytesLength)
  {
     using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] saltBytes = new byte[] {Convert.ToByte(salt,16)};
                byte[] saltedInputBytes = new byte[inputBytes.Length + saltBytes.Length];
                System.Buffer.BlockCopy(inputBytes, 0, saltedInputBytes, 0, inputBytes.Length);
                System.Buffer.BlockCopy(saltBytes, 0, saltedInputBytes, inputBytes.Length, saltBytes.Length);                                
                byte[] hashBytes = md5.ComputeHash(saltedInputBytes);
                byte[] reducedBytes = hashBytes.Take(bytesLength).ToArray();
                return BitConverter.ToString(reducedBytes);
            }
  }
  // https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings
  public static string StringGenerator(string chars, int length)
  {
    StringBuilder res = new StringBuilder();
    Random rnd = new Random();
    while ( 0 < length--){
        res.Append(chars[rnd.Next(chars.Length)]);
    }
    return res.ToString();
  }
  public static bool Compare(string h1, string h2, int bytes)
  {
    byte[] h1Bytes = Encoding.UTF8.GetBytes(h1);
    byte[] h2Bytes = Encoding.UTF8.GetBytes(h2);
    for(int i = 0; i < bytes; i++)
    {
        if(h1Bytes[i] != h2Bytes[i])
        {
            return false;
        }
    }
    return true;
  }
  public static string BirthdayAttack(string[] args)
  {
    Dictionary<string, string> hashTable = new Dictionary<string, string>();
    while(true)
    {
        string salt = args[0];
        const string validChars = "abcdefghijqlmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        int length = 10;
        int bytes = 5;
        string s1 = StringGenerator(validChars,length);
        string hash = CreateMD5(s1, salt, bytes);
        if(!hashTable.ContainsKey(hash))
        {
            hashTable.Add(hash, s1);
        }
        else{
            if(s1 != hashTable[hash])
            {
                Console.WriteLine(s1 +","+hashTable[hash]);
                return s1 + "," + hashTable[hash];
            }
        }
    }
  }
  public static void Main(String[] args){
    BirthdayAttack(args);
  }

}