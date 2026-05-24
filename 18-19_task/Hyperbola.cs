namespace MyProgram;

using System;

[Serializable]
internal class Hyperbola : Function
{
    public double a { get; set; }
    public double b { get; set; }

    public Hyperbola() { }
    public Hyperbola(double a, double b)
    {
        this.a = a;
        this.b = b;
    }
    public Hyperbola(Hyperbola other)
    {
    if (other == null) throw new ArgumentNullException(nameof(other));
    this.a = other.a;
    this.b = other.b;
    }

    public override double getValue(double x)
    {
        if (x == 0) throw new DivideByZeroException();
        return a / x + b;
    }
    public override double getCoefficientA() => a;
    public override string ToString() => $"y = {a}/x + {b}";
}