using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("AfrazTestScene");
    }

    public void LoadGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
