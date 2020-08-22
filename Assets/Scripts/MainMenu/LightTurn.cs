using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Effect of the ligth from main screen
 */
public class LightTurn : MonoBehaviour
{
    public float TurnSpeed = 10;


    public void Update(){
        this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y + TurnSpeed * Time.deltaTime, this.transform.rotation.eulerAngles.z);
    }
}
