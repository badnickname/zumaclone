using System;

namespace Share
{
    public interface IFactory
    {
        IFactory SetPrototype(ICloneable obj);
        IFactory SetCount(int count);
        IFactory SetTypes(int types);
        bool Next();
        object Instance();
    }
}