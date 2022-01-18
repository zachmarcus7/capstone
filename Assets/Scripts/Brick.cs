namespace MLBreakout
{
    using UnityEngine;

    /// <summary>
    /// This is just a simple class for accessing 
    /// the names of the brick tags easier.
    /// </summary>
    public class Brick
    {
        // these match up with the tags in the editor
        public string[] Colors = new string[]
        {
            "GreenBrick",
            "PurpleBrick",
            "BlueBrick",
            "YellowBrick",
            "RedBrick"
        };

        // this is to keep track of which layer has been reached
        // for applying new speeds with new layers
        public bool[] LayerReached = new bool[]
        {
            false,
            false,
            false,
            false,
            false
        };

        // this resets all the layers so their speed is reapplied
        // when the layer is reached again
        public void ResetLayers()
        {
            for (int i = 0; i < 5; i++)
            {
                LayerReached[i] = false;
            }
        }
    }
}
