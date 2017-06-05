using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Increases current health on target or targets
public class Heal : Effect {

    public float amount;
    public bool percent;
    public EffectType effectType {
        get {
            return EffectType.heal;
        }
    }

    public void Apply(Character caster, Skill skill, Character target){
        float playerDmg = skill.elementType == ElementType.physical ? caster.combatStats.PhysicalDamage : caster.combatStats.MagicalDamage;

        target.Heal(percent ? playerDmg*amount : playerDmg+amount);
    }

}