using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All gems can increase targetCount, can be amount or percent based
public enum SkillGemType {

    // Gems that modify element type, castEffect, and hitEffect
    aquamarine = 0,     // Ice
    sapphire = 0,       // Water
    amethyst = 0,       // Wind
    citrine = 0,        // Earth
    topaz = 0,          // Electric
    moonstone = 0,      // Magical
    pearl = 0,          // Physical
    emerald = 0,        // Poison
    ruby = 0,           // Fire
    
    spinel = 1,         // Damage Effect
    peridot = 2,        // Heal Effect

    // Buff Effects
    opal = 3,           // Buff Effect - Attributes
    lapis = 3,          // Buff Effect - CharStats
    morganite = 3,      // Buff Effect - CombatStats

    // Status Effects
    rubelite = 4,       // Burn/Bleed Status Effect
    jade = 4,           // Poison Status Effect
    paraiba = 4,        // Frost/Frozen Status Effect
    garnet = 4,         // Reflect/Heavy/Paralyze Effect
    diamond = 4,        // Regen/Invincible/Shield Effect
    onyx = 4,           // Petrify/Sleep/Confusion Status Effect

    // Blue
    alexandrite,
    tanzanite,
    turquoise,
    zircon,

    // Green
    tourmaline

}