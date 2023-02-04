using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    public GameObject prefab;

    private float cooldown = 0.25f;

    private void Update()
    {
        cooldown -= Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;
        if (cooldown >= 0.0f)
            return;
        
        cooldown = 0.25f;
        Instantiate(prefab, collision.gameObject.transform.position, Quaternion.identity);
    }
}
