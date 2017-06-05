using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An item that holds properties of a skill
public class SkillGem : Item{

    public SkillGemType gemType;

    public SkillGem(string name, string description, string id, Tier tier, ItemType itemType,
                      SkillGemType gemType) :
                      base (name, description, id, tier, itemType){
        this.gemType = gemType;
    }
    // Used to initialize variables of the given skill
    public virtual void ApplyTo(Skill skill){

    }

}