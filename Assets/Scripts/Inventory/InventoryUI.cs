using UnityEngine;
using TMPro;

namespace InventorySystem
{
    public class InventoryUI
    {
		private static readonly string MAIN_CAMERA_NAME = "Main Camera";
        private const string _defaultPopupMessage = "This is the default message.";
        private const int _previewTextLength = 5;
		private static readonly float _UIWidth=8f, _UIHeight=10f;
		private static readonly Color _UIBGColor = new Color(0, 0, 0, 0.9f);

        public static void MakePopUp() { MakePopUp(_defaultPopupMessage); }
        public static void MakePopUp(string message) { MakePopUp(new string[1] { message}); }
        public static void MakePopUp(string[] messages)
        {

            GameObject _inventoryUI = new GameObject("_InventoryUI"); //create empty
            RectTransform _tempRectTrasform = _inventoryUI.AddComponent<RectTransform>(); //Size this bitch up
            
			_tempRectTrasform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _UIWidth);
            _tempRectTrasform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _UIHeight);
            _inventoryUI.AddComponent<Canvas>(); //UI stuff
            _inventoryUI.AddComponent<CanvasRenderer>();
            _inventoryUI.AddComponent<UnityEngine.UI.Image>().color = _UIBGColor; //Color this bitch in

            //GameObject _tempObj;
            float _elementStack = 0, _elementHeight= 2;
            for(int i = 0; i!= messages.Length; i++)
            {
                GameObject _tempObj = new GameObject($"Text: {messages[i].Substring(0, _previewTextLength)}...");
                _tempObj.AddComponent<CanvasRenderer>();
                _tempRectTrasform = _tempObj.AddComponent<RectTransform>();
                _tempRectTrasform.SetParent(_inventoryUI.transform);
                _tempRectTrasform.anchorMin = Vector2.up;
                _tempRectTrasform.anchorMax = Vector2.one;
                _tempRectTrasform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _elementHeight);
                _tempRectTrasform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _UIWidth);
                _tempRectTrasform.anchoredPosition = new Vector2(0,0-(_elementStack+(_elementHeight/2)));

                TextMeshProUGUI _tempTMP = _tempObj.AddComponent<TextMeshProUGUI>();
                _tempTMP.text = messages[i];
                _tempTMP.alignment = TextAlignmentOptions.Center;
                _tempTMP.fontSizeMin = 1;
                _tempTMP.enableAutoSizing = true;
            }
               
            

            //no clue
            
        }

        private static void GenerateInventoryPopUp(Inventory inventory)
        {
            string _UITitle = "Inventory";
            float _UIWidth = 8f, _UIHeight = 10f;
            Color _UIBGColor = new Color(0, 0, 0, 0.9f);
            GameObject _inventoryUI = new GameObject("_InventoryUI"); //create empty
            RectTransform _tempRectTrasform = _inventoryUI.AddComponent<RectTransform>(); //Size this bitch up
            _tempRectTrasform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _UIWidth);
            _tempRectTrasform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _UIHeight);
            _inventoryUI.AddComponent<Canvas>().sortingOrder = 1; //UI stuff
            _inventoryUI.AddComponent<CanvasRenderer>();
            _inventoryUI.AddComponent<UnityEngine.UI.Image>().color = _UIBGColor; //Color this bitch in

            //Header
            float _elementStack = 0, _elementHeight = 2;
            GameObject _tempObj = new GameObject($"Text: {_UITitle.Substring(0, _previewTextLength)}...");
            _tempObj.AddComponent<CanvasRenderer>();
            _tempRectTrasform = _tempObj.AddComponent<RectTransform>();
            _tempRectTrasform.SetParent(_inventoryUI.transform);
            _tempRectTrasform.anchorMin = Vector2.up;
            _tempRectTrasform.anchorMax = Vector2.one;
            _tempRectTrasform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _elementHeight);
            _tempRectTrasform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _UIWidth);
            _tempRectTrasform.anchoredPosition = new Vector2(0, 0 - (_elementStack + (_elementHeight / 2)));

            TextMeshProUGUI _tempTMP = _tempObj.AddComponent<TextMeshProUGUI>();
            _tempTMP.text = _UITitle;
            _tempTMP.alignment = TextAlignmentOptions.Center;
            _tempTMP.fontSizeMin = 1;
            _tempTMP.enableAutoSizing = true;

            _elementStack += _elementHeight;

            //Body

            //Scroll Rect
            GameObject _scroller = new GameObject("Scroller");
            _tempRectTrasform = _scroller.AddComponent<RectTransform>();
            _tempRectTrasform.SetParent(_inventoryUI.transform);
            _tempRectTrasform.anchorMin = Vector2.up;
            _tempRectTrasform.anchorMax = Vector2.one;
            _tempRectTrasform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (_UIHeight-_elementStack));
            _tempRectTrasform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _UIWidth);
            _tempRectTrasform.anchoredPosition = new Vector2(0, 0 - (_elementStack + ((_UIHeight - _elementStack) / 2)));

            UnityEngine.UI.ScrollRect _scrollRect = _scroller.AddComponent<UnityEngine.UI.ScrollRect>();
            _scroller.AddComponent<CanvasRenderer>();
            _scroller.AddComponent<UnityEngine.UI.Image>().color = new Color(0,0,0,0.005f);
            _scroller.AddComponent<UnityEngine.UI.Mask>();

            //Scroll Container
            GameObject _scrollContainer = new GameObject("Scroll Container");
            _tempRectTrasform = _scrollContainer.AddComponent<RectTransform>();
            _tempRectTrasform.SetParent(_scroller.transform);
            _tempRectTrasform.anchorMin = Vector2.up;
            _tempRectTrasform.anchorMax = Vector2.one;
            _tempRectTrasform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _UIWidth);
            _tempRectTrasform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (inventory.InventoryItems.Count * _elementHeight));
            _tempRectTrasform.anchoredPosition = Vector2.zero;

            _elementStack = 0;

            //Items
            for(int i = 0; i!=inventory.InventoryItems.Count; i++)
            {
                //Item container
                _tempObj = new GameObject($"Item: {inventory.InventoryItems[i].Name}");
                _tempRectTrasform = _tempObj.AddComponent<RectTransform>();
                _tempRectTrasform.SetParent(_scrollContainer.transform);
                _tempRectTrasform.anchorMin = Vector2.up;
                _tempRectTrasform.anchorMax = Vector2.one;
                _tempRectTrasform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _UIWidth);
                _tempRectTrasform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _elementHeight);
                _tempRectTrasform.anchoredPosition = new Vector2(0, 0 - (_elementStack + (_elementHeight / 2)));

                //item icon
                _tempRectTrasform = new GameObject($"Icon: {inventory.InventoryItems[i].Name}").AddComponent<RectTransform>();
                _tempRectTrasform.SetParent(_tempObj.transform);
                _tempRectTrasform.anchorMin = Vector2.up;
                _tempRectTrasform.anchorMax = Vector2.up;
                _tempRectTrasform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _elementHeight);
                _tempRectTrasform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _elementHeight);
                _tempRectTrasform.anchoredPosition = new Vector2(_elementHeight/2,0-(_elementHeight/2));

                UnityEngine.UI.Image _itemImage = _tempRectTrasform.gameObject.AddComponent<UnityEngine.UI.Image>();
                _itemImage.sprite = inventory.InventoryItems[i].Icon;

                _elementStack += _elementHeight;
            }

            //Link-up
            _scrollRect.content = _scrollContainer.GetComponent<RectTransform>();
            _inventoryUI.AddComponent<UnityEngine.EventSystems.EventSystem>();
            _inventoryUI.AddComponent<UnityEngine.EventSystems.BaseInput>();
            _inventoryUI.AddComponent<UnityEngine.InputSystem.UI.InputSystemUIInputModule>();
			PlaceUnderCamera(_inventoryUI);
        }
		#region utility
		/// <summary>
		/// Places the provided ui under the main camera.<br>
		/// Locates the main camera by name, assuming it is "Main Camera".
		/// <br/>
		/// This could be abstracted to some kind of utilities class.
		/// </summary>
		/// <param name="ui">The Inventory UI to place under the camera.</param>
		private static void PlaceUnderCamera(GameObject ui){
			PlaceUnderCamera(ui, MAIN_CAMERA_NAME);
		}
		
		/// <summary>
		/// Places the provided ui under the main camera.<br/>
		/// Locates the camera game object using the name provided.
		/// <br/>
		/// This could be abstracted to some kind of utilities class
		/// <br/>
		/// <span>
		/// 	uses 
		/// 	<seealso cref="GameObject.Find(string)"/>
		/// </span>
		/// </summary>
		/// <param name="ui">The Inventory UI to place under the camera.</param>
		private static void PlaceUnderCamera(GameObject ui, string CameraName){
			PlaceUnderCamera(ui, GameObject.Find(CameraName));
		}

		/// <summary>
		/// Places the provided ui under the main camera.<br/>
		/// Directly defines the child/parent relationship between the 
		/// provided inventory UI and camera objects
		/// <br/>
		/// This could be abstracted to some kind of utilities class
		/// <br/>
		/// <span>
		/// 	uses 
		/// 	<seealso cref="Transform.SetParent(Transform)"/>
		/// </span>
		/// </summary>
		/// <param name="ui">The Inventory UI to place under the camera.</param>
		private static void PlaceUnderCamera(GameObject ui, GameObject camera){
			ui.transform.SetParent(camera.transform);
		}

        public static void OpenInventory(Inventory targetInventory)
        {
            Debug.Log("UwU - Someone opened up an inventory!");

            GenerateInventoryPopUp(targetInventory);
            //MakePopUp("Sup dude? Hows the testing going?");
            //MakePopUp(new string[2]{ "This is a test","this is another fucking test"});
            //I have no fucking clue
        }
		#endregion utility
    }
}