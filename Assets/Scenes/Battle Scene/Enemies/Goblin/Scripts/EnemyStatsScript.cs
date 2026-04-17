using UnityEngine;

public class EnemyStatsScript : MonoBehaviour
{
    //set enemy stats in an array 
    [Header("Enemy Stats")]
    public string[] enemy_name;
    public int[] enemy_lvl;
    public int[]  enemy_hp;
    public int[] enemy_atk;


    private void Awake()
    {
        TrackEnemyStats();



    }
    public void TrackEnemyStats() {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (this.name == "Goblin")
            {
                enemy_name[i] = "Goblin";
                enemy_lvl[i] = 7;
                enemy_hp[i] = 100;
                enemy_atk[i] = 15;
            }
            else
              if (this.name == "Slime")
            {
                enemy_name[i] = "Slime";
                enemy_lvl[i] = 5;
                enemy_hp[i] = 75;
                enemy_atk[i] = 10;
            }
        }
        }

       
    
}