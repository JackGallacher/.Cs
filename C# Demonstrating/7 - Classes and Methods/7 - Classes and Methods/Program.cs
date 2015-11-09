using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop7
{
    class Program
    {
        static void Main(string[] args)
        {
            int circle_radius = 0;
            int triangle_base = 0;
            int triangle_height = 0;
            int pyramid_height = 0;
            int pyramid_base = 0;

            Program x = new Program();

            Console.WriteLine("Enter circle radius: ");
            circle_radius = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Static area is: {0:f3}", static_circle_area(circle_radius));
            Console.WriteLine("Non-Static area is: {0:f3}", x.circle_area(circle_radius));

            Console.WriteLine("Enter triangle base: ");
            triangle_base = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter triangle height: ");
            triangle_height = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Static area is: {0:f1}", static_area_triangle(triangle_base, triangle_height));
            Console.WriteLine("Non-Static area is: {0:f1}", x.area_triangle(triangle_base, triangle_height));

            Console.Write("Enter pyramid height: ");
            pyramid_height = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter pyramid base: ");
            pyramid_base = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Static volume is: {0:f2}", static_pyramid_volume(static_area_triangle(pyramid_base, pyramid_height), pyramid_height));
            Console.WriteLine("Non-Static volume is: {0:f2}", x.pyramid_volume(x.area_triangle(pyramid_base, pyramid_height), pyramid_height));
            Console.ReadLine();
        }

        public static double static_circle_area(int radius)
        {
            return Math.PI * (radius * radius);
        }
        public static double static_area_triangle(int triangle_base, int height)
        {
            return (triangle_base / 2) * height;
        }
        public double circle_area(int radius)
        {
            return Math.PI * (radius * radius);
        }
        public double area_triangle(int triangle_base, int height)
        {
            return (triangle_base / 2) * height;
        }
        public static double static_pyramid_volume(double area, double height)
        {
            return (area * height) / 3;
        }
        public double pyramid_volume(double area, double height)
        {
            return (area * height) / 3;
        }

    }
}
