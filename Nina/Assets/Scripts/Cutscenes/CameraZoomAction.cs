using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cutscenes
{
    public class CameraZoomAction : FloatTweenAction
    {
        public Camera target;

        private void Awake()
        {
            OnUpdate += TweenUpdated;
        }

        private void TweenUpdated(ICutsceneAction tweenAction, float size)
        {
            target.orthographicSize = size;
        }
    }
}