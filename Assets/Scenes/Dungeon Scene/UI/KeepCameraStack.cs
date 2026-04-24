using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
public class KeepCameraStack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Camera baseCamera;
    public Camera overlayCamera;
    Scene scene;
    void Awake()
    {
        baseCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        if (scene.name != "BattleScene") {
            overlayCamera = GameObject.Find("MiniMap").GetComponent<Camera>();

            var baseData = baseCamera.GetUniversalAdditionalCameraData();

            //if overlay cameras are null, assign minimap camera as an overlay
            if (!baseData.cameraStack.Contains(overlayCamera))
            {
                baseData.cameraStack.Add(overlayCamera);

            }
        }

    }

    void Update()
    {
        scene = SceneManager.GetActiveScene();

        print(scene.name);
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

