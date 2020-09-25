using System;
using UnityEngine;

namespace E1on.UI.Tools {
    public class LookAtCamera : MonoBehaviour {

        private Transform elementTransform;
        private Transform cameraTransform;
        private void Start() {
            elementTransform = GetComponent<Transform>();
            cameraTransform = Camera.main.transform;
        }

        private void Update() {
            elementTransform.LookAt(cameraTransform);
        }
    }
}