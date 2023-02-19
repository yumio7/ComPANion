using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ProjectileAttack : MonoBehaviour
{
    [SerializeField] private float shootSpeed;
    
    private Vector2 _direction;
    private GameObject _target;
    private float _angle;
    private Vector3 _shootPoint;

    private LevelManager _lm;

    [HideInInspector] public bool isParried;
    
    private void Awake()
    {
        var transform1 = this.transform;
        _target = GameObject.FindGameObjectWithTag("Chef");
        var position = _target.transform.position;
        _shootPoint = position;
        _direction = position - transform1.position;
        _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(_angle - 90, Vector3.forward);

        _lm = FindObjectOfType<LevelManager>();
        isParried = false;
    }

    private void Update()
    {
        //if (PerfectParry.Waiting) return;
        
        transform.position = !isParried 
            ? Vector2.MoveTowards(transform.position, _shootPoint, shootSpeed / 10) 
            : Vector2.MoveTowards(transform.position, _shootPoint, -shootSpeed / 10);

        if (this.transform.position == _target.transform.position)
            _lm.TakeDamage();
        
        if(this.transform.position != _shootPoint) return;
        Destroy(gameObject);
    }

    public void Parry()
    {
        if (isParried) return;
        isParried = true;
        var t = transform;
        Vector3 angle = t.eulerAngles + (Vector3.forward * 180f);
        t.eulerAngles = angle;

        Destroy(gameObject, 10);
        // TODO: possibly tint sprite different color
    }
}
