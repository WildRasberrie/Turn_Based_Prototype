using UnityEngine;
using UnityEngine.UI;


public class DPadMovement : MonoBehaviour
{
    public Button[] move_buttons;
    public bool upPressed, downPressed, rightPressed, leftPressed;

    public void UpPressed()=> upPressed=true;
    public void DownPressed()=> downPressed = true;
    public void RightPressed()=> rightPressed = true;
    public void LeftPressed()=> leftPressed = true;

//on release set bools to false 
    public void OnUpRealease() => upPressed=false;
    public void OnDownRelease()=> downPressed=false;
    public void OnLeftRelease()=> leftPressed=false;
    public void OnRightRelease()=> rightPressed=false;
}
    