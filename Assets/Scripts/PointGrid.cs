using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointGrid : MonoBehaviour
{
    public GameObject pointPrefab;
    public float scale;
    public Transform parent;
    public Edit edit;

    public float distance;

    public int[] rule;
    public InputField ruleSetter;

    public Text buttonText;
    public Slider slider;

    public Dictionary<Vector3, GameObject> points = new Dictionary<Vector3, GameObject>();

    public Simulation simulation;

    private bool play = false;
    private int fps;

    // Start is called before the first frame update
    void Start()
    {
        ruleSetter.text = string.Join("", rule);
        simulation = new Simulation(this, rule);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            edit.editMode = !edit.editMode;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayButton();
        }
        if (play)
        {
            fps++;
            if (fps >= (1f / Time.deltaTime) / slider.value)
            {
                simulation.Simulate();
                fps = 0;
            }
        }
    }

    public void AddPoint(Vector3 pos)
    {
        if (!points.ContainsKey(pos))
        {
            points.Add(pos, Instantiate(pointPrefab));
            points[pos].transform.parent = parent;
            points[pos].transform.position = pos;
        }
    }

    public void RemovePoint(Vector3 pos)
    {
        if (points.ContainsKey(pos))
        {
            Destroy(points[pos]);
            points.Remove(pos);
        }
    }

    public void PlayButton()
    {
        play = !play;
        buttonText.text = play ? "stop" : "play";
    }

    public void UpdateRule()
    {
        try
        {
            rule[0] = int.Parse(ruleSetter.text.Substring(0, 1));
            rule[1] = int.Parse(ruleSetter.text.Substring(1, 1));
            rule[2] = int.Parse(ruleSetter.text.Substring(2, 1));
            rule[3] = int.Parse(ruleSetter.text.Substring(3, 1));
        }
        catch (System.Exception e)
        {
            ruleSetter.text = string.Join("", rule);
        }
    }

    public void Clear()
    {
        foreach (var kvp in points)
        {
            Destroy(kvp.Value);
        }
        points.Clear();
    }
}
