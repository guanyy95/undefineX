using System.IO;
using System.Text;
using UnityEngine;

public class GameLogger
{
    enum DebugLevel
    {
        Log = 0,
        Warn = 1,
        Error = 2,
    }

    private static string s_logFileSavePath; // 日志持久化地址
    private static StringBuilder s_logStr = new StringBuilder();

    public static void Init()
    {
        var t = System.DateTime.Now.ToString("yyyyMMddhhmmss");
        s_logFileSavePath = string.Format("{0}/output_{1}.log", Application.persistentDataPath, t);
    }

    private static void OnLogCallBack(string condition, string stackTrace, LogType type)
    {
        s_logStr.Append(condition);
        s_logStr.Append("\n");
        s_logStr.Append(stackTrace);
        s_logStr.Append("\n");

        if (s_logStr.Length <= 0) return;
        if (!File.Exists(s_logFileSavePath))
        {
            using (FileStream fs = File.Create(s_logFileSavePath))
            {
                fs.Close();
            }

            using (var sw = File.AppendText(s_logFileSavePath))
            {
                sw.WriteLine(s_logStr.ToString());
            }
            s_logStr.Remove(0, s_logStr.Length);
        }
    }

    public static void UploadLogFile(string desc)
    {
    }
}
