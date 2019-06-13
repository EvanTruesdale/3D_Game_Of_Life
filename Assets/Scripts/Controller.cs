using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    string type;

    // Variables For Computer
    float horizontal;
    float vertical;
    float move;
    float angle_x = 0;
    float angle_y = 90;
    public float speed;

    // Variables For GVR Headset

    void Start () {
        if (SystemInfo.deviceType == DeviceType.Desktop) {
            type = "desktop";
        } else if (SystemInfo.deviceType == DeviceType.Handheld) {
            type = "handheld";
        }
    }

	void Update () {
        if (type == "desktop") {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            move = Input.GetAxis("Fire1");

            angle_x += vertical;
            angle_y += horizontal;

            Transform trans = GetComponent<Transform>();
            trans.rotation = Quaternion.identity;
            trans.Rotate(new Vector3(0, angle_y, 0));
            trans.Rotate(new Vector3(-angle_x, 0, 0));
            trans.position += trans.forward * move * speed;
        } else if (type == "handheld") {

        }
	}
}
