using UnityEngine;
using TMPro;

namespace InventorySystem
{
    public class InventoryUI
    {
		public static readonly string _UITitle = "Inventory";
		private static readonly string _MainCameraName = "Main Camera";
        private const string _defaultPopupMessage = "This is the default message.";
        private const int _previewTextLength = 5;
		private static readonly float _UIWidth=8f, _UIHeight=10f;
		private static readonly Color _UIBGColor = new Color(0, 0, 0, 0.9f);

        public static GameObject MakePopUp() => MakePopUp(_defaultPopupMessage);
        public static GameObject MakePopUp(string message) => MakePopUp(new string[1] {message});
        public static GameObject MakePopUp(string[] messages)
        {
            GameObject _inventoryUI = new GameObject("_InventoryUI"); //create empty
            RectTransform _tempRectTrasform = _inventoryUI.AddComponent<RectTransform>(); //Size this bitch up
            
			_tempRectTrasform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _UIWidth);
            _tempRectTrasform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _UIHeight);
			SetAnchoredChild(_tempRectTrasform, GameObject.Find(_MainCameraName), _UIHeight, _UIWidth, Vector2.zero);
            _inventoryUI.AddComponent<Canvas>().sortingOrder = 1; //UI stuff
            _inventoryUI.AddComponent<CanvasRenderer>();
            _inventoryUI.AddComponent<UnityEngine.UI.Image>().color = _UIBGColor; //Color this bitch in

            //GameObject _tempObj;
            float _elementStack = 0, _elementHeight= 2;
            for(int i = 0; i!= messages.Length; i++)
            {
                GameObject _tempObj = new GameObject($"Text: {messages[i].Substring(0, _previewTextLength)}...");
                _tempObj.AddComponent<CanvasRenderer>();
                _tempRectTrasform = _tempObj.AddComponent<RectTransform>();
				SetAnchoredChild(_tempRectTrasform, _inventoryUI, _elementHeight, _UIWidth, new Vector2(0,0-(_elementStack+(_elementHeight/2))));

                TextMeshProUGUI _tempTMP = _tempObj.AddComponent<TextMeshProUGUI>();
                _tempTMP.text = messages[i];
                _tempTMP.alignment = TextAlignmentOptions.Center;
                _tempTMP.fontSizeMin = 1;
                _tempTMP.enableAutoSizing = true;
            }
               
            return _inventoryUI;
        }

        private static void GenerateInventoryPopUp(Inventory inventory)
        {

            GameObject _inventoryUI = MakePopUp(_UITitle);
			GameObject _tempObj = new GameObject("[temp obj]");
			_tempObj.transform.SetParent(_inventoryUI.transform);
			RectTransform _tempRectTrasform = _inventoryUI.GetComponent<RectTransform>();
            float _elementStack = 2, _elementHeight = 2;
            
            //Scroll Rect
            GameObject _scroller = new GameObject("Scroller");
            _tempRectTrasform = _scroller.AddComponent<RectTransform>();
			SetAnchoredChild(_tempRectTrasform, _inventoryUI, (_UIHeight-_elementStack), _UIWidth, new Vector2(0, 0 - (_elementStack + ((_UIHeight - _elementStack) / 2))));

            UnityEngine.UI.ScrollRect _scrollRect = _scroller.AddComponent<UnityEngine.UI.ScrollRect>();
            _scroller.AddComponent<CanvasRenderer>();
            _scroller.AddComponent<UnityEngine.UI.Image>().color = new Color(0,0,0,0.005f);
            _scroller.AddComponent<UnityEngine.UI.Mask>();

            //Scroll Container
            GameObject _scrollContainer = new GameObject("Scroll Container");
            _tempRectTrasform = _scrollContainer.AddComponent<RectTransform>();

            SetAnchoredChild(_tempRectTrasform, _scroller, _UIWidth, (inventory.InventoryItems.Count * _elementHeight), Vector2.zero);
        
			_elementStack = 0;

            //Items
            for(int i = 0; i!=inventory.InventoryItems.Count; i++)
            {
                //Item container
                _tempObj = new GameObject($"Item: {inventory.InventoryItems[i].Name}");
                _tempRectTrasform = _tempObj.AddComponent<RectTransform>();
				SetAnchoredChild(_tempRectTrasform, _scrollContainer, _elementHeight, _UIWidth, new Vector2(0, 0 - (_elementStack + (_elementHeight / 2))));

                //item icon
                _tempRectTrasform = new GameObject($"Icon: {inventory.InventoryItems[i].Name}").AddComponent<RectTransform>();
				SetAnchoredChild(_tempRectTrasform, _tempObj, _elementHeight, _elementHeight, new Vector2(_elementHeight/2,0-(_elementHeight/2)), Vector2.up, Vector2.up);

                UnityEngine.UI.Image _itemImage = _tempRectTrasform.gameObject.AddComponent<UnityEngine.UI.Image>();
                _itemImage.sprite = inventory.InventoryItems[i].Icon;

                _elementStack += _elementHeight;
            }

            //Link-up
            _scrollRect.content = _scrollContainer.GetComponent<RectTransform>();
            _inventoryUI.AddComponent<UnityEngine.EventSystems.EventSystem>();
            _inventoryUI.AddComponent<UnityEngine.EventSystems.BaseInput>();
            _inventoryUI.AddComponent<UnityEngine.InputSystem.UI.InputSystemUIInputModule>();

        }

		#region utility
		private static void SetAnchoredChild(RectTransform _rectTransform, GameObject _parent, float verticalSize, float horizontalSize, Vector2 _anchorPos) => 
		SetAnchoredChild( _rectTransform, _parent, verticalSize, horizontalSize, _anchorPos, Vector2.up, Vector2.one);


		private static void SetAnchoredChild(RectTransform _rectTransform, GameObject _parent, float verticalSize, float horizontalSize, Vector2 _anchorPos, Vector2 min, Vector2 max){
            _rectTransform.SetParent(_parent.transform);
            _rectTransform.anchorMin = min;
            _rectTransform.anchorMax = max;
            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, verticalSize);
            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, horizontalSize);
            _rectTransform.anchoredPosition = _anchorPos;
		}

		/// <summary>
		/// Places the provided ui under the main camera.<br>
		/// Locates the main camera by name, assuming it is "Main Camera".
		/// <br/>
		/// This could be abstracted to some kind of utilities class.
		/// </summary>
		/// <param name="ui">The Inventory UI to place under the camera.</param>
		private static void PlaceUnderCamera(GameObject ui){
			PlaceUnderCamera(ui, _MainCameraName);
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