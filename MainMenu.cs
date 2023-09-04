using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void QuitGame(){
        Application.Quit();
    }

    public void StartEasy(){
        SceneManager.LoadScene("Easy");
    }

    public void StartMedium(){
        SceneManager.LoadScene("SampleScene");
    }

    public void StartHard(){
        SceneManager.LoadScene("Hard");
    }

}
