using System;
using System.Numerics;


class Point //To define x and y coordinates of points
{
    double xPoint;
    double yPoint;

    public double XPoint
    {
        get { return xPoint; }
        set { xPoint = value; }
    }

    public double YPoint
    {
        get { return yPoint; }
        set { yPoint = value; }
    }

    public Point(double x, double y)
    {
        xPoint = x; 
        yPoint = y;
    }

}//Point
abstract class ImplicitGeometry
{
    public abstract bool IsInside(double xPoint, double yPoint);

    //public void Visualize()
    //{

    //}

}//ImplicitGeometry

class Circle : ImplicitGeometry
{

    private double radius;
    private double xCenter;
    private double yCenter;

    public double Radius
    { 
        get { return radius; } 
        set { radius = value; }    
    }

    public double XCenter 
    { 
        get { return xCenter; } 
        set {  xCenter = value; } 
    }

    public double YCenter 
    {  
        get { return yCenter; } 
        set { yCenter = value; } 
    }

    public Circle (double _radius, double _xCenter, double _yCenter)
    {
        radius = _radius;
        xCenter = _xCenter;
        yCenter = _yCenter;
    }

    public override bool IsInside(double xPoint, double yPoint)
    {
        double distanceToCenter = Math.Sqrt(Math.Pow((xPoint - xCenter),2) + Math.Pow((yPoint - yCenter),2));
        return distanceToCenter < radius;
    }

}//Circle

class Rectangle : ImplicitGeometry
{
    private double x1;
    private double x2;
    private double y1;
    private double y2;

    public double X1
    {
        get { return x1; }
        set { x1 = value; }
    }
    public double X2
    {
        get { return x2; }
        set { x2 = value; }
    }
    public double Y1
    {
        get { return y1; }
        set { y1 = value; }
    }
    public double Y2
    {
        get { return y2; }
        set { y2 = value; }
    }

    public Rectangle(double _x1, double _y1, double _x2, double _y2)
    {
        x1 = _x1;
        y1 = _y1;
        x2 = _x2;
        y2 = _y2;
    }

    public override bool IsInside(double xPoint, double yPoint)
    {
        double minX = Math.Min(x1, x2);
        double minY = Math.Min(y1, y2);
        double maxX = Math.Max(x1, x2);
        double maxY = Math.Max(y1, y2);

        return (xPoint >= minX && xPoint <= maxX && yPoint >= minY && yPoint <= maxY);
    }
}//Rectangle


abstract class Operation : ImplicitGeometry
{
    
}

class Union : Operation
{

    ImplicitGeometry operand1, operand2;
    public Union(ImplicitGeometry Operand1, ImplicitGeometry Operand2)
    {
        operand1 = Operand1;
        operand2 = Operand2;
    }

    public override bool IsInside(double xPoint, double yPoint)
    {
        return operand1.IsInside(xPoint, yPoint) || operand2.IsInside(xPoint,yPoint);
    }
}

class Program
{
    static void Main(string[] args)
    {

        Point point_1 = new Point(0, 0);
        Point point_2 = new Point(4, 6);
        Point point_3 = new Point(5, 7);
        Point[] points = { point_1, point_2, point_3 };

        Circle circle1 = new Circle(5, 0, 0);
        Rectangle rectangle1 = new Rectangle(1, 1, 5, 5);
        Union union = new Union(circle1, rectangle1);

        foreach ( Point point in points)
        {
            Console.Write($"Is point ({point.XPoint}, {point.YPoint}) inside the Circle: ");
            Console.WriteLine(circle1.IsInside(point.XPoint, point.YPoint));

            Console.Write($"Is point ({point.XPoint}, {point.YPoint}) inside the Rectangle: ");
            Console.WriteLine(rectangle1.IsInside(point.XPoint, point.YPoint));

            Console.Write($"Is point ({point.XPoint}, {point.YPoint}) inside the Union: ");
            Console.WriteLine(union.IsInside(point.XPoint, point.YPoint));
            Console.WriteLine("\n");
        }


    }
}//Program