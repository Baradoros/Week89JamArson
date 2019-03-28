using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    public static MusicManager mm;
    public AudioSource menuTheme;
    public AudioSource gameLoop;

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }

    void Start() {
        // Singleton
        if (mm == null)
            mm = this;
        else if (mm != this)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name.Equals("MainMenu")) {
            menuTheme.Play();
            gameLoop.Stop();

        }
        else {
            if (!gameLoop.isPlaying)
                gameLoop.Play();
            menuTheme.Stop();
        }
    }
}
