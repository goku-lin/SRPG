using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPoint : MonoBehaviour
{
    public int LevelID; //关卡ID

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameApp.MessageCenter.PostEvent(Defines.ShowLevelDesEvent, LevelID);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameApp.MessageCenter.PostEvent(Defines.HideLevelDesEvent);
    }
}
