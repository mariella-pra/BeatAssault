using UnityEngine;
using Cinemachine;

public class LookAtTarget : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;  
    public CinemachineSmoothPath dollyTrack;        
    public CinemachineDollyCart dollyCart;         
    public Transform[] lookAtTargets;               

    public float[] pathPoints;                      

    private int currentLookTargetIndex = 0;          

    void Start()
    {
        if (lookAtTargets.Length > 0)
        {
            virtualCamera.LookAt = lookAtTargets[0];
        }
    }

    void Update()
    {
        float currentPathPosition = dollyCart.m_Position;

       
        if (currentLookTargetIndex < pathPoints.Length && currentPathPosition >= pathPoints[currentLookTargetIndex])
        {
         
            virtualCamera.LookAt = lookAtTargets[currentLookTargetIndex];
            currentLookTargetIndex++;  
        }
    }
}
