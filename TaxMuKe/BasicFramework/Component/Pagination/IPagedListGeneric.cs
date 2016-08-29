using System.Collections.Generic;

namespace BasicFramework.Component
{

    public interface IPagedList<out T> : IPagedList, IEnumerable<T>
    {

        T this[int index] { get; }

        int Count { get; }

        IPagedList GetMetaData();
    }
}