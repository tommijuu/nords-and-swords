using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //This script is for camera and the buttons to follow the player

    private Transform playerTransform;

    public GameObject player;
    public GameObject enemy;
    public GameObject buttons;
    private Vector3 playerPos;

    public float cameraOffSet = 3.5f;

    public GameManager gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        //player = GameObject.FindGameObjectWithTag("Player");
        //enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (player != null) { playerTransform = player.transform; }

        buttons = GameObject.Find("CanvasButtons").transform.GetChild(0).gameObject;
    }

    void LateUpdate()
    {
        if (player != null && enemy != null)
        {

            //Updating Camera position
            Vector3 playerPos = player.transform.position;
            Vector3 enemyPos = enemy.transform.position;
            float distanceX = (playerPos.x + enemyPos.x) / 2;
            float distanceZ = (playerPos.x - enemyPos.x) / 2;
            distanceZ = distanceZ - 10;

            float distanceY = (playerPos.x - enemyPos.x) / 6;
            if (distanceY < 0) { distanceY = distanceY * -1; }

            distanceY = distanceY + 1.5f;

            transform.position = new Vector3(distanceX, distanceY, distanceZ);


            //Updating Button position

            //Getting the central point of player model's child (keho) with its renderer's central point
            playerPos = Camera.main.WorldToScreenPoint(player.transform.GetChild(1).transform.GetChild(0).GetComponent<Renderer>().bounds.center);

            //Buttons stay centralized to the central point of player model's child (keho)
            buttons.transform.position = playerPos;
        }



        // VANHA TAPA
        //   Vector3 temp = transform.position;
        //   temp.x = playerTransform.position.x;
        //   temp.x += cameraOffSet;
        //   transform.position = temp;



        //Updating Buttons

        //if (buttons == null && gm.turn % 2 == 0)
        //{
        //    buttons = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        //}

        //if (buttons != null)
        //{

        //}

    }
}
