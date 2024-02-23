using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnClickPlay()
    {
        UIManager.Instance.animatorMainMenu.SetTrigger(UIManager.Instance.disappear);
        UIManager.Instance.animatorGame1.SetTrigger(UIManager.Instance.appear);
    }
}
