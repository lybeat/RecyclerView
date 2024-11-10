using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;

namespace NaiQiu.Framework.Resource
{
    public class ResourceManager
    {
        public Dictionary<string, SpriteAtlas> SpriteAtlas { get; private set; } = new();
        public Dictionary<string, AudioClip> AudioClips { get; private set; } = new();
        public Dictionary<string, GameObject> Views { get; private set; } = new();
        public Dictionary<string, TextAsset> Jsons { get; private set; } = new();

        private Action onResourceLoaded;

        private int resCounter = 0;
        private readonly int maxCount = 3;

        public ResourceManager(Action onResourceLoaded)
        {
            this.onResourceLoaded = onResourceLoaded;

            LoadSprites();
            LoadViews();
            LoadJsons();
        }

        private void HandleLoadCompleted()
        {
            resCounter += 1;
            if (resCounter >= maxCount)
            {
                onResourceLoaded?.Invoke();
            }
        }

        private void LoadSprites()
        {
            Addressables.LoadAssetsAsync<SpriteAtlas>(new List<string> { "Atlas" }, t =>
                                                {
                                                    SpriteAtlas[t.name] = t;
                                                }, Addressables.MergeMode.Union).Completed += handle =>
                                                {
                                                    HandleLoadCompleted();
                                                };
        }

        private void LoadViews()
        {
            Addressables.LoadAssetsAsync<GameObject>(new List<string> { "View" }, t =>
                                                {
                                                    Views[t.name] = t;
                                                }, Addressables.MergeMode.Union).Completed += handle =>
                                                {
                                                    HandleLoadCompleted();
                                                };
        }

        private void LoadJsons()
        {
            Addressables.LoadAssetsAsync<TextAsset>(new List<string> { "Json" }, t =>
                                                {
                                                    Jsons[t.name] = t;
                                                }, Addressables.MergeMode.Union).Completed += handle =>
                                                {
                                                    HandleLoadCompleted();
                                                };
        }
    }
}
