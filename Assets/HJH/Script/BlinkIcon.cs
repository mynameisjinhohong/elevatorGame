using UnityEngine;
using UnityEngine.UI;

public class BlinkIcon : MonoBehaviour
{
    public float speed;
    bool down = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (down)
        {
            Color color = GetComponent<Image>().color;
            color.a -= speed * Time.deltaTime;
            if(color.a <= 0)
            {
                down = false;
            }
            GetComponent<Image>().color = color;
            
        }
        else
        {
            Color color = GetComponent<Image>().color;
            color.a += speed * Time.deltaTime;
            if (color.a >= 1)
            {
                down = true;
            }
            GetComponent<Image>().color = color;

        }
    }
}
