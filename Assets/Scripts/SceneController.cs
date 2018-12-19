using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {

    public GameObject DefeatScreen, VictoryScreen;
    public float PlayerHealth, enemyCount;
    [SerializeField] float screenTimer;
    public Text enemyCountText;

	// Use this for initialization
	void Start () {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        //currentEnemies = maxEnemies;
        screenTimer = 3;
	}
	
	// Update is called once per frame
	void Update () {

        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if(GetComponent<CharacterFistOnly>().enabled == true)
        {
            PlayerHealth = GetComponent<CharacterFistOnly>().playerCurrentHP;
        } else if (GetComponent<Character>().enabled == true)
        {
            PlayerHealth = GetComponent<Character>().playerCurrentHP;
        }

        
        //EnemyHealth = GameObject.Find("Enemy").GetComponent<EnemyScript>().enemyCurrentHP;

        if (PlayerHealth <= 0)
        {
            DefeatScreen.SetActive(true);
            screenTimer -= Time.deltaTime;

        }

        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            VictoryScreen.SetActive(true);
            screenTimer -= Time.deltaTime;
        }

        if (screenTimer <= 0)
        {
            SceneManager.LoadScene("Game Scene");
        }

        
	}
}
