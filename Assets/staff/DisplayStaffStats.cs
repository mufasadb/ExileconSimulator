using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayStaffStats : MonoBehaviour
{
    public GameObject canvas;
    private Outline outline;

    float hideCooldown = 0;
    [SerializeField] float stayShownTime = 0.5f;
    bool mouseOn = false;
    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponentInChildren<Outline>();
        hide();
    }
    private void OnMouseEnter()
    {
        mouseOn = true;
        hideCooldown = 1f;
        show();
    }
    private void OnMouseExit()
    {
        mouseOn = false;
        outline.OutlineColor = Color.white;
        outline.OutlineWidth = 1.5f;
        StartCoroutine(HideStats());
    }
    // Update is called once per frame
    public void show()
    {

        canvas.SetActive(true);

        float val = (canvas.transform.position - GlobalVariables.instance.player.transform.position).magnitude * 0.06f;
        if (val > 0.6f)
        {
            canvas.transform.localScale = new Vector3(val, val, val);
        }

        // outline.enabled = true;.
        outline.OutlineColor = Color.red;
        outline.OutlineWidth = 3;

    }
    public void hide()
    {

        canvas.SetActive(false);
        canvas.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        // outline.enabled = false;


    }
    public void showNLock()
    {
        canvas.SetActive(true);

    }
    public void hideNUnlock()
    {
        canvas.SetActive(false);

    }
    IEnumerator HideStats()
    {
        while (hideCooldown > 0)
        {

            yield return new WaitForEndOfFrame();
        }
        hide();
    }
    private void Update()
    {
        if (!mouseOn && hideCooldown > 0)
        {
            hideCooldown -= Time.deltaTime;
        }
    }
}
