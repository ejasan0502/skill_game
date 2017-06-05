using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : Item {

    public Skill skill;

    public Rune(Skill skill) : base(skill.name, skill.description, "", Tier.legendary, ItemType.rune){
        this.skill = skill;
    }
}