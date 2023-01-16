using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResponseButton : MonoBehaviour
{
    public Button button;
    public TMP_Text text;
    public void SetText(string value) => text.text = value;
    
    [ReadOnly] public int responseId;
    void Start() => button.onClick.AddListener(OnClick);
    void OnClick() { onClick?.Invoke(responseId); }
    public Action<int> onClick;
}