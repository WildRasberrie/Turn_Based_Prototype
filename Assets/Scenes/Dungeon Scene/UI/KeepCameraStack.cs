using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
public class KeepCameraStack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Camera baseCamera;
    public Camera[] overlayCamera;
    Scene scene;
    void Awake()
    {
        baseCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        if (scene.name == "DungeonScene") {

            var baseData = baseCamera.GetUniversalAdditionalCameraData();
            for (int i = 0; i < overlayCamera.Length; i++)
            {
                if (overlayCamera[i] != null) baseData.cameraStack.Add(overlayCamera[i]);
                if (overlayCamera[1] ==null) overlayCamera[1] = GameObject.Find("Player Camera").GetComponent<Camera>();
            }


        }

    }

    void Update()
    {
        scene = SceneManager.GetActiveScene();

        //print(scene.name);
        if (scene.name == "BattleScene")
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}

