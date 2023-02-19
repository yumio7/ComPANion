using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PerfectParry : MonoBehaviour
{
    [SerializeField] private float stopTime = 0.5f;
    
    public static bool Waiting = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Projectile")) return;
        FindObjectOfType<LevelManager>().HealDamage();
        HitStop(stopTime);

        // TODO: que audio clip
    }

    private void HitStop(float duration){
        if (Waiting) return;
        Time.timeScale = 0.0f;
        StartCoroutine(Wait(duration));
    }
    
    private IEnumerator Wait(float duration){
        Waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        Waiting = false;
    }
}
