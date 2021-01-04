using System;
using DarkRift;
using DarkRift.Client.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace E1on {
    public class ServerConnection : MonoBehaviour {

        private UnityClient unityClient;
        
        private void Start() {
            unityClient = GameController.getInstance.unityClient;
        }

        private void Update() {
            if (unityClient.ConnectionState == ConnectionState.Connected) SceneManager.LoadScene("LoginScene");
        }
    }
}