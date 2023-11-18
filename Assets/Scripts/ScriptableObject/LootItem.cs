using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Loot
{
    public GameObject barang;
    public int lootChance;
}

[CreateAssetMenu]
public class LootItem : ScriptableObject
{
    public Loot[] loots;

    public GameObject lot()
    {
        int cumProb = 0;
        int currentProb = Random.Range(0, 100);
        for (int i = 0; i < loots.Length; i++)
        {
            cumProb += loots[i].lootChance;
            if (currentProb <= cumProb)
            {
                return loots[i].barang;
            }
        }
        return null;
    }
}
