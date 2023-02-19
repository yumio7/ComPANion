using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Transactions;
using UnityEngine;
using UnityEngine.Serialization;
using Random = Unity.Mathematics.Random;

/// <summary>
/// <para> chef is either 1.) cookin that shit up 2.) collecting ingredients or 3.)
/// teleporting to or from kitchen locations</para>
///
/// TODO: implement all of it
/// </summary>
public class ChefBehavior : MonoBehaviour
{
    // true when the chef is cookin da soup
    [HideInInspector] public bool letHimCook;
    
    private LevelManager _lm;
    private float _collectTimer;
    private bool _collecting;
    private Random _rnd;

    private static readonly Vector2 _colPoint0 = new Vector2(-4.0f, 3.0f);
    private static readonly Vector2 _colPoint1 = new Vector2(4.0f, 3.0f);
    private static readonly Vector2 _colPoint2 = new Vector2(7.0f, 0.0f);
    private static readonly Vector2 _colPoint3 = new Vector2(4.0f, -3.0f);
    private static readonly Vector2 _colPoint4 = new Vector2(-4.0f, -3.0f);
    private static readonly Vector2 _colPoint5 = new Vector2(-7.0f, 0.0f);

    private void Awake()
    {
        _lm = FindObjectOfType<LevelManager>();
        
        print("cooking??: " + letHimCook);
        letHimCook = true;
        
        _collectTimer = 10f;
        _collecting = false;
        _rnd = new Random((uint) DateTime.Now.Millisecond);
    }

    private void LateUpdate()
    {
        if (!letHimCook && _collectTimer >= 10f)
        {
            _collecting = true;
            Teleport();
        }

        if (!_collecting) return;
        _collectTimer -= Time.deltaTime;
        
        if (!(_collectTimer <= 0)) return;
        _collecting = false;
        _collectTimer = 10f;
        Return();
        letHimCook = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {   
        if (!col.gameObject.CompareTag("Enemy")) return;
        var enemy = col.GetComponent<Enemy>();

        if (enemy.enemy != Enemy.EnemyType.Vegetable) return;
        _lm.TakeDamage();
        Destroy(col.gameObject);
    }

    private void Teleport()
    {
        int next = _rnd.NextInt(0, 5);

        var t = transform;
        t.position = next switch
        {
            0 => _colPoint0,
            1 => _colPoint1,
            2 => _colPoint2,
            3 => _colPoint3,
            4 => _colPoint4,
            5 => _colPoint5,
            _ => t.position
        };
    }

    private void Return()
    {
        transform.position = Vector2.zero;
    }
}
