using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour {

    private bool isShaking = false;
    private float camera_original_positionX;
    private float camera_original_positionY;
    private float camera_original_positionZ;
    private float intensity;
    private int shakes = 0;

	void Start ()
    {
        camera_original_positionX = transform.position.x;
        camera_original_positionY = transform.position.y;
        camera_original_positionZ = transform.position.z;

        intensity = 0.1f;
	}

	void Update ()
    {
	    if(isShaking)
        {
            float random_shakingX = Random.Range(-intensity, intensity);
            float random_shakingY = Random.Range(-intensity, intensity);
            float random_shakingZ = Random.Range(0, intensity * 2);
            transform.position = new Vector3(camera_original_positionX + random_shakingX, camera_original_positionY + random_shakingY, camera_original_positionZ + random_shakingZ);

            shakes--;

            if(shakes <= 0)
            {
                isShaking = false;
                transform.position = new Vector3(camera_original_positionX, camera_original_positionY, transform.position.z);
            }
        }
	}

    public void Shaking(float in_intensity){
        isShaking = true;
        shakes = 5;
        intensity = in_intensity;
    }
}
