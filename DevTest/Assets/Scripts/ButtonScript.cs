using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void MenuButton()
    {

    }

    public void QuitAppplication()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
