using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VegeAttack : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (!other.CompareTag("Enemy")) return;
    other.GetComponent<Enemy>().TakeDamage(1);
  }
}
