using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClicker : MonoBehaviour
{
    //Name of the scene that is supposed to load next
    public string sceneNameLoad;

    //Animator to fade in/out between scenes 
    public Animator animator;

    //Script that is responsible for changing the scene 
    public SceneLoader sl;

    //Function that is responsible for going to the next scene after a mouse click
    public void OnMouseDown()
    {
        sl.sceneName = sceneNameLoad;
        FadeToLevel();
    }
        
    void FadeToLevel()
    {
        animator.SetTrigger("Fade_Out");
    }

}
