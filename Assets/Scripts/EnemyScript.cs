using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour {

    private Animator anim;
    public Transform Player;
    public float punchCD;
    public float enemyMaxHP, enemyCurrentHP;
    private bool _punch;
    public GameObject leftHandHitBox;
    public AudioSource _handAudioSource;
    public AudioClip _hitAudio;
    public Slider enemyHealthBar;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        //transform.LookAt()
        enemyHealthBar.value = EnemyHealthCalculation();
        enemyCurrentHP = enemyMaxHP;
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Player);

        if(enemyCurrentHP <= 0)
        {
            anim.SetTrigger("KO");
        }

        punchCD -= Time.deltaTime;

        if(punchCD <= 0)
        {
            _punch = true;
            punchCD = 5;

        } else
        {
            _punch = false;
        }

        if(_punch)
        {
            anim.SetTrigger("Punch");
            leftHandHitBox.SetActive(true);
            
        }
        
	}

    float EnemyHealthCalculation()
    {
        return enemyCurrentHP / enemyMaxHP;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" )
        {
            Debug.Log("hit");
            anim.SetTrigger("EnemyHit");
            enemyCurrentHP -= 1;
            enemyHealthBar.value = EnemyHealthCalculation();
            _handAudioSource.clip = _hitAudio;
            _handAudioSource.Play();
            
        }
        
    }

    public void HitBoxOff()
    {
        //rightHandHitBox.SetActive(false);
        leftHandHitBox.SetActive(false);
    }
}
