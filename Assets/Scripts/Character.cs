using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

    private Animator anim;
    private Animation _myAnim;
    private Rigidbody _myRb;
    public float playerMaxHP, playerCurrentHP, swingCD;
    private bool _isBlocking, hasSwung, beenHit;
    public Slider pHealthBar;
    public AudioSource _handAudioSource;
    public AudioClip _hitAudio, _swordAudio;
    [SerializeField] Transform Enemy;
    [SerializeField] float speed, hitTimer, damping;
    [SerializeField] GameObject rightHandHitBox, leftHandHitBox, footHitBox, rackText;
    public GameObject sword, shield;
    
    public Rigidbody barrelRb;
    //private float _forward, _sideWalk;

    private void Start()
    {
        anim = GetComponent<Animator>();
        _isBlocking = false;
        _myAnim["Block"].wrapMode = WrapMode.ClampForever;
        playerCurrentHP = playerMaxHP;
        pHealthBar.value = HealthCalculation();
        sword.SetActive(true);
        shield.SetActive(true);
        Cursor.visible = false;
    }

    public void Move(float _forward, float _sideWalk)
    {
        anim.SetFloat("Forward", _forward);
        anim.SetFloat("Side Walk", _sideWalk);

        if(hasSwung == false)
        {
            transform.Translate(_sideWalk * speed, 0.0f, _forward * speed);
        }
        
    }

    private void FixedUpdate()
    {
        //transform.LookAt(Enemy);
        anim.SetBool("ShieldBlock", _isBlocking);
        anim.SetBool("hasSwung", hasSwung);

        /*if(Camera.main.GetComponent<CombatChangeScript>().swordPickUP == true)
        {
            Camera.main.GetComponent<CombatChangeScript>().swordPickUP = false;
            Camera.main.GetComponent<CombatChangeScript>().enabled = false;
            rackText.SetActive(false);
        }*/

        Cursor.lockState = CursorLockMode.Confined;


        if (GetComponent<LockOnScript>().lockedOn == false)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            RaycastHit hit;

            if (Physics.Raycast(mouseRay, out hit))
            {
                var lookPos = hit.transform.position - transform.position;

                lookPos.y = 0;

                var Rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, Time.deltaTime * damping);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (hasSwung == false)
            {
                anim.SetTrigger("Swing 1");
                hasSwung = true;
            } else
            {
                Enemy.GetComponent<EnemyScript>().beenHit = false;
                anim.SetTrigger("Swing 2");
                swingCD = 1;
            }
            //anim.SetTrigger("Punch 1");
            //leftHandHitBox.SetActive(true);
            
            //transform.Translate(0.0f, 0.0f, 0.0f);
        }

        if(Input.GetMouseButton(1))
        {
            //anim.SetTrigger("Punch 2");
            //rightHandHitBox.SetActive(true);
            _isBlocking = true;
            GetComponent<UserInput>().enabled = false;
        } else
        {
            _isBlocking = false;
            GetComponent<UserInput>().enabled = true;
            
        }

        if(playerCurrentHP <= 0)
        {
            anim.SetTrigger("pKO");
        }

        if(hasSwung)
        {
            swingCD -= Time.deltaTime;
            sword.GetComponent<CapsuleCollider>().enabled = true;
        }

        if(swingCD <= 0)
        {
            hasSwung = false;
            swingCD = 1;
            sword.GetComponent<CapsuleCollider>().enabled = false;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Kick");
            footHitBox.SetActive(true);
            
        }

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

    float HealthCalculation()
    {
        return playerCurrentHP / playerMaxHP;
    }

    public void HitBoxOff()
    {
        rightHandHitBox.SetActive(false);
        leftHandHitBox.SetActive(false);
        footHitBox.SetActive(false);
        sword.GetComponent<CapsuleCollider>().enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "EnemyHand" && _isBlocking == false && beenHit == false)
        {
            anim.SetTrigger("PlayerHit");
            playerCurrentHP -= 1;
            pHealthBar.value = HealthCalculation();
            if(Enemy.GetComponent<EnemyScript>()._punch)
            {
                _handAudioSource.clip = _hitAudio;
            } 
            if(Enemy.GetComponent<EnemyScript>()._swing)
            {
                _handAudioSource.clip = _swordAudio;
            }
            
            _handAudioSource.Play();
            beenHit = true;
            
        }

        if(collision.gameObject.tag == "EnemyHand" && _isBlocking == true)
        {
            _handAudioSource.clip = _hitAudio;
            _handAudioSource.Play();
            
        }

        if(collision.gameObject.name == "Barrel")
        {
            
        }
    }


}

