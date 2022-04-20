using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public float displaytime;
    public GameObject dialoguebox;
    private float timerdisplay;
    // Start is called before the first frame update
    void Start()
    {
        dialoguebox.SetActive(false);
        timerdisplay = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerdisplay >= 0) {
            timerdisplay -= Time.deltaTime;
            if (timerdisplay < 0) {
                dialoguebox.SetActive(false);
            }

        }
    }

    public void DisplayDialogue() {
        timerdisplay = displaytime;
        dialoguebox.SetActive(true);
    }
}
