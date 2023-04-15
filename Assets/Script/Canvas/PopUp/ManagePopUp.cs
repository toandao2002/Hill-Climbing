using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PopUpName
{
    PopUpItemsCar,
    PopUpChoseMap,
    PopupTune,
}
public class ManagePopUp : MonoBehaviour
{
    // Start is called before the first frame update
    public static ManagePopUp Instance;
    public List<BasePopUp> Popups ;
    public GameObject Paused;
    public GameObject GameLose;
    public UpgratePopUp upgratePopUp;
    private void Awake()
    {
         if (Instance == null)
        {
            Instance = this;
        }
    }
    public void ShowPopUp(PopUpName namepopup)
    {
        Popups[(int)namepopup].Show();
    }
    public void HidePopUp(PopUpName namepopup)
    {
        Popups[(int)namepopup].Hide();
    }
    public void ShowPaused()
    {
        if (Paused != null)
            Paused.SetActive(true);
    }
    public void HidePaused()
    {
        if (Paused != null)
            Paused.SetActive(false);
    }
    public void ShowGameLose()
    {
        if (GameLose != null)
        {
            GameLose.SetActive(true);
        }
    }
    public void HideGameLose()
    {
        if (GameLose != null)
        {
            GameLose.SetActive(false);
        }
    }
}
