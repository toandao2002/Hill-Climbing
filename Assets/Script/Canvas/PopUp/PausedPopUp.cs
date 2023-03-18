using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedPopUp : MonoBehaviour
{
    public MyButton Resume;
    public MyButton Restart;
    public MyButton Exit;
    private void Start()
    {
        Resume.onClick.AddListener(()=> {
            PopUp.Instance.HidePaused();
        });
        Restart.onClick.AddListener(()=> {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
        Exit.onClick.AddListener(()=> {
            SceneManager.LoadScene("MainMenu");
        });
    }

}
