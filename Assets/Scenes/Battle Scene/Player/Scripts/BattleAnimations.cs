using UnityEngine;

public class BattleAnimations : MonoBehaviour
{
    BattleSystemScript BSS;
    Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //grab the Battle System Script component from the Battle Canvas GameObject
        BSS = GameObject.Find("Battle Canvas").GetComponent<BattleSystemScript>();
        anim= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        print ("Basic Attack Requested: " + BSS.basic_attack_requested);
        //if basic attack is requested in hte Battle System Script , play the basic attack animation
        if (BSS.basic_attack_requested)
            {
                //play basic attack animation
                anim.Play("Basic Attack");
                //reset basic attack request
                BSS.basic_attack_requested = false;
        }

        if (BSS.magic_attack_requested) { 
            anim.Play("Magic Attack");
            //reset magic attack 
            BSS.magic_attack_requested = false;
        }

    }
}
