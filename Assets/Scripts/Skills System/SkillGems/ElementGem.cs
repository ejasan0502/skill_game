using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gives the skill an element type and its appearance in game
public class ElementGem : SkillGem {

    public ElementType element;
    public Vector3 castOffset;
    public string castEffect;
    public string hitEffect;

    public ElementGem(string name, string description, string iconPath, Tier tier, ItemType itemType,
                      SkillGemType gemType,
                      Vector3 castOffset, string castEffect, string hitEffect) :
                      base (name, description, iconPath, tier, itemType, gemType){
        this.castOffset = castOffset;
        this.castEffect = castEffect;
        this.hitEffect = hitEffect;

        if ( (int)gemType > 9 ){
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
        skill.castOffset = castOffset;
        skill.castEffect = castEffect;
        skill.hitEffect = hitEffect;
    }

}