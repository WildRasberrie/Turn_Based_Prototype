using UnityEngine;

public class CutScenePan : MonoBehaviour
{
    public Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();

        //Play the pan animation
       anim.Play("Fast Pan");

    }
}