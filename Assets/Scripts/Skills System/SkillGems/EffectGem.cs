using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gives an effect property to the skill
public class EffectGem : SkillGem {

    public Effect effect;

    public EffectGem(string name, string description, string id, Tier tier, ItemType itemType, 
                     SkillGemType gemType,
                     Effect effect) :
                      base (name, description, id, tier, itemType, gemType){
        this.effect = effect;
    }

    public override void ApplyTo(Skill skill){
        skill.effects.Add(effect);
    }

}