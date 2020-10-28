using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Shared {

    /**
     * A collection of audio clips.
     */
    [CreateAssetMenu]
    public class AudioTheme : ScriptableObject {
        public AudioEvent[] events = {
            new AudioEvent { name = "Main Music" },
            new AudioEvent { name = "Game Music" },
            new AudioEvent { name = "Count Down" },
            new AudioEvent { name = "Player Win" },
            new AudioEvent { name = "Player Jump" },
            new AudioEvent { name = "Player Walk" },
            new AudioEvent { name = "Player Run" },
            new AudioEvent { name = "Player Hurt" },
            new AudioEvent { name = "Player Die" },
            new AudioEvent { name = "Player Fire" },
            new AudioEvent { name = "Monster Hurt" },
            new AudioEvent { name = "Monster Die" },
            new AudioEvent { name = "Activate Extralife" },
            new AudioEvent { name = "Activate Flower" },
            new AudioEvent { name = "Activate Mushroom" },
            new AudioEvent { name = "Activate Star" },
            new AudioEvent { name = "Collect Coin" },
            new AudioEvent { name = "Collect Extralife" },
            new AudioEvent { name = "Collect Flower" },
            new AudioEvent { name = "Collect Mushroom" },
            new AudioEvent { name = "Collect Star" },
            new AudioEvent { name = "Collect Points" }
        };
    }
}
