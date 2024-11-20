using System.Collections;
using UnityEngine;

public abstract class Event : MonoBehaviour
{

    public GameObject Panel;
    public abstract IEnumerator StartEvent();
}
