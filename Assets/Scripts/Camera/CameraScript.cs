using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);
    [SerializeField] private float _smoothTime = 0.5f;
    
    Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        if(target != null)
        {
            Vector3 targetPosition = target.position + offset;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, _smoothTime);
            //Mathf.Clamp(transform.position.x, -5f, 5f); didn't work.
        }
    }
}
