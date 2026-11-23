using System.Collections.Generic;
using UnityEngine;

public class snap_point : MonoBehaviour
{
    public bool hovering_sp;
    public int rotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hovering_sp = false;
    }

    public List<Vector3> GetPoints(string name, int r)
    {
        List<Vector3> points = new List<Vector3>();

        Vector3 p = transform.position;

        if (gameObject.name == "conveyor")
        {
            if (name == "conveyor_preview")
            {
                if (rotation == 0 && r == 0)
                {
                    points.Add(p + new Vector3(0, 0, 2));
                    points.Add(p + new Vector3(0, 0, -2));
                }
                else if (rotation == 1 && r == 1)
                {
                    points.Add(p + new Vector3(2, 0, 0));
                    points.Add(p + new Vector3(-2, 0, 0));
                }
            }
            else if (name == "conveyor_short_preview")
            {
                if (rotation == 0 && r == 0)
                {
                    points.Add(p + new Vector3(0, 0, 1.7f));
                    points.Add(p + new Vector3(0, 0, -1.7f));
                }
                else if (rotation == 1 && r == 1)
                {
                    points.Add(p + new Vector3(1.7f, 0, 0));
                    points.Add(p + new Vector3(-1.7f, 0, 0));
                }
            }
            else if (name == "conveyor_incline_preview")
            {
                if (rotation == 0 && r == 0)
                {
                    points.Add(p + new Vector3(0, 1.297904f - 0.9546665f, -2));
                    points.Add(p + new Vector3(0, -(0.9546665f - 0.56f), 1.96f));
                }
                else if (rotation == 1 && r == 1)
                {
                    points.Add(p + new Vector3(-2, 1.297904f - 0.9546665f, 0));
                    points.Add(p + new Vector3(1.96f, -(0.9546665f - 0.56f), 0));
                }
                else if (rotation == 0 && r == 2)
                {
                    points.Add(p + new Vector3(0, 1.297904f - 0.9546665f, 1.8f));
                    points.Add(p + new Vector3(0, -(0.9546665f - 0.56f), -2.15f));
                }
                else if (rotation == 1 && r == 3)
                {
                    points.Add(p + new Vector3(1.8f, 1.297904f - 0.9546665f, 0));
                    points.Add(p + new Vector3(-2.15f, -(0.9546665f - 0.56f), 0));
                }
            }
            else if ((name == "box_spawner_preview") || (name == "can_spawner_preview"))
            {
                points.Add(p + new Vector3(0, 1.5f - 0.9546665f, 0));
            }
        }
        else if (gameObject.name == "conveyor_short")
        {
            if (name == "conveyor_preview")
            {
                if (rotation == 0 && r == 0)
                {
                    points.Add(p + new Vector3(0, 0, 1.7f));
                    points.Add(p + new Vector3(0, 0, -1.7f));
                }
                else if (rotation == 1 && r == 1)
                {
                    points.Add(p + new Vector3(1.7f, 0, 0));
                    points.Add(p + new Vector3(-1.7f, 0, 0));
                }
            }
            else if (name == "conveyor_short_preview")
            {
                if (rotation == 0 && r == 0)
                {
                    points.Add(p + new Vector3(0, 0, 1.4f));
                    points.Add(p + new Vector3(0, 0, -1.4f));
                }
                else if (rotation == 1 && r == 1)
                {
                    points.Add(p + new Vector3(1.4f, 0, 0));
                    points.Add(p + new Vector3(-1.4f, 0, 0));
                }
            }
            else if (name == "conveyor_incline_preview")
            {
                if (rotation == 0 && r == 0)
                {
                    points.Add(p + new Vector3(0, 1.297904f - 0.9546665f, -1.7f));
                    points.Add(p + new Vector3(0, -(0.9546665f - 0.56f), 1.66f));

                }
                else if (rotation == 1 && r == 1)
                {
                    points.Add(p + new Vector3(-1.7f, 1.297904f - 0.9546665f, 0));
                    points.Add(p + new Vector3(1.66f, -(0.9546665f - 0.56f), 0));
                }
                else if (rotation == 0 && r == 2)
                {
                    points.Add(p + new Vector3(0, 1.297904f - 0.9546665f, 1.5f));
                    points.Add(p + new Vector3(0, -(0.9546665f - 0.56f), -1.81f));
                }
                else if (rotation == 1 && r == 3)
                {
                    points.Add(p + new Vector3(1.5f, 1.297904f - 0.9546665f, 0));
                    points.Add(p + new Vector3(-1.81f, -(0.9546665f - 0.56f), 0));
                }
            }
            else if ((name == "box_spawner_preview") || (name == "can_spawner_preview"))
            {
                points.Add(p + new Vector3(0, 1.5f - 0.9546665f, 0));
            }
        }
        else if (gameObject.name == "conveyor_incline")
        {
            if (name == "conveyor_preview")
            {
                if (rotation == 0 && r == 0)
                {
                    points.Add(p + new Vector3(0, -(1.297904f - 0.9546665f), 2));
                    points.Add(p + new Vector3(0, 1.69f - 1.297904f, -2));
                }
                else if (rotation == 1 && r == 1)
                {
                    points.Add(p + new Vector3(2, -(1.297904f - 0.9546665f), 0));
                    points.Add(p + new Vector3(-2, 1.69f - 1.297904f, 0));
                }
                else if (rotation == 2 && r == 0)
                {
                    points.Add(p + new Vector3(0, -(1.297904f - 0.9546665f), -1.8f));
                    points.Add(p + new Vector3(0, 1.69f - 1.297904f, 2.15f));
                }
                else if (rotation == 3 && r == 1)
                {
                    points.Add(p + new Vector3(-1.8f, -(1.297904f - 0.9546665f), 0));
                    points.Add(p + new Vector3(2.15f, 1.69f - 1.297904f, 0));
                }
            }
            else if (name == "conveyor_short_preview")
            {
                if (rotation == 0 && r == 0)
                {
                    points.Add(p + new Vector3(0, -(1.297904f - 0.9546665f), 1.7f));
                    points.Add(p + new Vector3(0, 1.69f - 1.297904f, -1.7f));
                }
                else if (rotation == 1 && r == 1)
                {
                    points.Add(p + new Vector3(1.7f, -(1.297904f - 0.9546665f), 0));
                    points.Add(p + new Vector3(-1.7f, 1.69f - 1.297904f, 0));
                }
                else if (rotation == 2 && r == 0)
                {
                    points.Add(p + new Vector3(0, -(1.297904f - 0.9546665f), -1.53f));
                    points.Add(p + new Vector3(0, 1.69f - 1.297904f, 1.85f));
                }
                else if (rotation == 3 && r == 1)
                {
                    points.Add(p + new Vector3(-1.53f, -(1.297904f - 0.9546665f), 0));
                    points.Add(p + new Vector3(1.85f, 1.69f - 1.297904f, 0));
                }
            }
            else if (name == "conveyor_incline_preview")
            {
                if (rotation == 0 && r == 0)
                {
                    points.Add(p + new Vector3(0, 2 - 1.297904f, -1.85f));
                    points.Add(p + new Vector3(0, -(1.297904f - 0.59f), 1.85f));
                }
                else if (rotation == 1 && r == 1)
                {
                    points.Add(p + new Vector3(-1.85f, 2 - 1.297904f, 0));
                    points.Add(p + new Vector3(1.85f, -(1.297904f - 0.59f), 0));
                }
                else if (rotation == 2 && r == 2)
                {
                    points.Add(p + new Vector3(0, 2 - 1.297904f, 1.85f));
                    points.Add(p + new Vector3(0, -(1.297904f - 0.59f), -1.85f));
                }
                else if (rotation == 3 && r == 3)
                {
                    points.Add(p + new Vector3(1.85f, 2 - 1.297904f, 0));
                    points.Add(p + new Vector3(-1.85f, -(1.297904f - 0.59f), 0));
                }
            }
        }

        return points;
    }
}
