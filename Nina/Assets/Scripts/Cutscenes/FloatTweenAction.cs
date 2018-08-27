using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cutscenes
{
    public class FloatTweenAction : MonoBehaviour, ICutsceneAction
    {
        public event Action<ICutsceneAction> OnStarted;
        public event Action<ICutsceneAction> OnEnded;
        public event Action<ICutsceneAction, float> OnUpdate;

        public List<GameObject> simultaneousActions;

        [Header("Attributes")]
        public float startValue = 0f;
        public float endValue = 1f;
        public float movementDuration = 1f;
        public float startDelay = 0f;
        public float endDelay = 0f;
        private float progress = 0f;

        void ICutsceneAction.Start()
        {
            if (OnStarted != null)
                OnStarted(this);
            StartCoroutine(Tween());
            PlaySimultaneousActions();
        }

        public void PlaySimultaneousActions()
        {
            foreach (GameObject obj in simultaneousActions)
            {
                obj.GetComponent<ICutsceneAction>().Start();
            }
        }
        private IEnumerator Tween()
        {
            if (startDelay >= 0.01f)
                yield return new WaitForSeconds(startDelay);

            for (progress = 0f; progress < 1f; progress += Time.deltaTime / movementDuration)
            {
                if (OnUpdate != null)
                    OnUpdate(this, startValue + (progress * (endValue - startValue)));
                yield return null;
            }
            if (OnUpdate != null)
                OnUpdate(this, endValue);

            if (endDelay >= 0.01f)
                yield return new WaitForSeconds(endDelay);
            if (OnEnded != null)
                OnEnded(this);
        }
    }
}