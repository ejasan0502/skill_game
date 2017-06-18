using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Handles turn by turn combat between players and/or ai
// This system uses the speed of each character to determine which character takes priority.
// Note: All monster sprites MUST be below the monsterSpawn width and height divided by MAX_MONSTER_COUNT
public class BattleManager : MonoBehaviour {

    private const int MAX_MONSTER_COUNT = 5;

    public float playerSpacing = 2f;            // Space between player characters
    public BattlePhase battlePhase;             // Current phase of battle

    [Header("Object References")]
    public RectTransform list;                  // Scrollview for list
    public GameObject listItem;                 // Object reference to an item in list
    public GameObject battleLog;                // Logs every action performed by a character
    public GameObject battleMenu;               // Battle UI elements in overlay canvas
    public RectTransform monsterSpawn;          // Position in camera of where monsters will appear
    public Transform playerStartPos;            // Position of where to place the first player character

    private AIManager aiManager;                // Handles AI
    private Monster[] monsterPool;              // Pool of all monsters that the player might face depending on region
    private List<Monster> monsters;             // List of monster objects in battle
    private List<CharacterObj> playerChars;     // List of all player objects in battle

    private GameObject playerActionSelect;      // BattleMenu for Action select
    private GameObject listScroll;              // Scrollview for list
    private int selectedCharacter = 0;          // Currently selected character for the player to select an action for
    private List<CharacterAction> actions;      // List of all actions that will be performed in a battle round
    private List<GameObject> listObjs;          // List of skills or items from list objects
    private List<Consumable> items;             // List of all consumables in player's inventory
    private bool onAnimEnd = false;             // Determines whether to continue to next animation 

    private CharacterAction CurrentAction {
        get {
            return actions[actions.Count-1];
        }
    }

    void Awake(){
        // Fill monsterPool with monsters depending on region
        monsterPool = Resources.LoadAll<Monster>(Player.instance.RegionPath);

        // Initialize managers
        aiManager = new AIManager();

        playerActionSelect = battleMenu.transform.Find("ActionMenu").gameObject;
        listScroll = list.parent.parent.gameObject;
    }
    void Start(){
        GenerateCharacters();
        PlayerAction();
    }

    // Create player characters/monsters for battle
    private void GenerateCharacters(){
        // Instantiate player characters based on character name
        playerChars = new List<CharacterObj>();
        for (int i = 0; i < Player.instance.characters.Count; i++){
            GameObject o = (GameObject) Instantiate(Resources.Load("Characters/"+Player.instance.characters[i].name));
            o.name = i+"";

            CharacterObj co = o.GetComponent<CharacterObj>();
            co.character = Player.instance.characters[i];

            playerChars.Add(co);
        }

        // Space player characters based on startPos
        // Assume that all characters are of same width
        float width = ((RectTransform)playerChars[0].transform).rect.width;
        for (int i = 0; i < playerChars.Count; i++){
            playerChars[i].transform.position = playerStartPos.position + new Vector3(i*(width+playerSpacing),0,0);
        }

        // Instantiate monster objects
        monsters = new List<Monster>();
        int monsterCount = Random.Range((int)Player.instance.currentRegion, MAX_MONSTER_COUNT);
        width = 0f;
        for (int i = 0; i < monsterCount; i++){
            Monster o = Instantiate(monsterPool[Random.Range(0,monsterPool.Length)]);
            o.transform.SetParent(monsterSpawn);
            o.name = (i+playerChars.Count)+"";

            width += ((RectTransform)o.transform).rect.width;

            monsters.Add(o);
        }

        // Space monsters based on size of each
        float space = (monsterSpawn.rect.width - width) / (monsterCount+1);
        float x = -monsterSpawn.rect.width/2.00f;
        for (int i = 0; i < monsterCount; i++){
            float w1 = i > 0 ? ((RectTransform)monsters[i-1].transform).rect.width/2.00f : 0f;
            float w2 = ((RectTransform)monsters[i].transform).rect.width/2.00f;
            x += w1 + space + w2;

            monsters[i].transform.localPosition = new Vector3(x,0,0);
        }
    }
    // Generate list of skills
    private void ShowSkills(){
        // Clear listObjs
        if ( listObjs != null && listObjs.Count > 0 ){
            for (int i = listObjs.Count-1; i >= 0; i--){
                Destroy(listObjs[i]);
            }
        }
        listObjs = new List<GameObject>();

        // Create list of skills
        float height = ((RectTransform)list.parent).rect.height;
        list.sizeDelta = new Vector2(list.sizeDelta.x, height*CurrentAction.character.skills.Count);
        float startY = list.rect.height/2.00f - height/2.00f;
        for (int i = 0; i < CurrentAction.character.skills.Count; i++){
            GameObject o = Instantiate(listItem);
            o.transform.SetParent(list);
            o.transform.localScale = Vector3.one;

            // Position ui element in scroll view
            RectTransform rt = (RectTransform)o.transform;
            rt.anchoredPosition = new Vector2(0f,startY-i*height);

            // Icon
            Image icon = (Image) o.transform.Find("icon").GetComponent<Image>();
            icon.sprite = CurrentAction.character.skills[i].icon;

            // Name
            Text nameText = (Text) o.transform.Find("name").GetComponent<Text>();
            nameText.text = CurrentAction.character.skills[i].name;

            // Button
            Button button = (Button) o.GetComponent<Button>();
            int index = i;
            button.onClick.AddListener(() => SetSkill(index));

            listObjs.Add(o);
        }
    }
    // Generate list of usable items
    private void ShowItems(){
        // Clear listObjs
        if ( listObjs != null && listObjs.Count > 0 ){
            for (int i = listObjs.Count-1; i >= 0; i--){
                Destroy(listObjs[i]);
            }
        }
        listObjs = new List<GameObject>();

        // Create list of skills
        List<Inventory.InventoryItem> inventoryItems = Player.instance.inventory.items.Where((i) => i.item.itemType == ItemType.consumable).ToList();
        items = new List<Consumable>();
        foreach (Inventory.InventoryItem ii in inventoryItems){
            items.Add(ii.item as Consumable);
        }

        float height = ((RectTransform)list.parent).rect.height;
        list.sizeDelta = new Vector2(list.sizeDelta.x, height*items.Count);
        float startY = list.rect.height/2.00f - height/2.00f;
        for (int i = 0; i < items.Count; i++){
            GameObject o = Instantiate(listItem);
            o.transform.SetParent(list);
            o.transform.localScale = Vector3.one;

            // Position ui element in scroll view
            RectTransform rt = (RectTransform)o.transform;
            rt.anchoredPosition = new Vector2(0f,startY-i*height);

            // Icon
            Image icon = (Image) o.transform.Find("icon").GetComponent<Image>();
            icon.sprite = items[i].Icon;

            // Name
            Text nameText = (Text) o.transform.Find("name").GetComponent<Text>();
            nameText.text = items[i].name;

            // Button
            Button button = (Button) o.GetComponent<Button>();
            int index = i;
            button.onClick.AddListener(() => SetItem(index));

            listObjs.Add(o);
        }
    }
    // Setup UI to allow player to perform actions for their characters
    private void PlayerAction(){
        // Display actions
        battleMenu.SetActive(true);

        // Reset selectedCharacter
        selectedCharacter = 0;
        
        // Clear actionsList
        actions = new List<CharacterAction>();

        SetPhase(BattlePhase.actionSelect);
    }
    // Move to next playerCharacter
    private void NextPlayerCharacter(){
        selectedCharacter++;

        // Check if the player selected actions for all their characters
        if ( selectedCharacter >= playerChars.Count ){
            // End player turn
            SetPhase(BattlePhase.enemyTurn);
        } else {
            SetPhase(BattlePhase.actionSelect);
        }
    }
    // Set battle phase and update UI accordingly
    private void SetPhase(BattlePhase phase){
        battlePhase = phase;

        if ( battlePhase == BattlePhase.actionSelect ){
            playerActionSelect.SetActive(true);
            battleLog.SetActive(false);
            listScroll.SetActive(false);
        } else if ( battlePhase == BattlePhase.battle ){
            playerActionSelect.SetActive(false);
            battleLog.SetActive(true);
            listScroll.SetActive(false);

            SortActions();
            StartCoroutine(Battle());
        } else if ( battlePhase == BattlePhase.end ){

        } else if ( battlePhase == BattlePhase.itemSelect || battlePhase == BattlePhase.skillSelect ){
            playerActionSelect.SetActive(false);
            battleLog.SetActive(false);
            listScroll.SetActive(true);
        } else if ( battlePhase == BattlePhase.targetSelect ){
            playerActionSelect.SetActive(false);
            battleLog.SetActive(false);
            listScroll.SetActive(false);
        } else if ( battlePhase == BattlePhase.enemyTurn ){
            EventManager.AddEventHandler("OnEnemyTurnComplete", OnEnemyTurnComplete);
            aiManager.SetupActions(actions, monsters, playerChars);
        }
    }
    // Play through battle
    private IEnumerator Battle(){
        yield break;
    }
    // Sort actions list based on characters speed
    private void SortActions(){
        List<CharacterAction> ordered = actions.OrderBy((a) => a.character.combatStats.spd).ToList();
        actions = ordered;
    }

    // All actions a player character can make.
    // Perform a basic attack to a target.
    public void Attack(){
        CharacterAction duplicate = actions.Where<CharacterAction>((ca) => ca.characterObj == playerChars[selectedCharacter]).FirstOrDefault();
        if ( duplicate != null ){
            actions.Remove(duplicate);
        }

        Debug.Log(playerChars[selectedCharacter].name + " will attack.");
        actions.Add( new CharacterAction(playerChars[selectedCharacter], ActionType.attack) );
        SetPhase(BattlePhase.targetSelect);
    }
    // Cast a spell on a target or targets.
    public void Cast(){
        CharacterAction duplicate = actions.Where<CharacterAction>((ca) => ca.characterObj == playerChars[selectedCharacter]).FirstOrDefault();
        if ( duplicate != null ){
            actions.Remove(duplicate);
        }

        Debug.Log(playerChars[selectedCharacter].name + " will cast a spell.");
        actions.Add( new CharacterAction(playerChars[selectedCharacter], ActionType.cast) );
        SetPhase(BattlePhase.skillSelect);

        // Show list of skills to select
        ShowSkills();
    }
    // Use an item on a target or targets
    public void Use(){
        CharacterAction duplicate = actions.Where<CharacterAction>((ca) => ca.characterObj == playerChars[selectedCharacter]).FirstOrDefault();
        if ( duplicate != null ){
            actions.Remove(duplicate);
        }

        Debug.Log(playerChars[selectedCharacter].name + " will use an item.");
        actions.Add( new CharacterAction(playerChars[selectedCharacter], ActionType.use) );
        SetPhase(BattlePhase.itemSelect);

        ShowItems();
    }
    // Run away from battle. Automatically ends players turn with a chance to e xit battle. 
    public void Run(){

    }

    // Set target for selected character based on index of targetable
    public void SetTarget(int index){
        if ( battlePhase != BattlePhase.targetSelect ) return;

        if ( index >= playerChars.Count ){
            // Player is targetting a monster
            index = index - playerChars.Count;

            if ( CurrentAction.targets.Contains(monsters[index]) ){
                // Remove target
                Debug.Log("Removed monster " + index + " from targets");
                CurrentAction.targets.Remove(monsters[index]);
            } else {
                // Add target
                Debug.Log("Added monster " + index + " to targets");
                CurrentAction.targets.Add(monsters[index]);
            }
        } else {
            // Player is targetting a player character
            if ( CurrentAction.targets.Contains(playerChars[index]) ){
                // Remove target
                Debug.Log("Removed player " + index + " from targets");
                CurrentAction.targets.Remove(playerChars[index]);
            } else {
                // Add target
                Debug.Log("Added player " + index + " to targets");
                CurrentAction.targets.Add(playerChars[index]);
            }
        }

        // Check if player selected enough targets
        if ( CurrentAction.IsAoe ){
            if ( CurrentAction.action == ActionType.cast ){
                if ( CurrentAction.targets.Count >= CurrentAction.skill.targetCount ){
                    NextPlayerCharacter();
                }
            } else {
                if ( CurrentAction.targets.Count >= CurrentAction.usable.targetCount ){
                    NextPlayerCharacter();
                }
            }
        } else {
            // Single target only
            NextPlayerCharacter();
        }
    }
    // Set skill for selected character based on index
    public void SetSkill(int index){
        CurrentAction.skill = CurrentAction.character.skills[index];
        Debug.Log("Casting " + CurrentAction.skill.name);
        SetPhase(BattlePhase.targetSelect);
    }
    // Set item for selected character based on index
    public void SetItem(int index){
        CurrentAction.usable = items[index];
        Debug.Log("Using " + CurrentAction.usable.name);
        SetPhase(BattlePhase.targetSelect);
    }

    // Start battle when aiManager is complete
    public void OnEnemyTurnComplete(object sender, MyEventArgs args){
        SetPhase(BattlePhase.battle);
        EventManager.RemoveEventHandler("OnEnemyTurnComplete", OnEnemyTurnComplete);
    }
    // Called when a sprite finishes an animation during battle coroutine
    public void OnAnimEnd(object sender, MyEventArgs args){

    }
}