using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settingdummyinfo : MonoBehaviour
{

    [Header("UI References")]
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text text;

    [Header("Data")]
    [SerializeField] private string nameText;
    [SerializeField] private Sprite image;



    private void Awake()
    {
        if (icon != null)
            icon.sprite = image;

        if (text != null)
            text.text = nameText;    
    }


}
