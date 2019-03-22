using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;


/// <summary>
/// Used for anything that needs to persist between scene like score, as well as any methods that need to be called from anywhere
/// </summary>
public class GameManager : MonoBehaviour {

    public static GameManager gm;

    public Image blackImage;

    void Awake() {
        // Singleton
        if (gm == null)
            gm = this;
        else if (gm != this)
            Destroy(this.gameObject);
    }

    void Start() {
        FadeFromBlack(1);
    }

    // These are separate methods so they can be used in OnClicked methods for UI buttons
    public void FadeToBlack(float speed = 1.0f) {
        // Speed cannot be negative
        speed = speed < 0 ? 0 : speed;
        blackImage.color = new Color(0, 0, 0, 0);
        blackImage.DOFade(1, speed);
    }

    public void FadeFromBlack(float speed = 1.0f) {
        // Speed cannot be negative
        speed = speed < 0 ? 0 : speed;
        blackImage.color = Color.black;
        blackImage.DOFade(0, speed);
    }

    private IEnumerator LoadSceneDelayed(string name, float time) {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    // Streamline scene loading
    public void LoadScene(string name, float delay = 0.0f) {
        if (delay > 0) {
            StartCoroutine(LoadSceneDelayed(name, delay));
        }
        else {
            SceneManager.LoadScene(name, LoadSceneMode.Single);
        }
    }

    void Quit() {

#if UNITY_EDITOR // If we're in Unity Editor, stop play mode
        if (UnityEditor.EditorApplication.isPlaying == true)
            UnityEditor.EditorApplication.isPlaying = false;
#else // If we are in a built application, quit to desktop
            Application.Quit();
#endif
    }
}
