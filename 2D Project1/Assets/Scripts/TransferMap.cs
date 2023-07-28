using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferMap : MonoBehaviour
{
    [SerializeField]
    private string transferMapName;

    private PlayerController playerController;
    private CameraController cameraController;

    private void Awake()
    {
        cameraController = FindObjectOfType<CameraController>();
        playerController = FindObjectOfType<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerController.currentMapName = transferMapName;
            Debug.Log(transferMapName);
            LoadingSceneController.LoadScene(transferMapName);
            cameraController.cameraSize = false;
        }
    }
}
