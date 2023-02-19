using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int hitPoints = 5;
    
    [SerializeField] private float totalCookTime = 60.0f;
    
    private float _cookPercent;
    private float _cookTime;
    private float _actualCookTime;
    private bool _isGameOver;

    private ChefBehavior _cb;

    // Start is called before the first frame update
    void Start()
    {
        _isGameOver = false;
        _cookPercent = 0f;
        _cb = GameObject.FindGameObjectWithTag("Chef").GetComponent<ChefBehavior>();
        _cookTime = 0f;
        _actualCookTime = 0f;
    }

    // TODO: convert countdown timer into chef movements
    // Update is called once per frame
    void Update()
    {
        if (_isGameOver) return;
        
        print("Cook%: " + _cookPercent);
        
        if (hitPoints <= 0)
        {
            _isGameOver = true;
            GameOver();
        }

        if (_cookPercent >= 100)
        {
            _isGameOver = true;
            GameWon();
        }

        if (!_cb.letHimCook) return;
        _actualCookTime += Time.deltaTime;
        _cookPercent = (_actualCookTime / totalCookTime) * 100;
        _cookTime += Time.deltaTime;

        if (!(_cookTime >= 20.0f)) return;
        _cb.letHimCook = false;
        _cookTime = 0.0f;
    }

    private void GameOver()
    {
        // TODO: add game over scene
    }

    private void GameWon()
    {
        for (int i = 0; i < 100; i++)
        {
            print("you fucking win ayayyyayayayyaayyaya");
        }
        // TODO: add game won scene
    }

    public void TakeDamage()
    {
        hitPoints--;
    }
}
