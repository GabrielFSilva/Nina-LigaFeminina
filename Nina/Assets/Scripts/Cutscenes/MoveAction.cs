using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Cutscenes
{
    public class MoveAction : MonoBehaviour, ICutsceneAction
    {
        public event Action<ICutsceneAction> OnStarted;
        public event Action<ICutsceneAction> OnEnded;

        public List<GameObject> simultaneousActions;

        public enum MovementDimension
        {
            MOVEMENT_2D,
            MOVEMENT_3D
        };
        public enum MarkersType
        {
            TRANSFORM,
            VECTOR
        };

        public MovementDimension movementDimension;
        public MarkersType markersType;


        public Transform target;

        [Header("Markers")]
        [SerializeField]
        public Transform startMarkerTransform;
        public Transform endMarkerTransform;
        public Vector3 startMarkerVector;
        public Vector3 endMarkerVector;

        [Header("Attributes")]
        public float movementDuration = 1f;
        public float startDelay = 0f;
        public float endDelay = 0f;

        private float progress = 0f;

        void ICutsceneAction.Start()
        {
            if (OnStarted != null)
                OnStarted(this);
            StartCoroutine(Move());
            PlaySimultaneousActions();
        }
        public void PlaySimultaneousActions()
        {
            foreach (GameObject obj in simultaneousActions)
            {
                obj.GetComponent<ICutsceneAction>().Start();
            }
        }
        private IEnumerator Move()
        {
            if (startDelay >= 0.01f)
                yield return new WaitForSeconds(startDelay);

            for (progress = 0f; progress < 1f; progress += Time.deltaTime / movementDuration)
            {
                target.position = Vector3.Lerp(startMarkerTransform.position, endMarkerTransform.position, progress);
                yield return null;
            }
            target.position = Vector3.Lerp(startMarkerTransform.position, endMarkerTransform.position, 1f);

            if (endDelay >= 0.01f)
                yield return new WaitForSeconds(endDelay);
            if (OnEnded != null)
                OnEnded(this);
        }
        
    }
    /*
    [CustomEditor(typeof(MoveAction))]
    public class MoveActionInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            MoveAction script = (MoveAction)target;
            if (script.markersType == MoveAction.MarkersType.TRANSFORM)
            {
                script.startMarkerTransform = (Transform)EditorGUILayout.ObjectField("Start:", script.startMarkerTransform, typeof(Transform), true);
            }
            DrawDefaultInspector();
        }
    }*/
}