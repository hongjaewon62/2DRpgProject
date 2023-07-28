using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    [SerializeField]
    private string startPoint;

    private PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        if (startPoint == playerController.currentMapName)
        {
            playerController.transform.position = this.transform.position;
        }
    }
}
