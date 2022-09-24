using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform playerT;
    public float smooth;
    private Vector3 velocity = Vector3.zero;
    public Transform topRightB, bottomLeftB;
    BoxCollider2D cameraBox;


    void Update()
    {
        Vector2 targetPos = playerT.position;
        targetPos.x = Mathf.Clamp(targetPos.x, bottomLeftB.position.x, topRightB.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, bottomLeftB.position.y, topRightB.position.y);
        transform.position = Vector3.SmoothDamp(new Vector3(transform.position.x, transform.position.y, -7), new Vector3(targetPos.x, targetPos.y, -7), ref velocity, smooth);
    }   

/*    void AspectRationBoxChange()
    {
        //16:10 ratio
        if (Camera.main.aspect >= (1.6f) && Camera.main.aspect < (1.7f))
            cameraBox.size = new Vector2(23f, 14.3f);

        //16:9 ratio
        if (Camera.main.aspect >= (1.7f) && Camera.main.aspect < (1.8f))
            cameraBox.size = new Vector2(25.47f, 14.3f);

        //5:4 ratio
        if (Camera.main.aspect >= (1.25f) && Camera.main.aspect < (1.3f))
            cameraBox.size = new Vector2(18f, 14.3f);

        //4:3 ratio
        if (Camera.main.aspect >= (1.3f) && Camera.main.aspect < (1.4f))
            cameraBox.size = new Vector2(19.13f, 14.3f);

        //3:2 ratio
        if (Camera.main.aspect >= (1.5f) && Camera.main.aspect < (1.6f))
            cameraBox.size = new Vector2(21.6f, 14.3f);
    }*/
}
