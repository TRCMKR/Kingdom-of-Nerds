using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // public Transform lookAt;
    // private Vector3 deltaMove;
    
    
    public Transform lookAt;
    public float boundX;
    public float boundY;
    
    void FixedUpdate()
    {
        // Vector3 delta = Vector3.zero;
        // Vector3 lookAtPosition = lookAt.position;
        // Vector3 position = transform.position;
        //
        // delta.x = lookAtPosition.x - position.x;
        // delta.y = lookAtPosition.y - position.y;
        //
        // deltaMove = Vector3.Lerp(deltaMove, delta, Time.deltaTime);
        //
        // transform.position = new Vector3(deltaMove.x, deltaMove.y, position.z);
        
        Vector3 delta = Vector3.zero;

        float deltaX = lookAt.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX)
        {
            if (transform.position.x < lookAt.position.x)
                delta.x = deltaX - boundX;
            else
                delta.x = deltaX + boundX;
        }
        
        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < lookAt.position.y)
                delta.y = deltaY - boundY;
            else
                delta.y = deltaY + boundY;
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
