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

    private Dictionary<TrackableId, GameObject> hairstyleInstances = new Dictionary<TrackableId, GameObject>(); // Track hairstyle instances
    private Dictionary<TrackableId, MeshRenderer> faceRenderers = new Dictionary<TrackableId, MeshRenderer>(); // Track makeup

    void OnEnable()
    {
        arFaceManager.facesChanged += OnFacesChanged;
    }

    void OnDisable()
    {
        arFaceManager.facesChanged -= OnFacesChanged;
    }

    // Called when ARFaceManager detects new faces
    private void OnFacesChanged(ARFacesChangedEventArgs eventArgs)
    {
        foreach (ARFace face in eventArgs.added)
        {
            // Apply hairstyle and makeup when a new face is detected
            ApplyVirtualServicePreview(face);
        }

        foreach (ARFace face in eventArgs.updated)
        {
            // Optionally update the preview (e.g., for dynamic changes)
            UpdateVirtualServicePreview(face);
        }

        foreach (ARFace face in eventArgs.removed)
        {
            // Clean up when the face is removed
            RemoveVirtualServicePreview(face);
        }
    }

    private void ApplyVirtualServicePreview(ARFace face)
    {
        // Add hairstyle
        GameObject hairstyle = Instantiate(defaultHairstylePrefab, face.transform);
        hairstyle.transform.localPosition = Vector3.zero;
        hairstyle.transform.localRotation = Quaternion.identity;
        hairstyleInstances[face.trackableId] = hairstyle;

        // Add makeup (lipstick)
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
        // Update existing hairstyle or makeup if needed (e.g., change hair color dynamically)
        if (hairstyleInstances.TryGetValue(face.trackableId, out GameObject hairstyle))
        {
            // Update hairstyle position or rotation if needed (face movement)
            hairstyle.transform.localPosition = Vector3.zero;
        }
    }

    private void RemoveVirtualServicePreview(ARFace face)
    {
        // Remove hairstyle and cleanup
        if (hairstyleInstances.TryGetValue(face.trackableId, out GameObject hairstyle))
        {
            Destroy(hairstyle);
            hairstyleInstances.Remove(face.trackableId);
        }

        // Remove face makeup
        if (faceRenderers.TryGetValue(face.trackableId, out MeshRenderer faceRenderer))
        {
            Destroy(faceRenderer.gameObject);
            faceRenderers.Remove(face.trackableId);
        }
    }
}
