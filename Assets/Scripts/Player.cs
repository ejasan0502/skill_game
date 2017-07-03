using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterObj {
    
    public Inventory inventory;

    private static Player _instance;
    public static Player instance {
        get {
            if ( _instance == null ){
                _instance = GameObject.FindObjectOfType<Player>();
            }
            return _instance;
        }
    }

    protected override void Awake(){
        base.Awake();

        inventory = new Inventory();
    }
}