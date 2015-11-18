using UnityEngine;
using System.Collections;

public class PhysicsFactory : MonoBehaviour {

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 20), "Make a wall"))
        {
            CreateWall(10, 10);
        }

        if (GUI.Button(new Rect(10, 50, 150, 20), "Make a car"))
        {
            RaycastHit raycastHit;
            GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out raycastHit))
            {
                if (raycastHit.collider.gameObject.tag == "groundPlane")
                {
                    Vector3 pos = raycastHit.point;
                    pos.y = 10;
                    CreateCar(pos.x, pos.y, pos.z);
                }
            }
        }
        if (GUI.Button(new Rect(10, 90, 150, 20), "Make a gear"))
        {
            RaycastHit raycastHit;
            GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out raycastHit))
            {
                if (raycastHit.collider.gameObject.tag == "groundPlane")
                {
                    Vector3 pos = raycastHit.point;
                    pos.y = 20;
                    CreateGear(pos.x, pos.y, pos.z, 10, 12);
                }
            }
        }
    }

    GameObject CreateCylinder(float x, float y, float z, float diameter, float width, Quaternion q)
    {
        GameObject wheel = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        wheel.transform.localScale = new Vector3(diameter, width, diameter);
        wheel.transform.position = new Vector3(x, y, z);
        wheel.transform.rotation = q;
        wheel.GetComponent<Renderer>().material.color = Utilities.RandomColor();
        Rigidbody rigidBody = wheel.AddComponent<Rigidbody>();
        return wheel;
    }

    GameObject CreateCar(float x, float y, float z)
    {
        float width = 15;
        float height = 2;
        float length = 5;
        float wheelWidth = 1;
        float wheelDiameter = 4;
        float wheelOffset = 2.0f;

        Vector3 position = new Vector3(x, y, z);

        GameObject chassis = CreateBrick(x, y, z, width, height, length);
        Quaternion q = Quaternion.AngleAxis(90.0f, Vector3.right);

        Vector3[] wheelPositions = new Vector3[4];
        Vector3 offset = new Vector3(-(width / 2 - wheelDiameter), 0, -(length / 2 + wheelOffset));
        wheelPositions[0] = position + offset;
        offset = new Vector3(+(width / 2 - wheelDiameter), 0, -(length / 2 + wheelOffset));
        wheelPositions[1] = position + offset;
        offset = new Vector3(-(width / 2 - wheelDiameter), 0, +(length / 2 + wheelOffset));
        wheelPositions[2] = position + offset;
        offset = new Vector3(+(width / 2 - wheelDiameter), 0, +(length / 2 + wheelOffset));
        wheelPositions[3] = position + offset;

        foreach (Vector3 wheelPosition in wheelPositions)
        {
            GameObject wheel = CreateCylinder(
                wheelPosition.x
                ,wheelPosition.y
                ,wheelPosition.z
                ,wheelDiameter
                ,wheelWidth
                ,q
            );
            HingeJoint hinge = wheel.AddComponent<HingeJoint>();
            hinge.connectedBody = chassis.GetComponent<Rigidbody>();
            hinge.axis = Vector3.up;
            hinge.anchor = Vector3.up;
            hinge.autoConfigureConnectedAnchor = true;
        }
        
        
        return chassis;
       
    }   


    // Use this for initialization
    void Start () {       
    }

    GameObject CreateGear(float x, float y, float z, float diameter, int numCogs)
    {
        Quaternion q = Quaternion.AngleAxis(90, Vector3.right);
        GameObject cyl = CreateCylinder(x, y, z, diameter, 1.0f, q);

        float radius = 1 + (diameter * 0.5f);
        float thetaInc = (Mathf.PI * 2.0f) / numCogs;
        for (int i = 0; i < numCogs; i++)
        {
            
            float theta = thetaInc * i;
            Vector3 cogPos = new Vector3();
            cogPos.x = x + (Mathf.Sin(theta) * radius);
            cogPos.y = y + (Mathf.Cos(theta) * radius);  
            cogPos.z = z;

            // Make the cog rotation
            Quaternion cogQ = Quaternion.AngleAxis(- theta * Mathf.Rad2Deg, Vector3.forward);
            
            GameObject cog = CreateBrick(cogPos.x, cogPos.y, cogPos.z);
            cog.transform.rotation = cogQ;
            FixedJoint joint = cog.AddComponent<FixedJoint>();
            joint.connectedBody = cyl.GetComponent<Rigidbody>();
            joint.autoConfigureConnectedAnchor = true;
        }
        return cyl;
    }

    GameObject CreateBrick(float x, float y, float z, float xScale = 1.0f, float yScale = 1.0f, float zScale = 1.0f)
    {
        GameObject brick = GameObject.CreatePrimitive(PrimitiveType.Cube);
        brick.tag = "brick";
        brick.transform.localScale = new Vector3(xScale, yScale, zScale);
        brick.transform.position = new Vector3(x, y, z);
        brick.GetComponent<Renderer>().material.color = Utilities.RandomColor();
        Rigidbody rigidBody = brick.AddComponent<Rigidbody>();
        rigidBody.mass = 1.0f;
        return brick;
    }

    void CreateWall(int width, int height)
    {
        
        for (int y = height - 1; y >= 0; y--)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject brick = CreateBrick(x - (width / 2), 0.5f + y * 1.1f, 0);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
