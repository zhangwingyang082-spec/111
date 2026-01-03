using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionDetector : MonoBehaviour
{
   public Vector3 MouseWorldPosition
    {
        get 
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            pos.z = 0;
            return pos;
        }
    }
    void Start()
    {
       
      
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(MouseWorldPosition);
    }
}
