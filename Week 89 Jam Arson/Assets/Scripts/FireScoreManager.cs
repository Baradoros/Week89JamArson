using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireScoreManager : MonoBehaviour
{

    public LevelManager levelManager;
    public Image[] images;

    private void Update() {
        Debug.Log(levelManager.score);
        for (int i = 0; i < images.Length; i++) {
            images[i].color = Color.black;
        }
        for (int i = 0; i < levelManager.score; i++) {
            images[i].color = Color.white;
        }
    }
}
