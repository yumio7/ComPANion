using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int hitPoints = 5;
    [SerializeField] private float levelTime = 60.0f;

    public bool isGameOver;
    
    private float _countdown;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver) return;
        
        if (hitPoints <= 0)
        {
            isGameOver = true;
            GameOver();
        }

        if (_countdown <= 0)
        {
            isGameOver = true;
            GameWon();
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
    
    // TODO: convert countdown timer into chef movements

    public void TakeDamage()
    {
        hitPoints--;
    }
}
