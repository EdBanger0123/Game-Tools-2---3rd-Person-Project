using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour {

    private Animator anim;
    public Transform Player;
    public float punchCD, groundTime;
    public float enemyMaxHP, enemyCurrentHP;
    [SerializeField] bool _punch, onGround;
    [SerializeField] GameObject leftHandHitBox, playerLeftHandHitBox, playerRightHandHitBox, playerFootHitbox;
    public AudioSource _handAudioSource;
    public AudioClip _hitAudio;
    public Slider enemyHealthBar;

    NavMeshAgent navMesh;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        //transform.LookAt()
        enemyHealthBar.value = EnemyHealthCalculation();
        enemyCurrentHP = enemyMaxHP;
        navMesh = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Player);

        float dist = Vector3.Distance(transform.position, Player.transform.position);

        if(dist <= 5000)
        {
            navMesh.SetDestination(Player.transform.position);
        }

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
        if (onGround == true)
        {
            groundTime -= Time.deltaTime;
        }

        if (groundTime <= 0)
        {
            onGround = false;
        }

        if (onGround == false && groundTime <= 0)
        {
            anim.SetTrigger("Stand");
            groundTime = 1;
        }

    }

    float EnemyHealthCalculation()
    {
        return enemyCurrentHP / enemyMaxHP;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "PlayerHand" )
        {
            Debug.Log("hit");
            anim.SetTrigger("EnemyHit");
            enemyCurrentHP -= 1;
            enemyHealthBar.value = EnemyHealthCalculation();
            _handAudioSource.clip = _hitAudio;
            _handAudioSource.Play();
            playerFootHitbox.SetActive(false);
            playerLeftHandHitBox.SetActive(false);
            playerRightHandHitBox.SetActive(false);
            
        }

        if(collision.gameObject.name == "Barrel")
        {
            anim.SetTrigger("KO");
            onGround = true;
        }
    }

    public void HitBoxOff()
    {
        //rightHandHitBox.SetActive(false);
        leftHandHitBox.SetActive(false);
    }
}
