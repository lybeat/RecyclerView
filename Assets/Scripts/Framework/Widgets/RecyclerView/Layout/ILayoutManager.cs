using UnityEngine;

namespace NaiQiu.Framework.View
{
    public interface ILayoutManager
    {
        /// <summary>
        /// 滚动时，刷新整个页面的布局
        /// </summary>
        void UpdateLayout();

        /// <summary>
        /// 为 ViewHolder 设置布局
        /// </summary>
        /// <param name="viewHolder"></param>
        /// <param name="index"></param>
        void Layout(ViewHolder viewHolder, int index);

        /// <summary>
        /// 设置 Content 大小
        /// </summary>
        void SetContentSize();

        /// <summary>
        /// 计算 Content 的大小
        /// </summary>
        /// <returns></returns>
        Vector2 CalculateContentSize();

        /// <summary>
        /// 计算第 index 个 ViewHolder 到顶部的距离
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        Vector2 CalculatePosition(int index);

        /// <summary>
        /// 计算 ViewHolder 相对于内容长度的偏移
        /// </summary>
        /// <returns></returns>
        Vector2 CalculateContentOffset();

        /// <summary>
        /// 计算 ViewHolder 相对于视口的偏移
        /// </summary>
        /// <returns></returns>
        Vector2 CalculateViewportOffset();

        /// <summary>
        /// 获取当前显示的第一个 ViewHolder 下标
        /// </summary>
        /// <returns></returns>
        int GetStartIndex();

        /// <summary>
        /// 获取当前显示的最后一个 ViewHolder 下标
        /// </summary>
        /// <returns></returns>
        int GetEndIndex();

        /// <summary>
        /// 数据下标转换成在布局中对应的位置
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        float IndexToPosition(int index);

        /// <summary>
        /// 在布局中的位置转换成数据下标
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        int PositionToIndex(float position);

        /// <summary>
        /// 滚动时，item 对应的动画
        /// </summary>
        void DoItemAnimation();

        /// <summary>
        /// 判断第一个 ViewHolder 是否完全可见
        /// </summary>
        /// <param name="index">数据的真实下标</param>
        /// <returns></returns>
        bool IsFullVisibleStart(int index);

        /// <summary>
        /// 判断第一个 ViewHolder 是否完全不可见
        /// </summary>
        /// <param name="index">数据的真实下标</param>
        /// <returns></returns>
        bool IsFullInvisibleStart(int index);

        /// <summary>
        /// 判定最后一个 ViewHolder 是否完全可见
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        bool IsFullVisibleEnd(int index);

        /// <summary>
        /// 判定最后一个 ViewHolder 是否完全不可见
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        bool IsFullInvisibleEnd(int index);

        /// <summary>
        /// 判定第 index ViewHolder是否可见
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        bool IsVisible(int index);
    }
}
