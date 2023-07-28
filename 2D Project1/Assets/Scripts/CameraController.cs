using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector2 center;
    [SerializeField]
    private Vector2 size;

    private float height;
    private float width;

    public bool cameraSize = false;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    private void Update()
    {
        CameraLimitSize();
        //if (playerController.currentMapName == "Chapt1")
        //{
        //    if (cameraSize == false)
        //    {
        //        center.x = 13.5f;
        //        center.y = -1f;

        //        size.x = 50f;
        //        size.y = 25;
        //        cameraSize = true;
        //    }
        //}
    }

    private void CameraLimitSize()
    {
        if(cameraSize == false)
        {
            switch (playerController.currentMapName)
            {
                case "GameTownScene":
                    center.x = 2.12f;
                    center.y = 2f;
                    size.x = 42f;
                    size.y = 20f;
                    cameraSize = true;
                    Debug.Log("실행");
                    break;
                case "Chapter1Scene":
                    center.x = 13.5f;
                    center.y = -1f;
                    size.x = 50f;
                    size.y = 25;
                    cameraSize = true;
                    break;
            }
        }
    }

    //모든 Update 함수가 호출된 후, 마지막으로 호출됩니다.
    //주로 오브젝트를 따라가게 설정한 카메라는 LateUpdate 를 사용
    //카메라가 따라가는 오브젝트가 Update함수 안에서 움직일 경우가 있기 때문
    private void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y + 2, -10f);

        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, size);
    }
}
