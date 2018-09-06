using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Cutscenes
{
    public class EnableObjectAction : MonoBehaviour, ICutsceneAction
    {
        public event Action<ICutsceneAction> OnStarted;
        public event Action<ICutsceneAction> OnEnded;

        public GameObject target;
        public bool enable;

        public float startDelay = 0f;
        public float endDelay = 0f;

        public void PlaySimultaneousActions()
        {
            throw new NotImplementedException();
        }

        void ICutsceneAction.Start()
        {
            if (OnStarted != null)
                OnStarted(this);
            StartCoroutine(Enable());
        }

        private IEnumerator Enable()
        {
            if (startDelay >= 0.01f)
                yield return new WaitForSeconds(startDelay);
            target.SetActive(enable);
            if (endDelay >= 0.01f)
                yield return new WaitForSeconds(endDelay);
            if (OnEnded != null)
                OnEnded(this);
        }

    }
}