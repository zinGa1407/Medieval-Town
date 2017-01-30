using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterOnOffDistance : MonoBehaviour {

    public GameObject _riverWater;

    private int _calculationTimer = 60;

	// Update is called once per frame
	void Update () {
		if(_calculationTimer > 0)
        {
            _calculationTimer--;
        }
        else if(_calculationTimer == 0)
        {
            CalculateDistance();
            _calculationTimer = 60;
        }
	}

    void CalculateDistance()
    {
        if(this.gameObject.transform.position.z < 11f)
        {
            _riverWater.SetActive(false);
        }
        else if(_riverWater.activeInHierarchy == false)
        {
            _riverWater.SetActive(true);
        }
    }
}
