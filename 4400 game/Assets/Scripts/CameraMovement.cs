using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;
   

    // Start is called before the first frame update
    void Start()
    {
        target = dontDestroy.Instance.transform; 
    }
  
    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null)
        {


            //GameObject temp = GameObject.FindGameObjectWithTag("Player");
            // dontDestroy.Instance.GetCurrentPosition(); //temp.transform.position;
            Debug.Log(target.position);
            if (transform.position != target.position)
            {
                transform.position = Vector3.Lerp(transform.position,
                new Vector3(target.position.x, target.position.y, -10f), smoothing);
            }
        }
    }
}