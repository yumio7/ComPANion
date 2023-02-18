using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VegeAttack : MonoBehaviour
{
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Enemy"))
    {
      other.GetComponent<Enemy>().TakeDamage(1);
    }
  }
}
