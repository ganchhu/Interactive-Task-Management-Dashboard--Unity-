using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Taskitementry : MonoBehaviour
{
    [SerializeField] public int id;
    [SerializeField] public Task_item task;
    [SerializeField] TMP_Text taskName, taskDate, taskcurrentStatus;
    [SerializeField] public TMP_Dropdown statusDropdown;

    private void OnEnable()
    {
        statusDropdown.onValueChanged.AddListener(OnStatusChanged);
    }
    private void OnDisable()
    {
        statusDropdown.onValueChanged.RemoveListener(OnStatusChanged);
    }

    public void Populate(int id,bool Editiable )
    {
        this.id = id;
        taskName.text = id.ToString() + " " + task.Name.ToString();
        taskDate.text = task.Date.ToString();
        if(Editiable)
        {
            statusDropdown.gameObject.SetActive(true);
            taskcurrentStatus.gameObject.SetActive(false);
            statusDropdown.SetValueWithoutNotify((int)task.currentStatus);

        }
        else {
            taskcurrentStatus.gameObject.SetActive(true);
            taskcurrentStatus.text = task.currentStatus.ToString();
            statusDropdown.gameObject.SetActive(false);
        }       
    }
    private void OnStatusChanged(int value)
    {
        Status newStatus = (Status)value;
        Debug.Log("Status changed to: " + newStatus);       
        if (task.currentStatus == newStatus)
            return;
        task.currentStatus = newStatus;
        Control_manager.CM_Instance.Getinfoontask();
    }
}
