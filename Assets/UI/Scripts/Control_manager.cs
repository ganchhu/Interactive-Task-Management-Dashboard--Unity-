using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor.VersionControl;

public class Control_manager : MonoBehaviour
{
 

    public static Control_manager CM_Instance { get; private set; }
    [Header("UI")]
    [SerializeField] private GameObject badgeRoot;
    [SerializeField] private TMP_Text badgeText;
    [SerializeField] ResponsiveSidebarController sidebarController;
    [Header("Task Counters")]
    [SerializeField] private Status currentStatus;

    [SerializeField] List<GameObject> screens= new List<GameObject>();
    [SerializeField] TMP_Text noofTotaltask, noofcompletedtask, noofpendingtask,noofongoingtask;
    [Header("Task List")]
    [SerializeField] List<Task_item> tasks= new List<Task_item>();
    [SerializeField] private Transform taskContent;    
    [SerializeField] private GameObject taskPrefab;
    private float peritemheight = 50f;
    [Header("Report")]
    [SerializeField] private Transform ReportContent;
    [Header("Progress Bar Shader")]
    [SerializeField] private Material progressMaterial;
    [SerializeField] private GameObject[] progressImage;
    [SerializeField] private float shineSpeed = 1.2f;
    [SerializeField] private float speed = 0.01f;


    private static readonly int ShinePosID = Shader.PropertyToID("_ShinePos");
    private static readonly int ProgressID = Shader.PropertyToID("_Progress");


    private int unreadCount;
    private float currentProgress;

    private void Awake()
    {
       
        if (CM_Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        CM_Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        SetUnreadCount(3);
        Getinfoontask();
        Screen(0);     
    }

    public void RefreshAll()
    {
        Getinfoontask();
    }
    public void Getinfoontask()
    {   
        int completed = 0;
        int pending = 0;
        int ongoing = 0;
        int total = tasks.Count;

        foreach (var task in tasks)
        {
            switch(task.currentStatus)
            {
                case Status.Completed:
                    completed++;
                    break;
                case Status.Pending:
                    pending++;
                    break;
                case Status.InProgress:
                    ongoing++;
                    break;
            }
        }
        // Counters
        noofTotaltask.text = total.ToString();
        noofcompletedtask.text = completed.ToString();
        noofpendingtask.text = pending.ToString();
        noofongoingtask.text = ongoing.ToString();
        // UI
        PopulateTasks_information();
        PopulateTasks_Report();

        // progress
        Debug.Log("total"+ total+"   "+ "completed"+ completed);
        UpdateProgressBar(total, completed);
        currentProgress = (float)completed / total;
    }
    private void UpdateProgressBar(int totalTasks, int completedTasks)
    {
        currentProgress = totalTasks > 0
        ? (float)completedTasks / totalTasks
        : 0f;

        progressMaterial.SetFloat(ProgressID, currentProgress);
       foreach(var img in progressImage)
        {
            img.SetActive(false);
            img.SetActive(true);
        }
    }

    private void Update()
    {
        //if (currentProgress > 0f)
        //{
        //    float shinePos = Mathf.Repeat(Time.time * shineSpeed, 1f);
        //    progressMaterial.SetFloat(ShinePosID, shinePos);

        //}
    }

    public void PopulateTasks_information()
    {
        ClearChildren(taskContent, 2);        
        int countheight = tasks.Count + 2;
        for (int i = 0; i < tasks.Count; i++)
        {
           GameObject T = Instantiate(taskPrefab, taskContent);
            var entry = T.GetComponent<Taskitementry>();
            entry.task= tasks[i];
            entry.id = i + 1;
            entry.Populate(i + 1, false);
        }
        ResizeContent(taskContent, tasks.Count);
    }
    public void PopulateTasks_Report()
    {
        ClearChildren(ReportContent, 2);
        for (int i = 0; i < tasks.Count; i++)
        {
            GameObject T = Instantiate(taskPrefab, ReportContent);
            var entry = T.GetComponent<Taskitementry>();
            entry.task = tasks[i];
            entry.id = i + 1;
            entry.Populate(i + 1, true);
        }
        ResizeContent(ReportContent, tasks.Count);
    }

    private void ClearChildren(Transform root, int keep)
    {
        for (int i = root.childCount - 1; i >= keep; i--)
        {
            Destroy(root.GetChild(i).gameObject);
        }
    }
    private void ResizeContent(Transform root, int count)
    {
        int height = count + 2;
        root.GetComponent<RectTransform>()
            .SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height * peritemheight);
    }

    public void AddTask(Task_item newTask)
    {
        tasks.Add(newTask);
        Getinfoontask();
    }

    public void SetUnreadCount(int count)
    {
        unreadCount = count;

        if (unreadCount <= 0)
        {
            badgeRoot.SetActive(false);
        }
        else
        {
            badgeRoot.SetActive(true);
            badgeText.text = unreadCount > 9 ? "9+" : unreadCount.ToString();
        }
    }

    public void ClearNotifications()
    {
        SetUnreadCount(0);
    }
    public void Screen(int ID)
    {
        foreach (var screen in screens)
        {
            screen.SetActive(false);
        }
        screens[ID].SetActive(true);
        sidebarController.toggleSidebar_onclick();
    }

    private void OnSliderChanged(int start,int end,int completed)
    {
        progressMaterial.SetFloat(ProgressID, Mathf.Lerp(start, end, completed));
    }
 
  
    private void OnSliderChanged(int total, int completed)
    {
        float progress = total > 0 ? (float)completed / total : 0f;
        progressMaterial.SetFloat(ProgressID, progress);
    }

}
[System.Serializable]
public class Task_item
{
    public string Name;
    public string Date;
    public Status currentStatus;
}
[System.Serializable]
public enum Status
{
    Pending = 0,
    InProgress = 1,
    Completed = 2
}