using System;
using UnityEngine;

namespace E1on.UI.Tools {
    public class RotationElement : MonoBehaviour {

        private RectTransform transform;
        private void Start() {
            transform = GetComponent<RectTransform>();
        }

        private void Update() {
            transform.Rotate(Vector3.back);
        }
    }
}