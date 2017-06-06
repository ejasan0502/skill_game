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

    private Text skillInfoText;                 // Expected skill information from crafting
    private Text chanceText;                    // Text of chance to craft

    private float chanceToCraft = 100f;                                                         // Current chance to successfully craft the skill
    private Skill toCraft = null;                                                               // Current skill settings by combining skillGems
    private List<SkillGem> skillGems = new List<SkillGem>();                                    // Currently selected skill gems to combine
    private List<GameObject> gemsUI = new List<GameObject>();                                   // UI objects for gems
    private List<Inventory.InventoryItem> gems = new List<Inventory.InventoryItem>();           // All skillGems in player's inventory

    void Awake(){
        // Look for skillInfoText
        Transform trans = selectSkillGems.transform.Find("Craft Skill Info");
        if ( trans != null ) skillInfoText = trans.GetChild(0).GetComponent<Text>();

        // Look for chanceText
        trans = selectSkillGems.transform.Find("Chance of Success");
        if ( trans != null ) chanceText = trans.GetComponent<Text>();
    }

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
        foreach (SkillGem gem in skillGems){
            Player.instance.inventory.RemoveItem(gem, 1);
        }

        // Clear skillGems for next craft
        skillGems = new List<SkillGem>();

        if ( UnityEngine.Random.Range(0,100) < chanceToCraft ){
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
    // Add skillGem to pool based on index from gems list
    public void Select(int index){
        SkillGem skillGem = gems[index].item as SkillGem;

        // Select/Deselect skillGem
        if ( skillGems.Contains(skillGem) ){
            skillGems.Remove(skillGem);
            gemsUI[index].GetComponent<Image>().color = Color.white;
        } else {
            int skillGemVal = (int)skillGem.gemType;

            // Check if skillGem has any conflicting gems, if so remove them
            for (int i = skillGems.Count-1; i >= 0; i--){
                int gemVal = (int)skillGems[i].gemType;

                if ( skillGemVal < 10 ){
                    // skillGem is an element type skillGem
                    // Check if this gem is also an element type
                    if ( gemVal < 10 ){
                        // Remove gem
                        skillGems.RemoveAt(i);
                    }
                } else if ( skillGemVal == 10 || skillGemVal == 11 ){
                    // skillGem is a damage or heal effect
                    // Check if this gem is also a damage or heal effect
                    if ( gemVal == 10 || gemVal == 11 ){
                        // Remove gem
                        skillGems.RemoveAt(i);
                    }
                } else if ( skillGemVal == gemVal ){
                    // Remove skill gem since its of the same type
                    skillGems.RemoveAt(i);
                }
            }

            skillGems.Add(skillGem);

            for (int i = 0; i < gems.Count; i++){
                if ( skillGems.Contains(gems[i].item as SkillGem) ){
                    gemsUI[i].GetComponent<Image>().color = Color.yellow;   
                } else {
                    gemsUI[i].GetComponent<Image>().color = Color.white;   
                }
            }
        }

        UpdateSkill();
    }

    // Update the skill to craft
    // For UI purposes
    private void UpdateSkill(){
        // Clear last settings of the crafting skill
        toCraft = new Skill();

        // Apply skillGems to skill
        chanceToCraft = 0f;
        foreach (SkillGem gem in skillGems){
            gem.ApplyTo(toCraft);

            chanceToCraft += (int)gem.tier/skillGems.Count;
        }

        // Update success chance to craft
        chanceText.text = chanceToCraft + "% chance of success";

        // Update Skill Info UI
        string text = "";

        // Element and Graphics
        text += "Element: " + toCraft.elementType.ToString() + "\n";
        text += "Cast Effect: " + (toCraft.castEffect != null ? toCraft.castEffect.name : "None") + "\n";
        text += "Hit Effect: " + (toCraft.hitEffect != null ? toCraft.hitEffect.name : "None") + "\n";

        // Target Count
        text += "Target Count: " + toCraft.targetCount + "\n";

        // Effects
        text += "Effects: ";
        if ( toCraft.effects.Count > 0 ){
            text += "\n";
            for (int i = 0; i < toCraft.effects.Count; i++){
                text += (i+1) + ".) " + toCraft.effects[i].info + "\n";
            }
        } else {
            text += "None";
        }

        skillInfoText.text = text;
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
        
        skillGems = new List<SkillGem>();
        gems = Player.instance.inventory.items.Where<Inventory.InventoryItem>( (ii) => ii.item.itemType == ItemType.skillGem).ToList();
        

        // Create UI object of each skillGem in list
        float height = ((RectTransform)inventoryItem.transform).rect.height;
        contentTrans.sizeDelta = new Vector2(contentTrans.sizeDelta.x, height*skillGems.Count);
        float startY = contentTrans.rect.height/2.00f - height/2.00f;
        for (int i = 0; i < gems.Count; i++){
            GameObject o = Instantiate(inventoryItem);
            o.transform.SetParent(contentTrans);
            o.transform.localScale = Vector3.one;

            // Position ui element in scroll view
            RectTransform rt = (RectTransform)o.transform;
            rt.anchoredPosition = new Vector2(0f,startY-i*height);

            // Icon
            Image icon = (Image) o.transform.GetChild(0).GetComponent<Image>();
            icon.sprite = gems[i].item.Icon;

            // Name
            Text nameText = (Text) o.transform.GetChild(1).GetComponent<Text>();
            nameText.text = gems[i].item.name;

            // Amount
            Text amount = (Text) o.transform.GetChild(2).GetComponent<Text>();
            amount.text = ((SkillGem)gems[i].item).gemType.ToString();

            // Add listener when selecting this skillGem
            Button button = o.GetComponent<Button>();
            int index = i;
            button.onClick.AddListener(() => Select(index));

            gemsUI.Add(o);
        }
    }
}