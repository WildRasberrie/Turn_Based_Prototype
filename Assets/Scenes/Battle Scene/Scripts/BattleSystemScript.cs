using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public class BattleSystemScript : MonoBehaviour
{
    //Grab Starter Battle Text 
    [Header("Dialogue Management")]
    [SerializeField] GameObject dialogue_box;
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
    public bool dead1, dead2;

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
    [Space]
    public bool player_dead;
    Color[] enemy_HP_color;


    bool magic_button_pressed, basic_button_pressed;

    [Space]
 
    [SerializeField] SceneLoader SceneLoader;
    //[SerializeField] InventoryManager InventoryManager;

    void Awake() {
        SceneLoader = GameObject.FindWithTag("SceneLoader").GetComponent<SceneLoader>();
        //InventoryManager = GameObject.FindWithTag("InventoryBag").GetComponent<InventoryManager>();

    }
    void Start()
    {
        StartCoroutine(StartBattle());
    }

        void Update()
    {

        //if both enemies are dead, go back to dungeon scene 
        if (enemyHP[0].value <= 0 && enemyHP[1].value <= 0 || debugSkip)
        {
            StartCoroutine(BackToDungeon());
        }

        //dont allow mana to go below zero 
        if (SceneLoader.playerMP <= 0)
        {
            StartCoroutine(ManaWarning());
            
        }

        picked_target = picked_enemy_2 || picked_enemy_1;
        requested_action = basic_button_pressed || magic_button_pressed;


        PlayerAttackAnims();

        if (requested_action) nar_text.text = "Ok, now pick a target ...";

        TrackAttackSelection();
        TrackPlayerStats();
    }

    IEnumerator ManaWarning() {

        yield return new WaitForSeconds(1f);
        SceneLoader.playerMP = 0;
        nar_text.text = "I've got no Mana left, Go to your inventory to use a Mad Mana Potion!";
        yield return new WaitForSeconds(2f);

    }
    //set up player and enemy stats
    void SetUpStats() {
        //set up player stats
        player_name.text = "Jessie";
        player_lvl.text = "Lvl. 1";

        //grab hp from scene loader 
        //grab mp from scene loader 
        playerHP.value = SceneLoader.playerHP;
        playerMP.value = SceneLoader.playerMP;

        //set up enemy stats
        //grab enemy stats from enemy scriptable object
        for (int i = 0; i < GameObject.Find("Enemies").transform.childCount; i++)
        {
            if (GameObject.Find("Enemies").transform.GetChild(i).name == "Skeleton")
            {
                enemy_name[i].text = "Skeleton";
                enemy_lvl[i].text = "Lvl. " + 2;
                enemyHP[i].maxValue = 100;
                enemyHP[i].value = 100;
                enemy_damage[i] = 15;
            }
            else
              if (GameObject.Find("Enemies").transform.GetChild(i).name == "Ghost")
            {
                enemy_name[i].text = "Ghost";
                enemy_lvl[i].text = "Lvl. " + 1;
                enemyHP[i].maxValue = 75;
                enemyHP[i].value = 75;
                enemy_damage[i] = 10;
            }
        }
    }

    void TrackPlayerStats() {
        //grab hp from scene loader 
        //grab mp from scene loader 
        playerHP.value = SceneLoader.playerHP;
        playerMP.value = SceneLoader.playerMP;

        if (both_dead) SceneManager.LoadSceneAsync("Dungeon_lvl1");

        var player_HP_color = playerHP.GetComponentInChildren<Image>().color;

        if (SceneLoader.playerHP == 100)
        {
            player_HP_color = Color.green;
        }
        if (SceneLoader.playerHP < 50)
        {
            player_HP_color = Color.orange;
        }
        else if (SceneLoader.playerHP < 25)
        {
            player_HP_color = Color.red;
        }
    }

 
        //grab enemy health for both 
    public bool both_dead => enemyHP[0].value <= 0 && enemyHP[1].value <= 0;
    

    //set debug to get to dungeon scene 
 
    bool debugSkip => Input.GetKeyDown(KeyCode.Space);
    //set up battle system
    //set up battle intro
    IEnumerator StartBattle() {
        //set up battle text
        nar_text.text = "Time to clean out the place!";
        SetUpStats();

        yield return new WaitForSeconds(1f);

        
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
            StartCoroutine(PlayUI());

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
            StartCoroutine(PlayUI());

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
        dialogue_box.SetActive(true);
        nar_text.text = "I have to choose my next attack wisely... ";
        
        yield return new WaitForSeconds(2f);
    }

    //have player start co-routine for battle
    //check to see which pop up button is pressed
    public void BasicAttackAction()
    {
        StartCoroutine(PlayUI());
        //change action button color 
        basic_button_pressed = true;
        
    }

    public void MagicAttackAction()
    {
        StartCoroutine(PlayUI());


        //change action button color 
        magic_button_pressed = true;

    }

    public void TrackAttackSelection()
    {
        var button_background = basic_attack_button.GetComponent<Image>();

        if (basic_button_pressed)
        {
            //set magic button to not pressed 
            magic_button_pressed = false;
            button_background.color = Color.yellow;
        }
        else
        {
            button_background.color = Color.white;
        }

        var button_background_magic = magic_attack_button.GetComponent<Image>();

        if (magic_button_pressed)
        {
            //set basic button to not pressed
            basic_button_pressed = false;
            button_background_magic.color = Color.yellow;


        }
        else
        {
            button_background_magic.color = Color.white;
        }

    }
    void PlayerAttackAnims() {
        //allow player to select an action during turn
        basic_attack_button.interactable = true;
        magic_attack_button.interactable = true;


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
        if (SceneLoader.playerHP >= 100) SceneLoader.playerHP = 100;
        if (SceneLoader.playerHP < playerHP.maxValue) StartCoroutine(Heal());
    }

    //if attack button is pressed, pop up attack options
    public void AttackOptions()
    {
        StartCoroutine(PlayUI());

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

        //hide attack options
        pop_up.SetActive(false);
        // turn off Dialogue Box UI
        dialogue_box.SetActive(false);

        //have player choose enemy to attack 
        if (picked_enemy_1)
        {
            yield return new WaitForSeconds(1f);
            //play Damage Sound & player action sound

            //AudioLibrary.Instance.PlaySound(Sfx.Tone);


            AudioLibrary.Instance.PlaySound(Sfx.Attack);
            yield return new WaitForSeconds(1f);
            enemyHP[0].value -= damage;
            //play enemy hurt sound
            AudioLibrary.Instance.PlaySound(Sfx.Hurt);
        }
        else if (picked_enemy_2)
        {
            yield return new WaitForSeconds(1f);
            //play Damage SOund 
            AudioLibrary.Instance.PlaySound(Sfx.Tone);


            AudioLibrary.Instance.PlaySound(Sfx.Attack);
            yield return new WaitForSeconds(1f);
            enemyHP[1].value -= damage;
            //play enemy hurt sound
            AudioLibrary.Instance.PlaySound(Sfx.Hurt);
            //enemy's turn

        }
        //reset enemy pick
        ResetEnemyPick();
        

        yield return new WaitForSeconds(2f);

        //enemy's turn
        StartCoroutine(EnemyTurn());
        
        //check if enemy is dead
        if (enemyHP[0].value <= 0)
        {
            AudioLibrary.Instance.PlaySound(Sfx.Dead);
            nar_text.text = "We sent that " + enemy_name[0].text + "packing!";

            dead1= true;
            yield break;
        }
        if (enemyHP[1].value <= 0)
        {
            AudioLibrary.Instance.PlaySound(Sfx.Dead);

            dead2 = true;
            nar_text.text = "Bye-Bye Mr. " + enemy_name[1].text + "!";

            yield break;
        }
    }
    //magic attack co-routine
    public IEnumerator MagicAttack()
    {
        //deal damage to enemy

        int damage = 40;
        int MP_Depletion = 10;
        //hide attack options
        pop_up.SetActive(false);
        // turn off Dialogue Box UI
        dialogue_box.SetActive(false);
        //take away mana 
        SceneLoader.playerMP -= MP_Depletion;
        
        if (picked_enemy_1)
        {
            dialogue_box.SetActive(true);
            nar_text.text = "Feel the burn!";
            //play Damage SOund 
            AudioLibrary.Instance.PlaySound(Sfx.Tone);

            AudioLibrary.Instance.PlaySound(Sfx.Magic_attack);
            yield return new WaitForSeconds(1f);

            enemyHP[0].value -= damage;
            //play enemy hurt sound
            AudioLibrary.Instance.PlaySound(Sfx.Hurt);
        }
        else if (picked_enemy_2)
        {
            dialogue_box.SetActive(true);
            nar_text.text = "Feel the burn!";
            //play Damage SOund 
            AudioLibrary.Instance.PlaySound(Sfx.Tone);

            AudioLibrary.Instance.PlaySound(Sfx.Magic_attack);
            yield return new WaitForSeconds(1f);
            enemyHP[1].value -= damage;
            //play enemy hurt sound
            AudioLibrary.Instance.PlaySound(Sfx.Hurt);
        }
        //reset enemy pick
        ResetEnemyPick();
   
        yield return new WaitForSeconds(2f);

        //enemy's turn
        StartCoroutine(EnemyTurn());

        //check if enemy is dead
        if (enemyHP[0].value <= 0)
        {
            //turn off fill on slider 
            enemyHP[0].transform.GetChild(1).gameObject.SetActive(false);

            AudioLibrary.Instance.PlaySound(Sfx.Dead);

            nar_text.text = "We sent that " + enemy_name[0].text + "packing!";
            //make enemy unselectable
            enemy_1.interactable = false;
            //play enemy death anim 
            dead1 = true;

            yield break;
        }
        if (enemyHP[1].value <= 0)
        {
            //turn off fill on slider 
            enemyHP[1].transform.GetChild(1).gameObject.SetActive(false);

            AudioLibrary.Instance.PlaySound(Sfx.Dead);

            nar_text.text = "Bye-Bye Mr. " + enemy_name[1].text + "!";
            //make enemy unselectable if dead
            enemy_2.interactable = false;
            dead2 = true;
            yield break;
        }

    }

    //heal player co-routine
    IEnumerator Heal()
    {
        //hide attack options
        pop_up.SetActive(false);

        //heal player
        int healAmount = 50;
        //lose MP 
        int MP_Depletion = 10;

        SceneLoader.playerHP += healAmount;
        SceneLoader.playerMP -= MP_Depletion;
        //dialogue box set active
        dialogue_box.SetActive(true);
        nar_text.text = "Feeling much better now!";
        yield return new WaitForSeconds(1f);
        dialogue_box.SetActive(false);

        ResetEnemyPick();
        yield return new WaitForSeconds(2f);
        //enemy's turn
        StartCoroutine(EnemyTurn());
    }
    //enemy's turn co-routine
    IEnumerator EnemyTurn()
    {
        //dont allow player to select an action during enemy turn
        basic_attack_button.interactable = false;
        magic_attack_button.interactable = false;
        //turn off dialogue box 
        dialogue_box.SetActive(false);

        //set up enemy anim request
        enemy_attack_requested = true;
        //enemy attacks player
        if (enemyHP[0].value != 0)
        {
            SceneLoader.playerHP -= enemy_damage[0];

            //play enemy attack sound 
            AudioLibrary.Instance.PlaySound(Sfx.Attack);
            yield return new WaitForSeconds(1f);

            AudioLibrary.Instance.PlaySound(Sfx.Hurt);

            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(1f);
        if (enemyHP[1].value != 0)
        {
            //enemy 2 attacks player
            AudioLibrary.Instance.PlaySound(Sfx.Slap);
            yield return new WaitForSeconds(1f);

            AudioLibrary.Instance.PlaySound(Sfx.Hurt);

            SceneLoader.playerHP -= enemy_damage[1];
        }
        yield return new WaitForSeconds(2f);

        //check if player is dead
        if (SceneLoader.playerHP <= 0)
        {
            dialogue_box.SetActive(true);
            nar_text.text = "OUCH!";
            AudioLibrary.Instance.PlaySound(Sfx.Dead);

            //play death anim 
            player_dead = true;

            yield return new WaitForSeconds(1f);
            SceneLoader.LoadScene("LoseScene");
        }

        StartCoroutine(ChooseAction());
    }

    public IEnumerator BackToDungeon() {
        SceneLoader.battlesWon += 1;
        yield return new WaitForSeconds(1f);
        dialogue_box.SetActive(true);
        if (SceneLoader.battlesWon == 3)
        {
            nar_text.text = "Wow, that was a tough fight! I better get going before more show up!";
        }
        if (SceneLoader.battlesWon == 2)
        {
            nar_text.text = "Phew, that was close! I better get going before more show up!";
        }
       
        yield return new WaitForSeconds(2f);
        

        if (SceneLoader.battlesWon == 3)
        {

            SceneLoader.LoadScene("WinScene");

        }
        else
        {
            SceneLoader.LoadScene("Dungeon_lvl1");
        }
    }
    //play UI sounds
    public IEnumerator PlayUI()
    {
        AudioLibrary.Instance.PlaySound(Sfx.Clicked_UI);
        yield return new WaitForSeconds(1f);

    }
}
