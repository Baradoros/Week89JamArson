using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnAnimationEnd : StateMachineBehaviour {

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Debug.Log("Disabling gameobject: " + animator.gameObject);
        animator.gameObject.SetActive(false);
    }

}
