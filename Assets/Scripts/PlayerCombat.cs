using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    private Animator anim;
    private Rigidbody _myRb;

    public GameObject rightHandHitBox, leftHandHitBox;
    public float dodgeDist = 1000;

    public Transform Player;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        _myRb = GetComponent<Rigidbody>();
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {


		if(Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Punch 1");
            leftHandHitBox.SetActive(true);
            //_myRb.isKinematic = false;



        }



        if(Input.GetMouseButtonDown(1))
        {
            anim.SetTrigger("Punch 2");
            rightHandHitBox.SetActive(true);
            
        }
	}

    void LeftDodge()
    {
        transform.Translate((Vector3.left * 1000f) * Time.deltaTime);
    }

    void RightDodge()
    {
        //transform.Translate((Vector3.right * 1000f) * Time.deltaTime);
        transform.position += transform.right * 1000f * Time.deltaTime;
    }

    public void HitBoxOff()
    {
        rightHandHitBox.SetActive(false);
        leftHandHitBox.SetActive(false);
    }

  
}
