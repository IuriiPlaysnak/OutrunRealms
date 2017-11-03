using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashboardCard : MonoBehaviour {

    public enum CardType { News, Video, Video360, Images, ThreeD, Custom};
    public CardType type;
    public RectTransform canvasDimensions;
    float screenspaceWidth;
    public float GetScreenspaceWidth() { return screenspaceWidth; }
    public void SetScreenspaceWidth(float newWidth) { screenspaceWidth = newWidth; }

    public GameObject toolbar;

	// Use this for initialization
	void Start () {
	}
    private void Awake()
    {
        screenspaceWidth = transform.localScale.x * canvasDimensions.rect.width * canvasDimensions.localScale.x;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
