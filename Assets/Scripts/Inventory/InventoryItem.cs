//using System;
//using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// The Inventory System
/// </summary>
namespace InventorySystem
{
    public class InventoryItem{

        public InventoryItem(
            string name = _defaultName,
            string description = _defaultDescription,
            int value = _defaultValue,
            int uses = _defaultUses,
            string iconLocation = _defaultIconLocation,
            string pictureLocation = _defaultPictureLocation,
            string spriteLocation = _defaultSpriteLocation,
            bool useable = _defaultUseable,
            bool removable = _defaultRemovable,
            bool destroyable = _defaultDestroyable
            ){
            _name = name;
            _description = description;
            _value = value;
            _uses = uses;
            //_iconLocation = iconLocation;
            //_pictureLocation = pictureLocation;
            //_spriteLocation = spriteLocation;
            _useable = useable;
            _removable = removable;
            _destroyable = destroyable;
            //_Effect = effect;

            Addressables.LoadAssetAsync<Sprite>(iconLocation).Completed += OnGotIcon;
            Addressables.LoadAssetAsync<Sprite>(pictureLocation).Completed += OnGotPic;
            Addressables.LoadAssetAsync<Sprite>(spriteLocation).Completed += OnGotSprite;
        }

        private void OnGotIcon(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<Sprite> icon) { _icon = icon.Result; }
        private void OnGotPic(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<Sprite> pic) { _picture = pic.Result; }
        private void OnGotSprite(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<Sprite> sprite) { _sprite = sprite.Result; }

        //private Sprite SpriteFromFile(string filepath)
        //{
        //    Sprite r;
        //    Texture2D spriteTexture = TextureFromFile(filepath);
        //    r = Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), Vector2.zero);

        //    return r;
        //}
        //private Texture2D TextureFromFile(string filepath)
        //{
        //    Texture2D texture;
        //    byte[] FileData;

        //    if (File.Exists(filepath))
        //    {
        //        FileData = File.ReadAllBytes(filepath);
        //        texture = new Texture2D(6, 9);
        //        if (texture.LoadImage(FileData))
        //            return texture;
        //    }
        //    Debug.LogWarning($"Failed to load texture from file {filepath}. Check that file location is corrct and file is not corrupted.");
        //    return null;
        //}

        #region attributes

        #region defaults
        private const string _defaultName = "doo-dad";
        private const string _defaultDescription = "This sure is neat, but also usless...";

        private const int _defaultValue = 0;
        private const int _defaultUses = -1;

        private const string _defaultIconLocation = "Assets / Scripts / Inventory / Default Images/flower.tif";
        private const string _defaultPictureLocation = "Assets / Scripts / Inventory / Default Images/flower.tif";
        private const string _defaultSpriteLocation = "Assets / Scripts / Inventory / Default Images/flower.tif";

        private const bool _defaultUseable = true;
        private const bool _defaultRemovable = true;
        private const bool _defaultDestroyable = true;

        //private const Func<void, void> _defaultEffect;
        #endregion

        #region fields
        private string _name;
        private string _description;

        private int _value;
        private int _uses;

        //private string _iconLocation;
        //private string _pictureLocation;
        //private string _spriteLocation;

        private Sprite _icon;
        private Sprite _picture;
        private Sprite _sprite;

        private bool _useable;
        private bool _removable;
        private bool _destroyable;

        //private function _Effect;
        #endregion

        #region properties
        public string Name { get => _name; }
        public string Description { get => _description; }

        public int Value { get => _value; }
        public int Uses { get => _uses; }
        
        public Sprite Icon { get => _icon; }
        public Sprite Picture { get => _picture; }
        public Sprite Sprite { get => _sprite; }

        public bool Usable { get => _useable; }
        public bool Removable { get => _removable; }
        public bool Destroyable { get => _destroyable; }
        #endregion

        #endregion
    }
}