using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    private Rigidbody barrelRb;
    public Transform Enemy;
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
        if(collision.gameObject.tag == "PlayerHand")
        {
            Debug.Log("barrel kick");
            transform.LookAt(Enemy);
            barrelRb.AddForce(transform.forward * 1000);
        }
    }
}
