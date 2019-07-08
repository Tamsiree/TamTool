using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//动态生成游戏物体
public class GameObjectManager : MonoBehaviour
{
    public static GameObjectManager Instance = null;
    
    //游戏物体 字典
    private Dictionary<string, GameObject> gameObjectDict = new Dictionary<string, GameObject>();

    public void Init()
    {
        Instance = this;
    }

    public static GameObject LoadGameObject(string gameObjectPath, bool cache = false)
    {
        if (!Instance.gameObjectDict.TryGetValue(gameObjectPath, out GameObject gameObject))
        {
            gameObject = Resources.Load<GameObject>(gameObjectPath);
            if (cache)
            {
                Instance.gameObjectDict.Add(gameObjectPath, gameObject);
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

    public static void GenerateWoodChest(Vector3 position)
    {
        GameObject gameObject = Resources.Load<GameObject>("Model/ModelWoodChest");
        GameObject.Instantiate(gameObject, position + Vector3.up, Quaternion.identity);
    }

}
