using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 选项
/// </summary>
public class OptionItem : MonoBehaviour
{
    OptionData op_data;

    public void Init(OptionData data)
    {
        op_data = data;
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            GameApp.MessageCenter.PostTempEvent(op_data.EventName); //执行配置配中的设置的事件
            GameApp.ViewManager.Close((int)ViewType.SelectOptionView);
        });
        transform.Find("txt").GetComponent<Text>().text = op_data.Name;
    }
}
