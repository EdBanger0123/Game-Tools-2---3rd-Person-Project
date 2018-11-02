using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitDash : MonoBehaviour {

    public Transform Player, Enemy;
    private Transform endPos;
    private Rigidbody _myRb;
    public float speed, distCovered, fracJourney;
    public float startTime;
    public float journeyLength;


	// Use this for initialization
	void Start () {
        startTime = Time.time;

        speed = 1000;
        _myRb = GetComponent<Rigidbody>();
       


        //endPos.position = journeyLength - 50;
    }
	
	// Update is called once per frame
	void Update () {

        if(Input.GetMouseButtonDown(0))
        {

            journeyLength = Vector3.Distance(Player.position, Enemy.position);

            distCovered = (Time.time - startTime) * speed;

            fracJourney = distCovered / journeyLength;




            if (GetComponent<PlayerMovement>().journeyLength >= 400)
            {
                transform.position = Vector3.Lerp(Player.position, Enemy.position, fracJourney);

                //GetComponent<PlayerHitDash>().enabled = false;
            }

            if (GetComponent<PlayerMovement>().journeyLength <= 400)
            {
                _myRb.velocity = Vector3.zero;
                distCovered = 0;
                fracJourney = 0;
            }
        }




    }
}
