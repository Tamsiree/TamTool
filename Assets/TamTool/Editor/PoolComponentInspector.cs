using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TamLee
{
    [CustomEditor(typeof(PoolComponent), true)]

    public class PoolComponentInspector : Editor
    {
        //释放间隔属性
        private SerializedProperty m_ClearInterval = null;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            PoolComponent component = base.target as PoolComponent;

            //绘制滑动条
            int clearInterval = (int)EditorGUILayout.Slider("清空类对象池间隔", m_ClearInterval.intValue, 10, 1800);
            if (clearInterval != m_ClearInterval.intValue)
            {
                component.m_ClearInterval = clearInterval;
            }
            else
            {
                m_ClearInterval.intValue = clearInterval;
            }

            GUILayout.BeginHorizontal("box");
            GUILayout.Label("类名");
            GUILayout.Label("池中数量", GUILayout.Width(50));
            GUILayout.Label("常驻数量", GUILayout.Width(50));
            GUILayout.EndHorizontal();
            if (component == null || component.PoolManager == null) return;

            foreach (var item in component.PoolManager.ClassObjectPool.InspectorDic)
            {
                GUILayout.BeginHorizontal("box");
                GUILayout.Label(item.Key.Name);
                GUILayout.Label(item.Value.ToString(), GUILayout.Width(50));

                int key = item.Key.GetHashCode();

                byte resideCount = 0;
                component.PoolManager.ClassObjectPool.ClassObjectCount.TryGetValue(key, out resideCount);
                GUILayout.Label(resideCount.ToString(), GUILayout.Width(50));
                GUILayout.EndHorizontal();
            }
            serializedObject.ApplyModifiedProperties();
            //重绘
            Repaint();
        }

        private void OnEnable()
        {
            //建立属性关系,
            m_ClearInterval = serializedObject.FindProperty("m_ClearInterval");
            serializedObject.ApplyModifiedProperties();
        }
    }
}