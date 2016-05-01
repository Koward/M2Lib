using System.Collections.Generic;
using M2Lib.m2;

namespace M2Lib.interfaces
{
    public interface IAnimated : IReferencer
    {
        void SetSequences(IReadOnlyList<M2Sequence> sequences);
    }
}