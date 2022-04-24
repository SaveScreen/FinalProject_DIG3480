using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatScript : MonoBehaviour
{
    public GameObject dialoguebox;
    public GameObject dialoguebox2;
    public bool catdialogue;
    public GameObject ruby;
    
    
    // Start is called before the first frame update
    void Start()
    {
        dialoguebox.SetActive(false);
        catdialogue = false;
        
        
    }

    // Update is called once per frame
    void Update()
    {
            if (catdialogue) {
                dialoguebox.SetActive(true);
            }
            if (!catdialogue) {
                dialoguebox.SetActive(false);
            }
    }
}
