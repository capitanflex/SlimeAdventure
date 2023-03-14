using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject player;
    private Player playerScript;

    
    private int indexWave;

    private void Start()
    {
        playerScript = player.gameObject.GetComponent<Player>();
    }
    
    public void NextWave()
    {
        indexWave++;
        
        for (int i = 0; i < indexWave; i++)
        {
            
            
            GameObject currentEnemy = Instantiate(enemyPrefab, player.transform.position + new Vector3(Random.Range(-1f, 1f) + 5, 0, 
                Random.Range(-2f, 2f)), new Quaternion(0,0,0, 0));
            
            currentEnemy.GetComponent<Enemy>().damage += 1 * indexWave;
            currentEnemy.GetComponent<Enemy>().hp += 2 * indexWave;
            
            playerScript.AddEnemy(currentEnemy);
            
        }
    }
    
   
}
