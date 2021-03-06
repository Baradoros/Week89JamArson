﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenu;
    public Animator panelAnim;

    // Broadcast events for game pausing. May not be needed but they're here just in case
    // Setting Time.timeScale = 0.00001f should be sufficient for pausing, but sometimes player controls still work during that and other things may need to happen when the game is paused (e.g. AutoSaving)
    public delegate void PauseAction();
    public static event PauseAction OnPaused;

    public delegate void ResumeAction();
    public static event ResumeAction OnResumed;

    public bool paused { get; private set; }

    // Ensure Pause Manu is disabled when a new scene is loaded
    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    private void Start() {
        if (pauseMenu.activeInHierarchy)
            pauseMenu.SetActive(false);
        paused = false;
    }

    private void Update() {
        if (Input.GetButtonUp("Cancel")) {
            if (!paused)
                PauseGame();
            else
                ResumeGame();
        }
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode) {
        if (pauseMenu.activeInHierarchy)
            pauseMenu.SetActive(false);
        paused = false;
    }

    public void PauseGame() {
        paused = true;
        OnPaused?.Invoke();
        pauseMenu.SetActive(true);
        panelAnim.SetTrigger("Open");
        Time.timeScale = 0.00001f;
        SlingSystem.pauseShooting = true;
    }

    public void ResumeGame() {
        paused = false;
        OnResumed?.Invoke();
        SlingSystem.pauseShooting = false;
        Time.timeScale = 1.0f;
    }
}
