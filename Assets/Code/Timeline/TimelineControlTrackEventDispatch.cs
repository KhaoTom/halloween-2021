using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Timeline;

namespace KhaoTom.Timeline
{
    /// <summary>
    /// General purpose component for responding to ControlTrack events in a Timeline.
    /// </summary>
    /// <remarks>
    /// ControlTrack events are sent even when the PlayableDirector consuming the timeline is set to Manual update mode.
    /// This makes them a decent replacement for Timeline Signals when Timeline is being driven manually rather than by a clock.
    /// </remarks>
    public class TimelineControlTrackEventDispatch : MonoBehaviour, ITimeControl
    {
        public UnityEvent onControlTrackIn;
        public UnityEvent onControlTrackOut;
        public UnityEvent<double> onControlTrackTimeDouble;
        public UnityEvent<float> onControlTrackTimeFloat;

        public void SetTime(double time)
        {
            onControlTrackTimeDouble.Invoke(time);
            onControlTrackTimeFloat.Invoke((float)time);
        }

        public void OnControlTimeStart()
        {
            onControlTrackIn.Invoke();
        }

        public void OnControlTimeStop()
        {
            onControlTrackOut.Invoke();
        }
    }
}
