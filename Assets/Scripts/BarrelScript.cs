using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    private Rigidbody barrelRb;
    //public Transform Enemy;
    public bool isKicked;
    // Start is called before the first frame update
    void Start()
    {
        barrelRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        float closestEnemyDistance = Mathf.Infinity;
        EnemyScript closestEnemy = null;
        //EnemySwordScript closestEnemy2 = null;
        EnemyScript[] allEnemies = GameObject.FindObjectsOfType<EnemyScript>();
        //EnemySwordScript[] allSwordEnemies = GameObject.FindObjectsOfType<EnemySwordScript>();

        foreach (EnemyScript currentEnemy in allEnemies)
        {
            float distToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;

            if (distToEnemy < closestEnemyDistance)
            {
                closestEnemyDistance = distToEnemy;
                closestEnemy = currentEnemy;
            }
        }

        if (collision.gameObject.tag == "PlayerHand")
        {
            isKicked = true;
            Debug.Log("barrel kick");
            transform.LookAt(closestEnemy.transform);
            barrelRb.AddForce(transform.forward * 1000);
            //isKicked = false;
        }
    }
}
