using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_View : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float limit_min_x;
    public float limit_max_x;

    public float limit_min_y;
    public float limit_max_y;

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            if (smoothedPosition.x >limit_min_x && smoothedPosition.x<limit_max_x && smoothedPosition.y > limit_min_y && smoothedPosition.y < limit_max_y)
            {
                transform.position = smoothedPosition;
            }else if((smoothedPosition.x < limit_min_x || smoothedPosition.x > limit_max_x) &&(smoothedPosition.y > limit_min_y && smoothedPosition.y < limit_max_y))
            {
                transform.position = new Vector3(transform.position.x, smoothedPosition.y, smoothedPosition.z);
            }else if ((smoothedPosition.y < limit_min_y || smoothedPosition.y > limit_max_y) && (smoothedPosition.x > limit_min_x && smoothedPosition.x < limit_max_x))
            {
                transform.position = new Vector3(smoothedPosition.x, transform.position.y, smoothedPosition.z);
            }

        }
    }

}
