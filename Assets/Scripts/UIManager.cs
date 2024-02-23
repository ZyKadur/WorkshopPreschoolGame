using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Animator animatorMainMenu;
    public Animator animatorGame1;

    [HideInInspector] public string appear = "Appear";
    [HideInInspector] public string disappear = "Disappear";

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        UIManager.Instance.animatorMainMenu.SetTrigger(UIManager.Instance.appear);
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
        Instance = null;
    }
}
