using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VegeAttack : MonoBehaviour
{
  [SerializeField] private AudioClip hitSound;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (!other.CompareTag("Enemy")) return;
    AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position);
    other.GetComponent<Enemy>().TakeDamage(1);
  }
}
