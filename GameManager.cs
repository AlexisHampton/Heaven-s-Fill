using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    //0 is good, 1 is bad
    [TextArea(7,12)]public string[] endings = new string[2];
    public List<NPC> guiltyNPCs;
    public List<NPC> allNpcs;
    public List<string> npcs;
    public List<string> tags;
    public bool isDone = false;
    public Player player;
    NPC endNpc;

    public void StartGame()
    {
        isDone = false;
        player.canMove = true;
        UIManager.Instance.TurnDialogueOn(true);
        UIManager.Instance.TurnInputOn(false);
        DialogueManager.Instance.LoadBeginningText();

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void EndGame(NPC npc)
    {
        isDone = true;
        endNpc = npc;

        UIManager.Instance.TurnDialogueOn(true);
        UIManager.Instance.TurnInputOn(false);
        Guard.Instance.TeleportNPC();
        DialogueManager.Instance.LoadEndText(); 
    }

    public void RealEnd()
    {
        UIManager.Instance.TurnEndScreenOn(endings[GetEnding(endNpc)]);
    }

    public int GetEnding(NPC npc)
    {
        if (guiltyNPCs.Contains(npc))
            return 1;
        else
            return 0;
    }

    public bool ContainsNPC(string npc)
    {
        if (npcs.Contains(npc))
            return true;
        return false;
    }

    public NPC FindNPC(string tag)
    {
        foreach (NPC npc in allNpcs)
            if (npc.name.ToLower() == tag)
                return npc;
        return null;
    }

    public bool ContainsTag(string tag)
    {
        if (tags.Contains(tag))
            return true;
        return false;
    }
}
