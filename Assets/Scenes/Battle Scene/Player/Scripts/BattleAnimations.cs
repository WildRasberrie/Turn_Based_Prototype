using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAnimations : MonoBehaviour
{
    BattleSystemScript BSS;
    public Animator anim;
    EnemyAnimation enemyAnimation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //grab the Battle System Script component from the Battle Canvas GameObject
        BSS = GameObject.Find("Battle Canvas").GetComponent<BattleSystemScript>();
        enemyAnimation = GameObject.Find("Enemies").GetComponentInChildren<EnemyAnimation>();
        anim= GameObject.Find("Player(Clone)").GetComponent<Animator>();
    }
    

    // Update is called once per frame
    void Update()
    {
        //if basic attack is requested in hte Battle System Script , play the basic attack animation
        if (BSS.basic_attack_requested)
            {
                //play basic attack animation
                anim.Play("Basic Attack");
            //play enemy hurt anim 
            StartCoroutine(PlayEnemyHurtAnim());
                //reset basic attack request
                BSS.basic_attack_requested = false;
        }

        if (BSS.magic_attack_requested) { 
            anim.Play("Magic Attack");
            //play enemy hurt anim 
            StartCoroutine(PlayEnemyHurtAnim());
            //reset magic attack 
            BSS.magic_attack_requested = false;
        }

        if (enemyAnimation.player_hurt) { 
            StartCoroutine(HurtAnim());
            enemyAnimation.player_hurt = false;
        }

        if (BSS.player_dead) {
            StartCoroutine(DeathAnim());
        }

    }

    IEnumerator PlayEnemyHurtAnim() {
        yield return new WaitForSeconds(0.5f);
        //grab enemy anim 
        StartCoroutine(enemyAnimation.PlayHurtAnim());
    }
    public IEnumerator HurtAnim() {
        yield return new WaitForSeconds(0.5f);
        anim = GameObject.Find("Player(Clone)").GetComponent<Animator>();
        anim.Play("Hurt");
        yield return new WaitForSeconds(2f);
    }

    IEnumerator DeathAnim() {
        yield return new WaitForSeconds(1f);
        anim = GameObject.Find("Player(Clone)").GetComponent<Animator>();
        anim.Play("Dead");
        yield return new WaitForSeconds(2f);

    }
}
