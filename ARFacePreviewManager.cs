using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ARFacePreviewManager : MonoBehaviour
{
    public ARFaceManager arFaceManager;
    public GameObject defaultHairstylePrefab;  
    public Texture2D defaultLipstickTexture;  
    public Material faceMaterial;  

    private Dictionary<TrackableId, GameObject> hairstyleInstances = new Dictionary<TrackableId, GameObject>(); 
    private Dictionary<TrackableId, MeshRenderer> faceRenderers = new Dictionary<TrackableId, MeshRenderer>(); 

    void OnEnable()
    {
        arFaceManager.facesChanged += OnFacesChanged;
    }

    void OnDisable()
    {
        arFaceManager.facesChanged -= OnFacesChanged;
    }

 
    private void OnFacesChanged(ARFacesChangedEventArgs eventArgs)
    {
        foreach (ARFace face in eventArgs.added)
        {
            
            ApplyVirtualServicePreview(face);
        }

        foreach (ARFace face in eventArgs.updated)
        {
            
            UpdateVirtualServicePreview(face);
        }

        foreach (ARFace face in eventArgs.removed)
        {
            
            RemoveVirtualServicePreview(face);
        }
    }

    private void ApplyVirtualServicePreview(ARFace face)
    {
        GameObject hairstyle = Instantiate(defaultHairstylePrefab, face.transform);
        hairstyle.transform.localPosition = Vector3.zero;
        hairstyle.transform.localRotation = Quaternion.identity;
        hairstyleInstances[face.trackableId] = hairstyle;

        if (faceMaterial != null)
        {
            MeshRenderer faceRenderer = face.GetComponent<MeshRenderer>();
            faceRenderer.material = faceMaterial;
            faceRenderer.material.SetTexture("_MainTex", defaultLipstickTexture);
            faceRenderers[face.trackableId] = faceRenderer;
        }
    }

    private void UpdateVirtualServicePreview(ARFace face)
    {
        
        if (hairstyleInstances.TryGetValue(face.trackableId, out GameObject hairstyle))
        {
          
            hairstyle.transform.localPosition = Vector3.zero;
        }
    }

    private void RemoveVirtualServicePreview(ARFace face)
    {
      
        if (hairstyleInstances.TryGetValue(face.trackableId, out GameObject hairstyle))
        {
            Destroy(hairstyle);
            hairstyleInstances.Remove(face.trackableId);
        }

      
        if (faceRenderers.TryGetValue(face.trackableId, out MeshRenderer faceRenderer))
        {
            Destroy(faceRenderer.gameObject);
            faceRenderers.Remove(face.trackableId);
        }
    }
}
