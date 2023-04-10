using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellManagerGeneral : MonoBehaviour
{

    public GameObject spellPrefab;
    public string spellName;

    public float cooldown;
    public float timer;
    public GameObject player;



    // Start is called before the first frame update
    void Awake()
    {
        timer = 0;

        cooldown = spellPrefab.GetComponent<spellBase>().getSpellCooldown();
        spellName = spellPrefab.GetComponent<spellBase>().getSpellName();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            GameObject spellGO = Instantiate(spellPrefab, transform.position, transform.rotation);

            spellGO.transform.rotation = player.GetComponent<playerMovement>().circle.rotation;
            spellGO.GetComponent<spellBase>().setSpellDirection(player.GetComponent<playerMovement>().circle.up);
            spellGO.GetComponent<spellBase>().setDamage(player.GetComponent<playerStats>().damage);

            timer = cooldown;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}