using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour {

    private Animator anim;
    public Transform Player;
    public float punchCD, groundTime, hitTimer;
    public float enemyMaxHP, enemyCurrentHP;
    public bool _punch, onGround, _swing;
    [SerializeField] GameObject leftHandHitBox, playerLeftHandHitBox, playerRightHandHitBox, playerFootHitbox;
    public AudioSource _enemyAudioSource;
    public AudioClip _hitAudio, _swordHitAudio;
    public EnemyScript thisScript;
    public bool usingSword, beenHit;
    private float deathTimer = 3;
    private bool _idle, _forward, _back;
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

        
        anim.SetBool("Idle", _idle);
        anim.SetBool("Forward", _forward);
        anim.SetBool("Back", _back);

        float dist = Vector3.Distance(transform.position, Player.transform.position);

        if(dist <= 50 && dist > navMesh.stoppingDistance)
        {
            _forward = true;
            _back = false;
            _idle = false;
            navMesh.SetDestination(Player.transform.position);
        }

        if(dist < navMesh.stoppingDistance - 2)
        {
            _forward = false;
            _back = true;
            _idle = false;
            transform.Translate(Vector3.back);
        }

        if(dist == navMesh.stoppingDistance)
        {
            _idle = true;
            _back = false;
            _forward = false;
        }

        if(enemyCurrentHP <= 0)
        {
            anim.SetTrigger("KO");
            deathTimer -= Time.deltaTime;
        }

        if(deathTimer <= 0)
        {
            Destroy(gameObject);
        }

        if(dist <= 50)
        {
            punchCD -= Time.deltaTime;
        }
        

        if(punchCD <= 0)
        {
            if (usingSword == false)
            {
                _punch = true;
            } else
            {
                _swing = true;
            }
        }
        
        if(punchCD > 0)
        {
            _punch = false;
            _swing = false;
        }

        if(_punch)
        {
            anim.SetTrigger("Punch");
            leftHandHitBox.SetActive(true);
            punchCD = 5;
        }


        if (_swing)
        {
            anim.SetTrigger("Swing 1");
            punchCD = 5;
        }

        if (onGround == true && EnemyHealthCalculation() > 0)
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

        /*if(EnemyHealthCalculation() <= 0 )
        {
            navMesh = null;
            Destroy(thisScript);
        }*/

        if (beenHit)
        {
            hitTimer -= Time.deltaTime;
        }

        if (hitTimer <= 0)
        {
            beenHit = false;
            hitTimer = 1;
        }


    }

    float EnemyHealthCalculation()
    {
        return enemyCurrentHP / enemyMaxHP;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerHand" && beenHit == false)
        {
            Debug.Log("hit");
                      
            anim.SetTrigger("EnemyHit");
            enemyCurrentHP -= 1;
            enemyHealthBar.value = EnemyHealthCalculation();
            if(Player.GetComponent<CharacterFistOnly>().enabled)
            {
                beenHit = true;
                _enemyAudioSource.clip = _hitAudio;
            } 
            if(Player.GetComponent<Character>().enabled)
            {
                _enemyAudioSource.clip = _swordHitAudio;
            }
            _enemyAudioSource.Play();
            playerFootHitbox.SetActive(false);
            playerLeftHandHitBox.SetActive(false);
            playerRightHandHitBox.SetActive(false);
            leftHandHitBox.SetActive(false);
            
        }

        if(collision.gameObject.name == "Barrel" && beenHit == true)
        {
            beenHit = true;
            _enemyAudioSource.clip = _hitAudio;
            _enemyAudioSource.Play();
            anim.SetTrigger("KO");
            enemyCurrentHP -= 1;
            enemyHealthBar.value = EnemyHealthCalculation();
            //GameObject.FindGameObjectWithTag("Barrel").GetComponent<BarrelScript>().isKicked = false;
            leftHandHitBox.SetActive(false);
            onGround = true;
        }
    }

    public void HitBoxOff()
    {
        //rightHandHitBox.SetActive(false);
        leftHandHitBox.SetActive(false);
    }
}
