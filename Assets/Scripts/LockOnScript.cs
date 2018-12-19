using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnScript : MonoBehaviour
{

    public bool lockedOn;
    [SerializeField] GameObject lockOnCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float closestEnemyDistance = Mathf.Infinity;
        EnemyScript closestEnemy = null;
        //EnemySwordScript closestEnemy2 = null;
        EnemyScript[] allEnemies = GameObject.FindObjectsOfType<EnemyScript>();
        //EnemySwordScript[] allSwordEnemies = GameObject.FindObjectsOfType<EnemySwordScript>();

        foreach (EnemyScript currentEnemy in allEnemies)
        {
            float distToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;

            if(distToEnemy < closestEnemyDistance)
            {
                closestEnemyDistance = distToEnemy;
                closestEnemy = currentEnemy;
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && lockedOn == false)
        {
            lockedOn = true;
        } else if(Input.GetKeyDown(KeyCode.F) && lockedOn == true)
        {
            lockedOn = false;
        } 

        if(lockedOn)
        {
            transform.LookAt(closestEnemy.transform);
        } 

    }
}
