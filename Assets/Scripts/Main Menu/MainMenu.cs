﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void playGame()
    {
        SceneManager.LoadScene(2);
    }
    public void openOptions()
    {
        SceneManager.LoadScene(1);
    }
    public void optionstoMenu()
    {
        SceneManager.LoadScene(0);
    }
}