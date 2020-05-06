using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool    canMove = false;

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

    // Update is called once per frame
    float endY;

    void Start()
    {
        endY = transform.position.y;
    }
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            canMove = !canMove;
        }


        if (!canMove)
        {
            return;
        }

        if ((Input.GetKey("z") || Input.mousePosition.y >= Screen.height - trayBorder) && this.transform.position.z < clampMaxZ)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * traySpeed, Space.World);
        }
        if ((Input.GetKey("s") || Input.mousePosition.y <= trayBorder) && this.transform.position.z > clampMinZ)
        {
            transform.Translate(Vector3.back * Time.deltaTime * traySpeed, Space.World);
        }
        if ((Input.GetKey("d") || Input.mousePosition.x >= Screen.width - trayBorder) && this.transform.position.x < clampMaxX)
        {
            transform.Translate(Vector3.right * Time.deltaTime * traySpeed, Space.World);
        }
        if ((Input.GetKey("q") || Input.mousePosition.x <= trayBorder) && this.transform.position.x > clampMinX)
        {
            transform.Translate(Vector3.left * Time.deltaTime * traySpeed, Space.World);
        }


        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if(scroll > 0 && transform.position.y > clampMinY)
        {
            endY = transform.position.y - scrollSpeed * scroll * Time.deltaTime;
        }
        else if (scroll < 0 && transform.position.y < clampMaxY)
        {
            endY = transform.position.y - scrollSpeed * scroll * Time.deltaTime;
        }
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, endY, Time.deltaTime * 10), transform.position.z);

        if (transform.position.y > clampMaxY)
        {
            transform.position = new Vector3(transform.position.x, clampMaxY + 0.1f, transform.position.z);
        }
        else if (transform.position.y < clampMinY)
        {
            transform.position = new Vector3(transform.position.x, clampMinY - 0.1f, transform.position.z);
        }

        float rota = clampZoomMax - ((this.transform.position.y - clampMinY) * ((clampZoomMax - clampZoomMin) / (clampMaxY - clampMinY)));
        this.transform.rotation = Quaternion.Euler(rota, 0, 0);

    }
}
