using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dnd_FallingAlternative : MonoBehaviour {

	public float minSpeed;
	public float speedReduction;
	public Color normalColor, highlightColor;
	public Transform dragParent;
	public CanvasGroup canvasGroup;

	private bool isDragging = false;
	private Text textObj;
	private Dnd_AltSettings settings;
	private Image outline;

	private RectTransform rt;
	
	void Awake() {
		textObj = transform.GetChild(0).GetComponent<Text>();
		outline = GetComponent<Image>();
		outline.color = normalColor;
		rt = GetComponent<RectTransform>();
	}

	void Update() {
		if(isDragging) {
			Vector2 v = Input.mousePosition;
			v.y -= rt.rect.height;// / 1.25f;
			transform.position = Input.mousePosition;
		} else {
			//textObj.text = settings.text + " : " + settings.speed;
			transform.Translate(Vector2.down * settings.speed * Time.deltaTime, Space.Self);
			if(transform.localPosition.y < settings.bottom) {
				Vector2 v = transform.localPosition;
				v.y = settings.top;
				transform.localPosition = v;
			}
			if(transform.localPosition.y > settings.top) {
				Vector2 v = transform.localPosition;
				v.y = settings.top;
				transform.localPosition = v;
			}
			settings.speed -= Time.deltaTime * speedReduction;
			if(settings.speed < minSpeed)
				settings.speed = minSpeed;
		}
	}

	public void SetValues(Dnd_AltSettings _settings) {
		settings = _settings;
		transform.localPosition = settings.startPos;
		textObj.text = settings.text;
	}

	public void StartDrag() {
		isDragging = true;
		transform.SetParent(dragParent);
		outline.color = normalColor;
		canvasGroup.blocksRaycasts = false;
		//transform.SetAsFirstSibling();
	}

	public void StopDrag() {
		isDragging = false;
		transform.SetParent(settings.fallingParent);
		Vector2 v = settings.startPos;
		v.y = transform.localPosition.y;
		transform.localPosition = v;
		canvasGroup.blocksRaycasts = true;
	}

	public void MouseEnter() {
		if(!isDragging)
			outline.color = highlightColor;
	}

	public void MouseExit() {
		outline.color = normalColor;
	}

	public struct Dnd_AltSettings {
		public float top, bottom, speed;
		public string text;
		public Vector2 startPos;
		public Transform fallingParent;
	}

}
