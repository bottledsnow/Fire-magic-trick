using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    public string name;
    [TextArea(3, 10)]
    public string[] sentences;
}
public class Dialogue_Content
{
    public string name;
    public string sentences;
    public Image CharacterIcon;

}