using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles target selecting and character animations
public class CharacterObj : MonoBehaviour {

    public Character character;
    private BattleManager battleManager;

    void Awake(){
        battleManager = GameObject.FindObjectOfType<BattleManager>();
    }

    public void Select(){
        battleManager.SetTarget(int.Parse(name));
    }

}