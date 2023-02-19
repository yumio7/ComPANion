using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

/// <summary>
/// <para> chef is either 1.) cookin that shit up 2.) collecting ingredients or 3.)
/// teleporting to or from kitchen locations</para>
///
/// TODO: implement all of it
/// </summary>
public class ChefBehavior : MonoBehaviour
{
    private LevelManager _levelManager;
    private void OnTriggerEnter2D(Collider2D col)
    {   
        if (!col.gameObject.CompareTag("Enemy")) return;
        Enemy enemy = col.GetComponent<Enemy>();

        if (enemy.enemy == Enemy.EnemyType.Vegetable)
        {
            _levelManager.TakeDamage();
        }
    }
}
