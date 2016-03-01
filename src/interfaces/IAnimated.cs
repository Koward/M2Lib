using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using m2lib_csharp.m2;

namespace m2lib_csharp.interfaces
{
    public interface IAnimated : IReferencer
    {
        void SetSequences(IReadOnlyList<Sequence> sequences);
    }
}