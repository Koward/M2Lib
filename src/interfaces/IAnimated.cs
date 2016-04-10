using System.Collections.Generic;
using m2lib_csharp.m2;

namespace m2lib_csharp.interfaces
{
    public interface IAnimated : IReferencer
    {
        void SetSequences(IReadOnlyList<M2Sequence> sequences);
    }
}