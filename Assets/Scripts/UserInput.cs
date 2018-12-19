using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour {

    private float _forward, _sideWalk;
    public float speed;
    private CharacterFistOnly _character;

	// Use this for initialization
	void Start () {
        _character = GetComponent<CharacterFistOnly>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        _forward = Input.GetAxis("Vertical");
        _sideWalk = Input.GetAxis("Horizontal");

        _character.Move(_forward, _sideWalk);
	}
}
