using Unity.VisualScripting;
using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    void Awake()
    {
        //if theres a duplicate, remove until theres only one 

        //set item to not destroy on load
        if (this != null)
        {
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

}
