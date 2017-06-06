using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Object that can be killed or perform actions
[System.Serializable]
public class Character {

    // Graphics
    public string name;
    public string iconPath;
    public Sprite icon {
        get {
            return (Sprite)Resources.Load<Sprite>(iconPath) ?? (Sprite)Resources.Load<Sprite>("Icons/default");
        }
    }

    // Stat variables
    public AttributeStats attributes, maxAttributes;    // Base attributes, Attributes modified by equipment and skills
    public CharStats charStats, maxCharStats;           // Current character stats, Max character stats
    public CombatStats combatStats, maxCombatStats;     // Current combat stats, Max combat stats

    // Other
    public Dictionary<Buff,int> buffs;                  // List of all buffs currently on the character
    public Dictionary<StatusEffect,int> statusEffects;  // List of all status effects currently on the character
    public List<Skill> skills;                          // List of all skills available for the character

    public bool isAlive {
        get {
            return charStats.health > 0;
        }
    }
    public bool HasBuff(Buff buff){
        foreach (Buff b in buffs.Keys){
            if ( b == buff )
                return true;
        }

        return false;
    }
    public bool HasStatus(StatusEffect status){
        foreach (StatusEffect s in statusEffects.Keys){
            if ( s.status == status.status ){
                return true;
            }
        }
        return false;
    }

    public Character(){
        buffs = new Dictionary<Buff,int>();
        statusEffects = new Dictionary<StatusEffect,int>();
        skills = new List<Skill>();
    }

    // Inflict health and check for death
    public void Hit(ElementType hitType, float rawDmg){
        float def = hitType == ElementType.physical ? combatStats.physDef : combatStats.magDef;
        float inflictDmg = rawDmg - def;
        if ( inflictDmg < 1 ) inflictDmg = 1f;

        charStats.health -= inflictDmg;
        if ( !isAlive ){
            Death();
        }
    }
    // Replenish health and check for health cap
    public void Heal(float amount){
        charStats.health += amount;
        if ( charStats.health > maxCharStats.health ){
            charStats.health = maxCharStats.health;
        }
    }
    // Give character a buff
    public void AddBuff(Buff buff){
        buffs.Add(buff, buff.duration);
        UpdateStats();
    }
    // Give character a status effect
    public void AddStatus(StatusEffect status){
        statusEffects.Add(status, status.duration);
    }

    // Apply all status effects at the end turn
    // Make sure to remove status if duration ended
    public void ApplyStatus(){
        List<StatusEffect> effectsToRemove = new List<StatusEffect>();

        foreach (KeyValuePair<StatusEffect,int> statusEffect in statusEffects){
            statusEffect.Key.Apply(null,null,this);
            
            int duration = statusEffects[statusEffect.Key]-1;
            if ( duration < 1 ){
                effectsToRemove.Add(statusEffect.Key);
            } else {
                statusEffects[statusEffect.Key] = duration;
            }
        }

        foreach (StatusEffect statusEffect in effectsToRemove){
            statusEffects.Remove(statusEffect);
        }
    }

    // What to do upon character death
    private void Death(){

    }
    // Update all stats of character in order of: (TO DO)
    // Base attribute stats -> attribute stats from equipment and buffs -> character/combat stats from attribute stats
    // -> character/combat stats from equipment and buffs
    private void UpdateStats(){

    }

}