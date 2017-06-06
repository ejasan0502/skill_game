using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gives the skill an element type and its appearance in game
public class ElementGem : SkillGem {

    public ElementType element;
    public string castEffectPath;
    public string hitEffectPath;

    public ElementGem(string name, string description, string iconPath, Tier tier, ItemType itemType,
                      SkillGemType gemType,
                      string castEffectPath, string hitEffectPath) :
                      base (name, description, iconPath, tier, itemType, gemType){
        this.castEffectPath = castEffectPath;
        this.hitEffectPath = hitEffectPath;

        if ( (int)gemType != 0 ){
            Debug.LogError(name + " has an invalid gemType for ElementGem!");
            return;
        }

        switch (gemType.ToString()){
            case "aquamarine": element = ElementType.ice; break;
            case "sapphire": element = ElementType.water; break;
            case "amethyst": element = ElementType.wind; break;
            case "citrine": element = ElementType.earth; break;
            case "topaz": element = ElementType.electric; break;
            case "moonstone": element = ElementType.magical; break;
            case "pearl": element = ElementType.physical; break;
            case "emerald": element = ElementType.toxic; break;
            case "ruby": element = ElementType.fire; break;
        }
    }

    public override void ApplyTo(Skill skill){
        skill.elementType = element;
        skill.castEffect = (GameObject) Resources.Load(castEffectPath);
        skill.hitEffect = (GameObject) Resources.Load(hitEffectPath);
    }

}