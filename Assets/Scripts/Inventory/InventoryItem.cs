using System;
using System.IO;
using UnityEngine;
/// <summary>
/// The Inventory System
/// </summary>
namespace InventorySystem
{
    public class InventoryItem{

        public InventoryItem(string name = _defaultItemName, string description = _defaultItemDescription, int value = _defaultItemValue, int uses = _defaultItemUses, bool useable = _defaultItemUseable, bool removable = _defaultItemRemovable, bool destroyable = _defaultItemDestroyable){ //, function effect = _defaultItemEffect){
            _itemName = name;
            _itemDescription = description;
            _itemValue = value;
            _itemUses = uses;
            //_itemIcon = icon;
            //_itemPicture = picture;
            _itemUseable = useable;
            _itemRemovable = removable;
            _itemDestroyable = destroyable;
            //_itemEffect = effect;
        }

        private Sprite SpriteFromFile(string filepath)
        {
            Sprite r;
            Texture2D spriteTexture = TextureFromFile(filepath);
            r = Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), Vector2.zero);

            return r;
        }

        private Texture2D TextureFromFile(string filepath)
        {
            Texture2D texture;
            byte[] FileData;

            if (File.Exists(filepath))
            {
                FileData = File.ReadAllBytes(filepath);
                texture = new Texture2D(6, 9);
                if (texture.LoadImage(FileData))
                    return texture;
            }
            Debug.LogWarning($"Failed to load texture from file {filepath}. Check that file location is corrct and file is not corrupted.");
            return null;
        }

        #region attributes

        #region defaults
        private const string _defaultItemName = "doo-dad";
        private const string _defaultItemDescription = "This sure is neat, but also usless...";

        private const int _defaultItemValue = 0;
        private const int _defaultItemUses = -1;

        //private const Sprite _defaultItemIcon;
        //private const Sprite _defaultItemPicture;

        private const bool _defaultItemUseable = true;
        private const bool _defaultItemRemovable = true;
        private const bool _defaultItemDestroyable = true;

        //private const Func<void, void> _defaultItemEffect;
        #endregion

        #region fields
        private string _itemName;
        private string _itemDescription;

        private int _itemValue;
        private int _itemUses;

        private Sprite _itemIcon;
        private Sprite _itemPicture;

        private bool _itemUseable;
        private bool _itemRemovable;
        private bool _itemDestroyable;

        //private function _itemEffect;
        #endregion

        #region properties
        public string ItemName { get => _itemName; }
        public string ItemDescription { get => _itemDescription; }

        public int ItemValue { get => _itemValue; }
        
        public bool ItemUsable { get => _itemUseable; }
        public bool ItemRemovable { get => _itemRemovable; }
        public bool ItemDestroyable { get => _itemDestroyable; }
        #endregion

        #endregion
    }
}