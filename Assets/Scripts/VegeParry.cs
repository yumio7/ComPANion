using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegeParry : MonoBehaviour
{
  [SerializeField] private AudioClip hitSound;
  
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (!other.CompareTag("Projectile")) return;
    AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position);
    other.GetComponent<ProjectileAttack>().Parry();
  }
}
