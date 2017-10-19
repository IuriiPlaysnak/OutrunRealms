using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ZenFulcrum.EmbeddedBrowser {

/** 
 * Draws a crosshair in the middle of the screen which changes to cursors as you mouseover 
 * things in world-space browsers.
 * 
 * Often, this will be created automatically. If you want to alter parameters, add this script 
 * to an object (such as the camera) and edit them there.
 */
public class FPSCursorRenderer : MonoBehaviour {
	private static FPSCursorRenderer _instance;
	public static FPSCursorRenderer Instance {
		get {
			if (!_instance) {
				_instance = FindObjectOfType<FPSCursorRenderer>();
				if (!_instance) {
					var go = new GameObject("Cursor Crosshair");
					_instance = go.AddComponent<FPSCursorRenderer>();
				}
			}
			return _instance;
		}
	}

	[Tooltip("How large should we render the cursor?")]
	public float scale = .5f;

	[Tooltip("How far can we reach to push buttons and such?")]
	public float maxDistance = 7f;

	[Tooltip("What are we using to point at things? Leave as null to use Camera.main")]
	public Transform pointer;

		private Image reticleImage;

	/** 
	 * Toggle this to enable/disable input for all FPSBrowserUI objects.
	 * This is useful, for example, during plot sequences and pause menus.
	 */
	public bool EnableInput { get; set; }

	public static void SetUpBrowserInput(Browser browser, MeshCollider mesh) {
		var crossHair = Instance;

		var pointer = crossHair.pointer;
		if (!pointer) pointer = Camera.main.transform;//nb: don't use crossHair.pointer ?? camera, will incorrectly return null
		var fpsUI = FPSBrowserUI.Create(mesh, pointer, crossHair);
		fpsUI.maxDistance = crossHair.maxDistance;
		browser.UIHandler = fpsUI;
	}

	protected BrowserCursor baseCursor, currentCursor;

	public void Start() {
		EnableInput = true;
		baseCursor = new BrowserCursor();
		baseCursor.SetActiveCursor(BrowserNative.CursorType.Cross);
			GameObject guiReticle = GameObject.Find ("GUIReticle");
			if (guiReticle != null) {
				reticleImage = guiReticle.GetComponent<Image> ();
	}
		}

	public void OnGUI() {
			if (!EnableInput)
				return;

		var cursor = currentCursor ?? baseCursor;
			if (cursor == null)
				return;//hidden cursor
			
		var tex = cursor.Texture;
		
			if (tex == null)
				return;//hidden cursor

		var pos = new Rect(Screen.width / 2f, Screen.height / 2f, tex.width * scale, tex.height * scale);
		pos.x -= cursor.Hotspot.x * scale;
		pos.y -= cursor.Hotspot.y * scale;

            //GUI.DrawTexture(pos, tex);

            //if (BrowserCursor.type_ == ZenFulcrum.EmbeddedBrowser.BrowserNative.CursorType.Hand)
            //{
            //    if (reticleImage.color != Color.red)
            //    {
            //        reticleImage.color = Color.red;
            //    }
            //}
            //else
            //{
            //    if (reticleImage.color != Color.white)
            //    {
            //        reticleImage.color = Color.white;
            //    }
            //}
        }

		public void Disable() {
			if (reticleImage != null) {
				reticleImage.color = Color.white;
			}
			EnableInput = false;
		}

	public void SetCursor(BrowserCursor newCursor, FPSBrowserUI ui) {
		currentCursor = newCursor;

	}
}

}
