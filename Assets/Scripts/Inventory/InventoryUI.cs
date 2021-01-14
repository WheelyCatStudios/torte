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
		/// The permitted length for TMP text within object labels.<br/>
		/// Text longer than this value will be cut with '...'
		/// </summary>
        private const int _previewTextLength = 5;
		#endregion properties

		/// <summary>
		/// Generates a pop-up with no provided information.
		/// </summary>
		
		// [Obselete("Pop-ups should be provided with some information")] // I want to mark this as obsolete, but the attribute can't be located on mac. Seems to be a known bug.
		public static GameObject MakePopUp() => MakePopUp(_defaultPopupName);
        public static GameObject MakePopUp(string objName) => MakePopUp(objName, _defaultPopupMessage);
        public static GameObject MakePopUp(string objName, string message) => MakePopUp(objName, new string[1] {message});
        public static GameObject MakePopUp(string objName, string[] messages)
        {
            GameObject _PopUpUI = new GameObject(objName); 											// Create UI object
            RectTransform _tempRectTrasform = _PopUpUI.AddComponent<RectTransform>(); 				// Add and get a new recttransform to size the UI. 
			_tempRectTrasform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _UIWidth);	// Assign width and height
            _tempRectTrasform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _UIHeight);
			
			SetAnchoredChild(_tempRectTrasform, GameObject.Find(_MainCameraName), _UIHeight, _UIWidth, Vector2.zero); // Place the UI under the camera within the UI. NB : This also scales the UI.
            
			_PopUpUI.AddComponent<Canvas>().sortingOrder = 1; 										// Assign sorting order to render this UI above all other objects // TODO this should be a const
            _PopUpUI.AddComponent<CanvasRenderer>();												// Create a component to render our canvas.
            _PopUpUI.AddComponent<UnityEngine.UI.Image>().color = _UIBGColor; 						// Add a the background image, for now it's just a plain color.

            float _elementStack = 0, _elementHeight= 2;												// Variables used to correctly place multiple elements.
            foreach(string message in messages)
            {
                GameObject _tempObj = new GameObject($"Text: {message.Substring(0, _previewTextLength)}" + ((message.Length > _previewTextLength) ? "..." : ""));
                _tempObj.AddComponent<CanvasRenderer>();											// TODO do we need a canvas renderer for every TMP? if so, should this be moved into CreateTMP?
                _tempRectTrasform = _tempObj.AddComponent<RectTransform>();							// Create canvas renderer and rect transform for the label.
				SetAnchoredChild(_tempRectTrasform, _PopUpUI, _elementHeight, _UIWidth, new Vector2(0,0-(_elementStack+(_elementHeight/2)))); // Anchor the label to the UI, according to the element's position.

				createTMP(_tempObj, message);														// Add the text label to the anchored object.
            }
               
            return _PopUpUI;																		// Return the created pop-up UI.
        }

		/// <summary>
		/// Populates a Pop-Up with inventory items.
		/// </summary>
		/// <param name="inventory">The inventory to display.</param>
        private static void GenerateInventoryPopUp(Inventory inventory)
        {
            GameObject _inventoryUI = MakePopUp(_InventoryObjectName, _UITitle);		// Create a pop-up to use for the inventory's UI.
			RectTransform _mainTransform = _inventoryUI.GetComponent<RectTransform>();	// Fetch the main inventory UI's transform for use with placing objects.
			
																						// NB : volatile vars for placing objects.
			RectTransform _tempRectTrasform = _mainTransform;							// Temporary container for the recttransform that we're currently working on. For now, the inventory UI's.
			GameObject _tempObj;														// Temporary object container for use when adding objects. For now, null.
            float _elementStack = 2, _elementHeight = 2;								// Vars that define the placement of objects, according to thier place in the list.
            
            GameObject _scroller = new GameObject("Scroller");							// Create an object to use for a scrollable container
            _tempRectTrasform = _scroller.AddComponent<RectTransform>();				// Create and get a rect transform
																						// Set it as an anchored child of the inventory UI
			SetAnchoredChild(_tempRectTrasform, _inventoryUI, (_UIHeight-_elementStack), _UIWidth, new Vector2(0, 0 - (_elementStack + ((_UIHeight - _elementStack) / 2))), false);

            UnityEngine.UI.ScrollRect _scrollRect = _scroller.AddComponent<UnityEngine.UI.ScrollRect>(); // Add a scroll rect to the object.
            _scroller.AddComponent<CanvasRenderer>();													 // Add a canvas renderer to the scroll pane.
            _scroller.AddComponent<UnityEngine.UI.Image>().color = new Color(0,0,0,0.005f);				 // Set the background color of the scroll pane
            _scroller.AddComponent<UnityEngine.UI.Mask>();												 // Add a mask?

            GameObject _scrollContainer = new GameObject("Scroll Container");			// Container to house the objects in the scroll pane.
            _tempRectTrasform = _scrollContainer.AddComponent<RectTransform>();			
																						// Set as a child of the scroll pane.
            SetAnchoredChild(_tempRectTrasform, _scroller, _UIWidth, (inventory.InventoryItems.Count * _elementHeight), Vector2.zero, false);
        
			_elementStack = 0;															// Reset element stack.

            //Items
            foreach(InventoryItem currentItem in inventory.InventoryItems)				// for every item in the inventory:
            {
                //Item container
                _tempObj = new GameObject($"Item: {currentItem.Name}");					// Create a Game Object to represent it,
                _tempRectTrasform = _tempObj.AddComponent<RectTransform>();				// Add a rect, and add it as a child to the scroll able container.
				SetAnchoredChild(_tempRectTrasform, _scrollContainer, _elementHeight, _UIWidth, new Vector2(0, 0 - (_elementStack + (_elementHeight / 2))), false);

                //item icon
                _tempRectTrasform = new GameObject("Icon").AddComponent<RectTransform>(); // Create an object to display the item's icon, and add it to the above GO.
				SetAnchoredChild(_tempRectTrasform, _tempObj, _elementHeight, _elementHeight, new Vector2(_elementHeight/2,0-(_elementHeight/2)), false, Vector2.up, Vector2.up);
                _tempRectTrasform.gameObject.AddComponent<UnityEngine.UI.Image>().sprite = currentItem.Icon; // Create an image, and assign it the icon of the current item.

				//item text
				GameObject _label = new GameObject("Label");								// create an object to display the item's name, and add it to the above item container.
                _tempRectTrasform = _label.AddComponent<RectTransform>();
				SetAnchoredChild(_tempRectTrasform, _tempObj, _elementHeight, _mainTransform.sizeDelta.x , new Vector2(_elementHeight * 3, 0-(_elementHeight/2)), false, Vector2.up, Vector2.up);
				createTMP(_label, currentItem.Name);										// Add a TMP label with the item's text.

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