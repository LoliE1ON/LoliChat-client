using System;
using System.IO;
using UnityEngine;

namespace E1on.Asset {
    public class Avatar : MonoBehaviour {
        private void Start() {
            
            var documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            var savePath = (documentsFolder + "/FreedomVR/Avatars/Neru/avatar.fvra").Replace("/", "\\");
            
            var myLoadedAssetBundle = AssetBundle.LoadFromFile(savePath);
            if (myLoadedAssetBundle == null)
            {
                Debug.Log("Failed to load AssetBundle!");
                return;
            }
            
            var prefab = myLoadedAssetBundle.LoadAsset<GameObject>("avatar");
            Instantiate(prefab);

            myLoadedAssetBundle.Unload(false);
            

        }


    }
}