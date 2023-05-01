using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Line
{
    public string tag;
    public int index;
    public List<string> dialogue = new List<string>();

    public override bool Equals(object obj)
    {
        if (obj.GetType() != typeof(Line)) return false;
        Line other = (Line)obj;
        if (tag != other.tag) return false;
        if (index != other.index) return false;
        if (!dialogue.SequenceEqual(other.dialogue)) return false;
        return true;
    }
}

public class Character : MonoBehaviour
{
    [Header("Dialogue")]
    public TextAsset allLines;
    public List<Line> lines = new List<Line>();
    public Line otherLine;

    // Start is called before the first frame update
    void Start()
    {
        UnZipTextAsset();
        if (lines != null)
            otherLine = lines[lines.Count - 1];
    }

    void UnZipTextAsset()
    {
        string[] charLines = allLines.ToString().Split('\n');
        foreach (string s in charLines)
            lines.Add(CreateLine(s));
    }

    public List<Line> FindLine(string tag)
    {
        List<Line> npcLines = new List<Line>();
        foreach (Line line in lines)
            if (line.tag.ToLower() == tag.ToLower())
                npcLines.Add(line);
        return npcLines;
    }

    private static Line CreateLine(string s)
    {
        string[] charLine = s.Split('|');
        Line line = new Line();
        line.tag = charLine[0].Substring(0, charLine[0].Length-1);
        try { line.index = int.Parse(charLine[1]); }
        catch
        {
            line.dialogue = new List<string>(charLine[1].Split(';'));
            return line;
        }

        line.dialogue = new List<string>(charLine[2].Split(';'));
        return line;
    }


}
