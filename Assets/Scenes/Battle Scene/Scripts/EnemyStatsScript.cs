using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting;

public class EnemyStatsScript : MonoBehaviour
{
    //set enemy stats in an array 
    [Header("Enemy Stats")]
    public string[] enemy_name;
    public int[] enemy_lvl;
    public int[] enemy_hp;
    public int[] enemy_atk;
    public Battle_Instantiation battle_Instantiation;

    private void Awake()
    {
        if (battle_Instantiation == null) battle_Instantiation = GameObject.Find("Player Battle Station").GetComponent<Battle_Instantiation>();
        TrackEnemyStats();


    }

    private void Update()
    {
        TrackEnemyStats();



    }
    public void TrackEnemyStats() {

        for (int i = 0; i < battle_Instantiation.enemyPrefabs.Length; i++)
        {
           enemy_hp[i] = 100;

            if (battle_Instantiation.enemyClone[i].name == "Goblin")
            {
                enemy_name[i] = "Goblin";
                enemy_lvl[i] = 7;
                enemy_hp[i] = 100;
                enemy_atk[i] = 15;
            }else 
            if (battle_Instantiation.enemyClone[i].name == "Slime" )
            {
                enemy_name[i] = "Slime";
                enemy_lvl[i] = 5;
                enemy_hp[i] = 75;
                enemy_atk[i] = 10;
            }
        }

       
    }
}