using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarWithStaticButtons : MonoBehaviour {

	[SerializeField]
	private RectTransform _buttonPlaceholder;

	[SerializeField]
	private List<ToolbarButton> _buttons;

	void Awake() {

		InteractiveItem interaction = gameObject.GetComponent<InteractiveItem> ();

		if (interaction != null) {
			interaction.OnOver += OnOver;
			interaction.OnOut += OnOut;
		}
	}

	void OnOver() {

		foreach (var button in _buttons) {
			button.ChangeParent (_buttonPlaceholder);
		}
	}

	void OnOut() {

		foreach (var button in _buttons) {
			button.ReleaseFromParent ();
		}
	}
}