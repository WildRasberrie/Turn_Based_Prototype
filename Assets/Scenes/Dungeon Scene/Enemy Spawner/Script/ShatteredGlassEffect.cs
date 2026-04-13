using UnityEngine;

public class ShatteredGlassEffect : MonoBehaviour
{
    Transform[] children;
    Vector2 force = new Vector2(200, 500);
    void Awake() {
        //get children in parent 
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
           children[i] = gameObject.transform.GetChild(i);
          
        }

    }

    void Update()
    {
        //apply force to children 
        for (int i = 0; i < children.Length; i++)
        {
            children[i].GetComponent<Rigidbody2D>().AddForce(force);
        }
    }
}

