using UnityEngine;
using System.Collections;
using UnityEngine.PostProcessing;

public class DofFocus : MonoBehaviour
{
    private PostProcessingProfile post;
    private float defaultDistance;
    [SerializeField] private float focusSpeed = 2f;

    // Use this for initialization
    void Start()
    {
        post = GetComponent<PostProcessingBehaviour>().profile;
        defaultDistance = post.depthOfField.settings.focusDistance;
    }

    // Update is called once per frame
    void Update()
    {
        var target = new Vector3(0.0f, 0.0f, 0.0f);
        Ray ray = GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        var mask = ~(1 << 9);
        if (Physics.SphereCast(ray, 0.2f, out hit, 100f, mask))
        {
            target = hit.point;
        }
        /*if (Physics.Raycast(ray, out hit, 100f, mask))
        {
            // print("It hit" + hit.transform.name);
            target = hit.point;
        }*/
        else
        {
            target = transform.position;
        }
        var distance = Vector3.Distance(ray.origin, target);
        var dof = post.depthOfField.settings;
        dof.focusDistance = Mathf.Lerp(dof.focusDistance, distance, Time.deltaTime * focusSpeed);
        post.depthOfField.settings = dof;
    }

    /*Vector3 FindTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
        RaycastHit hit;
        var mask = ~(1 << 9);
        if (Physics.Raycast(ray, out hit, 100f, mask))
        {
            //print(hit.collider.gameObject.ToString());
            return hit.point;
        }
        return transform.position;
    }*/

    private void OnDestroy()
    {
        var dof = post.depthOfField.settings;
        dof.focusDistance = defaultDistance;
        post.depthOfField.settings = dof;
    }
}
