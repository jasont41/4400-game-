using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{

    public Vector2 cameraChange;
    public Vector3 playerChange;
    private CameraMovement cam;

    public bool needText;
    public string placeName;

    public GameObject text;
    public Text placeText; 



    // Start is called before the first frame update
    void Start()
    {

        text = canvas_dont_destroy.Instance.roomTransferText_theObject;
        placeText = canvas_dont_destroy.Instance.roomTransferText; 
        cam = Camera.main.GetComponent<CameraMovement>();
       /* text = GameObject.FindGameObjectWithTag("room_transfer_text");
        placeText = GameObject.FindGameObjectWithTag("room_transfer_text").GetComponent<Text>(); 
         */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange;
            other.transform.position += playerChange;
            if (needText)
            {
                StartCoroutine(placeNameCo()); 
            }
        }
    }

    private IEnumerator placeNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(3f);
        text.SetActive(false); 
    }

}
