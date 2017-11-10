using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashboard : MonoBehaviour {

	private const float DISTANCE_BETWEEN_CARDS = 1f;

    public DashboardCard[] cards;

	// Use this for initialization

	void Start () {
        float angle = 0;
        float previousWidth = 0;
        float DashboardRadius = 3.5f;
        Vector3 previousMergePoint = Vector3.zero;
        //Place cards on a circle with a distance from one another based on theire length
        //the angle (a) between too cards depends on the length (x1, x2) of the cards
        // a = cot(x1/2r)+cot(x2/2r)
        foreach (DashboardCard card in cards)
        {
            float thisAngle = Mathf.Atan2(card.GetScreenspaceWidth(), 2 * DashboardRadius) * Mathf.Rad2Deg;
            if (previousWidth != 0)
            {
                angle += thisAngle + Mathf.Atan2(previousWidth, 2 * DashboardRadius) * Mathf.Rad2Deg;
            }
            
			Vector3 direction = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
            card.transform.localPosition = Vector3.Normalize(direction) * DashboardRadius;

            Vector3 thisMerge = card.transform.localPosition + Vector3.Normalize(Quaternion.AngleAxis(-90, Vector3.up) * card.transform.localPosition) * card.GetScreenspaceWidth() / 2;
            float distOfMergePoints = (previousMergePoint - thisMerge).magnitude;
            if (distOfMergePoints > 0.075f && distOfMergePoints != 0 && previousWidth != 0)
            {
                float tooLong = Mathf.Sin(thisAngle * Mathf.Deg2Rad) * distOfMergePoints;
                float factor = 1 - (2 * tooLong / card.GetScreenspaceWidth());
           
                card.SetScreenspaceWidth(card.GetScreenspaceWidth() * (2 - factor));
                angle -= thisAngle;
                thisAngle = Mathf.Atan2(card.GetScreenspaceWidth(), 2 * DashboardRadius) * Mathf.Rad2Deg;
                angle += thisAngle;
                direction = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
                card.transform.localPosition = Vector3.Normalize(direction) * DashboardRadius * factor;
            }

			card.transform.localRotation = Quaternion.LookRotation (card.transform.localPosition - Vector3.zero);

			previousWidth = card.GetScreenspaceWidth() + DISTANCE_BETWEEN_CARDS;
            previousMergePoint = card.transform.localPosition + Vector3.Normalize(Quaternion.AngleAxis(90, Vector3.up) * card.transform.localPosition) * card.GetScreenspaceWidth() / 2;
        }
        transform.localRotation = Quaternion.AngleAxis(-angle / 2, Vector3.up);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
