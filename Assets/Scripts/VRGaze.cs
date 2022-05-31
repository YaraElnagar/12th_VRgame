using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VRGaze : MonoBehaviour
{
    private bool gvrStatus =false;
    public Image gazeImage;
    public float totalTime = 2;
    private float currentGazeTimer = 0;

    private Ray ray;
    private float rayDistance = 3;
    private RaycastHit hit;

    public GameObject teleport1, teleport2;


    public void GVROn()
    {
        gvrStatus = true;
    }


    public void GVROFF()
    {
        gvrStatus = false;
        currentGazeTimer = 0;
        gazeImage.fillAmount = 0;
    }

    private void Update()
    {
        ray = Camera.main.ViewportPointToRay(new Vector3 (0.5f,0.5f,0.5f));
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (gvrStatus)
                {
                    currentGazeTimer += Time.deltaTime;
                    gazeImage.fillAmount = currentGazeTimer / totalTime;

                    if (gazeImage.fillAmount == 1 && hit.transform.tag =="Ball")
                    {
                        hit.transform.GetComponent<Animator>().enabled = !hit.transform.GetComponent<Animator>().enabled;
                        GVROFF();
                    }

                     if (gazeImage.fillAmount == 1 && hit.transform.tag =="teleport1")
                    {
                        this.transform.position = teleport2.transform.position;
                        GVROFF();
                    }

                    if (gazeImage.fillAmount == 1 && hit.transform.tag =="teleport2")
                    {
                        this.transform.position = teleport1.transform.position;
                        GVROFF();
                    }
                }
        }

        
    }

}
