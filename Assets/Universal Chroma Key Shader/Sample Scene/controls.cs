using UnityEngine;
using System.Collections;

public class controls : MonoBehaviour {

	
	float sensS, cutoffS;
	Color colS;

	// Use this for initialization
	void Start () {
		sensS = GetComponent<Renderer>().material.GetFloat("_Sens");
		cutoffS = GetComponent<Renderer>().material.GetFloat("_Cutoff");
		colS = GetComponent<Renderer>().material.GetColor("_Color");

		sens = sensS;
		cutoff = cutoffS; 

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	float sens, cutoff;
	string r = "99", g = "205", b ="77";
	void OnGUI () {
		sens = GUI.HorizontalSlider (new Rect (25, 25, 100, 30), sens, 0.0f, 1.0f);
		cutoff = GUI.HorizontalSlider (new Rect (25, 70, 100, 30), cutoff, 0.0f, 1.0f);

		r = GUI.TextField (new Rect (25, 120, 40, 20), r);
		g = GUI.TextField (new Rect (70, 120, 40, 20), g);
		b = GUI.TextField (new Rect (120, 120, 40, 20), b);

		if (GUI.Button (new Rect (25, 160, 100, 30), "Reset")) {
			sens = sensS;
			cutoff = cutoffS;

			r = (colS.r * 255f).ToString();
			g = (colS.g * 255f).ToString();
			b = (colS.b * 255f).ToString();
		}


		GetComponent<Renderer>().material.SetFloat("_Sens", sens);
		GetComponent<Renderer>().material.SetFloat("_Cutoff", cutoff);
		try{
			Color col = new Color(int.Parse(r) / 255f, int.Parse(g) / 255f, int.Parse(b) / 255f);
			print (col);
			GetComponent<Renderer>().material.color = col; 

		}catch(UnityException e){
		}
	}
}
