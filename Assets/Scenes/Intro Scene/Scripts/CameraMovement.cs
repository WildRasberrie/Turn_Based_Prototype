using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float speed;
    // Update is called once per frame
    void Update()
    {
        //get mouse position delta 
        Vector3 mouseInput = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);

        print (mouseInput);
        //move camera based on mouse position delta 
        if (mouseInput != Vector3.zero) 
        {
           
            GetComponent<RectTransform>().anchoredPosition += (Vector2)(mouseInput * (speed * Time.deltaTime));
          
        }

    }
}
