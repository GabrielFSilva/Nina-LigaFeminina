using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cutscenes
{
    public class WaitAction : MonoBehaviour, ICutsceneAction
    {
        public event Action<ICutsceneAction> OnStarted;
        public event Action<ICutsceneAction> OnEnded;

        public List<GameObject> simultaneousActions;

        public float waitTime = 1.0f;

        void ICutsceneAction.Start()
        {
            if (OnStarted != null)
                OnStarted(this);
            StartCoroutine(Wait());
            PlaySimultaneousActions();
        }
        public void PlaySimultaneousActions()
        {
            foreach (GameObject obj in simultaneousActions)
            {
                obj.GetComponent<ICutsceneAction>().Start();
            }
        }
        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(waitTime);
            if (OnEnded != null)
                OnEnded(this);
        }
        
    }
}