using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles target selecting and character animations
public class CharacterObj : MonoBehaviour {

    public Character character;
    public Animator anim;

    private BattleManager battleManager;

    void Awake(){
        battleManager = GameObject.FindObjectOfType<BattleManager>();
        anim = GetComponent<Animator>();
    }
    void OnMouseDown(){
        battleManager.SetTarget(int.Parse(name));
    }

    // Creates hit text above object and apply hit to character
    public void Hit(float dmg){

    }

}