using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All gems can increase targetCount, can be amount or percent based
public enum SkillGemType {

    // Gems that modify element type, castEffect, and hitEffect
    aquamarine = 1,     // Ice
    sapphire = 2,       // Water
    amethyst = 3,       // Wind
    citrine = 4,        // Earth
    topaz = 5,          // Electric
    moonstone = 6,      // Magical
    pearl = 7,          // Physical
    emerald = 8,        // Poison
    ruby = 9,           // Fire
    
    spinel = 10,         // Damage Effect
    peridot = 11,        // Heal Effect

    // Buff Effects
    opal = 12,           // Buff Effect - Attributes
    lapis = 13,          // Buff Effect - CharStats
    morganite = 14,      // Buff Effect - CombatStats

    // Status Effects
    rubelite = 15,       // Burn/Bleed Status Effect
    jade = 16,           // Poison Status Effect
    paraiba = 17,        // Frost/Frozen Status Effect
    garnet = 18,         // Reflect/Heavy/Paralyze Effect
    diamond = 19,        // Regen/Invincible/Shield Effect
    onyx = 20,           // Petrify/Sleep/Confusion Status Effect

    // Blue
    alexandrite,
    tanzanite,
    turquoise,
    zircon,

    // Green
    tourmaline

}