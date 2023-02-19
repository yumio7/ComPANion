using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private int _hitPoints;
    [SerializeField] private int totalHp = 5;
    
    [SerializeField] private float totalCookTime = 60.0f;

    [Header("UI")] 
    [SerializeField] private Image cookBar;
    [SerializeField] private Image healthBar;

    // [Header("End Scenes")] 
    // [SerializeField] public Scene winScene;
    // [SerializeField] public Scene lossScene;
    
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
        _hitPoints = totalHp;
        healthBar.fillAmount = (_hitPoints / totalHp);
    }

    // TODO: convert countdown timer into chef movements
    // Update is called once per frame
    void Update()
    {
        if (_isGameOver) return;
        
        if (_hitPoints <= 0)
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
        cookBar.fillAmount = _cookPercent / 100;
        _cookTime += Time.deltaTime;

        if (!(_cookTime >= 20.0f)) return;
        HealDamage();
        _cb.letHimCook = false;
        _cookTime = 0.0f;
    }

    private void GameOver()
    {
        for (int i = 0; i < 100; i++)
        {
            print("you fucking LOSELSOELOSELSOESLELO");
        }
        // TODO: add game over scene
        SceneManager.LoadScene("Scenes/LossScene");
    }

    private void GameWon()
    {
        for (int i = 0; i < 100; i++)
        {
            print("you fucking win ayayyyayayayyaayyaya");
        }
        // TODO: add game won scene
        SceneManager.LoadScene("Scenes/WinScene");
    }

    public void TakeDamage()
    {
        _hitPoints--;
        _hitPoints = Mathf.Clamp(_hitPoints,0, totalHp);
        var healthCalc = (float)_hitPoints / (float)totalHp;
        healthBar.fillAmount = healthCalc;
        print("Health: " + _hitPoints);
    }

    public void HealDamage()
    {
        _hitPoints++;
        _hitPoints = Mathf.Clamp(_hitPoints,0, totalHp);
        var healthCalc = (float)_hitPoints / (float)totalHp;
        healthBar.fillAmount = healthCalc;
        print("Health: " + _hitPoints);
    }
}
