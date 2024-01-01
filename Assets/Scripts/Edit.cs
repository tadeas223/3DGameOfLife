using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edit : MonoBehaviour
{
    public GameObject editPointPrefab;
    public PointGrid pointGrid;
    public Transform player;

    public int radius;
    public float rayDistance;
    public float scale;

    public bool editMode;
    private bool editModeOld;

    private bool pointsShown;
    private Dictionary<Vector3, GameObject> editPoints = new Dictionary<Vector3, GameObject>();
    private Vector3 playerOld = new Vector3();


    // Update is called once per frame
    void Update()
    {
        if (editMode)
        {
            if (playerOld != player.transform.position || editMode != editModeOld)
            {
                UpdatePoints();
            }
        }
        else
        {
            if (pointsShown)
            {
                HideEditPoints();
            }
        }

        playerOld = player.transform.position;
        editModeOld = editMode;

        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(player.position, player.forward, out hit, rayDistance) &&
            Cursor.lockState == CursorLockMode.Locked)
        {
            if (pointGrid.points.ContainsKey(hit.transform.position))
            {
                pointGrid.RemovePoint(hit.transform.position);
            }
            else
            {
                pointGrid.AddPoint(hit.transform.position);
            }
        }
    }

    public void UpdatePoints()
    {
        Vector3 referencePoint = MakeVectorDivisibleBy(player.position, pointGrid.distance);
        List<Vector3> oldVectors = new List<Vector3>(editPoints.Keys);

        for (int y = -radius; y < radius; y++)
        {
            for (int z = -radius; z < radius; z++)
            {
                for (int x = -radius; x < radius; x++)
                {
                    Vector3 vector = new Vector3(referencePoint.x + x * pointGrid.distance, referencePoint.y + y * pointGrid.distance, referencePoint.z + z * pointGrid.distance);
                    if (!editPoints.ContainsKey(vector))
                    {
                        GameObject g = Instantiate(editPointPrefab, vector, Quaternion.identity, pointGrid.parent);
                        g.transform.localScale = Vector3.one * scale;
                        editPoints.Add(vector, g);
                    }
                    oldVectors.Remove(vector);
                }
            }
        }

        foreach (Vector3 v in oldVectors)
        {
            Destroy(editPoints[v]);
            editPoints.Remove(v);
        }
        pointsShown = true;
    }

    void HideEditPoints()
    {
        foreach (var kvp in editPoints)
        {
            Destroy(kvp.Value);
        }
        editPoints.Clear();
        pointsShown = false;
    }

    // https://forum.unity.com/threads/round-a-vector3-to-the-nearest-power-of-3.366578/
    public static float MakeDivisibleBy(float input, float divide)
    {
        if (input % divide == 0)
            return input;
        else
        {
            float x = Mathf.Round(input / divide);
            if (x == 0f && input > 0f) x += 1f;
            x *= divide;
            return x;
        }
    }

    public static Vector3 MakeVectorDivisibleBy(Vector3 vec, float divide)
    {
        Vector3 vector = new Vector3(MakeDivisibleBy(vec.x, divide), MakeDivisibleBy(vec.y, divide), MakeDivisibleBy(vec.z, divide));
        return vector;
    }

    public void ChangeEditMode()
    {
        editMode = !editMode;
    }
}