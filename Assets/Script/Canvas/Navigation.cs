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
            HidePoUp(PopUpName.PopupTune);
            ShowPopUp(PopUpName.PopUpItemsCar);
            HidePoUp(PopUpName.PopUpChoseMap);

        });
        BtnStage.onClick.AddListener( () => {
            ShowPopUp(PopUpName.PopUpChoseMap);
            HidePoUp(PopUpName.PopupTune);
            HidePoUp(PopUpName.PopUpItemsCar);
        });
        BtnStart.onClick.AddListener(()=> {
            int id = DataGame.Get(DataGame.Stage);
            SceneManager.LoadScene($"GamePlay {id}");
        });
        BtnTune.onClick.AddListener(()=> {
            HidePoUp(PopUpName.PopUpItemsCar);
            HidePoUp(PopUpName.PopUpChoseMap);
            ShowPopUp(PopUpName.PopupTune);
        });
    }
    public void ShowPopUp(PopUpName namePopup)
    {
        ManagePopUp.Instance.ShowPopUp(namePopup);
    }
    public void HidePoUp (PopUpName namePopup)
    {
        ManagePopUp.Instance.HidePopUp(namePopup);
    }
}
