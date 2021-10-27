using UnityEngine;
using UnityEngine.Playables;

namespace KhaoTom.Timeline
{

    public class PlayableDirectorManualUpdater : MonoBehaviour
    {
        public PlayableDirector playableDirector;

        public void PlayForward(float deltaTime)
        {
            playableDirector.Evaluate(deltaTime);
        }

        public void PlayBackward(float deltaTime)
        {
            playableDirector.EvaluateBackward(deltaTime);
        }

        public void PlayByRatio(float ratio)
        {
            Debug.Log(ratio);
            var deltaTime = (float)playableDirector.duration * ratio;
            if (deltaTime < 0f)
            {
                playableDirector.EvaluateBackward(Mathf.Abs(deltaTime));
            }
            else
            {
                playableDirector.Evaluate(deltaTime);
            }
        }
    }
}
