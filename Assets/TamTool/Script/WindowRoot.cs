using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowRoot : MonoBehaviour
{

    public void SetWindState(bool isActive)
    {
        if (gameObject.activeSelf != isActive)
        {
            SetActive(gameObject, isActive);
        }
        if (isActive)
        {
            InitWind();
        }
        else
        {
            ClearWind();
        }
    }

    protected virtual void InitWind()
    {

    }

    protected virtual void ClearWind()
    {

    }
    protected void SetActive(GameObject gameObject, bool isActive = true)
    {
        gameObject.SetActive(isActive);
    }
    protected void SetActive(Transform trans, bool state = true) { trans.gameObject.SetActive(state); }
    protected void SetActive(RectTransform rectTrans, bool state = true) { rectTrans.gameObject.SetActive(state); }
    protected void SetActive(Image img, bool state = true) { img.transform.gameObject.SetActive(state); }
    protected void SetActive(Text txt, bool state = true) { txt.transform.gameObject.SetActive(state); }
    protected void SetText(Transform trans, int num = 0) { SetText(trans.GetComponent<Text>(), num); }
    protected void SetText(Transform trans, string context = "") { SetText(trans.GetComponent<Text>(), context); }
    protected void SetText(Text txt, int num = 0) { SetText(txt, num.ToString()); }

    protected void SetText(Text txt, string context = "")
    {
        txt.text = context;
    }
}
