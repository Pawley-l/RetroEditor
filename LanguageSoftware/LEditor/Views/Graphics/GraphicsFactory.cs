using System;
using System.Diagnostics;
using LEditor.Views.Graphics.Elements;

namespace LEditor.Views.Graphics
{
    public class GraphicsFactory
    {
        /**
         * <summary>Creates a graphics element of a given type</summary>
         * <param name="type">Type of graphics element to be made</param>
         */
        public IGraphicsElement Create(GraphicsType type)
        {
            return CreateElement(type);
        }

        /**
         * 
         */
        private static IGraphicsElement CreateElement(GraphicsType type)
        {
            return type switch
            {
                GraphicsType.Ellipse => new Ellipse(),
                GraphicsType.Line => new Line(),
                GraphicsType.Polygon => new Polygon(),
                GraphicsType.Rectangle => new Rectangle(),
                GraphicsType.Triangle => new Triangle(),
                _ => throw new Exception("Error creating graphics element")
            };
        }
    }
}