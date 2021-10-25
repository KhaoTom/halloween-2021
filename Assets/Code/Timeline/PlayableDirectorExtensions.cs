using UnityEngine.Playables;

namespace KhaoTom.Timeline
{
    public static class PlayableDirectorExtensions
    {
        /// <summary>
        /// Moves time forwards and evaluate playable graph with specified delta time. 
        /// </summary>
        public static void Evaluate(this PlayableDirector director, float deltaTime)
        {
            director.playableGraph.Evaluate(deltaTime);
        }

        /// <summary>
        /// Moves time backwards and evaluates playable graph with specified delta time.
        /// </summary>
        public static void EvaluateBackward(this PlayableDirector director, float deltaTime)
        {
            var dt = deltaTime;
            var t = director.time - dt;

            if (t < 0f)
            {
                if (director.extrapolationMode == DirectorWrapMode.Loop)
                {
                    t = director.duration + t;
                }
                else
                {
                    dt = (float)t;
                    t = 0;
                }
            }

            director.time = t;
            director.playableGraph.Evaluate(dt);
            director.time = t;
        }
    }
}