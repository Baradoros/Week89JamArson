using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public LevelManager levelManager;

    public Text text;

    void Update()
    {
        text.text = (levelManager.initialBuildings - levelManager.buildingsLeft).ToString() + " / " + levelManager.initialBuildings.ToString();
    }
}
