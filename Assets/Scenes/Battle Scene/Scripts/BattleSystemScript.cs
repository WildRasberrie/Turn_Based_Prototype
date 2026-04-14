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
    public Button basic_attack_button;
    public Button magic_attack_button;
    public Button heal_button;

    [Space]
    [Header("Player Stats")]
    public Slider playerHP;
    public Slider playerMP;
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
    public bool picked_enemy_1;
    public Button enemy_1;
    public bool dead;
    [Space]
    [Header ("Second Enemy")]
    public bool picked_enemy_2;
    public bool dead_also;
    public Button enemy_2;

    [Space]
    [Header ("Trackable Booleans")]
    [Space]
    [Space]
    public bool requested_action;
    public bool picked_target;

    [Space]
    public bool basic_attack_requested;
    [Space]
    public bool magic_attack_requested;
    [Space]
    public bool enemy_attack_requested;


    bool magic_button_pressed, basic_button_pressed;
    //pull enemy stats from enemy scriptable object
    EnemyStatsScript enemyStatsScript;

    [SerializeField] SceneAsset dungeon_scene;
    [SerializeField] BattleAnimations Battle_Anims;

    void Start()
    {
        enemyStatsScript = gameObject.GetComponent<EnemyStatsScript>();
        if (GameObject.Find("Player") != null) Battle_Anims = GameObject.Find("Player").GetComponent<BattleAnimations>();
        StartCoroutine(StartBattle());
    }

    //set up player and enemy stats
    void SetUpStats() {
        //set up player stats
        player_name.text = "Hero";
        player_lvl.text = "Lvl. 1";

        //grab hp from scene loader 
        playerHP.value = 100;

        //grab mp from scene loader 
        playerMP.value = 50;

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

        //dont allow mana to go below zero 
        if (playerMP.value <= 0)
        {
            playerMP.value = 0;
            nar_text.text = "No Mana left, Go to your inventory to use a potion!";
        }

        picked_target = picked_enemy_2 || picked_enemy_1;
        requested_action = basic_button_pressed || magic_button_pressed;


        PlayerAttackAnims();

        if (requested_action) nar_text.text = "Choose an enemy";

        TrackAttackSelection();

    }

    bool debugSkip => Input.GetKeyDown(KeyCode.Space);
    //set up battle system
    //set up battle intro
    IEnumerator StartBattle() {
        //set up stats
        SetUpStats();
        //set up battle text
        nar_text.text = "You were spotted by enemies!";
        //wait for 2 seconds
        yield return new WaitForSeconds(2f);
        StartCoroutine(ChooseAction());
    }
    //choose enemy to attack 
    public void PickEnemy1()
    {
        if (requested_action)
        {
            picked_enemy_1 = true;
            picked_enemy_2 = false;
            enemy_stat_background[0].color = Color.yellow;
            enemy_stat_background[1].color = Color.blue;
        }
    }
    public void PickEnemy2()
    {
        if (requested_action)
        {
            picked_enemy_1 = false;
            picked_enemy_2 = true;
            enemy_stat_background[1].color = Color.yellow;
            enemy_stat_background[0].color = Color.blue;
        }
    }

    // set to enemy to false after player attack
    void ResetEnemyPick() {
        picked_enemy_1 = false;
        picked_enemy_2 = false;
        enemy_stat_background[0].color = Color.blue;
        enemy_stat_background[1].color = Color.blue;

        magic_button_pressed = false;
        basic_button_pressed = false;

        var magic_button_color = magic_attack_button.GetComponent<Image>().color;
        var basic_button_color = basic_attack_button.GetComponent<Image>().color;

        magic_button_color = Color.white;
        basic_button_color = Color.white;
    }
    //Bool to see if anything was selected

    IEnumerator ChooseAction() {

        nar_text.text = "Choose an action: ";

        yield return new WaitForSeconds(2f);
    }

    //have player start co-routine for battle
    //check to see which pop up button is pressed
    public void BasicAttackAction()
    {
        //change action button color 
        basic_button_pressed = true;
        
    }

    public void MagicAttackAction()
    {

        //change action button color 
        magic_button_pressed = true;

    }

    public void TrackAttackSelection()
    {
        var button_background = basic_attack_button.GetComponent<Image>();

        if (basic_button_pressed)
        {
            button_background.color = Color.yellow;
        }
        else
        {
            button_background.color = Color.white;
        }

        var button_background_magic = magic_attack_button.GetComponent<Image>();

        if (magic_button_pressed)
        {
            button_background_magic.color = Color.yellow;

        }
        else
        {
            button_background_magic.color = Color.white;
        }

    }

    void PlayerAttackAnims() {

        //flip attack bool to start animation if enemy is picked 
        if (picked_target && basic_button_pressed)
        {
            basic_button_pressed = false;
            requested_action = false;

            StartCoroutine(BasicAttack());

            basic_attack_requested = true;
        }


        if (picked_target && magic_button_pressed)
        {
            magic_button_pressed = false;
            requested_action = false;
            //start magic attack co-routine
            StartCoroutine(MagicAttack());
            //flip magic bool tosstart animation if enemy is picked
            magic_attack_requested = true;
        }
    }

    public void HealAction(){
        if (playerHP.value < playerHP.maxValue) StartCoroutine(Heal());
    }

    //if attack button is pressed, pop up attack options
    public void AttackOptions()
    {
        //track if button is pressed already
        bool isPressed = pop_up.activeSelf;

        if (!picked_target)pop_up.SetActive(true);
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
        int MP_Depletion = 10;

        //take away mana 
        playerMP.value -= MP_Depletion;
        
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
            //make enemy unselectable
            enemy_1.interactable = false;
            //play enemy death anim 
            dead = true;

            yield break;
        }
        if (enemyHP[1].value <= 0)
        {
            nar_text.text = "You defeated the " + enemy_name[1].text + "!";
            //make enemy unselectable if dead
            enemy_1.interactable = false;
            dead = true;
            yield break;
        }

    }

    //heal player co-routine
    IEnumerator Heal()
    {
        
        //heal player
        int healAmount = 50;
        //lose MP 
        int MP_Depletion = 10;

        playerHP.value += healAmount;
        playerMP.value -= MP_Depletion;
        nar_text.text = "You healed " + healAmount + " HP!";
        yield return new WaitForSeconds(1f);
        nar_text.text = "You lost " + MP_Depletion + "MP!";
        yield return new WaitForSeconds(2f);

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
        
        yield return new WaitForSeconds(2f);
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
