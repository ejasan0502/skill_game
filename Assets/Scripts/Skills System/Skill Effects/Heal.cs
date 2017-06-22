using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Increases current health on target or targets
[System.Serializable]
public class Heal : Effect {

    public float amount;
    public bool percent;

    public EffectType effectType {
        get {
            return EffectType.heal;
        }
    }
    public string info {
        get {
            return "Heals " + (percent ? amount + "%" : amount+"");
        }
    }

    public Heal(float amount, bool percent){
        this.amount = amount;
        this.percent = percent;
    }

    public void Apply(Character caster, Skill skill, Character target){
        target.Heal(percent ? target.maxCharStats.health*amount : amount);
    }

}