using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defines player object in scene
public class Player : MonoBehaviour {

    public Inventory inventory;
    public Character character;
    public EventManager eventManager;

    private GameObject castObj;
    private int castIndex = -1;                             // Save index of casted spell to apply effects of skill

    private static Player _instance;
    public static Player instance {
        get {
            if ( _instance == null ){
                _instance = GameObject.FindObjectOfType<Player>();
            }
            return _instance;
        }
    }

    void Awake(){
        inventory = new Inventory();
        eventManager = new EventManager();
        castObj = transform.Find("Cast").gameObject;
    }

    // Cast the skill with the given index
    public void Cast(int index){
        if ( index < character.skills.Count && index >= 0 ){
            Debug.Log("Casting skill #" + index);
            eventManager.AddEventHandler("OnCastEnd", OnCastEnd);

            castObj.SetActive(true);
            Debug.Log(character.skills[index].castOffset+"");
            castObj.transform.localPosition = character.skills[index].castOffset;

            EffectObj eo = castObj.GetComponent<EffectObj>();
            eo.eventName = "OnCastEnd";
            eo.player = this;

            string castEffect = character.skills[index].castEffect;
            Animator anim = castObj.GetComponent<Animator>();
            anim.Play(castEffect);
        }
    }
    // Called at the end of cast effect animation
    public void OnCastEnd(object sender, MyEventArgs args){
        Debug.Log("OnCastEnd");
        eventManager.RemoveEventHandler("OnCastEnd", OnCastEnd);
        castObj.SetActive(false);
    }
}