using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PerfectParry : MonoBehaviour
{
    [SerializeField] private float stopTime = 0.5f;
    
    public static bool Waiting = false;

    private bool _alreadyHit;

    private void Start()
    {
        _alreadyHit = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_alreadyHit) return;
        _alreadyHit = true;
        if (!other.CompareTag("Projectile")) return;
        FindObjectOfType<LevelManager>().HealDamage();
        HitStop(stopTime);
    }

    private void HitStop(float duration){
        if (Waiting) return;
        Time.timeScale = 0.0f;
        StartCoroutine(Wait(duration));
    }
    
    private IEnumerator Wait(float duration)
    {
        print("here1");
        Waiting = true;
        print("here2");
        yield return new WaitForSecondsRealtime(duration);
        print("here3");
        Time.timeScale = 1.0f;
        print("here4");
        Waiting = false;
        _alreadyHit = false;
    }
}
