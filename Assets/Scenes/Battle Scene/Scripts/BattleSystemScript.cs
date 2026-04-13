using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEditor;
using UnityEngine.SceneManagement;
public class BattleSystemScript : MonoBehaviour
{
    //Grab Starter Battle Text 
    [Header("Battle Text")]
    [SerializeField] TextMeshProUGUI nar_text;

    [Header("Command Box")]
    [Header("Attack Options")]
    [SerializeField] GameObject pop_up;

    [Space]
    [Header("Player Stats")]
    public Slider playerHP;
    [SerializeField] TextMeshProUGUI player_name;
    [SerializeField] TextMeshProUGUI player_lvl;

    [Space]
    [Header("Enemy Stats")]
    [SerializeField] int[] enemy_damage;
    public Slider[] enemyHP;
    [SerializeField] TextMeshProUGUI[] enemy_name;
    [SerializeField] TextMeshProUGUI[] enemy_lvl;
    [SerializeField] Image[] enemy_stat_background;
    [Space]
    [Header ("First Enemy")]
    [SerializeField] bool picked_enemy_1;
    [Space]
    [Header ("Second Enemy")]
    [SerializeField] bool picked_enemy_2;
    [Space]
    public bool basic_attack_requested;
    public bool magic_attack_requested;
    public bool enemy_attack_requested;
    //pull enemy stats from enemy scriptable object
    EnemyStatsScript enemyStatsScript;

    [SerializeField] SceneAsset dungeon_scene;

    void Start()
    {
        enemyStatsScript = gameObject.GetComponent<EnemyStatsScript>();
        StartCoroutine(StartBattle());
    }

    //set up player and enemy stats
    void SetUpStats() {
        //set up player stats
        player_name.text = "Hero";
        player_lvl.text = "Lvl. 1";
        playerHP.value = 100;

        //set up enemy stats
        //grab enemy stats from enemy scriptable object
        for (int i = 0; i < enemyStatsScript.enemy_name.Length; i++)
        {
            //set enemy name text to enemy name stat
            enemy_name[i].text = enemyStatsScript.enemy_name[i];
            //set enemy level text to enemy level stat
            enemy_lvl[i].text = "Lvl. " + enemyStatsScript.enemy_lvl[i];
            //set enemy HP slider max value and current value to enemy HP stat
            enemyHP[i].maxValue = enemyStatsScript.enemy_hp[i];
            enemyHP[i].value = enemyStatsScript.enemy_hp[i];
            //set enemy damage to enemy attack stat
            enemy_damage[i] = enemyStatsScript.enemy_atk[i];
        }
    }
    //set debug to get to dungeon scene 
    void Update() {
        //if both enemies are dead, go back to dungeon scene 
        if (enemyHP[0].value <= 0 && enemyHP[1].value <= 0 || debugSkip) 
        {
            SceneManager.LoadScene(dungeon_scene.name);
        }

    }

    bool debugSkip => Input.GetKeyDown(KeyCode.Space);
    //set up battle system
    //set up battle intro
    IEnumerator StartBattle() {
        //set up stats
        SetUpStats();
        //set up battle text
        nar_text.text = "An angry " + enemy_name[0].text + " appeared!";
        //wait for 2 seconds
        yield return new WaitForSeconds(2f);
        StartCoroutine(ChooseAction());

    }
    //choose enemy to attack 
    public void PickEnemy1()
    {
        picked_enemy_1 = true;
        picked_enemy_2 = false;
        enemy_stat_background[0].color = Color.yellow;
        enemy_stat_background[1].color = Color.blue;
        nar_text.text = "Choose an Action:";
    }
    public void PickEnemy2()
    {
        picked_enemy_1 = false;
        picked_enemy_2 = true;
        enemy_stat_background[1].color = Color.yellow;
        enemy_stat_background[0].color = Color.blue;    
        nar_text.text = "Choose an Action:";

    }

    // set to enemy to false after player attack
    void ResetEnemyPick() {
        picked_enemy_1 = false;
        picked_enemy_2 = false;
        enemy_stat_background[0].color = Color.blue;
        enemy_stat_background[1].color = Color.blue;
    }

    IEnumerator ChooseAction() {
        if (!picked_enemy_1 && !picked_enemy_2)
        {
            nar_text.text = "Choose an Enemy!";

        }
        yield return new WaitForSeconds(2f);
    }


    //have player start co-routine for battle
    //check to see which pop up button is pressed
    public void BasicAttackAction()
    {

        //start basic attack co-routine
        StartCoroutine(BasicAttack());
        //flip attack bool 
        basic_attack_requested = true;
    }
    public void MagicAttackAction() {
        //start magic attack co-routine
        StartCoroutine(MagicAttack());
        //flip magic bool 
        magic_attack_requested = true;

    }
    public void HealAction(){
        StartCoroutine(Heal());

    }
    //if attack button is pressed, pop up attack options
    public void AttackOptions()
    {
        //track if button is pressed already
        bool isPressed = pop_up.activeSelf;

        if (picked_enemy_2 || picked_enemy_1) pop_up.SetActive(true);
        if (isPressed) pop_up.SetActive(false);
    }
    
    //basic attack co-routine
    public IEnumerator BasicAttack()
    {
       
        //deal damage to enemy
        int damage = 25;

        //have player choose enemy to attack 
        if (picked_enemy_1)
        {
            yield return new WaitForSeconds(1f);
            nar_text.text = "You dealt " + damage + " damage!";
            yield return new WaitForSeconds(1f);
            enemyHP[0].value -= damage;
        }
        else if (picked_enemy_2)
        {
            yield return new WaitForSeconds(1f);
            nar_text.text = "You dealt " + damage + " damage!";
            yield return new WaitForSeconds(1f);
            enemyHP[1].value -= damage;
            //enemy's turn

        }
        //reset enemy pick
        ResetEnemyPick();
        //hide attack options
        pop_up.SetActive(false);

        yield return new WaitForSeconds(2f);

        //enemy's turn
        StartCoroutine(EnemyTurn());
        //check if enemy is dead
        if (enemyHP[0].value <= 0)
        {
            nar_text.text = "You defeated the " + enemy_name[0].text + "!";
            yield break;
        }
        if (enemyHP[1].value <= 0)
        {
            nar_text.text = "You defeated the " + enemy_name[1].text + "!";
            yield break;
        }
    }
    //magic attack co-routine
    public IEnumerator MagicAttack()
    {
        //deal damage to enemy

        int damage = 40;

        if (picked_enemy_1)
        {
            nar_text.text = "You dealt " + damage + " damage!";
            yield return new WaitForSeconds(1f);
            enemyHP[0].value -= damage;
        }
        else if (picked_enemy_2)
        {
            nar_text.text = "You dealt " + damage + " damage!";
            yield return new WaitForSeconds(1f);
            enemyHP[1].value -= damage;
        }
        //reset enemy pick
        ResetEnemyPick();
        //hide attack options
        pop_up.SetActive(false);
        yield return new WaitForSeconds(2f);

        //enemy's turn
        StartCoroutine(EnemyTurn());

        //check if enemy is dead
        if (enemyHP[0].value <= 0)
        {
            nar_text.text = "You defeated the " + enemy_name[0].text + "!";
            yield break;
        }
        if (enemyHP[1].value <= 0)
        {
            nar_text.text = "You defeated the " + enemy_name[1].text + "!";
            yield break;
        }

    }

    //heal player co-routine
    IEnumerator Heal()
    {
        
        //heal player
        int healAmount = 50;
        playerHP.value += healAmount;
        nar_text.text = "You healed " + healAmount + " HP!";
        yield return new WaitForSeconds(1f);
        nar_text.text = "You forfieted your attack turn!";
        ResetEnemyPick();
        yield return new WaitForSeconds(2f);
        //enemy's turn
        StartCoroutine(EnemyTurn());
    }
    //enemy's turn co-routine
    IEnumerator EnemyTurn()
    {
        //set up enemy anim request
        enemy_attack_requested = true;
        //enemy attacks player
        playerHP.value -= enemy_damage[0];
        nar_text.text = "The " + enemy_name[0].text + " dealt " + enemy_damage[0] + " damage!";
        yield return new WaitForSeconds(2f);

        //enemy 2 attacks player
        playerHP.value -= enemy_damage[1];
        nar_text.text = "The " + enemy_name[1].text + " dealt " + enemy_damage[1] + " damage!";
        yield return new WaitForSeconds(2f);

        //check if player is dead
        if (playerHP.value <= 0)
        {
            nar_text.text = "You were defeated!";
            yield break;
        }

        StartCoroutine(ChooseAction());
    }

}
