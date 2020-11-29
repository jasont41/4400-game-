using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class LoadingSceneButtonManager : MonoBehaviour
{
    public void ResumeButton()
    {
        PlayerPrefs.SetString("PreviousScene", "LoadSave");
        SceneManager.LoadScene("SampleScene"); 

    }
    public void NewSave()
    {
        PlayerPrefs.DeleteAll(); 
        SceneManager.LoadScene("SampleScene");
    }
}
