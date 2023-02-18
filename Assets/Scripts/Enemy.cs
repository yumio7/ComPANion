using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    private SpawnNode _spawnNode;
    private Vector3 _initialDestination;
    [SerializeField] private float speed;
    [SerializeField] private float distance;
    private Transform _target;
    private Vector3 _destination;
    private bool _hasChanged;
    private bool _isChef;

    private int _healthPoints;

    
    private enum EnemyType
    {
        Rat,
        Vegetable,
        Chef
    }

    [SerializeField] EnemyType enemy;
    private void Awake()
    {
        _spawnNode = this.GetComponentInParent<SpawnNode>();
        _initialDestination = _spawnNode.startPosition.position;
        _destination = _initialDestination;
        _hasChanged = false;

        switch (enemy)
        {
            case EnemyType.Rat:
                _target = GameObject.FindGameObjectWithTag("Player").transform;
                _isChef = false;
                _healthPoints = 1;
                break;
            case EnemyType.Vegetable:
                _target = GameObject.FindGameObjectWithTag("Chef").transform;
                _isChef = false;
                _healthPoints = 1;
                break;
            case EnemyType.Chef:
                _target = GameObject.FindGameObjectWithTag("Chef").transform;
                _isChef = true;
                _healthPoints = 2;
                break;
        }
    }

    private void Update()
    {
        var step = speed * Time.deltaTime;

        if (this.transform.position == _initialDestination)
            _hasChanged = true;

        if (_hasChanged)
        {
            _destination = _target.position;
        }

        if (_isChef && _destination == _target.position)
        {
            var currentDistance = Vector2.Distance(transform.position, _destination);
            if(currentDistance >= distance)
                this.transform.position = Vector2.MoveTowards(transform.position, _destination, step);
            else
            {
                ThrowProjectile();
            }
        }
        else
        {
            this.transform.position = Vector2.MoveTowards(transform.position, _destination, step);
        }
    }



    public void TakeDamage(int amt)
    {
        _healthPoints -= amt;
    }

    private void ThrowProjectile()
    {
        

    }
}
