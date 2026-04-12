using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsScript : MonoBehaviour
{
    //set enemy stats in an array 
    [Header("Enemy Stats")]
    public string[] enemy_name;
    public int[] enemy_lvl;
    public int[] enemy_hp;
    public int[] enemy_atk;
    public Sprite[] enemy_sprite;
}