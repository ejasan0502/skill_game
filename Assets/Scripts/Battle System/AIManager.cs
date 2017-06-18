using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles the AI system during battle
public class AIManager {

    // Fill actions list with monster actions
    public void SetupActions(List<CharacterAction> actions, List<Monster> monsters, List<CharacterObj> playerChars){
        // Attack random playerChar
        foreach (Monster m in monsters){
            actions.Add( new CharacterAction(m, ActionType.attack) );
            actions[actions.Count-1].targets.Add(playerChars[Random.Range(0,playerChars.Count)]);
        }

        GameObject.FindObjectOfType<BattleManager>().OnEnemyTurnCompleted();
    }

}