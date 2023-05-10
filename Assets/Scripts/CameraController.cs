using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;    

    void Update()
    {
        //keep camera focused on player
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2, -10f);
    }
}
