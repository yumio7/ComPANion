using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegeParry : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (!other.CompareTag("Projectile")) return;
    other.GetComponent<ProjectileAttack>().Parry();

    // TODO: que audio clip
  }
}
