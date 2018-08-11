using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeDataController : MonoBehaviour {

  public Vector2 final_dimensions;
  public float scaling_time;
  public float time_on_screen;

  public GameObject unselected_cross;
  public GameObject selected_cross;

  private float first_time;
  private Vector2 original_scale;
  private float cross_cd;
  private float cross_limit;
  private bool descale;

 

	// Use this for initialization
	void Start () {
    first_time = Time.time;
    original_scale = transform.localScale;

    unselected_cross.SetActive(true);
    selected_cross.SetActive(false);

    cross_limit = 0.8f;
    cross_cd = cross_limit;
    descale = false;

	}
	
	// Update is called once per frame
	void Update () {

    if(cross_limit > 0.2f) {
      float t = (Time.time - first_time) / scaling_time;
      Vector3 new_scale = new Vector3(
        Mathf.SmoothStep(original_scale.x,final_dimensions.x,t),
        Mathf.SmoothStep(original_scale.y,final_dimensions.y,t),
        1.0f);
      transform.localScale = new_scale;


      if(Time.time - first_time > time_on_screen - 2.0f) {
        if(cross_cd >= cross_limit) {
          unselected_cross.SetActive(!unselected_cross.activeSelf);
          selected_cross.SetActive(!selected_cross.activeSelf);

          if(cross_limit > 0.15f) {
            cross_limit -= 0.1f;
          }
          cross_cd = 0.0f;
        }
        cross_cd += Time.deltaTime;
      } 

    } else {
      if(!descale) {
        first_time = Time.time;
        descale = true;
      }
      float t = (Time.time - first_time) / scaling_time;
      Vector3 new_scale = new Vector3(
        Mathf.SmoothStep(final_dimensions.x, original_scale.x,t),
        Mathf.SmoothStep(final_dimensions.y, original_scale.y,t),
        1.0f);
      transform.localScale = new_scale;
      if(t > 0.9f) Destroy(gameObject);
    }
	}
}
