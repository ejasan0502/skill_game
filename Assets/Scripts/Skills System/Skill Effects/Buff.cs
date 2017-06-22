using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Increases stats on target or targets
[System.Serializable]
public class Buff : Effect, IComparable {

    public bool percent;
    public int duration = 1;
    public AttributeStats attributes;
    public CharStats charStats;
    public CombatStats combatStats;

    public EffectType effectType {
        get {
            return EffectType.buff;
        }
    }
    public string info {
        get {
            string text = attributes.ToString();
            if ( charStats != null ) text += "," + charStats.ToString();
            if ( combatStats != null ) text += "," + combatStats.ToString();
            return text;
        }
    }

    public Buff(bool percent, int duration, AttributeStats attributes, CharStats charStats, CombatStats combatStats){
        this.percent = percent;
        this.duration = duration;
        this.attributes = attributes;
        this.charStats = charStats;
        this.combatStats = combatStats;
    }

    public void Apply(Character caster, Skill skill, Character target){
        if ( !target.HasBuff(this) ){
            target.AddBuff(this);
        }
    }

    public int CompareTo(object obj){
        if ( obj == null ) return 0;

        Buff otherBuff = obj as Buff;
        if ( otherBuff != null ){
            // Loop through each field and compare if either value is greater than 0
            // Buffs are considered similar when the same field is more than 0
            FieldInfo[] statFields1 = this.GetType().GetFields();
            FieldInfo[] statFields2 = otherBuff.GetType().GetFields();

            for (int i = 0; i < statFields1.Length; i++){
                FieldInfo[] buffFields1 = statFields1[i].GetType().GetFields();
                FieldInfo[] buffFields2 = statFields2[i].GetType().GetFields();

                for (int j = 0; j < buffFields1.Length; j++){
                    if ( (float)buffFields1[j].GetValue(statFields1[i]) > 0 && (float)buffFields2[j].GetValue(statFields2[i]) > 0 ){
                        return 1;
                    }
                }
            }
        }
        return 0;
    }

}