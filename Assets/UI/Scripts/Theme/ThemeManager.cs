using UnityEngine;
using System;

public class ThemeManager : MonoBehaviour
{
    public static ThemeManager Instance { get; private set; }
    bool isDark = false;
    [Header("Themes")]
    [SerializeField] private UITheme lightTheme;
    [SerializeField] private UITheme darkTheme;

    public UITheme CurrentTheme { get; private set; }

    public static event Action<UITheme> OnThemeChanged;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SetLightTheme(); // default
         isDark = false;
    }

    public void SetLightTheme()
    {
        ApplyTheme(lightTheme);
         isDark = false;
    }

    public void SetDarkTheme()
    {
        ApplyTheme(darkTheme);
         isDark = true;
    }

    private void ApplyTheme(UITheme theme)
    {
        CurrentTheme = theme;
        OnThemeChanged?.Invoke(theme);
    }

    public void OnThemeToggle()
    {
         isDark = !isDark;
        if (isDark)
            ThemeManager.Instance.SetDarkTheme();
        else
            ThemeManager.Instance.SetLightTheme();
    }
}
