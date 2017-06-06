using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Properties of character
public class CharStats {
    
    public float health;
    public float mana;

    public float Sum {
        get {
            float sum = 0f;
            foreach (FieldInfo field in GetType().GetFields()){
                sum += (float)field.GetValue(this);
            }
            return sum;
        }
    }

    // Constructors
    public CharStats(){
        FieldInfo[] fields = GetType().GetFields();
        foreach (FieldInfo fi in fields){
            fi.SetValue(this, 0f);
        }
    }
    public CharStats(CombatStats stats){
        FieldInfo[] fields1 = GetType().GetFields();
        FieldInfo[] fields2 = stats.GetType().GetFields();

        for (int i = 0; i < fields1.Length; i++){
            fields1[i].SetValue(this, (float)fields2[i].GetValue(stats));
        }
    }

    public static CharStats operator+(CharStats stats1, CharStats stats2){
        CharStats stats = new CharStats();

        FieldInfo[] fields = stats.GetType().GetFields();
        FieldInfo[] fields1 = stats1.GetType().GetFields();
        FieldInfo[] fields2 = stats2.GetType().GetFields();
        for (int i = 0; i < fields.Length; i++){
            fields[i].SetValue(stats, (float)fields1[i].GetValue(stats1)+(float)fields2[i].GetValue(stats2));
        }

        return stats;
    }
    public static CharStats operator-(CharStats stats1, CharStats stats2){
        CharStats stats = new CharStats();

        FieldInfo[] fields = stats.GetType().GetFields();
        FieldInfo[] fields1 = stats1.GetType().GetFields();
        FieldInfo[] fields2 = stats2.GetType().GetFields();
        for (int i = 0; i < fields.Length; i++){
            fields[i].SetValue(stats, (float)fields1[i].GetValue(stats1)-(float)fields2[i].GetValue(stats2));
        }

        return stats;
    }

    public override string ToString(){
        string text = "";
        
        // Assume when a value is under 1, its a percentage
        foreach (FieldInfo field in GetType().GetFields()){
            float val = (float)field.GetValue(this);
            text += field.Name + " +" + val + (val < 1 && val > 0 ? "%" : "") + ", ";
        }

        return text;
    }
}