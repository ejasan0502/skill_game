using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Object that animates sprite sheets and trigger events for player
public class EffectObj : MonoBehaviour {

    // Replace this with a character object script
    // This object MUST have an event manager variable in order to trigger end cast events
    public CharacterObj characterObj;
    public string eventName;

    private List<Character> targets = new List<Character>();

    void OnTriggerEnter2D(Collider2D other){
        CharacterObj charObj = other.GetComponent<CharacterObj>();
        if ( charObj != null && !targets.Contains(charObj.character) ){
            targets.Add(charObj.character);
        }
    }

    public void Trigger(){
        if ( characterObj != null && eventName != "" ){
            ArrayList args = new ArrayList();
            args.Add(targets);
            characterObj.eventManager.TriggerEvent(eventName,new MyEventArgs(args));
        }
    }
}