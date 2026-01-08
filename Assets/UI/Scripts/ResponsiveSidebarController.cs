using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResponsiveSidebarController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform sidebar;          // Assign SidebarContainer
    [SerializeField] private Button hamburgerButton;
    [SerializeField] LayoutElement Side_container_layout;
    [SerializeField] VerticalLayoutGroup mainLayout;

    [Header("Layout Settings")]
    [SerializeField] private float sidebarWidth = 260f;
    [SerializeField] private float mobileBreakpoint = 768f;

    [Header("Animation Settings")]
    [SerializeField] private float tweenDuration = 0.3f;
    [SerializeField] private Ease tweenEase = Ease.OutCubic;
    private Tween widthTween;
    private bool isMobile;
    private bool isSidebarOpen;
    private int lastScreenWidth;
    [SerializeField] float tweenanimation_timing;

    [SerializeField] int totalTasks = 20;
    [SerializeField] int completedTasks = 8;
    [SerializeField] int inProgressTasks = 7;
    [SerializeField] int pendingTasks = 5;

    [SerializeField] float CompletedRatio => (float)completedTasks / totalTasks;
    [SerializeField] float InProgressRatio => (float)inProgressTasks / totalTasks;
    [SerializeField] float PendingRatio => (float)pendingTasks / totalTasks;



    [Header("Desktop")]
    [SerializeField] int desktopPadding = 24;
    [SerializeField] int desktopSpacing = 16;

    [Header("Mobile")]
    [SerializeField] int mobilePadding = 12;
    [SerializeField] int mobileSpacing = 12;



    private void Start()
    {
        if (hamburgerButton != null)
            hamburgerButton.onClick.AddListener(ToggleSidebar);

        lastScreenWidth = Screen.width;
        EvaluateScreenSize(force: true);
    }

    private void Update()
    {
        // Detect screen width change (resize / rotation)
        if (Screen.width != lastScreenWidth)
        {
            lastScreenWidth = Screen.width;
            EvaluateScreenSize();
            ApplyLayout();
        }
    }

    void ApplyLayout()
    {
        bool isMobile = Screen.width < mobileBreakpoint;

        mainLayout.padding.left =
        mainLayout.padding.right =
        mainLayout.padding.top =
        mainLayout.padding.bottom = isMobile ? mobilePadding : desktopPadding;

        mainLayout.spacing = isMobile ? mobileSpacing : desktopSpacing;
    }
    private void EvaluateScreenSize(bool force = false)
    {
        bool shouldBeMobile = Screen.width < mobileBreakpoint;

        if (!force && shouldBeMobile == isMobile)
            return;

        isMobile = shouldBeMobile;

        if (isMobile)
        {
            if (hamburgerButton != null)
                hamburgerButton.gameObject.SetActive(true);

            CloseSidebar(immediate: true);
        }
        else
        {
            if (hamburgerButton != null)
                hamburgerButton.gameObject.SetActive(false);

            OpenSidebar(immediate: true);
        }
    }

    public void ToggleSidebar()
    {
        if (isSidebarOpen)
            CloseSidebar();
        else
            OpenSidebar();
    }


    public void toggleSidebar_onclick()
    {
        if(isMobile&& isSidebarOpen)
        {
            CloseSidebar();
        }
    }

    private void OpenSidebar(bool immediate = false)
    {
        isSidebarOpen = true;
        
        float targetX = 0f;
        AnimateWidth(sidebarWidth);
        AnimateSidebar(targetX, immediate);
    }

    private void CloseSidebar(bool immediate = false)
    {
        isSidebarOpen = false;
        
        float targetX = -sidebarWidth;
        AnimateWidth(0);
        AnimateSidebar(targetX, immediate);
    }

    private void AnimateSidebar(float targetX, bool immediate)
    {
        sidebar.DOKill(); // Kill any running tween

        if (immediate)
        {
            sidebar.anchoredPosition =
                new Vector2(targetX, sidebar.anchoredPosition.y);
          
        }

        else
        {
            sidebar.DOAnchorPosX(targetX, tweenDuration).SetEase(tweenEase);
           
        }
  
    }

    private void AnimateWidth(float targetWidth)
    {
        widthTween?.Kill();

        widthTween = DOTween.To(
            () => Side_container_layout.preferredWidth,
            x => Side_container_layout.preferredWidth = x,
            targetWidth,
            tweenDuration
        ).SetEase(Ease.OutCubic);
    }
}
