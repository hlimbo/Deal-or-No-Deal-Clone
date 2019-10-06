using UnityEngine;

[CreateAssetMenu(fileName = "HighlightColors", menuName = "ScriptableObject/HighlightColors")]
public class HighlightColors : ScriptableObject
{
    public Color hoverElement;
    public Color selectedElement;
}
