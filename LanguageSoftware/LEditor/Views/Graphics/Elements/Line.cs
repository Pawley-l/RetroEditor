using System.Numerics;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LEditor.Views.Graphics.Elements
{
    public class Line : IGraphicsElement
    {
        private System.Windows.Shapes.Line _impl;

        public Vector2 Position1
        {
            set
            {
                _impl.X1 = value.X;
                _impl.Y1 = value.Y;
            }
        }
        
        public Vector2 Position2
        {
            set
            {
                _impl.X2 = value.X;
                _impl.Y2 = value.Y;
            }
        }
        
        public Color Colour
        {
            set
            {
                _impl.Stroke = new SolidColorBrush(value);
            }
        }

        internal Line()
        {
            _impl = new System.Windows.Shapes.Line();
            _impl.StrokeThickness = 2;
        }

        public void DrawOnCanvas(Canvas canvas)
        {
            canvas.Children.Add(_impl);
        }
    }
}