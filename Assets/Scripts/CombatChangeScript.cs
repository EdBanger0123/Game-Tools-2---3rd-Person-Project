using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatChangeScript : MonoBehaviour
{
    public Character _swordScript;
    public CharacterFistOnly _fistScript;
    public bool swordPickUP;
    public Animator playerAnim;
    public Text changeWeaponsText;
    [SerializeField] GameObject WeaponRack, Player;

    // Start is called before the first frame update
    void Start()
    {
        _fistScript.enabled = true;
        _swordScript.enabled = false;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = Vector3.Distance(WeaponRack.transform.position, Player.transform.position);

        if(dist < 20)
        {
            Debug.Log("At rack");
            changeWeaponsText.text = "Press E to pick up a Sword and Shield at the weapon rack";
            if(Input.GetKeyDown(KeyCode.E))
            {
                
                swordPickUP = true;
            }
        }

        if(swordPickUP)
        {
            Player.transform.LookAt(WeaponRack.transform);
            playerAnim.SetTrigger("PickUp");
            changeWeaponsText.text = null;
            Player.transform.LookAt(WeaponRack.transform);
            _swordScript.playerCurrentHP = _fistScript.playerCurrentHP;
            _fistScript.enabled = false;
            _swordScript.enabled = true;
            _swordScript.shield.SetActive(true);
            _swordScript.sword.SetActive(true);
            changeWeaponsText.enabled = false;
            GetComponent<CombatChangeScript>().enabled = false;
            swordPickUP = false;
            //gameObject.AddComponent<Character>();
        }
    }
}
