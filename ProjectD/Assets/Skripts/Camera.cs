using UnityEngine;
using System.Collections;
using JetBrains.Annotations;

public class Camera : MonoBehaviour
{
    public Transform player1;
    public Transform player2;

    private float playerDistanceX = 0f;
    private float playerDistanceY = 0f;
    public static float playerDis= 0;

    public Camera m_OrthographicCamera;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerDistanceX = player2.transform.position.x - player1.transform.position.x;
        playerDistanceY = player2.transform.position.y - player1.transform.position.y;
        playerDistanceX = player1.transform.position.x + (playerDistanceX / 2);
        playerDistanceY = player1.transform.position.y + (playerDistanceY / 2);
        this.transform.position = new Vector3(playerDistanceX,playerDistanceY+3, -1f);

        playerDis = Vector3.Distance(player1.transform.position, player2.transform.position);
        if(playerDis >= 25 && UnityEngine.Camera.main.orthographicSize <= 15)
        {// 거리값이 25보다 크고 50보다 작으면 카메라크기 15로키우기
            UnityEngine.Camera.main.orthographicSize += 0.1f;
        }
        if (playerDis < 25 && UnityEngine.Camera.main.orthographicSize >= 10)
        {// 거리값이 25보다 작으면 카메라 크기 10으로 줄이기
            UnityEngine.Camera.main.orthographicSize -= 0.1f;
        }
    }
}
