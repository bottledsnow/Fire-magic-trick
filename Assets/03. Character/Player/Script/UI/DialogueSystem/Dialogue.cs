using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    public Dialogue_Content[] contents;
}
[System.Serializable]
public class Dialogue_Content
{
    public string name;
    public Sprite CharacterIcon;
    [TextArea(3, 10)]
    public string sentences;

}