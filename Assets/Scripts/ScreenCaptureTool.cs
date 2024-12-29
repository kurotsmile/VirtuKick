
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ScreenCaptureTool : MonoBehaviour
{
    [Header("Obj Main")]
    public App app;
    public RawImage displayImage;
    public string filePath = @"D:\a\a.png";

    //Rect Mini Map
    public int cropX = 728;
    public int cropY = 70;
    public int cropW = 150;
    public int cropH = 230;

    private bool is_run=false;
    private float timer_cap=0f;

    public void CaptureScreen(string savePath)
    {
        this.app.RunADBCommand("adb shell screencap -p /sdcard/screen.png");
        Debug.Log(this.app.RunADBCommand($"adb pull /sdcard/screen.png {savePath}"));
    }

    private void Update() {
        if(this.is_run){
            this.timer_cap+=1f*Time.deltaTime;
            if(this.timer_cap>0.5f){
                this.CaptureScreen(filePath);
                this.LoadAndDisplayImage(filePath);
                this.timer_cap=0;
            }
        }
    }

    public void On_play(){
        this.CaptureScreen(filePath);
        this.LoadAndDisplayImage(filePath);
        this.is_run=true;
    }

    void LoadAndDisplayImage(string path)
    {
        Texture2D originalTexture = LoadTexture(path);
        if (originalTexture != null)
        {
            Texture2D croppedTexture = CropTexture(originalTexture, cropX, cropY, cropW, cropH);
            if (displayImage != null)
            {
                displayImage.texture = croppedTexture;
                displayImage.SetNativeSize();
            }
        }
    }
    Texture2D LoadTexture(string path)
    {
        if (File.Exists(path))
        {
            byte[] fileData = File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(2, 2);
            if (texture.LoadImage(fileData)) return texture;
        }
        else
        {
            Debug.LogError("File không tồn tại: " + path);
        }
        return null;
    }

    Texture2D CropTexture(Texture2D original, int startX, int startY, int width, int height)
    {
        if (startX + width > original.width || startY + height > original.height)
        {
            Debug.LogError("Kích thước crop vượt quá giới hạn của ảnh gốc!");
            return null;
        }

        Color[] pixels = original.GetPixels(startX, startY, width, height);
        Texture2D croppedTexture = new Texture2D(width, height);
        croppedTexture.SetPixels(pixels);
        croppedTexture.Apply();

        return croppedTexture;
    }
}
