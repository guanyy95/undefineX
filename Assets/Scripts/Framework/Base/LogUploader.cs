using System.Collections;
using System.IO;
using UnityEngine;

namespace Framework.Base
{
    public class LogUploader : MonoBehaviour
    {
        public static void StartUploadLog()
        {
        }

        private IEnumerator UploadLog(string logFilePath, string url, string desc)
        {
            var fileName = Path.GetFileName(logFilePath);
            return null;
        }

        private byte[] ReadLogFile(string logFilePath)
        {
            byte[] data = null;
            using (FileStream fs = File.OpenRead(logFilePath))
            {
                int index = 0;
                long len = fs.Length;
                data = new byte[len];

                int offset = data.Length > 1024 ? 1024 : data.Length;
                while (index < len)
                {
                    int readByteCount = fs.Read(data, index, offset);
                    index += readByteCount;
                    long leftByteCount = len - index;
                    offset = leftByteCount > offset ? offset : (int)leftByteCount;
                }

                return data;
            }
        }
    }
}