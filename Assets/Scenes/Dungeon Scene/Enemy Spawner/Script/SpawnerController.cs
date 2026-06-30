using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class SpawnerController : MonoBehaviour
{
    public GameObject[] enemy_loc; // The enemy prefab to spawn
    [Header("Spawn Settings")]
    [Tooltip("The percentage chance of the player encountering the enemy. Value should be between 0 and 100.")]
    [Range(0, 100)]
    public float spawn_ratio = 100;
    float ratio; // The ratio of player encountering the enemy
    [Space]
    public GameObject player;
    public SceneLoader SceneLoader;
    [Space]
    float random;
    bool spawn;
    Transform player_transform;
  


 
    void Start()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");

        if (SceneLoader == null) SceneLoader = GameObject.FindWithTag("SceneLoader").GetComponent<SceneLoader>();
        //player_transform = player.transform;
        if (SceneLoader.battlesWon != 0) player.transform.position = SceneLoader.storedPosition;
 
    }

    void Update()
    {
        TempSpawnEnemyTrigger();

        //if battle won, enemy_loc.length will increase by 1
        if (SceneLoader.battlesWon > 0 && SceneLoader.battlesWon < 3)
        {
            enemy_loc[SceneLoader.battlesWon - 1].SetActive(false);

        }

        print("Chance of enemy encounter is " + spawn_ratio + "%"
          +"\n spawn chance: " + spawn);
    }

    public void TempSpawnEnemyTrigger() {
 
        for (int i = 0; i < enemy_loc.Length; i++)
        {
            if (DistanceDetection(player.transform.position, i, 2))
            {
                print("ratio " + ratio);
                ratio = Random.Range(0, 100);

                var max_ratio = (spawn_ratio) / 100;

                random = ratio / 100; //get the ratio of player encountering the enemy
                if (random == max_ratio && max_ratio != 0)
                {
                    spawn = true;
                }
                else
                {
                    spawn = false;
                }
            }

            if (spawn)
            {
                SceneLoader.storedPosition = player.transform.position; //store player positiion in  dungeon scene 
                StartCoroutine(SpawnEnemy());
            }


        }
    }

    IEnumerator SpawnEnemy() {
        // have random number spawn once when called 

 

        if (spawn)
        {
            //p]ay fade out animation 
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("BattleScene");
            spawn = false;
        }
    }


    
    public bool DistanceDetection(Vector3 target, int index, int distance) {
        return (Vector3.Distance(enemy_loc[index].transform.position, target) < distance);
    }

}


