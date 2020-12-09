using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapMarker : MonoBehaviour
{
    public Texture selectedMarkerTexture;
    public Text textBox;
    public RawImage outline;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string name)
    {
        textBox.text = name;
    }

    public void SetSelected()
    {
        outline.texture = selectedMarkerTexture;
    }


}
