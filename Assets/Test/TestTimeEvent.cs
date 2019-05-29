using System.Collections;
using System.Collections.Generic;
using TamLee;
using UnityEngine;

public class TestTimeEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameEntry.Event.CommonEvent.AddEventListener(CommonEventId.RegComplete, OnRegComplete);
    }

    private void OnRegComplete(object userData)
    {
        Debug.Log(userData);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameEntry.Event.CommonEvent.Dispatch(CommonEventId.RegComplete, 123);
        }
    }
    private void OnDestroy()
    {
        GameEntry.Event.CommonEvent.RemoveEventListener(CommonEventId.RegComplete, OnRegComplete);
    }
}
