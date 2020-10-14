using System.Numerics;
using System.Windows.Controls;
using System.Windows.Media;

namespace LEditor.Views.Graphics.Elements
{
    public class Ellipse : IGraphicsElement
    {
        private System.Windows.Shapes.Ellipse _impl;
        private bool _fill;
        private Vector2 _position;
        
        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }

        public Vector2 Size
        {
            get => new Vector2((float) _impl.Width, (float) _impl.Height); // This isnt ideal
            set
            {
                _impl.Width = value.X;
                _impl.Height = value.Y;
            }
        }

        public bool Fill
        {
            get => _fill;
            set => _fill = value;
        }
        
        public Color OutlineColour
        {
            set => _impl.Stroke = new SolidColorBrush(value);
        }
        
        public Color FillColour
        {
            set => _impl.Fill = new SolidColorBrush(value);
        }

        internal Ellipse()
        {
            _impl = new System.Windows.Shapes.Ellipse();
            _impl.StrokeThickness = 2;
        }

        public void DrawOnCanvas(Canvas canvas)
        {
            if (!_fill)
                _impl.Fill = null;
            
            Canvas.SetTop(_impl, _position.Y);
            Canvas.SetLeft(_impl, _position.X);

            canvas.Children.Add(_impl);
        }
    }
}