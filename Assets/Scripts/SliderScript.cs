using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        // Initiate any setup logic if needed
    }

    // Update is called once per frame
    void Update()
    {
        // Implement any update logic if needed
    }

    public void SetText()
    {
        text.text = GetComponent<Slider>().value.ToString();
    }
}
