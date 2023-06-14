using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class KeyboardScript : MonoBehaviour
{

    [Header("Click sound")]
    public AudioClip click;
    public AudioSource player;

    private TMP_InputField TextField;

    [Header("Keyboards")]
    public GameObject RusLayoutSml, RusLayoutBig, EngLayoutSml, EngLayoutBig, SymbLayout;

    public void alphabetFunction(string alphabet)
    {
        player.PlayOneShot(click);
        TextField.text=TextField.text + alphabet;
    }

    public void BackSpace()
    {
        player.PlayOneShot(click);
        if (TextField.text.Length>0) TextField.text= TextField.text.Remove(TextField.text.Length-1);
    }

    public void Enter()
    {
        TextField.DeactivateInputField();
        gameObject.SetActive(false);
    }

    public void CloseAllLayouts()
    {
        player.PlayOneShot(click);
        RusLayoutSml.SetActive(false);
        RusLayoutBig.SetActive(false);
        EngLayoutSml.SetActive(false);
        EngLayoutBig.SetActive(false);
        SymbLayout.SetActive(false);
    }

    public void ShowLayout(GameObject SetLayout)
    {
        player.PlayOneShot(click);
        CloseAllLayouts();
        SetLayout.SetActive(true);
    }
    public void OpenKeyboard()
    {
        TextField = EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>();
        gameObject.SetActive(true);
    }
}
