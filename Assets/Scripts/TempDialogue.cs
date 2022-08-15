using UnityEngine.UI;
using UnityEngine;

public class TempDialogue : MonoBehaviour{

	private static GameObject instance;		
	public static void ShowMessage(string msg) {
		instance = GameObject.Find("DialogWindow(Clone)");		// find any existing dialog and destroy it.
		if (instance != null) GameObject.Destroy(instance);

		instance = GameObject.Instantiate(Resources.Load("DialogWindow", typeof(GameObject))) as GameObject;
		GameObject canvas = GameObject.Find("Canvas");
		instance.transform.SetParent(canvas.transform);
		instance.transform.Find("Image").Find("Text").GetComponent<Text>().text = msg;
		instance.transform.localPosition = new Vector3(-153, -61, 0);
		instance.transform.localScale = new Vector3(51,51,51);
	}
}
