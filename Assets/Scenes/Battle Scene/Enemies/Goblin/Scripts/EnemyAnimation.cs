using System.Collections;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    //grab battle System Script 
    BattleSystemScript BSS;
    //grab battle instantiation script 
    [SerializeField] GameObject enemy_BS;
    public GameObject[] enemy;
    void Start()
    {
        BSS = GameObject.Find("Battle Canvas").GetComponent<BattleSystemScript>();
        enemy_BS = GameObject.Find("Enemies");
            enemy[0] = enemy_BS.transform.GetChild(0).gameObject;
            enemy[1] = enemy_BS.transform.GetChild(1).gameObject;
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

    }

    IEnumerator PlayAttackAnim()
    {
        //play first enemy attack animation
        //if enemy is not dead, play attack animation
        if (BSS.enemyHP[0].value != 0)
        {
            enemy[0].GetComponent<Animator>().Play("Attack");
        }
        //wait for 2 seconds before second enemy anim 
        yield return new WaitForSeconds(2f);
        //play second enemy attack animation
        if (BSS.enemyHP[1].value != 0)
        {
            enemy[1].GetComponent<Animator>().Play("Attack");
        }
    }
}
