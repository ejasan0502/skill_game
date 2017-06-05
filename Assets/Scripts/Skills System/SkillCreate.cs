using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Creates a custom skill
// This system uses Skill Gems that contains preset variables and combines them into a single skill. Each skill will
// require skill gems that cover all variables of the skill.
public class SkillCreate : MonoBehaviour {

    [Header("Object References")]
    public RectTransform contentTrans;
    public GameObject selectSkillGems;
    public GameObject saveSkill;
    public GameObject inventoryItem;

    private Skill toCraft = null;                               // Current skill settings by combining skillGems
    private List<SkillGem> skillGems = new List<SkillGem>();    // Currently selected skill gems to combine
    private List<GameObject> gemsUI = new List<GameObject>();

    // By default, this script is inactive on the UI canvas. To start crafting a skill, enable the gameObject this script is on
    void OnEnable(){
        selectSkillGems.SetActive(true);
        saveSkill.SetActive(false);

        GenerateList();
    }

    // UI Methods
    // Methods called from UI events
    // Display crafting animation
    public void Craft(){
        // Chance to craft skill depending on amount of skill gems and tiers of each skill gem
        // Remove skill gems from inventory
        float chanceToSucceed = 0f;
        foreach (SkillGem gem in skillGems){
            chanceToSucceed += (float)gem.tier/skillGems.Count;

            Player.instance.inventory.RemoveItem(gem, 1);
        }

        // Clear skillGems for next craft
        skillGems = new List<SkillGem>();

        if ( UnityEngine.Random.Range(0,100) < chanceToSucceed ){
            // Crafting successful
        }
    }
    // Give skill to character
    public void SaveSkill(Character character){
        // Make sure we have a skill to save
        if ( toCraft != null ){
            // Check if there is a duplicate skill
            Skill duplicate = character.skills.Where<Skill>( (s) => s.name == toCraft.name).FirstOrDefault();
            if ( duplicate == null ){
                character.skills.Add(new Skill(toCraft));
                toCraft = null;
            } else {
                // There cannot be skills with the same name, Replace or change name of skill
            }
        }
    }
    // Save skill as a rune to player's inventory
    public void SaveSkill(){
        // Make sure we have a skill to save
        if ( toCraft != null ){
            Player.instance.inventory.AddItem(new Rune(toCraft),1);
        }
    }

    // Adds skill gem to list to combine
    public void AddSkillGem(SkillGem gem){
        // Check if a similar skillGem exists in the list
        SkillGem skillGem = skillGems.Where<SkillGem>( (sg) => (int)sg.gemType == (int)gem.gemType).FirstOrDefault();
        if ( skillGem == null ){
            skillGems.Add(gem);
        } else {
            // Replace gem
            skillGems.Remove(skillGem);
            skillGems.Add(gem);
        }

        UpdateSkill();
    }
    // Remove a skill gem from list
    public void RemoteSkillGem(int index){
        if ( index < skillGems.Count ){
            skillGems.RemoveAt(index);
            UpdateSkill();
        }
    }

    // Update the skill to craft
    // For UI purposes
    private void UpdateSkill(){
        toCraft = new Skill();

        foreach (SkillGem gem in skillGems){
            gem.ApplyTo(toCraft);
        }
    }

    // Generate UI list of skillGems available to craft with
    private void GenerateList(){
        // Clear UI objects if any
        if ( gemsUI.Count > 0 ){
            for (int i = gemsUI.Count-1; i >= 0; i--){
                Destroy(gemsUI[i]);
            }
            gemsUI = new List<GameObject>();
        }

        // Fill skillGem list
        IEnumerable<Inventory.InventoryItem> gems = Player.instance.inventory.items.Where<Inventory.InventoryItem>( (ii) => ii.item.itemType == ItemType.skillGem);
        skillGems = new List<SkillGem>();
        foreach (Inventory.InventoryItem ii in gems){
            skillGems.Add(ii.item as SkillGem);
        }

        // Create UI object of each skillGem in list
        float height = ((RectTransform)inventoryItem.transform).rect.height;
        contentTrans.sizeDelta = new Vector2(contentTrans.sizeDelta.x, height*skillGems.Count);
        float startY = contentTrans.rect.height/2.00f - height/2.00f;
        for (int i = 0; i < skillGems.Count; i++){
            GameObject o = Instantiate(inventoryItem);
            o.transform.SetParent(contentTrans);
            o.transform.localScale = Vector3.one;

            // Position ui element in scroll view
            RectTransform rt = (RectTransform)o.transform;
            rt.anchoredPosition = new Vector2(0f,startY-i*height);

            //// Icon
            //Image icon = (Image) o.transform.GetChild(0).GetComponent<Image>();
            //icon.sprite = skillGems[i].item.Icon;

            //// Name
            //Text nameText = (Text) o.transform.GetChild(1).GetComponent<Text>();
            //nameText.text = skillGems[i].item.name;

            //// Amount
            //Text amount = (Text) o.transform.GetChild(2).GetComponent<Text>();
            //amount.text = skillGems[i].gemType.ToString();

            gemsUI.Add(o);
        }
    }
}