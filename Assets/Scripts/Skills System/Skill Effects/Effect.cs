using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Interface of all known skill effects
public interface Effect {

    // Determines what kind of effect it is
    EffectType effectType { get; }

    // Apply to target/targets
    // Called when skill cast is successful
    void Apply(Character caster, Skill skill, Character target);

}