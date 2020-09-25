using System;
using UnityEngine;
using UnityEngine.UI;

namespace E1on.UI {
    public class SwitchLoginForms : MonoBehaviour {

        public GameObject loginForm, registrationForm;
        public Button loginButton, registrationButton;

        private Color activeColor = new Color(0.2f,0.2f,0.2f, 1f);
        private Color innactiveColor =  Color.white;

        private void Start() {
            
            var loginColor = loginForm.gameObject.activeSelf ? activeColor : innactiveColor;
            var registrationColor = registrationForm.gameObject.activeSelf ? activeColor : innactiveColor;
            
            SetButtonColor(loginColor, registrationColor);
        }

        public void ShowLoginForm() {
            SetDisplayForms(true, false);
            SetButtonColor(activeColor, innactiveColor);
        }
        
        public void ShowRegistrationForm() {
            SetDisplayForms(false, true);
            SetButtonColor(innactiveColor, activeColor);
        }

        private void SetDisplayForms(bool displayLogin, bool displayRegister) {
            registrationForm.SetActive(displayRegister);
            loginForm.SetActive(displayLogin);
        }
        
        private void SetButtonColor(Color loginColor, Color registerColor) {
            
            loginButton.image.color = loginColor;
            registrationButton.image.color = registerColor;
            
            loginButton.GetComponentInChildren<Text>().color = loginColor;
            registrationButton.GetComponentInChildren<Text>().color = registerColor;
        }
        
    }
}