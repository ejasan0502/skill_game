using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Display and cast skills via UI list
public class SkillsUI : MonoBehaviour {

    public GameObject skillItem;
    public RectTransform content;
    public GameObject skillCreate;

    private Player player;
    private List<GameObject> skillItems;

    void Awake(){
        player = Player.instance;
        skillItems = new List<GameObject>();
    }
    void OnEnable(){
        GenerateList();
    }

    private void GenerateList(){
        // Clear list
        if ( skillItems.Count > 0 ){
            for (int i = skillItems.Count-1; i >= 0; i--){
                Destroy(skillItems[i]);
            }
        }
        skillItems = new List<GameObject>();

        float skillItemWidth = ((RectTransform)skillItem.transform).rect.width;
        float skillItemHeight = ((RectTransform)skillItem.transform).rect.height;
        Vector2 sizeDelta = content.sizeDelta;
        sizeDelta.y = player.character.skills.Count*skillItemHeight;

        for (int i = 0; i < player.character.skills.Count; i++){
            GameObject o = Instantiate(skillItem);
            o.transform.SetParent(content);
            o.transform.localScale = Vector3.one;

            o.transform.localPosition = new Vector3(skillItemWidth/2.00f, - i*skillItemHeight - skillItemHeight*0.75f, -1f);

            o.transform.Find("Name").GetComponent<Text>().text = player.character.skills[i].name;
            o.transform.Find("Icon").GetComponent<Image>().sprite = player.character.skills[i].icon;

            int index = i;
            o.GetComponent<Button>().onClick.AddListener(() => SelectSkill(index));

            skillItems.Add(o);
        }
    }

    public void SelectSkill(int index){
        player.Cast(index);
    }
    public void OpenSkillCreate(){
        skillCreate.SetActive(true);
        gameObject.SetActive(false);
    }
}