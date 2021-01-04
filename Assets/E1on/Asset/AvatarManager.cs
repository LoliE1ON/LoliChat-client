using System;
using System.Collections.Generic;
using System.IO;
using E1on.Types;
using UnityEngine;

namespace E1on.Asset {
    public class AvatarManager : MonoBehaviour 
    {
        public List<AvatarBundle> Avatars = new List<AvatarBundle>();

        private void Start() {
            GetAllBundles();
        }

        private void GetAllBundles() 
        {
            var documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var avatarsPath = (documentsFolder + "/FreedomVR/Avatars").Replace("\\", "/");

            var directories = Directory.GetDirectories(avatarsPath);
            foreach (var directory in directories) {
                
                var bundle = Path.Combine(Path.Combine(avatarsPath, directory), "avatar.fvra").Replace("\\", "/");
                if (File.Exists(bundle)) {
                    var assetBundle = AssetBundle.LoadFromFile(bundle);
                    if (assetBundle != null) Avatars.Add(new AvatarBundle {
                        Name = Path.GetFileName(directory),
                        Bundle = assetBundle
                    });
                }
            }
        }
    }
}