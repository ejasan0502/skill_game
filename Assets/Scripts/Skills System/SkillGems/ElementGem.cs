using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gives the skill an element type and its appearance in game
public class ElementGem : SkillGem {

    public ElementType element;
    public string castEffectPath;
    public string hitEffectPath;

    public ElementGem(string name, string description, string id, Tier tier, ItemType itemType,
                      SkillGemType gemType,
                      ElementType elementType, string castEffectPath, string hitEffectPath) :
                      base (name, description, id, tier, itemType, gemType){
        this.element = elementType;
        this.castEffectPath = castEffectPath;
        this.hitEffectPath = hitEffectPath;
    }

    public override void ApplyTo(Skill skill){
        skill.elementType = element;
        skill.castEffect = (GameObject) Resources.Load(castEffectPath);
        skill.hitEffect = (GameObject) Resources.Load(hitEffectPath);
    }

}