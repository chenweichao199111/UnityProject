using System.Collections.Generic;
using UnityEngine;

namespace ZsCLJX
{
    public interface IDownRise
    {
        List<Vector3> DownRiseList
        {
            get;
        }

        Vector3 WorldPos
        {
            get;
        }

        void DownRise();
    }
}