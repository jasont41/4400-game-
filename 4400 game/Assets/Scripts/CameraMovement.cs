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
        target = PlayerMovement.Instance.transform; 
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
                float curr_x = target.transform.position.x;
                float curr_y = target.transform.position.y; 
                curr_x = Mathf.Clamp(curr_x, minPosition.x,maxPosition.x);
                curr_y = Mathf.Clamp(curr_y, minPosition.y, maxPosition.y);
                Vector3 temp_pos = new Vector3(curr_x, curr_y, -10f);
                transform.position = Vector3.Lerp(transform.position, temp_pos, smoothing); 
                //new Vector3(target.position.x, target.position.y, -10f), smoothing);
                //transform.position = Vector3.Lerp(transform.position,
                //new Vector3(target.position.x, target.position.y, -10f), smoothing);
            }
        }
    }
}