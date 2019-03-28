using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public LevelManager levelManager;
    public GameObject endScreenContainer;

    private bool isCheckingForBurningPieces = false;
    private int numberOfBurningBuildings = 0;
    private FlammableItem[] flammableItems;

    private void Start() {
        endScreenContainer.SetActive(false);
    }

    private void Update() {
        flammableItems = levelManager.BuildingListObject.GetComponentsInChildren<FlammableItem>();
        // If nothing has been lit on fire yet check for the first fire
        if (!isCheckingForBurningPieces) {
            for (int i = 0; i < flammableItems.Length; i++) {
                if (flammableItems[i]?.OnFire == true) {
                    isCheckingForBurningPieces = true;
                    Debug.Log("Found on fire object");
                    return;
                }
            }
        } else { // If something has been lit on fire check when there are no more fires
            for (int i = 0; i < flammableItems.Length; i++) {
                if (flammableItems[i]?.OnFire == true) {
                    Debug.Log("Things are still burning");
                    return;
                }
            }
            isCheckingForBurningPieces = false;
            EndGame();
        }
    }

    void EndGame() {
        Debug.Log("End Game");
        endScreenContainer.SetActive(true); 
        SlingSystem.pauseShooting = true;
        Time.timeScale = 0.00001f;
    }
}
