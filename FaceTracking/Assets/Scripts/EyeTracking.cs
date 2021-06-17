using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARFace))]
public class EyeTracking : MonoBehaviour
{
    public GameObject prefabOjo;
    public GameObject prefabCuernos;
    public GameObject prefabMascara1;
    public GameObject prefabMascara2;

    private GameObject leftEye;
    private GameObject rightEye;
    private ARFace aRFace;


    private void Awake()
    {
        aRFace = GetComponent<ARFace>();
    }

    private void OnEnable()
    {
        ARFaceManager faceManager = FindObjectOfType<ARFaceManager>();
        if(faceManager != null && faceManager.subsystem !=null && faceManager.subsystem.subsystemDescriptor.supportsEyeTracking)
        {
            aRFace.updated += OnUpdated;
            //Debug.Log("Sí soporta el tracking");
        }
        else
        {
            //Debug.Log("No soporta el tracking");
        }
    }

    private void OnDisable()
    {
        aRFace.updated -= OnUpdated;
        SetVisibility(false);
    }

    void OnUpdated(ARFaceUpdatedEventArgs eventArgs)
    {
        if(aRFace.leftEye != null && leftEye == null)
        {
            leftEye = Instantiate(prefabOjo, aRFace.leftEye);
            leftEye.SetActive(false);
        }
        if (aRFace.rightEye != null && rightEye == null)
        {
            rightEye = Instantiate(prefabOjo, aRFace.rightEye);
            rightEye.SetActive(false);
        }

        bool setVisible = (aRFace.trackingState == TrackingState.Tracking) && (ARSession.state > ARSessionState.Ready);
        SetVisibility(setVisible);
    }

    void SetVisibility(bool isVisible)
    {
        if(leftEye != null && rightEye != null)
        {
            leftEye.SetActive(isVisible);
            rightEye.SetActive(isVisible);
        }
    }
}
