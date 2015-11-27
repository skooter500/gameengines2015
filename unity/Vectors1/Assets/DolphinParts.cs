
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BGE
{
    public class DolphinParts : MonoBehaviour
    {
        public GameObject head;
        public GameObject body;
        public GameObject tail;
       
        // Animation stuff
        float theta;
        float angularVelocity = 5.00f;

        [Range(0.0f, 0.2f)]
        public float speedMultiplier;

        [Range(0, 180)]
        public float headField;

        [Range(0, 180)]
        public float tailField;

        [Range(0, 180)]
        public float bodyField;

        [Range(0, 200)]
        public float bodyWiggleHeight;

        [Range(0, 180)]
        public float maxTurnAngle;

        [HideInInspector]

        public DolphinParts()
        {            
            theta = 0;
            speedMultiplier = 1.0f;
            headField = 30;
            bodyField = 
            tailField = 20;

            bodyWiggleHeight = 20;
            maxTurnAngle = 50.0f;
        }

        public void Start()
        {
        }


        float oldHeadRot = 0;
        float oldBodyRot = 0; 
        float oldTailRot = 0;


        private float lastBodyWiggle = 0;

        

        // Using Rotate

        public void Update()
        {
            float speed = 20.0f;
            theta += speed * angularVelocity * Time.deltaTime * speedMultiplier;
            if (theta >= Mathf.PI * 2.0f)
            {
                theta -= (Mathf.PI * 2.0f);
            }
            // Animate the head            
            float headRot = Mathf.Sin(theta) * headField;
            head.transform.Rotate(Vector3.right, -(headRot - oldHeadRot));

            oldHeadRot = headRot;

            // Animate the tail
            float tailThetaOffset = 0;
            float tailRot = Mathf.Sin(theta + tailThetaOffset) * tailField;
            tail.transform.Rotate(Vector3.right, tailRot - oldTailRot);
            oldTailRot = tailRot;

            // Wiggle the body
            float wiggleThetaOffset = Mathf.PI / 6.0f;
            float bodyWiggle = Mathf.Sin(theta + wiggleThetaOffset) * bodyWiggleHeight;
            Vector3 bodyPos = body.transform.transform.position;
            bodyPos -= (bodyWiggle - lastBodyWiggle) * transform.up;
            body.transform.position = bodyPos;
            lastBodyWiggle = bodyWiggle;

            float bodyThetaOffset = Mathf.PI / 2.0f;
            float bodyRot = Mathf.Sin(theta + bodyThetaOffset) * bodyField;
            Vector3 rot = new Vector3(bodyRot - oldBodyRot, 0, 0);
            transform.Rotate(rot);
            oldBodyRot = bodyRot;

        }        
    }
}
