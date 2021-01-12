using UnityEngine;
using TMPro;

namespace InventorySystem
{
    public class InventoryUI
    {
        private const string _defaultPopupMessage = "This is the default message.";
        private const int _previewTextLength = 5;
        public static void MakePopUp() { MakePopUp(_defaultPopupMessage); }
        public static void MakePopUp(string message) { MakePopUp(new string[1] { message}); }
        public static void MakePopUp(string[] messages)
        {
            float _UIWidth=8f, _UIHeight=10f;
            Color _UIBGColor = new Color(0, 0, 0, 0.9f);
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
        public static void OpenInventory(Inventory targetInventory)
        {
            Debug.Log("UwU - Someone opened up an inventory!");

            MakePopUp("Sup dude? Hows the testing going?");
            //MakePopUp(new string[2]{ "This is a test","this is another fucking test"});
            //I have no fucking clue
        }
    }
}