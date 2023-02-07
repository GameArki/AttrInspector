using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameArki.AttrInspector.Sample {

    public class Sample_AttrInspectorApp : MonoBehaviour {

        [ATSlider(nameof(myMin), nameof(myMax))]
        public float mySlider;

        float myMin = 2;
        float myMax = 5;

    }

}
