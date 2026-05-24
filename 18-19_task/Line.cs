namespace MyProgram;

using System;

[Serializable]
internal class Line : Function
{
    public double a { get; set; }
    public double b { get; set; }

    public Line() { }
    public Line(double a, double b)
    {
        this.a = a;
        this.b = b;
    }

        public Line(Line other)
    {
        if (other == null) throw new ArgumentNullException(nameof(other));
        this.a = other.a;
        this.b = other.b;
    }
    
    public override double getValue(double x) => a * x + b;
    public override double getCoefficientA() => a;
    public override string ToString() => $"y = {a}x + {b}";
}