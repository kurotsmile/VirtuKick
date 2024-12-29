
using UnityEngine;

public class ScreenCaptureTool : MonoBehaviour
{
    [Header("Obj Main")]
    public App app;

    public void CaptureScreen(string savePath)
    {
        this.app.RunADBCommand("shell screencap -p /sdcard/screen.png");
        Debug.Log(this.app.RunADBCommand($"pull /sdcard/screen.png {savePath}"));
    }

    public void Test(){
        this.CaptureScreen("D:\\");
    }
}
