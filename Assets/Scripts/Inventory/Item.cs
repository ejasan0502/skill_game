using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defines what is an item
public class Item {

    public string name;
    public string description;
    public string iconPath;

    public Tier tier;
    public ItemType itemType;

    public Sprite Icon {
        get {
            return (Sprite)Resources.Load<Sprite>(iconPath) ?? (Sprite)Resources.Load<Sprite>("Icons/default");
        }
    }
    public bool stackable {
        get {
            return itemType == ItemType.consumable || itemType == ItemType.material;
        }
    }

    public Item(){
        name = "";
        description = "";
        iconPath = "";

        tier = Tier.common;
        itemType = ItemType.material;
    }
    public Item(string name, string description, string iconPath, Tier tier, ItemType itemType){
        this.name = name;
        this.description = description;
        this.iconPath = iconPath;
        this.tier = tier;
        this.itemType = itemType;
    }
    public Item(Item item){
        this.name = item.name;
        this.description = item.description;
        this.iconPath = item.iconPath;

        this.tier = item.tier;
        this.itemType = item.itemType;
    }

}