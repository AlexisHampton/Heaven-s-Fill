using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character
{
    [Header("Character")]
    public string pronoun;
    public string job;
    public string description;

    [Header("Arrest")]
    public string accuseText;

    private void OnTriggerStay2D(Collider2D collision)
    {
        UIManager.Instance.TurnOnButtons(true);

        if (Input.GetKey(KeyCode.E))
        {
            UIManager.Instance.FillPhone(name, pronoun, job, description);
        }
        if (Input.GetKey(KeyCode.F) && GameManager.Instance.player.canMove)
        {
            UIManager.Instance.TurnDialogueOn(true);
            DialogueManager.Instance.currNpc = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        UIManager.Instance.TurnOnButtons(false);
    }
}
