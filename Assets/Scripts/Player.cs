using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Inventory inventory;
    private List<SkillGem> gems = new List<SkillGem>(){
        new ElementGem("Pearl", "White ball thing.", "p-0", Tier.common, ItemType.skillGem, SkillGemType.pearl, ElementType.physical, "", ""),
        new EffectGem("Spinel", "Weird greenish thing.", "s-0", Tier.common, ItemType.skillGem, SkillGemType.spinel, new Damage(1f,3f,1,false))
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

        foreach (SkillGem gem in gems){
            inventory.AddItem(gem, 1);
        }
    }
}