using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Saves the action of the character in battle
public class CharacterAction {

    public Character character;
    public CharacterObj characterObj;
    public ActionType action;
    public List<CharacterObj> targets;
    public Skill skill;
    public Consumable usable;

    public bool IsAoe {
        get {
            return (action == ActionType.cast && skill != null && skill.IsAoe) ||
                   (action == ActionType.use && usable != null && usable.IsAoe);
        }
    }

    public CharacterAction( CharacterObj characterObj, ActionType action){
        this.character = characterObj.character;
        this.characterObj = characterObj;
        this.action = action;

        this.targets = new List<CharacterObj>();
        this.skill = null;
        this.usable = null;
    }

}