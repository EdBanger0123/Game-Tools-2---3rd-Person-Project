using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

    private Animator anim;
    private Animation _myAnim;
    private Rigidbody _myRb;
    public float playerMaxHP, playerCurrentHP;
    private bool _isBlocking;
    public Slider pHealthBar;
    public AudioSource _handAudioSource;
    public AudioClip _hitAudio;
    [SerializeField] Transform Enemy;
    [SerializeField] float speed;
    [SerializeField] GameObject rightHandHitBox, leftHandHitBox;
    //private float _forward, _sideWalk;

    private void Start()
    {
        anim = GetComponent<Animator>();
        _isBlocking = false;
        _myAnim["Block"].wrapMode = WrapMode.ClampForever;
        playerCurrentHP = playerMaxHP;
        pHealthBar.value = HealthCalculation();
    }

    public void Move(float _forward, float _sideWalk)
    {
        anim.SetFloat("Forward", _forward);
        anim.SetFloat("Side Walk", _sideWalk);

        transform.Translate(_sideWalk * speed, 0.0f, _forward * speed);
    }

    private void FixedUpdate()
    {
        transform.LookAt(Enemy);
        anim.SetBool("Blocking", _isBlocking);

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Punch 1");
            leftHandHitBox.SetActive(true);
            transform.Translate(0.0f, 0.0f, 0.0f);
        }

        if(Input.GetMouseButton(1))
        {
            //anim.SetTrigger("Punch 2");
            //rightHandHitBox.SetActive(true);
            _isBlocking = true;
        } else
        {
            _isBlocking = false;
        }

        if(playerCurrentHP <= 0)
        {
            anim.SetTrigger("pKO");
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "EnemyHand" && _isBlocking == false)
        {
            anim.SetTrigger("PlayerHit");
            playerCurrentHP -= 1;
            pHealthBar.value = HealthCalculation();
            _handAudioSource.clip = _hitAudio;
            _handAudioSource.Play();
            
        }

        if(collision.gameObject.tag == "EnemyHand" && _isBlocking == true)
        {
            _handAudioSource.clip = _hitAudio;
            _handAudioSource.Play();
            
        }
    }


}

