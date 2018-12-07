using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Taken from the SteamVR_LaserPointer script
// Only edit is to make the Laser Pointer only interact with UI elements

public class UIPointer : MonoBehaviour
{
    public bool active = true;
    public Color color;
    public float thickness = 0.002f;
    private GameObject holder;
    private GameObject pointer;
    bool isActive = true;
    private Transform reference;

    Button lastButton = null;
    float timeTouching = 0f;

    public GameObject canvas;
    private Image progressBar;
    public float progressBarDistance = 0.1f;

    // Use this for initialization
    void Start()
    {
        holder = new GameObject();
        holder.transform.parent = this.transform;
        holder.transform.localPosition = Vector3.zero;
        holder.transform.localRotation = Quaternion.identity;

        pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pointer.transform.parent = holder.transform;
        pointer.transform.localScale = new Vector3(thickness, thickness, 100f);
        pointer.transform.localPosition = new Vector3(0f, 0f, 50f);
        pointer.transform.localRotation = Quaternion.identity;
        BoxCollider collider = pointer.GetComponent<BoxCollider>();
        if (collider)
        {
            Object.Destroy(collider);
        }
        Material newMaterial = new Material(Shader.Find("Unlit/Color"));
        newMaterial.SetColor("_Color", color);
        pointer.GetComponent<MeshRenderer>().material = newMaterial;

        progressBar = canvas.GetComponentInChildren<Image>();
        progressBar.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Ray raycast = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(raycast, out hit, Mathf.Infinity, 1 << 5))
        {

            Button hitObject = hit.collider.gameObject.GetComponent<Button>();
            
            // If we hit an actual Button, then we'll call the function on it
            if (hitObject)
            {
                // If we've just gotten onto this button, then we reset the time
                if (hitObject != lastButton)
                {
                    lastButton = hitObject;
                    timeTouching = 0f;
                }
                timeTouching += Time.deltaTime;

                progressBar.fillAmount = timeTouching / 2f;
                canvas.transform.position = hit.point + raycast.direction * -1 * progressBarDistance;
                canvas.transform.rotation = Camera.main.transform.rotation;

                // If we've been touching the button for 2 seconds, we invoke it
                if (timeTouching >= 2f)
                {
                    timeTouching = 0f;
                    progressBar.fillAmount = 0f;
                    lastButton.onClick.Invoke();
                }

                pointer.transform.localScale = new Vector3(thickness * 5f, thickness * 5f, hit.distance);
                pointer.transform.localPosition = new Vector3(0f, 0f, hit.distance / 2f);
            }
            else
            {
                progressBar.fillAmount = 0f;
                lastButton = null;

                pointer.transform.localScale = new Vector3(thickness, thickness, hit.distance);
                pointer.transform.localPosition = new Vector3(0f, 0f, hit.distance / 2f);
            }
        }
        else
        {
            progressBar.fillAmount = 0f;
            lastButton = null;

            pointer.transform.localScale = new Vector3(thickness, thickness, 100f);
            pointer.transform.localPosition = new Vector3(0f, 0f, 50f);
        }
    }

    private void OnEnable()
    {
        holder.SetActive(true);
        pointer.SetActive(true);
        isActive = true;
    }

    private void OnDisable()
    {
        holder.SetActive(false);
        pointer.SetActive(false);
        isActive = false;
    }
}

