using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FlameViewer: MonoBehaviour
{
    public int level = 1;

    Image[] FlameImages;
    Button ParentButton;

    // Start is called before the first frame update
    void Start()
    {
        FlameImages = gameObject.GetComponentsInChildren<Image>();
        ParentButton = gameObject.GetComponentInParent<Button>();
        if(FlameImages.Length == 0)
        {
            Debug.LogError("There are no images UI in " + gameObject);
        }
        if(ParentButton == null)
        {
            Debug.LogError("There is no parent button UI in " + gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.currentLoadedData != null)
        {
            if(!GameManager.currentLoadedData.LevelDataArray[level - 1].isUnlocked)
            {
                if (ParentButton.enabled)
                {
                    ParentButton.enabled = false;
                    Image[] images = ParentButton.GetComponentsInChildren<Image>();
                    foreach (Image image in images)
                    {
                        if (image != null)
                        {
                            image.color = new Color(0.25f, 0.25f, 0.25f, 0.25f);
                        }
                    }
                    for (int index = 0; index < 3; index++)
                    {
                        FlameImages[index].enabled = false;
                    }
                }
                return;
            }
            else
            {
                ParentButton.enabled = true;
                Image[] images = ParentButton.GetComponentsInChildren<Image>();
                foreach (Image image in images)
                {
                    if (image != null)
                    {
                        image.color = new Color(1f, 1f, 1f, 1f);
                    }
                }
            }
            for (int index = 0; index < 3; index++)
            {
                if(index < GameManager.currentLoadedData.LevelDataArray[level - 1].score)
                {
                    FlameImages[index].enabled = true;
                } else
                {
                    FlameImages[index].enabled = false;
                }
            }
        }
    }
}
