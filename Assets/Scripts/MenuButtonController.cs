using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour
{
    public int index;
    public int maxIndex;
    private int item_h = 25;
    [SerializeField] RectTransform rectTransform;

  
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pressDown()
    {
      if (index < maxIndex) {
        index ++;
        if (index > 1 && index < maxIndex) {
          rectTransform.offsetMax -= (new Vector2(0, -item_h));
        }
        else if (index == maxIndex) {
          rectTransform.offsetMax = new Vector2(0, (maxIndex - 2)*item_h+item_h/2);
        }
      } else {
        index = 0;
        rectTransform.offsetMax = Vector2.zero;
      }
    }

    public void pressUp()
    {
      if (index > 0) {
        index --;
        if (index < maxIndex - 1 && index > 0) {
          rectTransform.offsetMax -= (new Vector2(0, item_h));
        }
        else if (index == 0) {
          rectTransform.offsetMax = Vector2.zero;
        }
      } else {
        index = maxIndex;
        rectTransform.offsetMax = new Vector2(0, (maxIndex - 2)*item_h+item_h/2);
      }
    }
}
