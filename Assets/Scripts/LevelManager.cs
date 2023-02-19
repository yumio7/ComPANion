using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int hitPoints = 5;
    [SerializeField] private float levelTime = 90.0f;
    
    private float _cookPercent;
    private float _countdown;
    private bool _isGameOver;

    private ChefBehavior _cb;

    // Start is called before the first frame update
    void Start()
    {
        _isGameOver = false;
        _cookPercent = 0f;
        _cb = GameObject.FindGameObjectWithTag("Chef").GetComponent<ChefBehavior>();
    }

    // TODO: convert countdown timer into chef movements
    // Update is called once per frame
    void Update()
    {
        if (_isGameOver) return;

        print("Total: " + _countdown);
        print("Cook: " + _cookPercent);
        
        if (hitPoints <= 0)
        {
            _isGameOver = true;
            GameOver();
        }

        if (_countdown <= 0)
        {
            _isGameOver = true;
            GameWon();
        }

        if (_cb.letHimCook)
        {
            _cookPercent += Time.deltaTime;

            if (Math.Abs(_cookPercent - 33) > 0.1f || Math.Abs(_cookPercent - 66) > 0.1f)
            {
                _cb.letHimCook = false;
            }
        }

        _countdown -= Time.deltaTime;
    }

    private void GameOver()
    {
        // TODO: add game over scene
    }

    private void GameWon()
    {
        // TODO: add game won scene
    }

    public void TakeDamage()
    {
        hitPoints--;
    }
}
