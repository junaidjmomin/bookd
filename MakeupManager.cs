using UnityEngine;
using UnityEngine.UI;

public class MakeupManager : MonoBehaviour
{
    public Texture2D[] lipstickTextures;  
    public MeshRenderer faceRenderer;
    public Button[] makeupButtons;  

    private void Start()
    {
   
        for (int i = 0; i < makeupButtons.Length; i++)
        {
            int index = i;
            makeupButtons[i].onClick.AddListener(() => ApplyLipstick(index));
        }
    }

    public void ApplyLipstick(int lipstickIndex)
    {
        if (faceRenderer != null)
        {
            faceRenderer.material.SetTexture("_MainTex", lipstickTextures[lipstickIndex]);
        }
    }
}
