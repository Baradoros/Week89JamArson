using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Camera Manager that follows the target object and are clamped to the game extremities.
/// </summary>
public class CameraManager : MonoBehaviour
{
    #region Fields & Properties
    [Tooltip("The object that the camera should follow.")]
    public GameObject followTarget;
    [Tooltip("Move the camera to the RIGHT to get this value.")]
    public float xLevelRightExtreme = -3.31f;
    [Tooltip("Move the camera to the LEFT to get this value.")]
    public float xLevelLeftExtreme = 12.68f;
    [Tooltip("Trial and error smooth camera grow value")]
    public Vector2 growFunction = new Vector3(1.3f, 1f);
    [Tooltip("Whether the camera should follow the target or grow to show target")]
    public bool shouldFollowTarget = true;
    [Tooltip("Just an offset variable for full grown camera")]
    public float cameraGrowOffset = -0.5f;

    private Camera cameraComponent;
    private Vector3 currentCameraPosition;
    private float defaultOrthographicSize;
    #endregion

    #region In-built Function
    // Start is called before the first frame update
    void Start()
    {
        cameraComponent = gameObject.GetComponent<Camera>();
        if (cameraComponent == null)
        {
            Debug.LogError("Camera Manager is attached to a gameobject with NO camera component.");
        }
        else
        {
            currentCameraPosition = gameObject.transform.position;
            defaultOrthographicSize = cameraComponent.orthographicSize;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // To see the whole map.
        if (Input.GetButtonDown("Fire2"))
        {
            currentCameraPosition.x = (xLevelLeftExtreme + xLevelRightExtreme) / 2;
            currentCameraPosition.x = Mathf.Min(currentCameraPosition.x, (xLevelLeftExtreme - cameraComponent.orthographicSize / growFunction.x) - cameraGrowOffset);

            cameraComponent.orthographicSize = defaultOrthographicSize + ((currentCameraPosition.x - xLevelRightExtreme) / growFunction.x);
            currentCameraPosition.y = (cameraComponent.orthographicSize - defaultOrthographicSize) * growFunction.y;

            gameObject.transform.position = currentCameraPosition;
        }

        if (Input.GetButtonUp("Fire2"))
        {
            if (followTarget != null)
            {
                currentCameraPosition.x = Mathf.Max(followTarget.transform.position.x, xLevelRightExtreme);
            } else
            {
                currentCameraPosition.x = xLevelRightExtreme;
            }

            currentCameraPosition.x = Mathf.Min(currentCameraPosition.x, (xLevelLeftExtreme - cameraComponent.orthographicSize / growFunction.x) - cameraGrowOffset);

            cameraComponent.orthographicSize = defaultOrthographicSize + ((currentCameraPosition.x - xLevelRightExtreme) / growFunction.x);
            currentCameraPosition.y = (cameraComponent.orthographicSize - defaultOrthographicSize) * growFunction.y;

            gameObject.transform.position = currentCameraPosition;
        }

        if (followTarget != null && cameraComponent != null)
        {
            if (shouldFollowTarget)
            {
                float followTargetX = followTarget.transform.position.x;
                currentCameraPosition.x = Mathf.Max(followTargetX, xLevelRightExtreme);
                currentCameraPosition.x = Mathf.Min(currentCameraPosition.x, xLevelLeftExtreme);

                gameObject.transform.position = currentCameraPosition;
            }
            else
            {
                float followTargetX = followTarget.transform.position.x;
                currentCameraPosition.x = Mathf.Max(followTargetX, xLevelRightExtreme);
                currentCameraPosition.x = Mathf.Min(currentCameraPosition.x, (xLevelLeftExtreme - cameraComponent.orthographicSize / growFunction.x) - cameraGrowOffset);

                cameraComponent.orthographicSize = defaultOrthographicSize + ((currentCameraPosition.x - xLevelRightExtreme) / growFunction.x);
                currentCameraPosition.y = (cameraComponent.orthographicSize - defaultOrthographicSize) * growFunction.y;

                gameObject.transform.position = currentCameraPosition;
            }
        }
    }
    #endregion
}
