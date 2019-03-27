﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int level;
    public int score = 0;
    public float timeLimit = 20.0f;
    public static bool PauseLevel = false;

    // The GameObject that has all the building Prefabs attached.
    public GameObject BuildingListObject;
    private int initialBuildings;
    private int buildingsLeft;

    public float currentTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        SlingSystem.pauseShooting = false;
        PauseLevel = false;

        if (BuildingListObject == null)
        {
            Debug.LogError("No BuildingListObject found in LevelManager");
        }

        initialBuildings = BuildingListObject.GetComponentsInChildren<FlammableItem>().Length;
        if(initialBuildings == 0)
        {
            Debug.LogError("No Flammable Buildings found in BuildingListObject");
        } else
        {
            buildingsLeft = initialBuildings;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseLevel)
        {
            currentTime += Time.deltaTime;
            buildingsLeft = BuildingListObject.GetComponentsInChildren<FlammableItem>().Length;
            score = (initialBuildings - buildingsLeft) * 50;

            if (buildingsLeft == 0 || currentTime > timeLimit)
            {
                PauseLevel = true;
                SaveObject saveObject;
                if (SaveSystem.Load(out saveObject)) {
                    saveObject.LevelDataArray[level - 1].score = Mathf.RoundToInt(((float)score / 50) * 3 / initialBuildings); //converting to 3 star system
                    if(saveObject.LevelDataArray[level - 1].score > 0)
                    {
                        saveObject.LevelDataArray[level].isUnlocked = true;
                    }
                    SaveSystem.Save(saveObject);
                }
            }
        }
    }
}