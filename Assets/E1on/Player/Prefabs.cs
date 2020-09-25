using System;
using System.Collections.Generic;
using UnityEngine;

namespace E1on.Player {
    public class Prefabs : MonoBehaviour {

        public GameObject player;

        [Header("Avatars")]
        public GameObject neru;
        public GameObject kira;
        public GameObject miku;
        
        [Header("UI")]
        public GameObject playerOverlay;
        
        public Dictionary<ushort, GameObject> list = new Dictionary<ushort, GameObject>();
        
        private void Start() {
            list.Add(AvatarType.Neru, neru);
            list.Add(AvatarType.Kira, kira);
            list.Add(AvatarType.Miku, miku);
        }
    }
}