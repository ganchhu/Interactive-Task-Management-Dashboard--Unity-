using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThemeImage : MonoBehaviour
{
    public enum ColorRole
    {
        Background,
        Surface,
        Icons,
        Card,
        Card2,
        Card3,
        Card4,
        button,
        Dropdown,
        Accent

    }

    [SerializeField] private ColorRole role;
    private Image image;
    private Button button;
    private TMP_Dropdown dropdown;

    private void OnEnable()
    {
        ThemeManager.OnThemeChanged += ApplyTheme;

        if (ThemeManager.Instance != null)
            ApplyTheme(ThemeManager.Instance.CurrentTheme);
    }

    private void OnDisable()
    {
        ThemeManager.OnThemeChanged -= ApplyTheme;
    }

    private void ApplyTheme(UITheme theme)
    {
        switch (role)
        {
            case ColorRole.Background:
                image = GetComponent<Image>();
                image.color = theme.backgroundColor;
                break;
            case ColorRole.Surface:
                image = GetComponent<Image>();
                image.color = theme.surfaceColor;
                break;
            case ColorRole.Icons:
                image = GetComponent<Image>();
                image.color = theme.Icons;
            break;
            case ColorRole.Card:
                image = GetComponent<Image>();
                image.color = theme.cardColor;
                break;
            case ColorRole.Card2:
                image = GetComponent<Image>();
                image.color = theme.CardColor2;
                break;
            case ColorRole.Accent:
                image = GetComponent<Image>();
                image.color = theme.accentColor;
                break; 
            case ColorRole.Card3:
                image = GetComponent<Image>();
                image.color = theme.CardColor3;
                break;
            case ColorRole.Card4:

            image.color = theme.CardColor4;
                break;
            case ColorRole.button:
                button = GetComponent<Button>();
                if (button != null)
                {
                    ColorBlock colors = button.colors;
                    colors.normalColor = theme.buttonNormal;
                    colors.highlightedColor = theme.buttonHighlighted;
                    colors.pressedColor = theme.buttonPressed;
                    colors.disabledColor = theme.buttonDisabled;
                    button.colors = colors;               
                }
                break;
            case ColorRole.Dropdown:
                dropdown = GetComponent<TMP_Dropdown>();
                if (dropdown != null)
                {
                    ColorBlock colors = dropdown.colors;
                    colors.normalColor = theme.buttonNormal;
                    colors.highlightedColor = theme.buttonHighlighted;
                    colors.pressedColor = theme.buttonPressed;
                    colors.disabledColor = theme.buttonDisabled;
                    dropdown.colors = colors;
                }
                break;


        }
    }
}