using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public GameObject DefeatScreen, VictoryScreen;
    float EnemyHealth, PlayerHealth;
    [SerializeField] float screenTimer;

	// Use this for initialization
	void Start () {
        
        screenTimer = 3;
	}
	
	// Update is called once per frame
	void Update () {

        PlayerHealth = GetComponent<Character>().playerCurrentHP;
        EnemyHealth = GameObject.Find("Enemy").GetComponent<EnemyScript>().enemyCurrentHP;

        if (PlayerHealth <= 0)
        {
            DefeatScreen.SetActive(true);
            screenTimer -= Time.deltaTime;

        }

        if(EnemyHealth <= 0)
        {
            VictoryScreen.SetActive(true);
            screenTimer -= Time.deltaTime;
        }

        if(screenTimer <= 0)
        {
            SceneManager.LoadScene("Game Scene");
        }

	}
}
