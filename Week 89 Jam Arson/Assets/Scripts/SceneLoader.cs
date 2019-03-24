using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //The scene that is supposed to load
    [HideInInspector]
    public string sceneName;

    //When an animation ends proceed to the next scene
    public void OnCompleteAnimation()
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
