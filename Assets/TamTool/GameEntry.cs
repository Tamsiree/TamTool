using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TamLee
{
    public class GameEntry : MonoBehaviour
    {


        #region 组件属性
        public static EventComponent Event
        {
            get;
            private set;
        }

        public static TimeComponent Time
        {
            get;
            private set;
        }

        public static FsmComponent Fsm
        {
            get;
            private set;
        }

        public static ProcedureComponent Procedure
        {
            get;
            private set;
        }

        public static DataTableComponent DataTable
        {
            get;
            private set;
        }
        public static SocketComponent Socket
        {
            get;
            private set;
        }
        public static HttpComponent Http
        {
            get;
            private set;
        }
        public static DataComponent Data
        {
            get;
            private set;
        }
        public static LocalizationComponent Localization
        {
            get;
            private set;
        }
        public static PoolComponent Pool
        {
            get;
            private set;
        }
        public static SceneComponent Scene
        {
            get;
            private set;
        }
        public static SettingComponent Setting
        {
            get;
            private set;
        }
        public static GameObjComponent GameObj
        {
            get;
            private set;
        }
        public static ResourceComponent Resource
        {
            get;
            private set;
        }
        public static DownloadComponent DownLoad
        {
            get;
            private set;
        }
        public static UIComponent UI
        {
            get;
            private set;
        }
        #endregion

        #region 基础组件管理
        /// <summary>
        /// 基础组件列表
        /// </summary>

        private static readonly LinkedList<TamLeeBaseComponent> m_BaseComponentList = new LinkedList<TamLeeBaseComponent>();

        #region 注册组件
        internal static void RegisterBaseComponent(TamLeeBaseComponent component)
        {
            Type type = component.GetType();
            LinkedListNode<TamLeeBaseComponent> curr = m_BaseComponentList.First;
            while (curr != null)
            {
                if (curr.Value.GetType() == type) return;
                curr = curr.Next;
            }
            //Debug.Log("" + type.Name + "已经加入链表");
            //把组件加入最后一个节点
            m_BaseComponentList.AddLast(component);
        }
        #endregion

        #region        获取基础组件
        private static T GetBaseComponent<T>() where T : TamLeeBaseComponent
        {
            return (T)GetBaseComponent(typeof(T));
        }

        internal static TamLeeBaseComponent GetBaseComponent(Type type)
        {
            LinkedListNode<TamLeeBaseComponent> curr = m_BaseComponentList.First;
            while (curr != null)
            {
                if (curr.Value.GetType() == type)
                {
                    return curr.Value;
                }
                curr = curr.Next;
            }
            return null;
        }
        #endregion

        #region 初始化组件
        private static void InitBaseComponents()
        {
            Event = GetBaseComponent<EventComponent>();
            Time = GetBaseComponent<TimeComponent>();
            Fsm = GetBaseComponent<FsmComponent>();
            Procedure = GetBaseComponent<ProcedureComponent>();
            DataTable = GetBaseComponent<DataTableComponent>();
            Socket = GetBaseComponent<SocketComponent>();
            Http = GetBaseComponent<HttpComponent>();
            Data = GetBaseComponent<DataComponent>();
            Localization = GetBaseComponent<LocalizationComponent>();
            Pool = GetBaseComponent<PoolComponent>();
            Scene = GetBaseComponent<SceneComponent>();
            GameObj = GetBaseComponent<GameObjComponent>();
            Resource = GetBaseComponent<ResourceComponent>();
            DownLoad = GetBaseComponent<DownloadComponent>();
            UI = GetBaseComponent<UIComponent>();
        }
        #endregion

        #endregion

        #region 注册更新组件

        private static readonly LinkedList<IUpdateComponent> m_UpdateComponentList = new LinkedList<IUpdateComponent>();

        public static void RegisterUpdateComponent(IUpdateComponent component)
        {
            m_UpdateComponentList.AddLast(component);
        }

        /// <summary>
        /// 移除更新组件
        /// </summary>
        /// <param name="component"></param>
        public static void RemoveUpdateComponent(IUpdateComponent component)
        {
            m_UpdateComponentList.Remove(component);
        }
        #endregion

        void Start()
        {
            InitBaseComponents();
        }

        // Update is called once per frame
        void Update()
        {
            //循环更新组件
            for (LinkedListNode<IUpdateComponent> curr = m_UpdateComponentList.First; curr != null; curr = curr.Next)
            {
                curr.Value.OnUpdate();
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                RemoveUpdateComponent(UI);
            }
        }
        private void OnDestroy()
        {
            //关闭所有基础组件
            for (LinkedListNode<TamLeeBaseComponent> curr = m_BaseComponentList.First; curr != null; curr = curr.Next)
            {
                curr.Value.Shutdown();
            }
        }
    }
}