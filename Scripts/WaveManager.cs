using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    public Wave[] waves;
    public int indexWave;

    public Transform[] spawnPoints;
    public float spawnTime;

    public bool winO;

    public int enemyCount;
    private bool inWave;
    public GameObject skip;

    public MenuManager winMenu;

    public void Start()
    {
        NextWave();
        skip.SetActive(false);
        inWave = true;
        InvokeRepeating("Spawn", spawnTime, spawnTime);
        indexWave = 0;
       Entity.OnEnemyDeath += RemoveEnemyCount;
    }

    public void Update()
    {
        if (WaveDone() == true)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {

                Win();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NextWave();
            }
        }
            
       
    }
    public void Win()
    {
        SceneManager.LoadScene("MenuManager", LoadSceneMode.Single);
        winMenu.WinMenu();
        
    }

    public void Spawn(Transform enemy)
    {
        
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }

    private void RemoveEnemyCount()
    {
        enemyCount--;
        
    }

    public bool WaveDone()
    {
        if(enemyCount== 0 && inWave == true)
        {
            inWave = false;
            skip.SetActive(true);
            indexWave++;

            return true;
        }
        return false;
    }

    public void NextWave()
    {
      skip.SetActive(false);
      inWave = true;

      for(int i = 0; i < waves[indexWave].specs.Length; i++)
      {
         for (int j = 0; j < waves[indexWave].specs[i].enemyCount; j++)
         {
             Spawn(waves[indexWave].specs[i].enemy.transform);
          }
                        
      }
    }
    

  


}
