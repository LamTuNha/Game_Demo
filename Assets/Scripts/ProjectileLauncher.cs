using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectilePrefabs;
    public Transform launchPoint;


    public void FireProjectile(){
        GameObject projectile =  Instantiate(projectilePrefabs, launchPoint.position, projectilePrefabs.transform.rotation);

        Vector3 oriScale = projectile.transform.localScale;
        projectile.transform.localScale = new Vector3(
                                                oriScale.x * transform.localScale.x > 0 ? 1:-1, 
                                                oriScale.y, 
                                                oriScale.z);
    }
}