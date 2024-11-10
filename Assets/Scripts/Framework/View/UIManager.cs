using System;
using System.Collections.Generic;
using NaiQiu.Framework.Resource;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NaiQiu.Framework.View
{
    public class UIManager : IResource
    {
        private List<View> views;
        private Dictionary<string, GameObject> dict = new();

        public Action OnLoaded;

        public Canvas GameUI { get; private set; }

        public UIManager(Dictionary<string, GameObject> dict)
        {
            this.dict = dict;
            views = new();
            GameUI = GameObject.Find("UICanvas").GetComponent<Canvas>();
        }

        /// <summary>
        /// 打开/关闭 View
        /// </summary>
        /// <param name="name">View名称</param>
        public void Toggle(string name)
        {
            if (TryGetView(name, out View view))
            {
                if (view.Visibility)
                {
                    StopView(view);
                }
                else
                {
                    StartView(view);
                }
            }
        }

        /// <summary>
        /// 打开 View
        /// </summary>
        /// <param name="name">View名称</param>
        public void Open(string name)
        {
            if (TryGetView(name, out View view))
            {
                if (!view.Visibility)
                {
                    StartView(view);
                }
                return;
            }
            if (CreateView(name, out view))
            {
                StartView(view);
            }
        }

        /// <summary>
        /// 打开 View，并获取 view 对象
        /// </summary>
        /// <param name="name">View名称</param>
        /// <param name="view">获取的 view 对象</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Open<T>(string name, out T view) where T : View
        {
            if (TryGetView(name, out view))
            {
                if (!view.Visibility)
                {
                    StartView(view);
                }
                return true;
            }
            if (CreateView(name, out View v))
            {
                view = v as T;
                StartView(view);
                return true;
            }
            return false;
        }

        public void Open(string name, View parent)
        {
            parent.Child = name;
            Close(parent);
        }

        /// <summary>
        /// 关闭 View
        /// </summary>
        /// <param name="name">View名称</param>
        public void Close(string name)
        {
            if (TryGetView(name, out View view))
            {
                Close(view);
            }
        }

        /// <summary>
        /// 关闭 View
        /// </summary>
        /// <param name="view">View对象</param>
        public void Close(View view)
        {
            if (view.Visibility)
            {
                StopView(view);
            }
        }

        /// <summary>
        /// 关闭最顶层的View，此View必须是可以回退的，CanBack == true
        /// </summary>
        public void CloseTopView()
        {
            if (views.Count == 0) return;

            View topView = null;
            foreach (var view in views)
            {
                if (view.ViewType == ViewType.Activity && view.Visibility)
                {
                    if (topView == null || topView.GetSortingOrder() < view.GetSortingOrder())
                    {
                        topView = view;
                    }
                }
            }
            if (topView != null)
            {
                Close(topView);
            }
        }

        /// <summary>
        /// 是否还有可以阻塞的View
        /// </summary>
        /// <returns></returns>
        public bool HasActivity()
        {
            int count = views.FindAll(v => v.ViewType == ViewType.Activity && v.Visibility).Count;
            return count > 0;
        }

        /// <summary>
        /// View关闭动画结束之后的操作
        /// </summary>
        /// <param name="view"></param>
        public void HandleClosed(View view)
        {
            if (view.Child != null)
            {
                Open(view.Child);
                view.Child = null;
            }
            else if (!view.Cache)
            {
                view.OnDestroy();
                views.Remove(view);
            }
        }

        /// <summary>
        /// 通过名称获取 View 对象
        /// </summary>
        /// <param name="name">View名称</param>
        /// <param name="view">获取的 view 对象</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool TryGetView<T>(string name, out T view) where T : View
        {
            View v = views.Find(v => v.Name == name);
            if (v != null)
            {
                view = v as T;
                return true;
            }

            if (CreateView(name, out v))
            {
                view = v as T;
                return true;
            }
            view = null;
            return false;
        }

        private bool CreateView(string name, out View view)
        {
            if (!dict[name].TryGetComponent(out View original))
            {
                view = null;
                return false;
            }

            view = Object.Instantiate(original);
            view.transform.SetParent(GameUI.transform, false);
            view.Name = name;
            view.OnCreate(this);

            views.Add(view);
            return true;
        }

        private bool TryGetView(string name, out View view)
        {
            view = views.Find(v => v.Name == name);
            if (view != null) return true;

            return CreateView(name, out view);
        }

        private void StartView(View view)
        {
            view.OnStart();

            if (view.ViewType == ViewType.Main
                || view.ViewType == ViewType.Activity
                || view.ViewType == ViewType.Dialog)
            {
                // Cursor.visible = true;
            }
            int count = views.FindAll(v => v.ViewType == view.ViewType && v.Visibility).Count - 1;
            view.SetSortingOrder(count);
        }

        private void StopView(View view)
        {
            view.OnStop();

            if (view.ViewType == ViewType.Main
                || view.ViewType == ViewType.Activity
                || view.ViewType == ViewType.Dialog)
            {
                // Cursor.visible = HasActivity();
            }
        }

        public void Clear()
        {
            
        }
    }
}
