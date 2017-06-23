using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Object that animates sprite sheets and trigger events for player
public class EffectObj : MonoBehaviour {

    // Replace this with a character object script
    // This object MUST have an event manager variable in order to trigger end cast events
    public Player player;
    public string eventName;

    public void Trigger(){
        if ( player != null && eventName != "" ){
            player.eventManager.TriggerEvent(eventName,MyEventArgs.empty);
            Destroy(gameObject);
        }
    }
}