using UnityEngine;

public class DistanceDetection : MonoBehaviour
{
    public Transform target;
    public Transform origin;
    public float distance;
    public bool Distance(Transform target,Transform origin,float distance) => Vector3.Distance(target.position, origin.position) <= distance;
    
}
