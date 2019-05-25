using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VonTool : MonoBehaviour
{
    public static VonTool _instance;

    public bl_HUDText HUDRoot;

    public GameObject smog;

    private void Awake()
    {
        _instance = this;
    }

    public void NewHUDText(string sub, Color textColor, Transform transform)
    {
        HUDTextInfo info7 = new HUDTextInfo(transform, sub);
        info7.Color = textColor;
        info7.Size = 20;
        info7.Speed = Random.Range(0.2f, 1);
        info7.VerticalAceleration = Random.Range(-2, 2f);
        info7.VerticalPositionOffset = 0.2f;
        info7.VerticalFactorScale = Random.Range(1.2f, 10);
        info7.Side = (Random.Range(0, 2) == 1) ? bl_Guidance.LeftDown : bl_Guidance.RightDown;
        info7.ExtraDelayTime = -1;
        info7.AnimationType = bl_HUDText.TextAnimationType.PingPong;
        info7.FadeSpeed = 200;
        info7.ExtraFloatSpeed = -11;
        //Send the information
        HUDRoot.NewText(info7);
    }

    public void NewHUDTextUP(string sub, Color textColor, Transform transform)
    {
        HUDTextInfo info7 = new HUDTextInfo(transform, sub);
        info7.Color = textColor;
        info7.Size = 20;
        info7.Speed = Random.Range(0.9f, 1f);
        info7.VerticalAceleration = Random.Range(-2, 0f);
        info7.VerticalPositionOffset = 0.2f;
        info7.VerticalFactorScale = Random.Range(1.2f, 10);
        info7.Side = bl_Guidance.Up;
        info7.ExtraDelayTime = -1;
        info7.AnimationType = bl_HUDText.TextAnimationType.SmallToNormal;
        info7.FadeSpeed = 200;
        info7.ExtraFloatSpeed = -11;
        //Send the information
        HUDRoot.NewText(info7);
    }

    public GameObject BuildSmog(Transform smogTransform)
    {
        if (smog != null)
        {
            GameObject g = (GameObject)Instantiate(smog, (smogTransform.position), smogTransform.rotation);
            g.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Destroy(g, 1.5f);
            return g;
        }
        else
        {
            return null;
        }
    }

    public static void ChangeLayer(Transform trans, string targetLayer)
    {
        if (LayerMask.NameToLayer(targetLayer) == -1)
        {
            Debug.Log("Layer中不存在,请手动添加LayerName");

            return;
        }
        //遍历更改所有子物体layer
        trans.gameObject.layer = LayerMask.NameToLayer(targetLayer);
        foreach (Transform child in trans)
        {
            ChangeLayer(child, targetLayer);
            Debug.Log(child.name + "子对象Layer更改成功！");
        }
    }

    /**
     * 判断物体是否在主摄像机视野内
     **/
    public static bool IsInView(Vector3 worldPos)
    {
        Transform camTransform = Camera.main.transform;
        Vector2 viewPos = Camera.main.WorldToViewportPoint(worldPos);
        Vector3 dir = (worldPos - camTransform.position).normalized;
        float dot = Vector3.Dot(camTransform.forward, dir);     //判断物体是否在相机前面
        if (dot > 0 && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1)
        {
            Ray ray = Camera.main.ScreenPointToRay(worldPos);
            RaycastHit raycastHit;
            bool isCollider = Physics.Raycast(ray, out raycastHit);
            if (isCollider && raycastHit.collider.tag == Tags.enemy)
            {
                return true;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    public static Color GetColorForString(string strColor)
    {
        Color nowColor;
        ColorUtility.TryParseHtmlString(strColor, out nowColor);
        return nowColor;
    }



    /// <summary>  
    /// 三维线  
    /// </summary>  
    /// <param name='PointA'>  
    /// 初始点  
    /// </param>  
    /// <param name='PointB'>  
    /// 结束点  
    /// </param>  
    public static void DrawLine3D(Vector3 PointA, Vector3 PointB)
    {
        GameObject LineObj = new GameObject("Lines");
        float LineWidth = 0.1f;
        float HorDisABx = PointB.x - PointA.x;
        float HorDisABz = PointB.z - PointA.z;
        float HorDisAB = Mathf.Sqrt(Mathf.Pow(HorDisABx, 2) + Mathf.Pow(HorDisABz, 2));  //求起点和终点的模长

        float offsetX = HorDisABz * LineWidth / HorDisAB;
        float offsetZ = HorDisABx * LineWidth / HorDisAB;

        Vector3 Point1 = new Vector3(PointA.x - offsetX, PointA.y, PointA.z + offsetZ);
        Vector3 Point2 = new Vector3(PointA.x + offsetX, PointA.y, PointA.z - offsetZ);
        Vector3 Point3 = new Vector3(PointB.x + offsetX, PointB.y, PointB.z - offsetZ);
        Vector3 Point4 = new Vector3(PointB.x - offsetX, PointB.y, PointB.z + offsetZ);

        string guid = System.Guid.NewGuid().ToString("N");

        GameObject go1 = new GameObject(guid + "_1");
        go1.transform.parent = LineObj.transform;
        Mesh mesh1 = go1.AddComponent<MeshFilter>().mesh;
        go1.AddComponent<MeshRenderer>();
        mesh1.vertices = new Vector3[] { Point1, Point2, Point3, Point4 };
        mesh1.triangles = new int[] { 2, 1, 0, 0, 3, 2 };

        Vector3 Point5 = new Vector3(PointA.x - offsetX, PointA.y + 2 * LineWidth, PointA.z + offsetZ);
        Vector3 Point6 = new Vector3(PointA.x + offsetX, PointA.y + 2 * LineWidth, PointA.z - offsetZ);
        Vector3 Point7 = new Vector3(PointB.x + offsetX, PointB.y + 2 * LineWidth, PointB.z - offsetZ);
        Vector3 Point8 = new Vector3(PointB.x - offsetX, PointB.y + 2 * LineWidth, PointB.z + offsetZ);

        GameObject go2 = new GameObject(guid + "_2");
        go2.transform.parent = LineObj.transform;
        Mesh mesh2 = go2.AddComponent<MeshFilter>().mesh;
        go2.AddComponent<MeshRenderer>();
        mesh2.vertices = new Vector3[] { Point5, Point6, Point7, Point8 };
        mesh2.triangles = new int[] { 2, 1, 0, 0, 3, 2 };

        GameObject go3 = new GameObject(guid + "_3");
        go3.transform.parent = LineObj.transform;
        Mesh mesh3 = go3.AddComponent<MeshFilter>().mesh;
        go3.AddComponent<MeshRenderer>();
        mesh3.vertices = new Vector3[] { Point6, Point2, Point3, Point7 };
        mesh3.triangles = new int[] { 2, 1, 0, 0, 3, 2 };

        GameObject go4 = new GameObject(guid + "_4");
        go4.transform.parent = LineObj.transform;
        Mesh mesh4 = go4.AddComponent<MeshFilter>().mesh;
        go4.AddComponent<MeshRenderer>();
        mesh4.vertices = new Vector3[] { Point1, Point5, Point8, Point4 };
        mesh4.triangles = new int[] { 2, 1, 0, 0, 3, 2 };

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.parent = LineObj.transform;
        sphere.GetComponent<SphereCollider>().isTrigger = true;
        sphere.transform.position = new Vector3(PointB.x, PointB.y + LineWidth, PointB.z);
        sphere.transform.localScale = new Vector3(LineWidth * Mathf.Sqrt(8.0f), LineWidth * Mathf.Sqrt(8.0f), LineWidth * Mathf.Sqrt(8.0f));
    }

    //根据碰撞器的高度 获取头顶位置
    public static Vector3 GetColliderHead(GameObject gameObject)
    {
        return gameObject.transform.GetComponent<Collider>().bounds.center + (((Vector3.up * gameObject.transform.GetComponent<Collider>().bounds.size.y) * 0.5f));
    }

    //根据碰撞器的高度 获取中间位置
    public static Vector3 GetColliderCenter(GameObject gameObject)
    {
        return gameObject.transform.GetComponent<Collider>().bounds.center;
    }
}
