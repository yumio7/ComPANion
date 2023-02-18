using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    [SerializeField] SpawnNode _spawnNode;
    [SerializeField] Vector3 initialDestination;
    [SerializeField] private float speed;
    private Transform target;
    private Vector3 _destination;
    private bool hasChanged;

    
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
        initialDestination = _spawnNode.startPosition.position;
        _destination = initialDestination;
        hasChanged = false;

        if (enemy == EnemyType.Rat)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else if (enemy == EnemyType.Vegetable || enemy == EnemyType.Chef)
        {
            target = GameObject.FindGameObjectWithTag("Chef").transform;
        }
    }

    private void Update()
    {
        var step = speed * Time.deltaTime;

        if (this.transform.position == initialDestination)
            hasChanged = true;

        if (hasChanged)
        {
            _destination = target.position;
        }
        this.transform.position = Vector2.MoveTowards(transform.position, _destination, step);
    }
}
