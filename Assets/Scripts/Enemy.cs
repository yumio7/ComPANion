using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private SpawnNode _spawnNode;
    private Vector3 _initialDestination;
    
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float distance;

    [Header("Projectile")] 
    [SerializeField] private GameObject projectile;
    [SerializeField] private float throwDistance;

    [Header("Enemy Sprites")] 
    [SerializeField] private Sprite badVegetableSprite;
    [SerializeField] private Sprite ratSprite;
    [SerializeField] private Sprite badChefSprite;

    private BoxCollider2D _collider2D;
    
    private Transform _target;
    private Vector3 _destination;
    
    private bool _hasChanged;
    private bool _isChef;

    private bool _canThrow;

    private int _healthPoints;

    private int _randomSelector;

    public enum EnemyType
    {
        Rat,
        Vegetable,
        Chef
    }

    [SerializeField] public EnemyType enemy;
    private void Awake()
    {
        _spawnNode = this.GetComponentInParent<SpawnNode>();
        _initialDestination = _spawnNode.startPosition.position;
        _destination = _initialDestination;
        _hasChanged = false;

        _collider2D = gameObject.GetComponent<BoxCollider2D>();
        
        _randomSelector = Random.Range(0, 10);

        if (_randomSelector < 2)
            enemy = EnemyType.Chef;
        else if(_randomSelector < 6)
            enemy = EnemyType.Rat;
        else if (_randomSelector < 10)
            enemy = EnemyType.Vegetable;

        switch (enemy)
        {
            case EnemyType.Rat:
                _target = GameObject.FindGameObjectWithTag("Player").transform;
                _isChef = false;
                _healthPoints = 1;
                gameObject.GetComponent<SpriteRenderer>().sprite = ratSprite;
                _collider2D.size = new Vector2(0.6475344f, 0.6807499f);
                break;
            case EnemyType.Vegetable:
                _target = GameObject.FindGameObjectWithTag("Chef").transform;
                _isChef = false;
                _healthPoints = 1;
                gameObject.GetComponent<SpriteRenderer>().sprite = badVegetableSprite;
                _collider2D.size = new Vector2(0.6095753f, 1.16f);
                break;
            case EnemyType.Chef:
                _target = GameObject.FindGameObjectWithTag("Chef").transform;
                _isChef = true;
                _healthPoints = 2;
                _canThrow = true;
                gameObject.GetComponent<SpriteRenderer>().sprite = badChefSprite;
                _collider2D.offset = new Vector2(0.1853431f, -0.09696102f);
                _collider2D.size = new Vector2(0.5031262f, 1.477449f);
                break;
        }
    }

    private void Update()
    {
        if (_healthPoints <= 0)
        {
            Destroy(gameObject);
            // TODO: add little shrink death animation thing
        }
        
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
                if (_canThrow)
                {
                    ThrowProjectile();
                    _canThrow = false;
                    Invoke(nameof(ResetThrow), 4);
                }
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
        var transform1 = transform;
        var position = transform1.position;
        Vector3 throwDirection = _destination - position;
        Instantiate(projectile, position + (throwDirection.normalized * throwDistance), transform1.rotation);
    }

    private void ResetThrow()
    {
        Debug.Log("Throwing");
        _canThrow = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Projectile")) return;
        ProjectileAttack proj = col.GetComponent<ProjectileAttack>();
        if(!proj.isParried) return;
        print("Oop " + _healthPoints);
        TakeDamage(2);
        if(enemy == EnemyType.Chef)
            Destroy(col.gameObject);
    }
}
