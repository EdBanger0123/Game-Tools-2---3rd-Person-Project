using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour {

    private float _forward, _sideWalk;
    public float speed;
    private Character _character;

	// Use this for initialization
	void Start () {
        _character = GetComponent<Character>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        _forward = Input.GetAxis("Vertical");
        _sideWalk = Input.GetAxis("Horizontal");

        _character.Move(_forward, _sideWalk);
	}
}
