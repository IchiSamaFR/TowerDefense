using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Control of the camera movements
 */
public class CameraController : MonoBehaviour
{
    public bool    canMove = true;
    public bool    mouseMove = false;

    [Header("Stats")]
    public float    scrollSpeed = 100f;
    public float    traySpeed = 10f;
    public float    trayBorder = 10f;

    [Header("x")]
    public float    clampMaxX = 10;
    public float    clampMinX = -10;

    [Header("y")]
    public float    clampMaxY = 13;
    public float    clampMinY = 3;

    public float    clampZoomMax = 40;
    public float    clampZoomMin = 70;

    [Header("z")]
    public float    clampMaxZ = 10;
    public float    clampMinZ = -10;

    //float endY;

    Vector3 toGo = new Vector3();

    /*
     * Set of the toGo position equal to it actual position
     */
    void Start()
    {
        //endY = transform.position.y;
        toGo = this.transform.position;
    }
    void Update()
    {
        Movement();
    }

    /*
     * Set of the position to go
     */
    void Movement()
    {
        if (Input.GetKeyDown("a"))
        {
            canMove = !canMove;
        }


        if (!canMove && Pause.pause)
        {
            return;
        }

        /*
        * Use ZQSD to move
        */
        if ((Input.GetKey("z") || Input.mousePosition.y >= Screen.height - trayBorder) && this.transform.position.z < clampMaxZ)
        {
            //transform.Translate(Vector3.forward * Time.deltaTime * traySpeed, Space.World);
            toGo += Vector3.forward * Time.deltaTime * traySpeed;
        }
        if ((Input.GetKey("s") || Input.mousePosition.y <= trayBorder) && this.transform.position.z > clampMinZ)
        {
            //transform.Translate(Vector3.back * Time.deltaTime * traySpeed, Space.World);
            toGo += Vector3.back * Time.deltaTime * traySpeed;
        }
        if ((Input.GetKey("d") || Input.mousePosition.x >= Screen.width - trayBorder) && this.transform.position.x < clampMaxX)
        {
            //transform.Translate(Vector3.right * Time.deltaTime * traySpeed, Space.World);
            toGo += Vector3.right * Time.deltaTime * traySpeed;
        }
        if ((Input.GetKey("q") || Input.mousePosition.x <= trayBorder) && this.transform.position.x > clampMinX)
        {
            //transform.Translate(Vector3.left * Time.deltaTime * traySpeed, Space.World);
            toGo += Vector3.left * Time.deltaTime * traySpeed;
        }


        /*
        * Mouse Scroll Wheel to zoom in or zoom out
        */
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0 && transform.position.y > clampMinY)
        {
            //endY = transform.position.y - scrollSpeed * scroll * Time.deltaTime;
            toGo.y = transform.position.y - scrollSpeed * scroll * Time.deltaTime;
        }
        else if (scroll < 0 && transform.position.y < clampMaxY)
        {
            //endY = transform.position.y - scrollSpeed * scroll * Time.deltaTime;
            toGo.y = transform.position.y - scrollSpeed * scroll * Time.deltaTime;
        }

        /*
        * Clamp the max height
        */
        if (toGo.y > clampMaxY)
        {
            toGo.y = clampMaxY + 0.1f;
        }
        else if (toGo.y < clampMinY)
        {
            toGo.y = clampMinY + 0.1f;
        }

        /*
         * Apply the position with a lerp to get a smooth camera
         */
        this.transform.position = Vector3.Lerp(this.transform.position, toGo, Time.deltaTime * 10);

        float rota = clampZoomMax - ((this.transform.position.y - clampMinY) * ((clampZoomMax - clampZoomMin) / (clampMaxY - clampMinY)));
        this.transform.rotation = Quaternion.Euler(rota, 0, 0);
    }

    
}
