using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    bool isCreatingGravityField = false;
    Vector3 GravityFieldStartLocation;
    Vector3 GravityFieldEndLocation;

    public enum GravityFieldStatus
    {
        CanCreatingGravityField,
        CreatingGravityField,
        DecidingGravityDirection
    }

    public GravityFieldStatus gracityFieldStatus;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Position is:" + Camera.main.ScreenToViewportPoint(Input.mousePosition));
        }
    }
}
