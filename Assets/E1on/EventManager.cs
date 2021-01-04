using UnityEngine;

namespace E1on {
    public class EventManager : MonoBehaviour {
        
        public delegate void OnConnectionEvent(string message);
        public static event OnConnectionEvent ConnectionEvent;
        
        public delegate void OnSuccessAuthorizationEvent();
        public static event OnSuccessAuthorizationEvent SuccessAuthorizationEvent;
        
        public delegate void OnFailedAuthorizationEvent();
        public static event OnFailedAuthorizationEvent FailedAuthorizationEvent;
        
        public void UiListEvent(string message) {
            ConnectionEvent(message);
        }
        
        public void LoginSuccessAuthorizationEvent() {
            SuccessAuthorizationEvent();
        }
        
        public void LoginFailedAuthorizationEvent() {
            FailedAuthorizationEvent();
        }
        
    }
}