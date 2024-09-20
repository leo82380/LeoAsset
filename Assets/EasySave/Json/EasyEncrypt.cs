namespace EasySave
{
    public class EasyEncrypt
    {
        public static string Encrypt(byte[] data)
        {
            return System.Convert.ToBase64String(data);
        }
        
        public static byte[] Decrypt(string data)
        {
            return System.Convert.FromBase64String(data);
        }
    }
}