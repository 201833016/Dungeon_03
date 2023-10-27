using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAddRoom : MonoBehaviour
{
    private TestRoomTemplates templates;

    private void Start() {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<TestRoomTemplates>();
        templates.rooms.Add(this.gameObject);
    }
}
