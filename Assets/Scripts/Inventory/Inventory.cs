using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Saves and loads items carried by player
public class Inventory {

    private const int MAX_ITEM_COUNT = 99;

    public class InventoryItem {
        public Item item;
        public int amount;

        public InventoryItem(Item item, int amount){
            this.item = item;
            this.amount = amount;
        }
    }
    public List<InventoryItem> items;

    public Inventory(){
        items = new List<InventoryItem>();
    }

    // Add item into inventory
    public void AddItem(Item item, int amount){
        // Check if item exists already
        InventoryItem inventoryItem = items.Where<InventoryItem>((ii) => ii.item.name == item.name).FirstOrDefault();
        if ( inventoryItem != null ){
            if ( inventoryItem.item.stackable ){
                if ( inventoryItem.amount+amount > MAX_ITEM_COUNT ){
                    int excess = inventoryItem.amount+amount - MAX_ITEM_COUNT;
                    inventoryItem.amount = MAX_ITEM_COUNT;
                    items.Add(new InventoryItem(item,excess));
                } else 
                    inventoryItem.amount += amount;
            } else {
                for (int i = 0; i < amount; i++){
                    items.Add(new InventoryItem(item,1));
                }
            }
        } else {
            items.Add(new InventoryItem(item,amount));
        }
    }
    // Remove item from inventory
    public void RemoveItem(Item item, int amount){
        InventoryItem inventoryItem = items.Where<InventoryItem>((ii) => ii.item.name == item.name).FirstOrDefault();
        if ( inventoryItem != null ){
            inventoryItem.amount -= amount;
            if ( inventoryItem.amount < 1 ){
                items.Remove(inventoryItem);
            }
        }
    }

}