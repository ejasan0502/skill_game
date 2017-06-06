using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Applies a status type to a target or targets
// Only certain status effects will work with certain element types. Ex./ poison status effect will only work with a toxic element type skill
public class StatusEffect : Effect {

    public Status status;
    public int duration;
    public float chance;

    public bool percent;
    public EffectType effectType {
        get {
            return EffectType.status;
        }
    }
    public string info {
        get {
            return chance + "% chance to " + status.ToString();
        }
    }

    private Character caster = null;
    private Skill skill = null;

    public StatusEffect(){
        status = Status.bleed;
        duration = 0;
        percent = false;
    }
    public StatusEffect(StatusEffect statusEffect){
        this.status = statusEffect.status;
        this.duration = statusEffect.duration;
        this.percent = statusEffect.percent;
        this.caster = statusEffect.caster;
        this.skill = statusEffect.skill;
    }

    public void Apply(Character caster, Skill skill, Character target){
        if ( !target.HasStatus(this) ){
            // Add status to target if target does not have status applied
            this.caster = caster;
            this.skill = skill;

            target.AddStatus(new StatusEffect(this));
        } else {
            // Apply the effect of the status to the target (TO DO)
            switch ( (int)status ){
                case (int)StatusType.damageOverTime:
                break;
                case (int)StatusType.slow:
                break;
                case (int)StatusType.inaction:
                break;
                case (int)StatusType.boost:
                break;
            }
        }
    }

}