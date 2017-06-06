using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Increases target count of the skill
// A skill will have at least 1 target
public class AoeGem : SkillGem {

    public int targetCount;

    public AoeGem(string name, string description, string iconPath, Tier tier, ItemType itemType, 
                  SkillGemType gemType,
                  int targetCount) :
                  base (name, description, iconPath, tier, itemType, gemType){
        this.targetCount = targetCount;
    }

    public override void ApplyTo(Skill skill){
        skill.targetCount = targetCount;
    }

}