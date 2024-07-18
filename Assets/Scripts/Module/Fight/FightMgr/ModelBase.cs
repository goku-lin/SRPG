using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单位的阵营，不同阵营的可以互相攻击
/// </summary>
public enum ModelSect
{
    Player,
    Enemy,
    /// <summary>
    /// player阵营的，默认只能自动
    /// </summary>
    Reinforcements,
    Third,
    Fourth,
    /// <summary>
    /// 机关，障碍物之类的
    /// </summary>
    Object,
    Max,    //无意义，计数用
}

public class ModelBase : MonoBehaviour
{
    public int Id;  //物体id
    public Dictionary<string, string> data; //数据表
    public int Step;    //行动力
    public int Attack;  //攻击力
    public int Type;    //类型
    public int MaxHp;   //最大血量
    public int CurHp;   //当前血量

    public int RowIndex;
    public int ColIndex;
    public SpriteRenderer bodySp;   //身体图片渲染组件
    public GameObject stopObj;      //停止行动的标记物体
    public Animator ani;    //动画组件

    private bool _isStop;   //是否移动完成
    public ModelSect sect; //阵营

    public bool IsStop
    {
        get { return _isStop; }
        set
        {
            stopObj.SetActive(value);

            if (value)
            {
                bodySp.color = Color.gray;
            }
            else
            {
                bodySp.color = Color.white;
            }

            _isStop = value;
        }
    }

    private void Awake()
    {
        bodySp = transform.Find("body").GetComponent<SpriteRenderer>();
        stopObj = transform.Find("stop").gameObject;
        ani = transform.Find("body").GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        AddEvents();
    }

    protected virtual void OnDestroy()
    {
        RemoveEvents();
    }

    //注册事件
    protected virtual void AddEvents()
    {
        GameApp.MessageCenter.AddEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
        GameApp.MessageCenter.AddEvent(Defines.OnUnSelectEvent, OnUnSelectCallBack);
    }

    //注销事件
    protected virtual void RemoveEvents()
    {
        GameApp.MessageCenter.RemoveEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
        GameApp.MessageCenter.RemoveEvent(Defines.OnUnSelectEvent, OnUnSelectCallBack);
    }

    //选中回调
    protected virtual void OnSelectCallBack(System.Object arg)
    {
        //执行未选中
        GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent);

        GameApp.MapManager.ShowStepGrid(this, Step);
    }

    //未选中回调
    protected virtual void OnUnSelectCallBack(System.Object arg)
    {
        GameApp.MapManager.HideStepGrid(this, Step);
    }

    //转向
    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    //移动到指定下标的格子
    public virtual bool Move(int rowIndex, int colIndex, float dt)
    {
        Vector3 pos = GameApp.MapManager.GetBlockPos(rowIndex, colIndex);

        pos.z = transform.position.z;

        if (transform.position.x > pos.x && transform.localScale.x > 0)
        {
            //转向
            Flip();
        }
        if (transform.position.x < pos.x && transform.localScale.x < 0)
        {
            Flip();
        }

        //如果离目的地很近 返回true
        if (Vector3.Distance(transform.position, pos) <= 0.02f)
        {
            this.RowIndex = rowIndex;
            this.ColIndex = colIndex;
            transform.position = pos;
            return true;
        }

        transform.position = Vector3.MoveTowards(transform.position, pos, dt);

        return false;
    }

    //播放动画
    public void PlayAni(string aniName)
    {
        ani.Play(aniName);
    }

    //受伤
    public virtual void GetHit(ISkill skill)
    {

    }

    //播放特效(特效物体)
    public virtual void PlayEffect(string name)
    {
        GameObject obj = Instantiate(Resources.Load($"Effect/{name}")) as GameObject;
        obj.transform.position = transform.position;
    }

    //计算两个model的距离（行列坐标计算）
    public float GetDis(ModelBase model)
    {
        return Mathf.Abs(RowIndex - model.RowIndex) + Mathf.Abs(ColIndex - model.ColIndex);
    }

    //播放音效（攻击 受伤等）
    public void PlaySound(string name)
    {
        GameApp.SoundManager.PlayEffect(name, transform.position);
    }

    //看向某个model
    public void LookAtModel(ModelBase model)
    {
        if ((model.transform.position.x > transform.position.x) && transform.localScale.x < 0)
        {
            Flip();
        }
        else if ((model.transform.position.x < transform.position.x) && transform.localScale.x > 0)
        {
            Flip();
        }
    }
}
