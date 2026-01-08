using UnityEngine;

[CreateAssetMenu(menuName = "UI/Theme")]
public class UITheme : ScriptableObject
{
    [Header("Base Colors")]
    public Color backgroundColor;
    public Color surfaceColor;    
    public Color Icons;
    public Color cardColor;
    public Color CardColor2;
    public Color CardColor3;
    public Color CardColor4;

    [Header("Text Colors")]
    public Color primaryText;
    public Color secondaryText;

    [Header("Button")]
    public Color buttonNormal;
    public Color buttonHighlighted;
    public Color buttonPressed;
    public Color buttonDisabled;


    [Header("Accent")]
    public Color accentColor;
}