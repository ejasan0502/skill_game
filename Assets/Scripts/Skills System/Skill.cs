using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defines the properties of a skill
[System.Serializable]
public class Skill {

    private const string DEFAULT_ICON = "Icons/Skills/default";

    // Graphic variables
    // These variables will be displayed on the UI
    public string name;
    public string description;
    public string iconPath;
    public Sprite icon {
        get {
            return Resources.Load<Sprite>(iconPath) ?? Resources.Load<Sprite>(DEFAULT_ICON);
        }
    }
    public GameObject castEffect;
    public GameObject hitEffect;

    // Effect variables
    // These variables directly applies to a target or targets
    public int targetCount;
    public ElementType elementType;
    public List<Effect> effects;

    public CharStats cost;

    public bool IsAoe {
        get {
            return targetCount > 1;
        }
    }

    public Skill(){
        name = "";
        description = "";
        iconPath = "";
        castEffect = null;
        hitEffect = null;
        targetCount = 1;
        elementType = ElementType.physical;
        effects = new List<Effect>();
    }
    public Skill(Skill skill){
        this.name = skill.name;
        this.description = skill.description;
        this.iconPath = skill.iconPath;
        this.castEffect = skill.castEffect;
        this.hitEffect = skill.hitEffect;
        this.targetCount = skill.targetCount;
        this.elementType = skill.elementType;
        this.effects = skill.effects;
        this.cost = skill.cost;
    }

    // Apply effects to target or targets
    public void Apply(Character caster, Character target){
        foreach (Effect effect in effects){
            effect.Apply(caster, this, target);
        }
    }
    public void Apply(Character caster, List<Character> targets){
        foreach (Character target in targets){
            Apply(caster, target);
        }
    }
}