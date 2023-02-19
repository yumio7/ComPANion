using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    private Vector2 _direction;
    private GameObject _target;
    private float _angle;
    private Vector3 _additionEulerAngles;
    private Quaternion _currentRotation;
    private void Awake()
    {
        _currentRotation = this.transform.rotation;
        var transform1 = this.transform;
        _target = GameObject.FindGameObjectWithTag("Chef");
        _direction = _target.transform.position - transform1.position;
        _angle = Vector2.Angle(transform1.up, _direction);
        _additionEulerAngles = new Vector3(0, 0, _angle);
        _currentRotation.eulerAngles += _additionEulerAngles;
        this.transform.rotation = _currentRotation;
    }
}
