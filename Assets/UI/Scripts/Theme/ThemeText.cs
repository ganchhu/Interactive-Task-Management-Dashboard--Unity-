using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class ThemeText : MonoBehaviour
{
    public enum TextRole
    {
        Primary,
        Secondary
    }

    [SerializeField] private TextRole role;
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

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
        text.color = role == TextRole.Primary
            ? theme.primaryText
            : theme.secondaryText;
    }
}