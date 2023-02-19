using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegeParry : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D other)
  {
    for (int i = 0; i < 50; i++)
    {
      print("bingle");
    }
    if (!other.CompareTag("Projectile")) return;
    other.GetComponent<ProjectileAttack>().Parry();
    
    // TODO: que audio clip and poop
  }
}
