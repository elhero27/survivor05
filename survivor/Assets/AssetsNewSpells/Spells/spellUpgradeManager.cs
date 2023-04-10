using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellUpgradeManager : MonoBehaviour
{

    public bool[] spellsActive;
    public int[] spellsLevel;
    public spellManagerGeneral[] listOfSpells;

    // alles in scriptableobjects speichern und daraus abrufen, lvl upgrades name etc

    void Awake()
    {
        listOfSpells = GetComponents<spellManagerGeneral>();
        spellsActive = new bool[listOfSpells.length];
        spellsLevel = new int[listOfSpells.length];
        int spellNumber = 0;
        foreach (spellManagerGeneral spell in listOfSpells)
        {

            string spellNameAct = spell.spellPrefab.GetComponent<spellBase>().getSpellName();

            switch (spellNameAct)
            {
                case "Fireball":
                    spellsActive[spellNumber] = true;
                    spellsLevel[spellNumber] = 0;
                    spell.enabled = true;
                    break;
                case "ChainLightning":
                    spellsActive[spellNumber] = true;
                    spellsLevel[spellNumber] = 0;
                    spell.enabled = false;
                    break;
                case "Icicle":
                    spellsActive[spellNumber] = true;
                    spellsLevel[spellNumber] = 0;
                    spell.enabled = false;
                    break;
            }
            spellNumber++;
        }
    }





}
