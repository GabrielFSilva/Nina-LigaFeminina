using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cutscenes
{
    public class WaitForTouchAction : MonoBehaviour, ICutsceneAction
    {
        public event Action<ICutsceneAction> OnStarted;
        public event Action<ICutsceneAction> OnEnded;

        public CustomCollider2D targetCollider;
        public bool touched;

        public float startDelay = 0f;
        public float endDelay = 0f;

        void ICutsceneAction.Start()
        {
            StartCoroutine(WaitForTouch());
        }

        private void OnColliderTouched(CustomCollider2D collider)
        {
            touched = true;
        }

        private IEnumerator WaitForTouch()
        {
            if (startDelay >= 0.01f)
                yield return new WaitForSeconds(startDelay);

            targetCollider.OnTouched += OnColliderTouched;
            while (!touched)
                yield return null;

            if (endDelay >= 0.01f)
                yield return new WaitForSeconds(endDelay);
            if (OnEnded != null)
                OnEnded(this);
        }
    }
}