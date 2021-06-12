using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredDoor : Powerable
{
    public override void PowerUp()
    {
        print("Power up");
    }

    public override void PowerDown()
    {
        print("Power down");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
