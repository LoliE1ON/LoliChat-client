using System;
using UnityEngine;

namespace E1on {
    public class InstanceConnector : MonoBehaviour {
        private void Start() {
            GameController.getInstance.networkMessageSender.ConnectionToInstance(GameController.getInstance.session);
        }
    }
}