using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    [SerializeField] private float shootSpeed;
    
    private Vector2 _direction;
    private GameObject _target;
    private float _angle;
    private Vector3 _shootPoint;
    private void Awake()
    {
        var transform1 = this.transform;
        _target = GameObject.FindGameObjectWithTag("Chef");
        var position = _target.transform.position;
        _shootPoint = position;
        _direction = position - transform1.position;
        _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(_angle - 90, Vector3.forward);
        // Destroy(this.gameObject, 1);
    }

    private void Update()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, _shootPoint, shootSpeed / 10);
        
        if(this.transform.position == _shootPoint)
        {
            Destroy(gameObject);
        }
    }
}
