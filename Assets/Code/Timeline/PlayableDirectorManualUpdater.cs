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
    }

}
