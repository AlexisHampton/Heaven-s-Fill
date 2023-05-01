using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Singleton<Guard>
{
    public Transform npcStand;

    [Header("arrest Dialogue")]
    public string arrestText;
    public string whyText;
    public string makeArrestText;
    public string wrongText;



    public TextAsset dialogue;
    public string[] allLines;
    public NPC accused;

    private void Start()
    {
        allLines = dialogue.ToString().Split('\n');
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        UIManager.Instance.fButton.SetActive(true);
        if (Input.GetKey(KeyCode.F) && GameManager.Instance.player.canMove)
        {

            UIManager.Instance.TurnDialogueOn(true);
            UIManager.Instance.continueButton.SetActive(true);
            DialogueManager.Instance.isArrest = true;
            DialogueManager.Instance.ContinueArrest();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        UIManager.Instance.fButton.SetActive(false);
    }

    public void TeleportNPC()
    {
        accused.transform.position = npcStand.position;
    }



}
