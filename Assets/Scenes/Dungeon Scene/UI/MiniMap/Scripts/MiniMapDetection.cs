using UnityEngine;

public class MiniMapDetection : MonoBehaviour
{
    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (DistanceDetection(player.transform.position, 10f))
        {
            //change object's layer to minimap layer
            gameObject.layer = LayerMask.NameToLayer("MiniMap");
        }

    }

    bool DistanceDetection(Vector3 target, float distance) {
            return Vector3.Distance(transform.position, target) < distance;
    }
}
