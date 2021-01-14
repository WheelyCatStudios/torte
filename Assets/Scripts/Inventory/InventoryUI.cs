using UnityEngine;
using TMPro;

namespace InventorySystem
{
	/// <summary>
	/// Static utility class that creates a UI which displays and interacts
	/// with a provided Inventory.
	/// </summary>
    public class InventoryUI
    {
		#region properies
		/// <summary>
		/// Controls the general scale of the UI.
		/// <br/>
		/// 1,1,1 = Render at the native size specified by <a cref="_UIWidth"/> and <a cref="_UIHeight"/><br/>
		/// 2,2,1 = Render at two times the the native size specified by <a cref="_UIWidth"/> and <a cref="_UIHeight"/>
		/// </summary>
		public static readonly Vector3 _UIScale = new Vector3(1,1,1);

		/// <summary>
		/// Specifies the height / width of the Inventory UI.
		/// <br/>
		/// <see cref="RectTransform#SetSizeWithCurrentAnchors"/>
		/// </summary>
		private static readonly float _UIWidth=80f, _UIHeight=70f;
		
		/// <summary>
		/// Background color of the inventory UI's background.
		/// </summary>
		private static readonly Color _UIBGColor = new Color(0, 0, 0, 0.9f);

		/// <summary>
		/// The title displayed within the inventory UI.
		/// TODO is not localized
		/// </summary>
		public static readonly string _UITitle = "Inventory";

		/// <summary>
		/// The name given to the game object holding the UI.
		/// </summary>
		public static readonly string _InventoryObjectName = "_inventoryUI";

		/// <summary>
		/// The name of the camera to which the created UI will be placed under.
		/// </summary>
		private static readonly string _MainCameraName = "Main Camera";

		/// <summary>
		/// Fallback message for pop-ups with no provided message
		/// </summary>
        private const string _defaultPopupMessage = "No message...";

		/// <summary>
		/// The name given to pop up objects.
		/// <br/>
		/// Fallback name for pop-ups with no provided object name.
		/// </summary>
		private const string _defaultPopupName = "NewPopUpObj";

		/// <summary>
		/// The permitted length for TMP text within popups.<br/>
		/// Text longer than this value will be cut with '...'
		/// </summary>
        private const int _previewTextLength = 5;
		#endregion properties

		public static GameObject MakePopUp() => MakePopUp(_defaultPopupName);
        public static GameObject MakePopUp(string objName) => MakePopUp(objName, _defaultPopupMessage);
        public static GameObject MakePopUp(string objName, string message) => MakePopUp(objName, new string[1] {message});
        public static GameObject MakePopUp(string objName, string[] messages)
        {
            GameObject _PopUpUI = new GameObject(objName); 											// Create UI object
            RectTransform _tempRectTrasform = _inventoryUI.AddComponent<RectTransform>(); //Size this bitch up
			_tempRectTrasform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _UIWidth);
            _tempRectTrasform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _UIHeight);
			
			SetAnchoredChild(_tempRectTrasform, GameObject.Find(_MainCameraName), _UIHeight, _UIWidth, Vector2.zero);
            _inventoryUI.AddComponent<Canvas>().sortingOrder = 1; //UI stuff
			_PopUpUI.AddComponent<Canvas>().sortingOrder = 1; 										// Assign sorting order to render this UI above all other objects // TODO this should be a const
            _PopUpUI.AddComponent<CanvasRenderer>();												// Create a component to render our canvas.
            _PopUpUI.AddComponent<UnityEngine.UI.Image>().color = _UIBGColor; 						// Add a the background image, for now it's just a plain color.

            //GameObject _tempObj;
            foreach(string message in messages)
            {
                GameObject _tempObj = new GameObject($"Text: {message.Substring(0, _previewTextLength)}" + ((message.Length > _previewTextLength) ? "..." : ""));
                _tempObj.AddComponent<CanvasRenderer>();
                _tempRectTrasform = _tempObj.AddComponent<RectTransform>();
				SetAnchoredChild(_tempRectTrasform, _PopUpUI, _elementHeight, _UIWidth, new Vector2(0,0-(_elementStack+(_elementHeight/2))));

				createTMP(_tempObj, message);
            }
               
            return _PopUpUI;
        }

        private static void GenerateInventoryPopUp(Inventory inventory)
        {

            GameObject _inventoryUI = MakePopUp(_InventoryObjectName, _UITitle);
			GameObject _tempObj;
			RectTransform _mainTransform = _inventoryUI.GetComponent<RectTransform>();
			RectTransform _tempRectTrasform = _mainTransform;
			
            float _elementStack = 2, _elementHeight = 2;
            
            //Scroll Rect
            GameObject _scroller = new GameObject("Scroller");
            _tempRectTrasform = _scroller.AddComponent<RectTransform>();
			SetAnchoredChild(_tempRectTrasform, _inventoryUI, (_UIHeight-_elementStack), _UIWidth, new Vector2(0, 0 - (_elementStack + ((_UIHeight - _elementStack) / 2))), false);

            UnityEngine.UI.ScrollRect _scrollRect = _scroller.AddComponent<UnityEngine.UI.ScrollRect>();
            _scroller.AddComponent<CanvasRenderer>();
            _scroller.AddComponent<UnityEngine.UI.Image>().color = new Color(0,0,0,0.005f);
            _scroller.AddComponent<UnityEngine.UI.Mask>();

            //Scroll Container
            GameObject _scrollContainer = new GameObject("Scroll Container");
            _tempRectTrasform = _scrollContainer.AddComponent<RectTransform>();

            SetAnchoredChild(_tempRectTrasform, _scroller, _UIWidth, (inventory.InventoryItems.Count * _elementHeight), Vector2.zero, false);
        
			_elementStack = 0;

            //Items
            foreach(InventoryItem currentItem in inventory.InventoryItems)
            {
                //Item container
                _tempObj = new GameObject($"Item: {currentItem.Name}");
                _tempRectTrasform = _tempObj.AddComponent<RectTransform>();
				SetAnchoredChild(_tempRectTrasform, _scrollContainer, _elementHeight, _UIWidth, new Vector2(0, 0 - (_elementStack + (_elementHeight / 2))), false);

                //item icon
                _tempRectTrasform = new GameObject("Icon").AddComponent<RectTransform>();
				SetAnchoredChild(_tempRectTrasform, _tempObj, _elementHeight, _elementHeight, new Vector2(_elementHeight/2,0-(_elementHeight/2)), false, Vector2.up, Vector2.up);
                _tempRectTrasform.gameObject.AddComponent<UnityEngine.UI.Image>().sprite = currentItem.Icon; // Create an image, and assign it the icon of the current item.

				//item text
				GameObject _label = new GameObject("Label");
                _tempRectTrasform = _label.AddComponent<RectTransform>();
				SetAnchoredChild(_tempRectTrasform, _tempObj, _elementHeight, _mainTransform.sizeDelta.x , new Vector2(_elementHeight * 3, 0-(_elementHeight/2)), false, Vector2.up, Vector2.up);
				createTMP(_label, currentItem.Name);

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
		SetAnchoredChild( _rectTransform, _parent, verticalSize, horizontalSize, _anchorPos, true);

		private static void SetAnchoredChild(RectTransform _rectTransform, GameObject _parent, float verticalSize, float horizontalSize, Vector2 _anchorPos, bool doScale) => 
		SetAnchoredChild( _rectTransform, _parent, verticalSize, horizontalSize, _anchorPos, doScale, Vector2.up, Vector2.one);

		private static void SetAnchoredChild(RectTransform _rectTransform, GameObject _parent, float verticalSize, float horizontalSize, Vector2 _anchorPos, Vector2 min, Vector2 max) =>
			SetAnchoredChild( _rectTransform, _parent, verticalSize, horizontalSize, _anchorPos, true,  min, max);

		private static void SetAnchoredChild(RectTransform _rectTransform, GameObject _parent, float verticalSize, float horizontalSize, Vector2 _anchorPos, bool doScale, Vector2 min, Vector2 max){
            _rectTransform.SetParent(_parent.transform);
            _rectTransform.anchorMin = min;
            _rectTransform.anchorMax = max;
            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, verticalSize);
            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, horizontalSize);
            _rectTransform.anchoredPosition = _anchorPos;
			_rectTransform.localScale = (doScale) ? _UIScale : new Vector3(1,1,1);
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

		private static TextMeshProUGUI createTMP(GameObject go, string s){
                TextMeshProUGUI _tempTMP = go.AddComponent<TextMeshProUGUI>();
                _tempTMP.text = s;
                _tempTMP.alignment = TextAlignmentOptions.Center;
                _tempTMP.fontSizeMin = 1;
                _tempTMP.enableAutoSizing = true;
				return _tempTMP;
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