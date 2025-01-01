using UnityEngine;

public class App : MonoBehaviour
{
    [Header("Config")]
    public string adbPath = @"C:\adb\adb.exe";

    void Start()
    {
        
    }

    public void Btn_connect(){
       // this.RunADBCommand("adb connect emulator-5554");
        Debug.Log(this.RunADBCommand("adb shell monkey -p com.firsttouchgames.dls7 -c android.intent.category.LAUNCHER 1"));
        Debug.Log("Connect Adb");
    }

    public void Btn_exit(){
        Application.Quit();
    }

    public string RunADBCommand(string command)
    {
        System.Diagnostics.Process process = new();
        process.StartInfo.FileName = "powershell.exe";
        process.StartInfo.Arguments = command;

        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.CreateNoWindow = true;
        process.Start();

        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        return string.IsNullOrEmpty(error) ? output : $"{error}";
    }

}
