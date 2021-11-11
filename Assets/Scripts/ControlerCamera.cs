using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlerCamera : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform player;

    private bool isMove = false;


    /// <summary>
    /// Camera following speed
    /// </summary>
    [SerializeField]
    private float cameraSpeed = 3.5f;

    
    /// <summary>
    /// Camera following precision
    /// </summary>
    [SerializeField]
    private float cameraPrecision = 0.1f;


    private float startCameraXGap;
    private float startCameraZGap;


    /// <summary>
    /// Check the start difference between camera and player position to reproduce it in the future camera movement.
    /// </summary>
    void Start()
    {
        transform.position = new Vector3(player.position.x -12, transform.position.y+15, player.position.z);

        startCameraXGap = this.transform.position.x - player.position.x;
        startCameraZGap = this.transform.position.z - player.position.z;
    }


    /// <summary>
    /// Adapt the Camera position according to the player movement, with delay.
    /// </summary>
    private void FixedUpdate()
    {   
        //If the difference between player and Camera is higher than the Camera precision
        if (Mathf.Abs(this.transform.position.x - player.position.x - startCameraXGap) > cameraPrecision || Mathf.Abs(this.transform.position.z - player.position.z - startCameraZGap) > cameraPrecision)
        {
            // Change the camera position following the Player position with Lerp Delay
            this.transform.position = new Vector3(Mathf.Lerp(this.transform.position.x, player.transform.position.x + startCameraXGap, Time.deltaTime * cameraSpeed),
                                                this.transform.position.y,
                                                Mathf.Lerp(this.transform.position.z, player.transform.position.z + startCameraZGap, Time.deltaTime * cameraSpeed)
                                                );
        }
    }
}
