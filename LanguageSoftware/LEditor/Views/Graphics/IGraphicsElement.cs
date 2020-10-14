using System.Windows.Controls;

namespace LEditor.Views.Graphics
{
    /**
     * <summary>Interface for any graphics element, such as rectangle or line </summary>
     */
    public interface IGraphicsElement
    {
        public void DrawOnCanvas(Canvas canvas);
    }
}