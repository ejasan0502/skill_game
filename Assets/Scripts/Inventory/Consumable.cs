using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item {

    public int targetCount = 1;

    public bool IsAoe {
        get {
            return targetCount > 1;
        }
    }

}