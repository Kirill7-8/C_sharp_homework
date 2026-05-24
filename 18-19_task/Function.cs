namespace MyProgram;

using System;
using System.Diagnostics.Contracts;

[Serializable]
public abstract class Function : IComparable<Function>
{

    public abstract double getCoefficientA();
    public abstract double getValue(double x);



 
    public int CompareTo(Function? other)
    {
        if (other is null) return 1;
        return getCoefficientA().CompareTo(other.getCoefficientA());
    }

}