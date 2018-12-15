using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] float cameraLerp;
    [SerializeField] Transform cameraPointRegular, cameraPointCombo, Player;

    Transform currentCameraPoint;

    Quaternion regularRotation, comboRotation, currentRotation, newRotation;

    Vector3 posDifference;

    Camera playerCamera;

    void Start()
    {
        playerCamera = GetComponent<Camera>();

        newRotation = Quaternion.Euler(Player.transform.rotation.x, Player.transform.rotation.y, Player.transform.position.z);
        currentCameraPoint = cameraPointRegular;

        regularRotation = cameraPointRegular.rotation;
        comboRotation = cameraPointCombo.rotation;

        currentRotation = regularRotation;
    }

    void Update()
    {
        
        playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, currentCameraPoint.position, cameraLerp);
        //playerCamera.transform.LookAt(Player);
        playerCamera.transform.rotation = Quaternion.Lerp(playerCamera.transform.rotation, Player.transform.rotation, cameraLerp);
    }

    /*public void UpdateCameraMode()
    {
        switch (LevelManager.instance.currentGameState)
        {

            case LevelManager.GameState.MENU:
                break;

            case LevelManager.GameState.DEFLECT:
                transform.position = LevelManager.instance.player.transform.position + posDifference;
                currentCameraPoint = cameraPointRegular;
                currentRotation = regularRotation;
                break;

            case LevelManager.GameState.COMBO:
                posDifference = LevelManager.instance.player.transform.position - transform.position;
                currentCameraPoint = cameraPointCombo;
                currentRotation = comboRotation;
                break;

        }
    }*/
}
