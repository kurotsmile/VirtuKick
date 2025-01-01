using UnityEngine;

public class PlayerAI : MonoBehaviour
{
    public App app;

    public void ControlPlayer(Rect playerPosition, Rect ballPosition)
    {
        if (playerPosition.x < ballPosition.x)
        {
            MoveRight();
        }
        else if (playerPosition.x > ballPosition.x)
        {
            MoveLeft();
        }

        if (Vector2.Distance(playerPosition.center, ballPosition.center) < 50)
        {
            KickBall();
        }
    }

    private void MoveRight()
    {
        Debug.Log("Moving right");
        this.app.RunADBCommand("adb shell input keyevent 32");
    }

    private void MoveLeft()
    {
        Debug.Log("Moving left");
        this.app.RunADBCommand("adb shell input keyevent 29");
    }

    private void MoveUp()
    {
        Debug.Log("Moving Up");
        this.app.RunADBCommand("adb shell input keyevent 51");
    }

    private void MoveDown()
    {
        Debug.Log("Moving Down");
        this.app.RunADBCommand("adb shell input keyevent 47");
    }

    private void KickBall()
    {
        Debug.Log("Kicking the ball");
        this.app.RunADBCommand("shell sendevent /dev/input/event0 1 45 1");
        System.Threading.Thread.Sleep((int)(1 * 1000));
        this.app.RunADBCommand("shell sendevent /dev/input/event0 1 45 0");
    }

    public void LowKick(){
        this.app.RunADBCommand("adb shell input keyevent 45");
    }

    public void HardKick(){
        this.app.RunADBCommand("adb shell input keyevent 62");// Keycode for Space
    }

    public void SwitchPlayer(){
        this.app.RunADBCommand("adb shell input keyevent 33");
    }
}
