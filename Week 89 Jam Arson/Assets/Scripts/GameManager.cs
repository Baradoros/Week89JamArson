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

    public static GameObject gm;

    public static SaveObject initialSaveObject = new SaveObject();

    public static SaveObject currentLoadedData;

    public Image blackImage;

    private void Start() {
        FadeFromBlack(1);
        initialSaveObject.LevelDataArray[0] = new LevelData(1, 0, true);
        initialSaveObject.LevelDataArray[1] = new LevelData(2, 0, false);
        initialSaveObject.LevelDataArray[2] = new LevelData(3, 0, false);
        initialSaveObject.LevelDataArray[3] = new LevelData(4, 0, false);
        initialSaveObject.LevelDataArray[4] = new LevelData(5, 0, false);

        if (!SaveSystem.SaveFilesExist())
        {
            SaveSystem.Save(initialSaveObject);
        }

        currentLoadedData = initialSaveObject;
    }

    public void UnlockAllLevels()
    {
        SaveObject saveObject = new SaveObject();
        saveObject.LevelDataArray[0] = new LevelData(1, 0, true);
        saveObject.LevelDataArray[1] = new LevelData(2, 0, true);
        saveObject.LevelDataArray[2] = new LevelData(3, 0, true);
        saveObject.LevelDataArray[3] = new LevelData(4, 0, true);
        saveObject.LevelDataArray[4] = new LevelData(5, 0, true);
        if (SaveSystem.Save(saveObject))
        {
            SaveSystem.Load(out currentLoadedData);
        }
    }

    public void LockAllLevels()
    {
        SaveObject saveObject = new SaveObject();
        saveObject.LevelDataArray[0] = new LevelData(1, 0, true);
        saveObject.LevelDataArray[1] = new LevelData(2, 0, false);
        saveObject.LevelDataArray[2] = new LevelData(3, 0, false);
        saveObject.LevelDataArray[3] = new LevelData(4, 0, false);
        saveObject.LevelDataArray[4] = new LevelData(5, 0, false);
        if (SaveSystem.Save(saveObject))
        {
            SaveSystem.Load(out currentLoadedData);
        }
    }

    public void LoadDataIfAny()
    {
        if (SaveSystem.SaveFilesExist())
        {
            SaveSystem.Load(out currentLoadedData);
        }
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

    // Streamline scene loading
    private IEnumerator LoadSceneDelayed(string name) {
        float delay = 1.0f;
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public void ReloadLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        LoadScene(scene.name);
    }

    public void LoadScene(string name) {
        Time.timeScale = 1.0f;
        StartCoroutine(LoadSceneDelayed(name));
    }

    public void QuitToMenu() {
        Time.timeScale = 1.0f; // In case game was paused when loading
        LoadScene("MainMenu");
    }

    public void QuitToDesktop() {
        Quit();
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
