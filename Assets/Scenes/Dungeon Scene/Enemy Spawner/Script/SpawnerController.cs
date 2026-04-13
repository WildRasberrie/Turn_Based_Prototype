using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections;

public class SpawnerController : MonoBehaviour
{
    public GameObject[] enemy_loc; // The enemy prefab to spawn
    [Header("Spawn Settings")]
    [Tooltip("The percentage chance of the player encountering the enemy. Value should be between 0 and 100.")]
    [Range(0, 100)]
    public float spawn_ratio = 100;
    float ratio; // The ratio of player encountering the enemy
    [Space]
    public SceneAsset scene_name; // The name of the scene to load when the enemy is spawned
    [Space]
    public GameObject player;
    public SceneLoader SceneLoader;
    [Space]
    float random;
    bool spawn;
    Transform player_transform;
  


 
    void Start()
    {
        player_transform = player.transform;
        player.transform.position = player_transform.position;
        //check if there is more than one player in the scene, if there is more than one player
        // destroy the extra player 
        player = GameObject.FindGameObjectWithTag("Player");
        var current_players = GameObject.FindGameObjectsWithTag("Player");
        if (current_players.Length > 1)
        {
            for (int i = 1; i < current_players.Length; i++)
            {
                Destroy(current_players[i]);
            }
        }
     
    }

    void Update()
    {
        
        for (int i = 0; i < enemy_loc.Length; i++) { 
            if (DistanceDetection(player.transform.position, i,5)) {

            spawn = true;
            print("Player is hitting enemy spawner");
            StartCoroutine(SpawnEnemy());
            }
        }

        print("Chance of enemy encounter is " + spawn_ratio + "%"
            +"\n spawn chance: " + spawn);

        //dont destroy player position after loading into battle scene 
        player_transform = player.transform;
        DontDestroyOnLoad(player_transform);
    }

    IEnumerator SpawnEnemy() {
        // have random number spawn once when called 

        ratio = Random.Range(0,spawn_ratio);

        random = ratio / 100f; //get the ratio of player encountering the enemy
        if (spawn)
        {
            if (random >= .9f)
            {
                //p]ay fade out animation 
                yield return new WaitForSeconds(1f);
                SceneLoader.LoadScene(scene_name.name);
            }
                spawn = false;
        }
    }
    public bool DistanceDetection(Vector3 target, int index, int distance) {
        return (Vector3.Distance(enemy_loc[index].transform.position, target) < distance);
    }

}


