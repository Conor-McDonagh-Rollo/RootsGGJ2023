using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterChance : MonoBehaviour
{
    private float cooldown = 0.25f;

    private void Update()
    {
        cooldown -= Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (cooldown >= 0.0f)
            return;
        cooldown = 0.25f;

        // SHOULD WE HAVE AN ENCOUNTER? RESET CHANCE IF NO
        int chance = Random.Range(0, 15);
        if (chance != 14)
            return;

        Encounter();
    }

    void Encounter()
    {
        SaveLoad.GetInstance().Save();
        SceneManager.LoadScene(2);
    }
}
