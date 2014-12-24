using System.Collections.Generic;
using Xamarin.Forms;

namespace Freehand
{
    public class Stroke {
        public Color Color { get; set; }
        public int Width { get; set; }
        public List<Point> Points { get; set; }
        public Stroke(Color color,int width) {
            Color = color;
            Width = width;
            Points = new List<Point>();
        }

        public void Add(Point point) {
            Points.Add(point);
        }
    }
}

