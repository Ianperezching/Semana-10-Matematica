using UnityEngine;
using System.Collections.Generic;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public Transform launchPoint; 
    public float maxLaunchForce = 20f;
    public float minLaunchForce = 5f; 
    public float maxChargeTime = 1f; 

    public GameObject trajectoryPointPrefab; 
    public int trajectoryPointCount = 20; 

    private float currentLaunchForce; 
    private float chargeSpeed; 
    private bool aiming; 
    private Vector3 aimDirection;
    private List<GameObject> trajectoryPoints; 

    void Start()
    {
        trajectoryPoints = new List<GameObject>();
        for (int i = 0; i < trajectoryPointCount; i++)
        {
            GameObject point = Instantiate(trajectoryPointPrefab);
            point.SetActive(false);
            trajectoryPoints.Add(point);
        }
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartAiming();
        }
        else if (Input.GetButton("Fire1") && aiming)
        {
            Aim();
        }
        else if (Input.GetButtonUp("Fire1") && aiming)
        {
            Launch();
        }
    }

    void StartAiming()
    {
        aiming = true;
        currentLaunchForce = minLaunchForce;
        chargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;
    }

    void Aim()
    {
        currentLaunchForce += chargeSpeed * Time.deltaTime;
        currentLaunchForce = Mathf.Clamp(currentLaunchForce, minLaunchForce, maxLaunchForce);

        // Obtener la dirección del objetivo
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(camRay, out hit))
        {
            aimDirection = hit.point - launchPoint.position;
            aimDirection.y = 0; 
        }

     
        ShowTrajectory(launchPoint.position, aimDirection.normalized * currentLaunchForce);
    }

    void Launch()
    {
        aiming = false;
        GameObject projectileInstance = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
        Rigidbody rb = projectileInstance.GetComponent<Rigidbody>();
        rb.velocity = aimDirection.normalized * currentLaunchForce;

        
        HideTrajectory();
    }

    void ShowTrajectory(Vector3 startPoint, Vector3 initialVelocity)
    {
        Vector3 position = startPoint;
        Vector3 velocity = initialVelocity;
        Vector3 gravity = Physics.gravity;
        float timeStep = 0.1f;

        for (int i = 0; i < trajectoryPoints.Count; i++)
        {
            trajectoryPoints[i].SetActive(true);
            trajectoryPoints[i].transform.position = position;
            position += velocity * timeStep + 0.5f * gravity * timeStep * timeStep;
            velocity += gravity * timeStep;
        }
    }

    void HideTrajectory()
    {
        foreach (GameObject point in trajectoryPoints)
        {
            point.SetActive(false);
        }
    }
}

