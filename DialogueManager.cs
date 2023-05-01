using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : Singleton<DialogueManager>
{
    public float timeUntilNextLetter = .03f;
    public Player player;
    public Guard guard;
    public NPC currNpc;
    public bool isArrest;

    int index = 0;
    int arrestInt = 0;
    string[] currDialogue;
    string currName;
    Line currNPCLine;
    bool npcTalk = false;
    bool isBegining = true;


    public void MatchTag()
    {
        if (isArrest)
        {
            arrestInt++;
            UIManager.Instance.TurnInputOn(true);
            ContinueArrest();
            return;
        }
        index = 0;
        string tag = InputTextManager.Instance.GetTag().ToLower();

        List<Line> playerLines = player.FindLine(tag);
        Line playerLine = new Line();

        if (currNpc.FindLine(tag).Count == 0)
        {
            currNPCLine = currNpc.otherLine;
            npcTalk = true;
            LoadCharacterDialogue(currNpc, currNPCLine);
            return;
        }

        currNPCLine = currNpc.FindLine(tag)[0];

        foreach (Line line in playerLines)
            if (line.index == currNPCLine.index)
                playerLine = line;

        npcTalk = false;
        LoadCharacterDialogue(player, playerLine);
    }

    void LoadCharacterDialogue(Character chara, Line line)
    {
        currName = chara.name;
        currDialogue = line.dialogue.ToArray();
        index = 0;
        LoadDialogue(currName, currDialogue);
    }

    public void LoadDialogue(string name, string[] dialogue)
    {
        Debug.Log(dialogue.Length);
        if (index < dialogue.Length)
            UIManager.Instance.FillDialogue(name, dialogue[index]);
        else
        {
            if (!npcTalk)
            {
                npcTalk = true;
                LoadCharacterDialogue(currNpc, currNPCLine);
            }
            else
                UIManager.Instance.TurnDialogueOn(false);
        }
    }

    public void LoadArrestDialogue(string name, string sentence)
    {
        UIManager.Instance.FillDialogue(name, sentence);
    }

    public void LoadDialogue(string name, string[] sentence, int index)
    {
        if (index < sentence.Length)
            UIManager.Instance.FillDialogue(name, sentence[index]);
        else
        {
            isBegining = false;
            if (GameManager.Instance.isDone)
                GameManager.Instance.RealEnd();
            UIManager.Instance.TurnDialogueOn(false);
            
        }
    }

    public void LoadBeginningText()
    {
        index = 0;
        isBegining = true;
        currName = guard.name;
        currDialogue = guard.allLines;
        LoadDialogue(currName, currDialogue, index);
    }

    public void LoadEndText()
    {
        index = 0;
        currName = guard.accused.name;
        currDialogue = guard.accused.accuseText.Split(';');
        LoadDialogue(currName, currDialogue, index);
    }

    public void Continue()
    {
        if(isArrest)
        {
            if (arrestInt == 2)
            {
                
                GameManager.Instance.EndGame(guard.accused);
            }
            else
                UIManager.Instance.TurnDialogueOn(false);

            arrestInt = 0;
            index = 0;
            isArrest = false;
            return;
        }

        UIManager.Instance.OnContinue();
        index++;
        if (isBegining)
        {
            LoadDialogue(currName, currDialogue, index);
            return;
        }
        LoadDialogue(currName, currDialogue);
    }

    public void ContinueArrest()
    {
        string arrestText = "";
        if(arrestInt == 0)
           arrestText = guard.arrestText;
        else if (arrestInt == 1)
        {
            string tag = InputTextManager.Instance.GetTag().ToLower();
            if (GameManager.Instance.ContainsNPC(tag))
            {
                arrestText = guard.whyText;
                guard.accused = GameManager.Instance.FindNPC(tag);
            }
            else
                arrestText = guard.wrongText;
        }
        else
        {
            UIManager.Instance.TurnInputOn(false);
            string tag = InputTextManager.Instance.GetTag().ToLower();
            if (GameManager.Instance.ContainsTag(tag))
                arrestText = guard.makeArrestText;
            else
                arrestText = guard.wrongText;
        }
        LoadArrestDialogue(guard.name, arrestText);
    }
}
