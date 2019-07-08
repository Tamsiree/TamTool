using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance = null;

    private Dictionary<string, GameObject> effectDict = new Dictionary<string, GameObject>();

    public void Init()
    {
        Instance = this;
    }

    public static GameObject LoadEffect(string effectPath, bool cache = false)
    {
        GameObject gameObject = null;
        if (!Instance.effectDict.TryGetValue(effectPath, out gameObject))
        {
            gameObject = Resources.Load<GameObject>(effectPath);
            //gameObject = (GameObject)Resources.Load(effectPath);
            if (cache)
            {
                Instance.effectDict.Add(effectPath, gameObject);
            }
        }
        return gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void SwordNormalAttack(Vector3 position)
    {
        GameObject effectGameObject = EffectManager.LoadEffect("Effect/Effect_Slash2", true);
        GameObject g = (GameObject)Instantiate(effectGameObject, position, Quaternion.identity);
        g.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Destroy(g, 1.1f);
    }


}
