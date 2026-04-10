using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Build;
using System.Collections;
public class BattleSystemScript : MonoBehaviour
{
    //Grab Starter Battle Text 
    [Header("Battle Text")]
    [SerializeField] TextMeshProUGUI nar_text;

    [Header ("Command Box")]
    [Header ("Attack Options")]
    [SerializeField] GameObject pop_up;

    [Space]
    [Header("Player Stats")]
    [SerializeField] Slider playerHP;
    [SerializeField] TextMeshProUGUI player_name;
    [SerializeField] TextMeshProUGUI player_lvl;

    [Space]
    [Header("Enemy Stats")]
    [SerializeField] Slider enemyHP;
    [SerializeField] TextMeshProUGUI enemy_name;
    [SerializeField] TextMeshProUGUI enemy_lvl;
    void Start()
    {
        StartCoroutine(StartBattle());
    }
    //set up battle system

    //set up battle intro
    IEnumerator StartBattle() { 
        //set up battle text
        nar_text.text = "An angry " + enemy_name.text + " appeared!";
        //wait for 2 seconds
        yield return new WaitForSeconds(2f);
        StartCoroutine(ChooseAction());

    }
    IEnumerator ChooseAction() {
        nar_text.text = "Choose an Action:";
        yield return new WaitForSeconds(2f);
    }
    //have player start co-routine for battle
    //check to see which pop up button is pressed
    public void BasicAttackAction()
    {

        //start basic attack co-routine
        StartCoroutine(BasicAttack());
    }
    public void MagicAttackAction() { 
        //start magic attack co-routine
        StartCoroutine(MagicAttack());

    }
    public void HealAction(){
        StartCoroutine(Heal());

    }
    //if attack button is pressed, pop up attack options
    public void AttackOptions()
    {
        //track if button is pressed already
        bool isPressed = pop_up.activeSelf;
        
        pop_up.SetActive(true);
        if (isPressed) pop_up.SetActive(false);
    }

    
    //Player attack functions
    //basic attack co-routine
    public IEnumerator BasicAttack()
    {
        //deal damage to enemy
        int damage = 10;
        enemyHP.value -= damage;
        nar_text.text = "You dealt " + damage + " damage!";
        yield return new WaitForSeconds(2f);
        //check if enemy is dead
        if (enemyHP.value <= 0)
        {
            nar_text.text = "You defeated the " + enemy_name.text + "!";
            yield break;
        }

        //enemy's turn
        StartCoroutine(EnemyTurn());
    }
    //magic attack co-routine
    public IEnumerator MagicAttack()
    {
        //deal damage to enemy
        int damage = 15;
        enemyHP.value -= damage;
        nar_text.text = "You dealt " + damage + " damage!";
        yield return new WaitForSeconds(2f);
        //check if enemy is dead
        if (enemyHP.value <= 0)
        {
            nar_text.text = "You defeated the " + enemy_name.text + "!";
            yield break;
        }

        //enemy's turn
        StartCoroutine(EnemyTurn());
    }

    //heal co-routine
    IEnumerator Heal()
    {
        //heal player
        int healAmount = 10;
        playerHP.value += healAmount;
        nar_text.text = "You healed for " + healAmount + " HP!";
        yield return new WaitForSeconds(2f);
    }
    //enemy's turn co-routine
    IEnumerator EnemyTurn()
    {
        //enemy attacks player
        int damage = 5;
        playerHP.value -= damage;
        nar_text.text = "The " + enemy_name.text + " dealt " + damage + " damage!";
        yield return new WaitForSeconds(2f);
        //check if player is dead
        if (playerHP.value <= 0)
        {
            nar_text.text = "You were defeated by the " + enemy_name.text + "!";
            yield break;
        }
        //player's turn
        StartCoroutine(ChooseAction());
    }

}
