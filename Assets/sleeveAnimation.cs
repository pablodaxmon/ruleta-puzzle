using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sleeveAnimation : MonoBehaviour
{
    public void sleeveAnimationEnded()
    {
        GameObject.Destroy(this.gameObject);
    }
}
