using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{

    public static GameRoot Instance = null;

    public DynamicText dynamicText;

    private void Awake()
    {
        Instance = this;
        //当前场景被销毁时保留
        DontDestroyOnLoad(this);
        //初始化鼠标指针管理者
        CursorManager cursorManager = GetComponent<CursorManager>();
        cursorManager.Init();
        //初始化声音管理者
        AudioManager audioManager = GetComponent<AudioManager>();
        audioManager.Init();
        //初始化特效管理者
        EffectManager effectManager = GetComponent<EffectManager>();
        effectManager.Init();
        //初始化游戏物体管理者
        GameObjectManager gameObjectManager = GetComponent<GameObjectManager>();
        gameObjectManager.Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        dynamicText.SetWindState(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void AddTips(string tips)
    {
        Instance.dynamicText.AddTips(tips);
    }
}
