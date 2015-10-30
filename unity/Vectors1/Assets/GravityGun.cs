using UnityEngine;
using System.Collections;

public class GravityGun : MonoBehaviour {
    GameObject pickedUp = null;
    public float holdDistance = 1.0f;
    public float powerfactor = 40.0f; // Higher values causes the targets moving faster to the holding point.
    public float maxVelocity = 30.0f;
    public Texture2D crosshairImage;

    public bool isPhysGun = false;

    void OnGUI()
    {

            float xMin = (Screen.width / 2) - (crosshairImage.width / 2);
            float yMin = (Screen.height / 2) - (crosshairImage.height / 2);
            GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // Check for lect click
        GameManager.PrintMessage("Picked up:" + (pickedUp == null ? "Nothing" : pickedUp.tag));
        if (Input.GetMouseButton(0))
        {
            if (pickedUp == null)
            {
                RaycastHit raycastHit;
                
                if (Physics.Raycast(transform.position, transform.forward, out raycastHit))
                {
                    if (raycastHit.collider.gameObject.tag != "groundPlane")
                    {
                        pickedUp = raycastHit.collider.gameObject;
                        if (isPhysGun)
                        {
                            holdDistance = Vector3.Distance(pickedUp.transform.position, transform.position);
                        }
                    }
                    else
                    {
                        pickedUp = null;
                    }
                }
            }
            else
            {
                Vector3 holdPos = transform.position + (transform.forward * holdDistance);
                //LineDrawer.DrawTarget(holdPos, Color.yellow);
                // Move the object towards the holdPos
                Vector3 toHoldPos = holdPos - pickedUp.transform.position;
                toHoldPos *= powerfactor;
                toHoldPos = Vector3.ClampMagnitude(toHoldPos, maxVelocity);
                // Alternatively...
                //if (toHoldPos.magnitude > maxVelocity)
                //{
                //    toHoldPos.Normalize();
                //    toHoldPos *= maxVelocity;
                //}
                
                GameManager.PrintMessage("ToHoldPos" + toHoldPos.magnitude);
                pickedUp.GetComponent<Rigidbody>().velocity = toHoldPos;
            }
        }
        else
        {
            pickedUp = null;
        }
	}
}
