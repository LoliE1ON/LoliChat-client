using System;
using E1on.Network;
using E1on.Player;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace E1on.UI {
    public class Login : MonoBehaviour {

        public InputField loginInput;
        public InputField passwordInput;
        public Dropdown avatarType;
        public Image backgroundImage;
        
        public GameObject loginUi;
        public GameObject loadingUi;
        public GameObject buttons;
        
        private bool allowChangeScene = false;
        private float time = 1f;

        private void Update() {
            if (allowChangeScene) ChangeScene();
        }
        
        public void HandleLogin() {
            if (!this.ValidateLogin()) return;
            GameController.getInstance.username = loginInput.text;
            GameController.getInstance.password = passwordInput.text;
            GameController.getInstance.avatar =
                avatarType.value == 0 ? AvatarType.Neru : (avatarType.value == 1 ? AvatarType.Kira : AvatarType.Miku);
            
            loginUi.SetActive(false);
            buttons.SetActive(false);
            loadingUi.SetActive(true);
            allowChangeScene = true;
        }

        private void ChangeScene() {
            if (time < 0f) SceneManager.LoadScene("WorldScene");
            else {
                var color = backgroundImage.color;
                backgroundImage.color = new Color(color.r, color.g, color.b, time);
                time -= Time.deltaTime;
            }
        }

        private bool ValidateLogin() {
            return Convert.ToBoolean(loginInput.text.Length);
        }
    }
}