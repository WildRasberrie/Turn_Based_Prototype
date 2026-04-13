using JetBrains.Annotations;
using UnityEngine;

public class Battle_Instantiation : MonoBehaviour
{
    public GameObject playerPrefab; 
    public Transform Player_Parent;
    public Transform Enemy_Parent;
    public GameObject[] enemyPrefabs;
    public GameObject[] enemyClone;
    public Transform player_spawn_point;
    public Transform[] enemy_spawn_point;
    public Vector3 player_offset,enemy_offset; // The offset to apply to the spawn position
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //instantiate player at target location 
        for (int i = 0; i < enemyPrefabs.Length; i++) {
            Instantiate(enemyPrefabs[i], enemy_spawn_point[i].position - enemy_offset, Quaternion.identity, Enemy_Parent);        
        }

        Instantiate(playerPrefab, player_spawn_point.position- player_offset, Quaternion.identity,Player_Parent);

    } 
}
