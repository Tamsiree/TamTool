using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WoodChest : MonoBehaviour
{
    public GameObject reversePoint;
    public GameObject chestTop;

    [SerializeField]
    private bool isLocked = false;

    private float angle = 150;
    private float angleD = 6;
    private float angleTemp = 0;

    private bool isOn = false;

    //检测玩家是否在身边
    protected bool isInGround = false;

    private AudioSource chestAudio;

    // Start is called before the first frame update
    void Start()
    {
        chestAudio = this.GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            if (angleTemp < angle)
            {
                angleTemp += angleD;
                if (chestTop != null)
                {
                    chestTop.transform.RotateAround(reversePoint.transform.position, -reversePoint.transform.right, angleD);
                }
            }
        }
        else
        {
            if (0 < angleTemp)
            {
                angleTemp -= angleD;
                if (chestTop != null)
                {
                    chestTop.transform.RotateAround(reversePoint.transform.position, reversePoint.transform.right, angleD);
                }
            }
        }

    }

    //当鼠标位于这个collider之上的时候，会在每一帧调用这个方法
    protected void OnMouseOver()
    {
        //当点击了NPC之后
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (isInGround)
            {
                //点击宝箱事件
                //OnMouseClick();

                //宝箱是打开状态下，锁定状态自动变为解锁
                if (isOn)
                {
                    isLocked = false;
                }
                if (isLocked)
                {
                    GameRoot.AddTips("宝箱锁住了，无法打开");
                    if (chestAudio != null)
                    {
                        chestAudio.clip = AudioManager.Instance.LoadAudioClip("Sound/wood_box_locked", true);
                        if (chestAudio.isPlaying)
                        {
                            chestAudio.Stop();
                        }
                        chestAudio.Play();
                    }
                }
                else
                {
                    isOn = !isOn;
                    if (isOn)
                    {
                        if (chestAudio != null)
                        {
                            chestAudio.clip = AudioManager.Instance.LoadAudioClip("Sound/wood_box_opened", true);
                            if (chestAudio.isPlaying)
                            {
                                chestAudio.Stop();
                            }
                            chestAudio.Play();
                        }
                    }
                    else
                    {
                        if (chestAudio != null)
                        {
                            chestAudio.clip = AudioManager.Instance.LoadAudioClip("Sound/wood_box_closed1", true);
                            if (chestAudio.isPlaying)
                            {
                                chestAudio.Stop();
                            }
                            chestAudio.Play();
                        }
                    }
                }
            }
            else
            {
                GameRoot.AddTips("距离太远，无法打开");
            }
        }
    }


    protected void OnTriggerStay(Collider other)
    {
        if (other.tag == Tags.player)
        {
            isInGround = true;
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.tag == Tags.player)
        {
            isInGround = false;
        }
    }

    protected void OnMouseEnter()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            CursorManager.Instance.SetPick();
        }
    }

    protected void OnMouseExit()
    {
        CursorManager.Instance.SetNormal();
    }

    public void SetLock(bool locked)
    {
        isLocked = locked;
    }

    public bool GetLock()
    {
        return isLocked;
    }

}
