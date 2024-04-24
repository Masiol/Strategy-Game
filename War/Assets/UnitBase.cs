using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Unit/Base")]


public class UnitBase : ScriptableObject
{
    public bool isPlayerUnit;
    public string unitName;
    public int commandPoints;
    public int baseHealth;
    public int baseDamage;    
    public float baseSpeed;
    public float baseAttackSpeed;
    public float baseArmor;
    
    public UnitRace race;
    public TypeUnit type;

    public int TotalHealth => baseHealth + race.additionalHealth;
    public int TotalDamage => baseDamage + race.additionalDamage;
    public float TotalSpeed => baseSpeed + race.additionalSpeed;

    public bool isUnlocked;
    public Sprite icon;


    public virtual void Attack()
    {
        Debug.Log($"{race.raceType} {unitName} attacks!");
    }
}
public enum TypeUnit
{
    Weak_Knight,
    Knight,
    Weak_Spearman,
    Spearman,
    Shield_Spearman,
    Mage,
    Archer,
    Axe_Man,
    ShieldMan,
    Calvary,
    Balista,
    Catapult,
    Trebuchet
}