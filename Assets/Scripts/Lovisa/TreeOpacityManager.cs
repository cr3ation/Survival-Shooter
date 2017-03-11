using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeOpacityManager : MonoBehaviour {

    private bool treeIsInvisible;
    private MeshRenderer tree;
    private Material treeMaterial;
    private float timer;
    private float currentMaxCutoff;
    private float currentMinCutoff;

	// Use this for initialization
	void Start () {
        treeIsInvisible = false;
        tree = this.GetComponentInChildren<MeshRenderer>();
        treeMaterial = tree.GetComponent<Renderer>().material;
        timer = 0;
        currentMinCutoff = 0.125f; //Parameter that changes the opacity of the leaves, 0.125f is default.
    }
	
	// Update is called once per frame
	void Update () {

        float fadeDuration = 1.5f;

        // Blend in/out the palm-leaves
        if(timer < fadeDuration)
        {
            if(treeIsInvisible)
            {
                currentMaxCutoff = Mathf.Lerp(currentMinCutoff, 0.26f, timer * fadeDuration);
                treeMaterial.SetFloat("_Cutoff", currentMaxCutoff);
                timer += Time.deltaTime;
            }
            else
            {
                currentMinCutoff = Mathf.Lerp(currentMaxCutoff, 0.125f, timer * fadeDuration);
                treeMaterial.SetFloat("_Cutoff", currentMinCutoff);
                timer += Time.deltaTime;
            }
        }


    }

    // If Lovisa enters the collider, start the fading-out
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Lovisa")
        {
            treeIsInvisible = true;
            timer = 0;
        }
    }

    // If Lovisa exits the collider, start the fading-in
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Lovisa")
        {
            treeIsInvisible = false;
            timer = 0;
        }
    }
}
