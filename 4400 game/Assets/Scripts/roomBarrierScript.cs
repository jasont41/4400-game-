using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI; 


public class roomBarrierScript : MonoBehaviour
{
    public GameObject textBox;
    public Text text;


    private void Awake()
    {
    }

    public string warningString; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        textBox = canvas_dont_destroy.Instance.dialogBox;
        text = canvas_dont_destroy.Instance.dialogBox_text; 
        if (collision.CompareTag("Player"))
        {
            text.text = warningString; 
            textBox.SetActive(true); 
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textBox.SetActive(false);
        }
    }
    IEnumerator printingMsg()
    {
        yield return new WaitForSeconds(1f); 
    }
}
