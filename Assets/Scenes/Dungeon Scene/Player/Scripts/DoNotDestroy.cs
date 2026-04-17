using Unity.VisualScripting;
using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    void Awake()
    {
        //set item to not destroy on load
        
        DontDestroyOnLoad(this);
    }

}
