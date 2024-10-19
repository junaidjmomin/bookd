using UnityEngine;
using UnityEngine.UI;

public class HairStyleManager : MonoBehaviour
{
    public GameObject[] hairstylePrefabs;  
    public Color[] hairColors;
    private GameObject currentHairStyle;
    private Renderer currentHairRenderer;


    public Button[] styleButtons;
    public Button[] colorButtons;

    private void Start()
    {
     
        for (int i = 0; i < styleButtons.Length; i++)
        {
            int index = i;
            styleButtons[i].onClick.AddListener(() => ChangeHairstyle(index));
        }

     
        for (int i = 0; i < colorButtons.Length; i++)
        {
            int index = i;
            colorButtons[i].onClick.AddListener(() => ChangeHairColor(index));
        }
    }

   
    public void ChangeHairstyle(int styleIndex)
    {
        if (currentHairStyle != null)
        {
            Destroy(currentHairStyle);
        }

        currentHairStyle = Instantiate(hairstylePrefabs[styleIndex], transform);
        currentHairRenderer = currentHairStyle.GetComponent<Renderer>();

        if (currentHairRenderer != null && hairColors.Length > 0)
        {
            currentHairRenderer.material.color = hairColors[0];
        }
    }

   
    public void ChangeHairColor(int colorIndex)
    {
        if (currentHairRenderer != null)
        {
            currentHairRenderer.material.color = hairColors[colorIndex];
        }
    }
}
