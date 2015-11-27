
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BGE
{
    public class NewFishParts : MonoBehaviour
    {
        public GameObject head;
        public GameObject tail;

        List<GameObject> segments;

        float segmentExtents = 3;
        public float gap;

        // Animation stuff
        float theta;
        float angularVelocity = 5.00f;


        [Range(0.0f, 2.0f)]
        public float speedMultiplier;

        public float headField;
        public float tailField;

        public NewFishParts()
        {
            segments = new List<GameObject>();

            theta = 0;
            speedMultiplier = 1.0f;
            headField = 5;
            tailField = 50;
        }

        public GameObject InstiantiateDefaultShape()
        {

            GameObject segment = null;
            segment = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Vector3 scale = new Vector3(1, segmentExtents, segmentExtents);
            segment.transform.localScale = scale;
            return segment;
        }

        public void OnDrawGizmos()
        {
            float radius = (1.5f * segmentExtents) + gap;
            Gizmos.color = Color.yellow;
            //Gizmos.DrawWireSphere(transform.position, radius);
        }


        public void Start()
        {            
            
        }
        

        float oldHeadRot = 0;
        float oldTailRot = 0;

        private float fleeColourWait;
        private bool fleeColourStarted;

        public void Update()
        {
            // Animate the head            
            float headRot = Mathf.Sin(theta) * headField;
            head.transform.Rotate(Vector3.right, headRot - oldHeadRot);

            oldHeadRot = headRot;

            // Animate the tail
            float tailRot = Mathf.Sin(theta) * tailField;
            tail.transform.Rotate(Vector3.up, tailRot - oldTailRot);
            oldTailRot = tailRot;
            
            theta += angularVelocity * Time.deltaTime * speedMultiplier;
            if (theta >= Mathf.PI * 2.0f)
            {
                theta -= (Mathf.PI * 2.0f);
            }
        }
    }
}
