using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKiteAnimation {

    void ToTheEnd();

    void Play();

    void Update();

    bool Enable { get; set; }
}
