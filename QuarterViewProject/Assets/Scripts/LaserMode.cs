using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMode : MonoBehaviour
{
    Transform startpos;
    Vector3[] laserDir = {Vector3.right, -Vector3.right, Vector3.forward, -Vector3.forward };
    [SerializeField]
    GameObject laserline;
    [SerializeField]
    AudioClip laserShootAudio;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        startpos = GetComponentInParent<Transform>();
        startpos.position += new Vector3(0, 0.5f, 0);
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Shooting Laser
    /// Shoot the laser in four directions around the player and hold it for 0.5 seconds. 
    /// When shooting a laser, it was implemented with a Line Renderer, 
    /// and box colliders were put on each to be treated if an enemy was shot.
    /// </summary>
    public void ShootLaser()
    {
        audioSource.PlayOneShot(laserShootAudio, 1f);
        foreach (var laser in laserDir)
        {
            Debug.Log("Shoot");
            GameObject line = Instantiate(laserline);
            LineRenderer laserLineRenderer= line.GetComponent<LineRenderer>();
            BoxCollider collider = line.GetComponentInChildren<BoxCollider>();
            laserLineRenderer.SetPosition(0, startpos.position);
            RaycastHit[] hitInfo;

            hitInfo = Physics.RaycastAll(startpos.position, laser, 1000f);

            for(int i = 0; i < hitInfo.Length; i++)
            {
                RaycastHit hit = hitInfo[i];

                if(hit.collider.gameObject.CompareTag("Wall"))
                {
                    laserLineRenderer.SetPosition(1, hit.point);
                    collider.size = new Vector3(1f, 1f, Vector3.Distance(startpos.position, hit.point));
                    line.transform.position = (startpos.position + hit.point) / 2;
                    collider.transform.LookAt(hit.point);
                    break;
                }
            }
        }
        
    }
}
