using System;
using System.Collections.Generic;
using E1on.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace E1on.UI {
    public class Login : MonoBehaviour {

        public InputField loginInput;
        public InputField passwordInput;
        public Dropdown avatarType;
        public Image backgroundImage;
        public Dropdown avatarsList;
        
        public GameObject loginUi;
        public GameObject loadingUi;
        public GameObject buttons;
        public GameObject error;
        
        private bool allowChangeScene = false;
        private float time = 1f;

        private void Start() {
            
            var avatars = new List<Dropdown.OptionData>();
            
            var playerAvatars = GameController.getInstance.avatarManager.Avatars;
            foreach (var playerAvatar in playerAvatars) {
                avatars.Add(new Dropdown.OptionData(playerAvatar.Name));
            }

            avatarsList.options = avatars;
        }

        private void Update() {
            if (allowChangeScene) ChangeScene();
        }
        
        private void Success() {
            allowChangeScene = true;
        }
        
        private void Failed() {

            // TODO: Костыль
            EventManager.SuccessAuthorizationEvent+= null;
            EventManager.FailedAuthorizationEvent+= null;
            
            loadingUi.SetActive(false);
            buttons.SetActive(true);
            loginUi.SetActive(true);
            error.SetActive(true);
        }
        
        public void HandleLogin() {
            if (!this.ValidateLogin()) return;
            loginUi.SetActive(false);
            buttons.SetActive(false);
            loadingUi.SetActive(true);
            
            // TODO: Костыль
            EventManager.SuccessAuthorizationEvent+= Success;
            EventManager.FailedAuthorizationEvent+= Failed;
            
            GameController.getInstance.username = loginInput.text;
            GameController.getInstance.password = passwordInput.text;
            GameController.getInstance.avatar =
                avatarType.value == 0 ? AvatarType.Neru : (avatarType.value == 1 ? AvatarType.Kira : AvatarType.Miku);
            
            GameController.getInstance.networkMessageSender.Authorization(GameController.getInstance.username, GameController.getInstance.password, GameController.getInstance.avatar);
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