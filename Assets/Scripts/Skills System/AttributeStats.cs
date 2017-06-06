using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Stats that increases a character's combatStats and charStats;
public class AttributeStats {

    public int strength;        // Increases physical damage and physical defense
    public int vitality;        // Increases health
    public int intelligence;    // Increases magical damage
    public int wisdom;          // Increases mana and magical defense
    public int dexterity;       // Increases accuracy
    public int agility;         // Increases evasion

    public float Sum {
        get {
            float sum = 0f;
            foreach (FieldInfo field in GetType().GetFields()){
                sum += (float)field.GetValue(this);
            }
            return sum;
        }
    }

    public AttributeStats(){
        foreach (FieldInfo field in GetType().GetFields()){
            field.SetValue(this, 0);
        }
    }
    public AttributeStats(AttributeStats stats){
        FieldInfo[] fields1 = this.GetType().GetFields();
        FieldInfo[] fields2 = stats.GetType().GetFields();

        for (int i = 0; i < fields1.Length; i++){
            fields1[i].SetValue(this,(float)fields2[i].GetValue(stats));
        }
    }

    public static AttributeStats operator+(AttributeStats stats1, AttributeStats stats2){
        AttributeStats stats = new AttributeStats();

        FieldInfo[] fields = stats.GetType().GetFields();
        FieldInfo[] fields1 = stats1.GetType().GetFields();
        FieldInfo[] fields2 = stats2.GetType().GetFields();
        for (int i = 0; i < fields.Length; i++){
            fields[i].SetValue(stats, (int)fields1[i].GetValue(stats1)+(int)fields2[i].GetValue(stats2));
        }

        return stats;
    }
    public static AttributeStats operator-(AttributeStats stats1, AttributeStats stats2){
        AttributeStats stats = new AttributeStats();

        FieldInfo[] fields = stats.GetType().GetFields();
        FieldInfo[] fields1 = stats1.GetType().GetFields();
        FieldInfo[] fields2 = stats2.GetType().GetFields();
        for (int i = 0; i < fields.Length; i++){
            fields[i].SetValue(stats, (int)fields1[i].GetValue(stats1)-(int)fields2[i].GetValue(stats2));
        }

        return stats;
    }

    public override string ToString(){
        string text = "";

        // Assume when a value is under 1, its a percentage
        foreach (FieldInfo field in GetType().GetFields()){
            int val = (int)field.GetValue(this);
            text += field.Name + " +" + val + (val < 1 && val > 0 ? "%" : "") + ", ";
        }

        return text;
    }
}