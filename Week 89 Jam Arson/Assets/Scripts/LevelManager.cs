using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int level;
    public int score = 0;
    public static bool PauseLevel = false;

    // The GameObject that has all the building Prefabs attached.
    public GameObject BuildingListObject;
    public int initialBuildings;
    public int buildingsLeft;

    public float currentTime = 0.0f;

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

    void Update()
    {
        if (!PauseLevel)
        {
            currentTime += Time.deltaTime;
            buildingsLeft = BuildingListObject.GetComponentsInChildren<FlammableItem>().Length;
            score = (initialBuildings - buildingsLeft) * 50;
        }
    }

    public void SaveGame() {
        SaveObject saveObject;
        if (SaveSystem.Load(out saveObject)) {
            saveObject.LevelDataArray[level - 1].score = Mathf.RoundToInt(((float)score / 50) * 3 / initialBuildings); //converting to 3 star system
            if (saveObject.LevelDataArray[level - 1].score > 0) {
                saveObject.LevelDataArray[level].isUnlocked = true;
            }
            SaveSystem.Save(saveObject);
        }
    }
}

