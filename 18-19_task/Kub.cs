namespace MyProgram;

using System;

[Serializable]
internal class Kub : Function
{
    public double a { get; set; }
    public double b { get; set; }
    public double c { get; set; }

    public Kub() { }
    public Kub(double a, double b, double c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }
    public Kub(Kub other)
    {
        if (other == null) throw new ArgumentNullException(nameof(other));
        this.a = other.a;
        this.b = other.b;
        this.c = other.c;
    }

    public override double getValue(double x) => a * x * x + b * x + c;
    public override double getCoefficientA() => a;
    public override string ToString() => $"y = {a}x² + {b}x + {c}";
}