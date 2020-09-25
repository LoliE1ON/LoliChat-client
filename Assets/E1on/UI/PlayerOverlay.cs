using System;
using UnityEngine;
using UnityEngine.UI;

namespace E1on.UI {
    public class PlayerOverlay : MonoBehaviour {
        
        public Text usernameText;

        private void Start() {
            usernameText.text = GameController.getInstance.username;
        }
    }
}