using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Trainnamespace
{
    public class TrainPathway 
    {
        public UnityEvent onSomething = new ();
        public UnityEvent<int> onIntSomething = new ();
        public readonly List<Vector2> pathway = new ();

        public void DoSomething()
        {
            onSomething.AddListener(pathway.Clear);
            onIntSomething.AddListener(DO);
            onIntSomething.AddListener((numero) => {COUCOU(new TrainPathway());});
            onSomething.Invoke();
            onIntSomething.Invoke(3);
        }

        public void DO(int numero)
        {

        }

        public void COUCOU(TrainPathway pathway)
        {

        }
    }
}
