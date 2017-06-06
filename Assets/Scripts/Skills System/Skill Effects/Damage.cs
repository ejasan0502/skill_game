using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Damage effect
// Inflict health on target or targets over effect duration
public class Damage : Effect {

    public int hitCount = 1;
    public float minDmg, maxDmg;
    public bool percent;
    public EffectType effectType {
        get {
            return EffectType.damage;
        }
    }
    public string info {
        get {
            return string.Format("{0}" + (percent ? "% " : " ") + "to {1}" + (percent ? "% " : " ") + "damage. {2} hit(s)", minDmg, maxDmg, hitCount);
        }
    }

    public Damage(float minDmg, float maxDmg, int hitCount, bool percent){
        this.minDmg = minDmg;
        this.maxDmg = maxDmg;
        this.hitCount = hitCount;
        this.percent = percent;
    }

    public void Apply(Character caster, Skill skill, Character target){
        for (int i = 0; i < hitCount; i++){
            float playerDmg = skill.elementType == ElementType.physical ? caster.combatStats.PhysicalDamage : caster.combatStats.MagicalDamage;
            float skillDmg = Random.Range(minDmg,maxDmg);

            target.Hit(skill.elementType, percent ? playerDmg*skillDmg : playerDmg+skillDmg);
        }
    }

}