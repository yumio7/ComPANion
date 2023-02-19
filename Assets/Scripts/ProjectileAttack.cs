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

    private LevelManager _lm;

    [HideInInspector] public bool _isParried;
    
    private void Awake()
    {
        var transform1 = this.transform;
        _target = GameObject.FindGameObjectWithTag("Chef");
        var position = _target.transform.position;
        _shootPoint = position;
        _direction = position - transform1.position;
        _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(_angle - 90, Vector3.forward);

        _lm = GameObject.FindObjectOfType<LevelManager>();
        _isParried = false;
        // Destroy(this.gameObject, 1);
    }

    private void Update()
    {
        if (!_isParried)
        {
            transform.position
                = Vector2.MoveTowards(transform.position, _shootPoint, shootSpeed / 10);
        }
        else
        {
            transform.position
                = Vector2.MoveTowards(transform.position, _shootPoint, -shootSpeed / 10);
        }

        if (this.transform.position != _shootPoint) return;
        _lm.TakeDamage();
        Destroy(gameObject);
    }

    public void Parry()
    {
        if (_isParried) return;
        _isParried = true;
        var t = transform;
        Vector3 angle = t.eulerAngles + (Vector3.forward * 180f);
        t.eulerAngles = angle;

        Destroy(gameObject, 10);
        // TODO: possibly tint sprite different color
    }
}
