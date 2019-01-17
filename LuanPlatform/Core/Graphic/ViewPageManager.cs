using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;
using System.Windows.Navigation;
using LuanUtils;
using LuanPlatform.PageView;
using LuanCore;
namespace LuanPlatform.Core.Graphic
{
    static class ViewPageManager
    {
        /// <summary>
        /// 在页面管理器中注册一个页面
        /// </summary>
        /// <param name="pageId">页面唯一标识符</param>
        /// <param name="pageRef">页引用</param>
        /// <returns>是否发生了覆盖</returns>
        public static bool RegisterPage(string pageId, Page pageRef)
        {
            LogUtils.Log("Register Page: " + pageId, "ViewPage Manager", LogLevel.Info);
            bool rFlag = ViewPageManager.pageDict.ContainsKey(pageId);
            ViewPageManager.pageDict[pageId] = pageRef;
            return rFlag;
        }

        /// <summary>
        /// 通过页面的唯一标识符获取页面的引用
        /// </summary>
        /// <param name="pageId">页面唯一标识符</param>
        /// <returns>页引用</returns>
        public static Page RetrievePage(string pageId)
        {
            return ViewPageManager.pageDict.ContainsKey(pageId) ? ViewPageManager.pageDict[pageId] : null;
        }

        /// <summary>
        /// 清空管理器中所有的WPF页引用
        /// </summary>
        public static void Clear()
        {
            ViewPageManager.pageDict.Clear();
        }

        /// <summary>
        /// 导航到目标页面
        /// </summary>
        /// <param name="toPageName">目标页面在页管理器里的唯一标识符</param>
        public static void NavigateTo(string toPageName)
        {
            // 不在主舞台就不处理调用堆栈
            if ((ViewPageManager.CurrentPage is Stage) &&
                toPageName != GlobalConfig.FirstViewPage)
            {
                World.PauseUpdateContext();
            }
            var rp = ViewPageManager.RetrievePage(toPageName);
            try
            {
                if (rp != null && ViewPageManager.CurrentPage != null)
                {
                    NavigationService.GetNavigationService(ViewPageManager.CurrentPage)?.Navigate(rp);
                    ViewPageManager.PageCallStack.Push(rp);
                }
                else
                {
                    LogUtils.Log(string.Format("Cannot find page: {0}, Navigation service ignored.", toPageName),
                        "ViewPageManager", LogLevel.Error);
                 }
            }
            catch (Exception ex)
            {
                LogUtils.Log(string.Format("Cannot find page: {0}, Navigation service ignored. {1}", toPageName, ex),
                        "ViewPageManager", LogLevel.Error);
             }
            // 如果目标页是主舞台就恢复处理调用堆栈
            if (toPageName == GlobalConfig.FirstViewPage)
            {
                World.ResumeUpdateContext();
            }
        }

        public static bool ShowUIPage(string uiPageName)
        {
            try
            {
                var up = ViewPageManager.RetrievePage(uiPageName);
                if (up == null)
                {
                    if (ViewPageManager.typeDict.ContainsKey(uiPageName))
                    {
                        var pageType = ViewPageManager.typeDict[uiPageName];
                        var pageObj = (Page)Activator.CreateInstance(pageType);
                        ViewPageManager.RegisterPage(uiPageName, pageObj);
                        ViewManager.mWnd.uiFrame.Visibility = System.Windows.Visibility.Visible;
                        ViewManager.mWnd.uiFrame.Content = pageObj;
                        return true;
                    }
                    return false;
                }
                ViewManager.mWnd.uiFrame.Visibility = System.Windows.Visibility.Visible;
                ViewManager.mWnd.uiFrame.Content = up;
                return true;
            }
            catch (Exception ex)
            {
                LogUtils.Log("Show UI Page in uiframe failed. " + ex, "ViewPageManager", LogLevel.Error);
                return false;
            }
        }

        /// <summary>
        /// 返回导航前的页面
        /// </summary>
        public static void GoBack()
        {
            try
            {
                if (ViewPageManager.CurrentPage != null && ViewPageManager.CurrentPage.NavigationService != null &&
                ViewPageManager.CurrentPage.NavigationService.CanGoBack)
                {
                    ViewPageManager.CurrentPage.NavigationService.GoBack();
                    ViewPageManager.PageCallStack.Pop();
                }
                else
                {
                    LogUtils.Log(string.Format("Cannot go back from page: {0}, Navigation service ignored.", ViewPageManager.CurrentPage?.Name),
                        "ViewPageManager", LogLevel.Error);
                 }
                if (ViewPageManager.CurrentPage is Stage)
                {
                    World.ResumeUpdateContext();
                }
            }
            catch (Exception ex)
            {
                LogUtils.Log(string.Format("Cannot go back from page: {0}, Navigation service ignored. {1}", ViewPageManager.CurrentPage?.Name, ex),
                        "ViewPageManager", LogLevel.Error);
             }
        }

        /// <summary>
        /// 获取当前是否位于主舞台页面
        /// </summary>
        /// <returns>是否在主舞台</returns>
        public static bool IsAtMainStage()
        {
            return ViewPageManager.PageCallStack.Count > 0 &&
                (ViewPageManager.PageCallStack.Peek() is PageView.Stage);
        }

        public static void InitFirstPage(Page fp)
        {
            ViewPageManager.PageCallStack.Push(fp);
        }

        /// <summary>
        /// 获取当前呈现在屏幕上的页面
        /// </summary>
        public static Page CurrentPage => ViewPageManager.PageCallStack.Count > 0 ? ViewPageManager.PageCallStack.Peek() : null;

        /// <summary>
        /// 页面转移栈
        /// </summary>
        private static readonly Stack<Page> PageCallStack = new Stack<Page>();

        /// <summary>
        /// 前端页引用字典
        /// </summary>
        private static readonly Dictionary<string, Page> pageDict = new Dictionary<string, Page>();

        /// <summary>
        /// 前端页类型字典
        /// </summary>
        private static readonly Dictionary<string, Type> typeDict = new Dictionary<string, Type>();
    }
}
