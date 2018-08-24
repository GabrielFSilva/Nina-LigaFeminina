using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cutscenes
{
    public class CutsceneManager : MonoBehaviour
    {

        public List<GameObject> actionObjects;
        public List<ICutsceneAction> actions = new List<ICutsceneAction>();

        public int activeIndex = 0;
        // Use this for initialization
        void Awake()
        {
            foreach (GameObject obj in actionObjects)
                actions.Add(obj.GetComponent<ICutsceneAction>());
            SetupAction(actions[0]);
        }

        private void ActionEnded(ICutsceneAction obj)
        {
            activeIndex++;
            if (activeIndex < actionObjects.Count)
                SetupAction(actions[activeIndex]);
            else
                Debug.Log("No More Actions");
        }

        private void SetupAction(ICutsceneAction action)
        {
            action.OnEnded += ActionEnded;
            action.Start();
        }
    }
}