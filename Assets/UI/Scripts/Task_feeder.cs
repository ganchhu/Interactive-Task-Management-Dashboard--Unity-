using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Task_feeder : MonoBehaviour
{
    [Header("Input Fields")]
    [SerializeField] private TMP_InputField taskNameInput;
    [SerializeField] private TMP_InputField dateInput;
    [Header("Panels")]
    [SerializeField] private RectTransform taskPanel;
    [SerializeField] private RectTransform confirmationPanel;
    [Header("Animation")]
    [SerializeField] private float animDuration = 0.35f;
    [Header("References")]
    [SerializeField] Button Clear, Cancel, Submit,Okay;
    private void Awake()
    {
        ResetPanels();
    }
    private void OnDisable()
    {
        Clear.onClick.RemoveListener(OnClear);
        Cancel.onClick.RemoveListener(OnCancel);
        Submit.onClick.RemoveListener(OnSubmit);
        Okay.onClick.RemoveListener(Close_conformationpanel);
    }
    private void OnEnable()
    {
        ClearFields();
        Clear.onClick.AddListener(OnClear);
        Cancel.onClick.AddListener(OnCancel);
        Submit.onClick.AddListener(OnSubmit);
        Okay.onClick.AddListener(Close_conformationpanel);
    }

    public void OpenTaskPanel()
    {
        taskPanel.gameObject.SetActive(true);
        confirmationPanel.gameObject.SetActive(false);

        taskPanel.localScale = Vector3.one * 0.85f;
        taskPanel.DOScale(1f, animDuration).SetEase(Ease.OutBack);
    }
    public void ShowConfirmation()
    {
        taskPanel.DOScale(0.9f, animDuration * 0.5f)
            .OnComplete(() =>
            {
                taskPanel.gameObject.SetActive(false);

                confirmationPanel.gameObject.SetActive(true);
                confirmationPanel.localScale = Vector3.one * 0.8f;
                confirmationPanel.DOScale(1f, animDuration).SetEase(Ease.OutBack);

                DOVirtual.DelayedCall(1.2f, Close_conformationpanel);
            });
    }
    void CloseAll()
    {
        
    }
    void ResetPanels()
    {
        taskPanel.gameObject.SetActive(false);
        confirmationPanel.gameObject.SetActive(false);

        taskPanel.localScale = Vector3.one;
        confirmationPanel.localScale = Vector3.one;
    }
    public void OnSubmit()
    {
        if (string.IsNullOrEmpty(taskNameInput.text))
            return;

        Task_item newTask = new Task_item
        {
            Name = taskNameInput.text,
            Date = dateInput.text,
            currentStatus = Status.Pending
        };


        Control_manager.CM_Instance.AddTask(newTask);
        Control_manager.CM_Instance.Getinfoontask();

        ShowConfirmation();
        ClearFields();

    }
    public void OnClear()
    {
        ClearFields();
    }

    public void Close_conformationpanel()
    {
        if (taskPanel.gameObject.activeSelf)
        {
            taskPanel.DOScale(0.8f, animDuration * 0.5f)
                .OnComplete(() => taskPanel.gameObject.SetActive(false));
        }

        if (confirmationPanel.gameObject.activeSelf)
        {
            confirmationPanel.DOScale(0.8f, animDuration * 0.5f)
                .OnComplete(() => confirmationPanel.gameObject.SetActive(false));
        }
    }

    public void OnCancel()
    {
        confirmationPanel.gameObject.SetActive(false);
        taskPanel.DOScale(0.8f, animDuration * 0.5f)
            .OnComplete(() =>
            {
                taskPanel.gameObject.SetActive(false);
            });
    }


    private void ClearFields()
    {
        taskNameInput.text = "";
        dateInput.text = "";
    }   

   
}
