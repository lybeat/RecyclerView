using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace NaiQiu.Framework.Resource
{
    public class SpriteManager : IResource
    {
        private Dictionary<string, SpriteAtlas> spriteAtlasDict;

        public SpriteManager(Dictionary<string, SpriteAtlas> spriteAtlasDict)
        {
            this.spriteAtlasDict = spriteAtlasDict;
        }

        /// <summary>
        /// 获取图集
        /// </summary>
        /// <param name="atlasName">图集名</param>
        /// <returns></returns>
        public SpriteAtlas GetSpriteAtlas(string atlasName)
        {
            return spriteAtlasDict[atlasName];
        }

        /// <summary>
        /// 获取图片，通过遍历所有图集来查找
        /// </summary>
        /// <param name="spriteName">图片名</param>
        /// <returns></returns>
        public Sprite GetSprite(string spriteName)
        {
            Sprite sprite = null;
            foreach (var atlas in spriteAtlasDict.Values)
            {
                sprite = atlas.GetSprite(spriteName);
                if (sprite != null) break;
            }
            return sprite;
        }

        /// <summary>
        /// 获取指定图集中的图片
        /// </summary>
        /// <param name="spriteName">图片名</param>
        /// <param name="atlasName">图集名</param>
        /// <returns></returns>
        public Sprite GetSprite(string spriteName, string atlasName)
        {
            SpriteAtlas atlas = spriteAtlasDict[atlasName];
            if (atlas == null) return null;

            return atlas.GetSprite(spriteName);
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }
    }
}
