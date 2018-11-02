using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Vector3 movement;
    public GameObject Enemy, camController, Player;
    private Rigidbody _myRb;
    public float speed;
    public float journeyLength;
    private bool _isMoving, _rightStrafing, _leftStrafing;
    public bool _lockedOn;
    private Animator anim;


	void Start () {
        _myRb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        
    
	}
	
	
	void Update () {


        float yMove = Input.GetAxis("Vertical") * Time.deltaTime * 150.0f;
        float xMove = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;

        anim.SetBool("isWalking", _isMoving);
        anim.SetBool("leftStrafe", _leftStrafing);
        anim.SetBool("rightStrafe", _rightStrafing);

        transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
        transform.Translate(xMove, 0.0f, yMove);

        /*Vector3 movement = new Vector3(xMove, 0.0f, yMove);
        _myRb.AddForce(movement * speed);*/

       
        if (Input.GetKeyDown(KeyCode.F) && _lockedOn == false)
        {
            _lockedOn = true;
        } else if (Input.GetKeyDown(KeyCode.F) && _lockedOn == true)
        {
            _lockedOn = false;
        }

        journeyLength = Vector3.Distance(Player.transform.position, Enemy.transform.position);

        if (_lockedOn)
        {
            transform.LookAt(Enemy.transform);         

        }

        /*if (Input.GetMouseButtonDown(0) && _lockedOn && journeyLength >= 400)
        {

            GetComponent<PlayerHitDash>().enabled = true;
                           
        }*/

        /*if(journeyLength <= 400)
        {
            GetComponent<PlayerHitDash>().enabled = false;
            GetComponent<PlayerHitDash>().distCovered = 0;
            GetComponent<PlayerHitDash>().fracJourney = 0;
            GetComponent<PlayerHitDash>().startTime = 0;
        }*/

        




        if (yMove > 0 || yMove < 0)
        {
            _isMoving = true;
           // camController.transform.position = Vector3.MoveTowards(camController.transform.position, transform.position, camSpeed);

        } else
        {
            _isMoving = false;

        }

        if((xMove > 0 || xMove < 0) && _lockedOn == false)
        {
            _isMoving = true;
        } 

        if(xMove > 0  && _lockedOn == true)
        {
            _rightStrafing = true;
            _leftStrafing = false;
            _isMoving = false;

        }

        if(xMove < 0 && _lockedOn == true)
        {
            _leftStrafing = true;
            _rightStrafing = false;
            _isMoving = false;
        }


	}
}
