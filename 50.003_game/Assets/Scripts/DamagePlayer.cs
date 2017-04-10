﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour {
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hit = collision.gameObject;
        var health = hit.GetComponent<Health>();
        var player = hit.GetComponent<PlayerScript>();
        if (health != null && player != null)
        {
            health.TakeDamage(damage);
            player.knockbackCount = player.knockbackLength;
            if (collision.transform.position.x < transform.position.x)
            {
                player.knockFromRight = true;
            }
            else
            {
                player.knockFromRight = false;
            }
        }
    }
}
