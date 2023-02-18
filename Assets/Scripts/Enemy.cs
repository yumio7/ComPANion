using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    [SerializeField] SpawnNode _spawnNode;
    [SerializeField] Vector2 initialDestination;
    [SerializeField] private float speed;
    private void Awake()
    {
        _spawnNode = this.GetComponentInParent<SpawnNode>();
        initialDestination = _spawnNode.startPosition.position;
    }

    private void Update()
    {
        var step = speed * Time.deltaTime;
        this.transform.position = Vector2.MoveTowards(transform.position, initialDestination, step);
    }
}
