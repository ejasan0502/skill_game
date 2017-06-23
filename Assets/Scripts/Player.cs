using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defines player object in scene
public class Player : MonoBehaviour {

    public Inventory inventory;
    public Character character;
    public EventManager eventManager;

    private int castIndex = -1;                             // Save index of casted spell to apply effects of skill
    private List<SkillGem> gems = new List<SkillGem>(){
        new ElementGem("Pearl", "White ball thing.", "", Tier.common, ItemType.skillGem, SkillGemType.pearl, "Wind of Frey", ""),
        new EffectGem("Spinel", "Weird greenish thing.", "", Tier.common, ItemType.skillGem, SkillGemType.spinel, new Damage(1f,3f,1,false)),
        new ElementGem("Moonstone", "White oval thing.", "", Tier.common, ItemType.skillGem, SkillGemType.moonstone, "", ""),
        new EffectGem("Peridot", "Green diamondish thing.", "", Tier.rare, ItemType.skillGem, SkillGemType.peridot, new Heal(10,true)),
        new EffectGem("Opal", "Ball thing", "", Tier.legendary, ItemType.skillGem, SkillGemType.opal, new Buff(false,1,new AttributeStats(2,1),null,null))
    };

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

        foreach (SkillGem gem in gems){
            inventory.AddItem(gem, 1);
        }
    }

    // Cast the skill with the given index
    public void Cast(int index){
        if ( index < character.skills.Count && index >= 0 ){
            Debug.Log("Casting skill #" + index);
            eventManager.AddEventHandler("OnCastEnd", OnCastEnd);

            GameObject o = (GameObject) Instantiate(Resources.Load("EffectObj"));
            o.transform.SetParent(transform);
            o.transform.localPosition = Vector3.zero;

            EffectObj eo = o.GetComponent<EffectObj>();
            eo.eventName = "OnCastEnd";
            eo.player = this;

            string castEffect = character.skills[index].castEffect;
            Animator anim = o.GetComponent<Animator>();
            anim.Play(castEffect);
        }
    }
    // Called at the end of cast effect animation
    public void OnCastEnd(object sender, MyEventArgs args){
        Debug.Log("OnCastEnd");
        eventManager.RemoveEventHandler("OnCastEnd", OnCastEnd);
    }
}