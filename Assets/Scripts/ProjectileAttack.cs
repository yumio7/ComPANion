using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    private Vector2 _direction;
    private GameObject _target;
    private float _angle;
    private void Awake()
    {
        var transform1 = this.transform;
        _target = GameObject.FindGameObjectWithTag("Chef");
        _direction = _target.transform.position - transform1.position;
        _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(_angle - 90, Vector3.forward);
    }
}
