using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    public MyButton BtnStart;
    public MyButton BtnTune;
    public MyButton BtnVehicle;
    public MyButton BtnStage;
    public MyButton BtnShop;
    private void Start()
    {
        BtnVehicle.onClick.AddListener( () => {
            ShowPopUp(PopUpName.PopUpItemsCar);
            HidePoUp(PopUpName.PopUpChoseMap);

        });
        BtnStage.onClick.AddListener( () => {
            ShowPopUp(PopUpName.PopUpChoseMap);
            HidePoUp(PopUpName.PopUpItemsCar);
        });
        BtnStart.onClick.AddListener(()=> {
            int id = DataGame.Get(DataGame.Stage);
            SceneManager.LoadScene($"GamePlay {id}");
        });
    }
    public void ShowPopUp(PopUpName namePopup)
    {
        PopUp.Instance.ShowPopUp(namePopup);
    }
    public void HidePoUp (PopUpName namePopup)
    {
        PopUp.Instance.HidePopUp(namePopup);
    }
}
