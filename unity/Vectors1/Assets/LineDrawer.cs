using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

// Must be attached to the camera so that OnPostRender gets called
public class LineDrawer : MonoBehaviour
{

    Camera[] cameras;
    struct Line
    {
        public Vector3 start;
        public Vector3 end;
        public Color color;
        public bool isDebug;
        public Line(Vector3 start, Vector3 end, Color color, bool isDebug)
        {
            this.start = start;
            this.end = end;
            this.color = color;
            this.isDebug = isDebug;
        }
    }

    static List<Line> lines = new List<Line>();

    Material lineMaterial;

    // Use this for initialization
    void Start()
    {
        cameras = new Camera[1];
        cameras = GameObject.FindObjectsOfType<Camera>();
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
        

    public static void DrawLine(Vector3 start, Vector3 end, Color colour)
    {
        Debug.DrawLine(start, end, colour);

        //lines.Add(new Line(start, end, colour, false));
    }

    public static void DrawTarget(Vector3 target, Color colour)
    {
        float dist = 1;
        DrawLine(new Vector3(target.x - dist, target.y, target.z), new Vector3(target.x + dist, target.y, target.z), colour);
        DrawLine(new Vector3(target.x, target.y - dist, target.z), new Vector3(target.x, target.y + dist, target.z), colour);
        DrawLine(new Vector3(target.x, target.y, target.z - dist), new Vector3(target.x, target.y, target.z + dist), colour);
    }

    public static void DrawSquare(Vector3 min, Vector3 max, Color colour)
    {
        Vector3 tl = new Vector3(min.x, min.y, max.z);
        Vector3 br = new Vector3(max.x, min.y, min.z);
                
        LineDrawer.DrawLine(min, tl, colour);
        LineDrawer.DrawLine(tl, max, colour);
        LineDrawer.DrawLine(max, br, colour);
        LineDrawer.DrawLine(br, min, colour);
    }

    public static void DrawCircle(Vector3 centre, float radius, int points, Color colour)
    {
        float thetaInc = (Mathf.PI * 2.0f) / (float)points;
        Vector3 lastPoint = centre + new Vector3(0, 0, radius);
        for (int i = 1; i <= points; i++)
        {
            float theta = thetaInc * i;
            Vector3 point = centre + 
                (new Vector3((float) Math.Sin(theta), 0, (float) Math.Cos(theta)) * radius);
            DrawLine(lastPoint, point, colour);
            lastPoint = point;
        }
    }

    public static void DrawSphere(Vector3 centre, float radius, int points, Color colour)
    {
        float thetaInc = (Mathf.PI * 2.0f) / (float)points;
        Vector3 lastPointHoriz = centre + new Vector3(0, 0, radius);
        Vector3 lastPointVert = centre + new Vector3(0, radius, 0);
        for (int i = 1; i <= points; i++)
        {
            float theta = thetaInc * i;
            Vector3 point = centre +
                (new Vector3((float)Math.Sin(theta), 0, (float)Math.Cos(theta)) * radius);
            DrawLine(lastPointHoriz, point, colour);
            lastPointHoriz = point;
            point = centre +
                (new Vector3((float)Math.Sin(theta), (float)Math.Cos(theta), 0) * radius);
            DrawLine(lastPointVert, point, colour);
            lastPointVert = point;
        }
    }

    public static void DrawVectors(Transform transform)
    {
        float length = 20.0f;

        DrawArrowLine(transform.position, transform.position + transform.forward * length, Color.blue, transform.rotation);
        DrawArrowLine(transform.position, transform.position + transform.right * length, Color.red, transform.rotation * Quaternion.AngleAxis(90, Vector3.up));
        DrawArrowLine(transform.position, transform.position + transform.up * length, Color.green, transform.rotation * Quaternion.AngleAxis(-90, Vector3.right));
    }

    public static void DrawArrowLine(Vector3 start, Vector3 end, Color color, Quaternion rot)
    {
        lines.Add(new Line(start, end, color, false));

	    float side = 1;
	    float back = -5;
	    Vector3[] points = new Vector3[3];
        points[0] = new Vector3(-side, 0, back);
        points[1] = new Vector3(0, 0, 0);
        points[2] = new Vector3(side, 0, back);
	
	    for (int i = 0 ; i < 3 ; i ++)
	    {
		    points[i] = (rot * points[i]) + end;
	    }

        lines.Add(new Line(points[0], points[1], color, false));
        lines.Add(new Line(points[2], points[1], color, false));
    }

    void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            lineMaterial = new Material("Shader \"Lines/Colored Blended\" {" +
                "SubShader { Pass { " +
                "    Blend SrcAlpha OneMinusSrcAlpha " +
                "    ZWrite Off Cull Off Fog { Mode Off } " +
                "    BindChannels {" +
                "      Bind \"vertex\", vertex Bind \"color\", color }" +
                "} } }");
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
        }
    }

    void OnPostRender()
    {
        CreateLineMaterial();
        // set the current material
        lineMaterial.SetPass(0);
        GL.Begin(GL.LINES);
        foreach (Line line in lines)
        {
            GL.Color(line.color);
            GL.Vertex3(line.start.x, line.start.y, line.start.z);
            GL.Vertex3(line.end.x, line.end.y, line.end.z);
        }
        GL.End();                
        lines.Clear();
    }
}
