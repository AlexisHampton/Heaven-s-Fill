using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [Header("How To")]
    public GameObject fButton;
    public GameObject eButton;

    [Header("Phone")]
    public GameObject phonePanel;
    public TextMeshProUGUI phoneNameText;
    public TextMeshProUGUI phoneDescriptionText;
    public TextMeshProUGUI phoneJobText;
    public TextMeshProUGUI phonePronounText;

    [Header("Dialogue")]
    public GameObject dialoguePanel;
    public GameObject inputPanel;
    public GameObject continueButton;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;

    [Header("Objects")]
    public GameObject objectPanel;
    public TextMeshProUGUI objectText;

    [Header("StartScreen")]
    public GameObject startPanel;

    [Header("EndScreen")]
    public GameObject endPanel;
    public TextMeshProUGUI endText;

    private void Start()
    {
        TurnDialogueOn(false);
        TurnPhoneOn(false);
        TurnObjectUIOn(false);
        TurnOnButtons(false);
    }

    public void TurnOnButtons(bool isOn)
    {
        fButton.SetActive(isOn);
        eButton.SetActive(isOn);
    }

    public void TurnDialogueOn(bool isOn)
    {
        Debug.Log("could you take me home?" + isOn);
        dialoguePanel.SetActive(isOn);
        continueButton.SetActive(!isOn);
        GameManager.Instance.player.canMove = !isOn;
        dialogueText.text = "";
        nameText.text = "";
        inputPanel.SetActive(isOn);
    }

    public void TurnInputOn(bool isOn)
    {
        inputPanel.SetActive(isOn);
    }

    void TurnPhoneOn(bool isOn)
    {
        phonePanel.SetActive(isOn);
        phoneNameText.text = "";
        phoneJobText.text = "";
        phoneDescriptionText.text = "";
        phonePronounText.text = "";
    }

    void TurnObjectUIOn(bool isOn)
    {
        objectPanel.SetActive(isOn);
        objectText.text = "";
    }

    public void TurnEndScreenOn(string ending)
    {
        endPanel.SetActive(true);
        endText.text = ending;
    }

    public void FillPhone(string name, string pronoun, string job, string description)
    {
        TurnPhoneOn(true);
        phoneNameText.text = name;
        phoneJobText.text = job;
        phoneDescriptionText.text = description;
        phonePronounText.text = pronoun;
    }

    public void FillDialogue(string name, string sentence)
    {
        nameText.text = name;
        dialogueText.text = sentence;
        continueButton.SetActive(true);
    }

    public void FillObject(string sentence)
    {
        TurnObjectUIOn(true);
        objectText.text = sentence;
    }

    public void OnContinue()
    {
        continueButton.SetActive(false);
        UIManager.Instance.dialogueText.text = "";
    }
}
