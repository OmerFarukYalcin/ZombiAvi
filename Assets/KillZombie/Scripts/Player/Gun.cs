using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    private Text infoText;
    public float range = 50f;
    public Camera fpsCam;
    public GameObject effect;
    public GameObject bullet;
    public Transform bulletPos;


    [SerializeField] List<string> tasks = new List<string>();

    RaycastHit hit;


    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Q))
        {
            Shoot();
        }

        AdjustRaycast();
    }

    private void AdjustRaycast()
    {
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 5))
        {
            TaskInfoRaycast(hit);

            if (Input.GetKeyDown(KeyCode.E))
                TakeTask(hit);

            if (Input.GetKeyDown(KeyCode.F))
                PutTask(hit);
        }
    }

    private void TaskInfoRaycast(RaycastHit hit)
    {
        bool isInfoValid = hit.transform.tag == "task";

        string infoString = hit.transform.tag == "task" ? "Press E to take goldenCross piece" : "Press F to put goldenCross piece";

        infoText.gameObject.SetActive(isInfoValid);

        infoText.text = infoString;
    }

    private void TakeTask(RaycastHit hit)
    {
        if (hit.transform.tag == "task")
        {
            hit.transform.gameObject.SetActive(false);
            tasks.Add(hit.transform.name);
        }
    }

    private void PutTask(RaycastHit hit)
    {
        if (hit.transform.name == "obelisk")
        {
            if (tasks.Count > 0)
            {
                hit.transform.GetComponent<Obelisk>().getStringName(tasks[0]);
                hit.transform.GetComponent<Obelisk>().putGameobject();
                tasks.RemoveAt(0);
            }
        }
    }

    void Shoot()
    {
        transform.parent.parent.GetComponent<PlayerController>().playShootSound();
        GameObject goEffect = Instantiate(effect, bulletPos.position, bulletPos.rotation);
        Destroy(goEffect.gameObject, 1f);
        bool hitObj = Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range);
        InstantiateBullet(hit, hitObj);
    }

    void InstantiateBullet(RaycastHit _hit, bool hitting)
    {
        if (hitting)
        {
            GameObject impactGo = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
            impactGo.GetComponent<Rigidbody>().AddForceAtPosition((_hit.point - bulletPos.position).normalized * 500f, _hit.point.normalized, ForceMode.Force);
            Destroy(impactGo, 2f);
        }
        else
        {
            GameObject impactGo = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
            impactGo.GetComponent<Rigidbody>().velocity = bulletPos.transform.forward * 10f;
            Destroy(impactGo, 2f);
        }

    }
}
