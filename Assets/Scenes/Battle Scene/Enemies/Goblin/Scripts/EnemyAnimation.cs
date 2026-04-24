using System.Collections;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    //grab battle System Script 
    BattleSystemScript BSS;
    //grab battle instantiation script 
    [SerializeField] GameObject enemy_BS;
    public GameObject[] enemy_GO;
    public Animator[] enemy_anim;
    public bool player_hurt;

    void Awake()
    {
        BSS = GameObject.Find("Battle Canvas").GetComponent<BattleSystemScript>();
        enemy_BS = GameObject.Find("Enemies");
        //get children 
        for (int i = 0; i < transform.childCount; i++)
        {
            enemy_GO[i] = transform.GetChild(i).gameObject;
            enemy_anim[i]= enemy_GO[i].GetComponent<Animator>();
        }
    }
        
    void Update()
    {
        //if enemy attack requested, play attack animation
            if (BSS.enemy_attack_requested) {
            //start coroutine to play attack animation
            StartCoroutine(PlayAttackAnim());
            //reset enemy attack request
            BSS.enemy_attack_requested = false;
        }
        if (BSS.dead1) StartCoroutine(Enemy1_DeathAnim());
        if (BSS.dead2)  StartCoroutine(Enemy2_DeathAnim());
    }

    IEnumerator PlayAttackAnim()
    {
        //play first enemy attack animation
        //if enemy is not dead, play attack animation
        if (BSS.enemyHP[0].value != 0)
        {
            enemy_anim[0].Play("Attack");
            // play player hurt anim
            yield return new WaitForSeconds(0.5f);

            player_hurt = true;
        }
        
        //wait for 2 seconds before second enemy anim 
        yield return new WaitForSeconds(2f);
        //play second enemy attack animation
        if (BSS.enemyHP[1].value != 0)
        {
            yield return new WaitForSeconds(0.5f);
            enemy_anim[1].Play("Attack");
            // play player hurt anim
            yield return new WaitForSeconds(0.5f);

                player_hurt = true;
        }
    }

    public IEnumerator PlayHurtAnim()
    {
        yield return new WaitForSeconds(2f);

        //play first enemy hurt anim 
        if (BSS.picked_enemy_1) enemy_anim[0].Play("Hurt");

        //play second enemy hurt anim
        if (BSS.picked_enemy_2) enemy_anim[1].Play("Hurt");
        yield return new WaitForSeconds(2f);
    }

    public IEnumerator Enemy1_DeathAnim() {
        enemy_anim[0].Play("Dead");
        yield return new WaitForSeconds(1f);

    }

    public IEnumerator Enemy2_DeathAnim() {
        enemy_anim[1].Play("Dead");
        yield return new WaitForSeconds(1f);
    }
}
