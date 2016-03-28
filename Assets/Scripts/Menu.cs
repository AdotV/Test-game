using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public void openPage()
	{
		GetComponent<RectTransform> ().localPosition = new Vector3 (0, 0, 0);
	}
	public void closePage()
	{
		GetComponent<RectTransform> ().localPosition = new Vector3 (4768, 4768, 0);
	}

}
