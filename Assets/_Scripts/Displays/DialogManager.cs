﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//Gestiona las selecciones en pantalla
public class DialogManager : MonoBehaviour {

    public Text question;
    public Image iconImage;
    public Button yesButton;
    public Button noButton;
    public Button cancelButton;
    public GameObject modalPanelObject;
    [HideInInspector] public static Dictionary<string, string> dialogueDB = new Dictionary<string, string>();

    private static DialogManager modalPanel;

    private void Start() {
        InitializeDialog();
    }

    public static DialogManager Instance() {
        if (!modalPanel) {
            modalPanel = FindObjectOfType(typeof(DialogManager)) as DialogManager;
            if (!modalPanel)
                Debug.LogError("There needs to be one active ModalPanel script on a GameObject in your scene.");
        }

        return modalPanel;
    }

    // Yes/No/Cancel: A string, a Yes event, a No event and Cancel event
    public void Choice(string question, UnityAction yesEvent, UnityAction noEvent, UnityAction cancelEvent, bool lastDialogue) {
        modalPanelObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        modalPanelObject.transform.SetAsLastSibling();


        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(yesEvent);
        yesButton.onClick.AddListener(ClosePanel);

        noButton.onClick.RemoveAllListeners();
        noButton.onClick.AddListener(noEvent);
        noButton.onClick.AddListener(ClosePanel);

        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(cancelEvent);
        cancelButton.onClick.AddListener(ClosePanel);

        this.question.text = question;

        this.iconImage.gameObject.SetActive(false);
        if (lastDialogue) {
            yesButton.gameObject.SetActive(true);
            noButton.gameObject.SetActive(true);
            cancelButton.gameObject.SetActive(true);
        } else {
            yesButton.gameObject.SetActive(false);
            noButton.gameObject.SetActive(false);
            cancelButton.gameObject.SetActive(false);
        }
    }
    
    void ClosePanel() {
        modalPanelObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void InitializeDialog() {
        dialogueDB.Add("NPC001_1", "Carlos...Carlos!");
        dialogueDB.Add("NPC001_2", "Carlos: Qué pasa? Tío, te encuentras bien?");
        dialogueDB.Add("NPC001_Yes", "Si, como en mi vida");
        dialogueDB.Add("NPC001_No", "La verdad es que…");
        dialogueDB.Add("NPC001_Cancel", "Sinceramente no");
    }
}
