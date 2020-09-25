using UnityEngine;

namespace E1on {
    public class EventManager : MonoBehaviour {
        
        public delegate void OnConnectionEvent(string message);
        public static event OnConnectionEvent ConnectionEvent;
        
        public void UiListEvent(string message) {
            ConnectionEvent(message);
        }
        
    }
}