using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class MD5Helper
{
    /// <summary>
    /// 对指定路径的文件加密，返回加密后的文本
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static string MD5EntryptFile(string filePath)
    {
        byte[] retVal;
        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            retVal = md5.ComputeHash(fs);
        }

        return retVal.ToString();
    }

    public static string MD5EncryptString(string originStr)
    {
        byte[] retVal;
        MD5 md5 = new MD5CryptoServiceProvider();
        retVal = md5.ComputeHash(Encoding.UTF8.GetBytes(originStr));
        return retVal.ToString();
    }
}
