using UnityEngine.Events;

namespace NaiQiu.Framework.View
{
    public interface IScroller
    {
        float Position { get; set; }

        void ScrollTo(float position, bool smooth = false);
    }

    public class ScrollerEvent : UnityEvent<float> { }
    public class MoveStopEvent : UnityEvent { }
    public class DraggingEvent : UnityEvent<bool> { }
}
