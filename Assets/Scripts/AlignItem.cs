using System.Collections;
using UnityEngine;

public class AlignItem : MonoBehaviour 
{
	private string _strHorizontal = "Horizontal";
	private string _strVertical = "Vertical";

	public void ShouldAlignHorizontally(bool isHorizontal)
	{
		transform.Find (_strVertical).gameObject.SetActive (!isHorizontal);
		transform.Find (_strHorizontal).gameObject.SetActive (isHorizontal);
	}
}
