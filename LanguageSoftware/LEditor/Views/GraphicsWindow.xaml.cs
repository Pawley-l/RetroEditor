using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows;
using System.Windows.Media;
using LEditor.Services;
using LEditor.ViewModels;
using LEditor.Views.Graphics;
using LLanguage.Exceptions;
using Rectangle = LEditor.Views.Graphics.Elements.Rectangle;
using Line = LEditor.Views.Graphics.Elements.Line;
using Ellipse = LEditor.Views.Graphics.Elements.Ellipse;
using LLanguage.Nodes.Types;

namespace LEditor.Views
{
    public partial class GraphicsWindow : Window
    {
        private GraphicsFactory _factory = new GraphicsFactory();
        private ScriptService _scriptService = new ScriptService();
        private EditorViewModel _currentContext;

        // Graphics Variables
        private bool _fill = true;
        private Color _outlineColour;
        private Color _fillColour;
        private Vector2 _pen_position;

        public GraphicsWindow(EditorViewModel context)
        {
            InitializeComponent();
            MapMethodsToService(_scriptService);

            SetContext(context);
        }

        public void Update()
        {
            ExecuteScript(_currentContext.File.FileModel.ContentsArray);
        }

        public void SetContext(EditorViewModel context)
        {
            this._currentContext = context;
        }
        
        public void ExecuteScript(IEnumerable<string> contents)
        {
            try
            {
                _scriptService.Reset();

                foreach (var line in contents)
                {
                    _scriptService.ParseLine(line.Replace("\t", ""));
                }
            
                _scriptService.Execute();
            }
            catch (UndeclaredSymbolException e)
            {
                Console.WriteLine(e.Line + ": " + e.Identifier);
            }
            catch (SyntaxErrorException e)
            {
                Console.WriteLine(e.Line + ": " + e.ErrorMessage);
            }
           
        }
        
        public void MapMethodsToService(ScriptService service)
        {
            service.MapMethod("Reset" , Reset);
            service.MapMethod("Clear" , Clear);
            service.MapMethod("MoveTo" , PenMoveTo);
            service.MapMethod("DrawTo" , PenDrawTo);
            service.MapMethod("SetOutlineColour" , SetOutlineColour);
            service.MapMethod("SetFillColour" , SetFillColour);
            service.MapMethod("ToggleFill" , ToggleFill);
            service.MapMethod("CreateRect" , CreateRect);
            service.MapMethod("CreateEllipse" , CreateEllipse);
            service.MapMethod("CreateLine" , CreateLine);
        }
        
        private IntNode PenMoveTo(List<IntNode> input)
        {
            _pen_position = new Vector2(input[0].Value, input[1].Value);
            
            return null;
        }
        
        private IntNode PenDrawTo(List<IntNode> input)
        {
            var new_pos = new Vector2(input[0].Value, input[1].Value);
            
            var line = _factory.Create(GraphicsType.Line) as Line;
            
            line.Colour = _fillColour;
            
            line.Position1 = _pen_position;
            line.Position2 = new_pos;
            
            line.DrawOnCanvas(GraphicsCanvas);

            _pen_position = new_pos;
            
            return null;
        }

        private IntNode Clear(List<IntNode> input)
        {
            GraphicsCanvas.Children.Clear();
            
            return null;
        }
        
        private IntNode Reset(List<IntNode> input)
        {
            _pen_position = Vector2.Zero;
            
            return null;
        }
        
        private IntNode ToggleFill(List<IntNode> input)
        {
            _fill = !_fill;
            
            return null;
        }
        
        private IntNode SetOutlineColour(List<IntNode> input)
        {
            _outlineColour = Color.FromRgb((byte) input[0].Value, (byte)input[1].Value,
                (byte)input[2].Value);
            
            return null;
        }
        
        private IntNode SetFillColour(List<IntNode> input)
        {
            _fillColour = Color.FromRgb((byte)input[0].Value, (byte)input[1].Value,
                (byte)input[2].Value);
            
            return null;
        }
        
        private IntNode CreateRect(List<IntNode> input)
        {
            if (!(_factory.Create(GraphicsType.Rectangle) is Rectangle rect)) return null;
            rect.Fill = true;
            rect.Position = new Vector2(input[0].Value, input[1].Value);
            rect.FillColour = _fillColour;
            rect.OutlineColour = _outlineColour;
            rect.Size = new Vector2(input[2].Value, input[3].Value);

            rect.DrawOnCanvas(GraphicsCanvas);

            return null;
        }

        private IntNode CreateLine(List<IntNode> input)
        {
            if (!(_factory.Create(GraphicsType.Line) is Line line)) return null;
            
            line.Colour = _fillColour;

            line.Position1 = new Vector2(input[0].Value, input[1].Value);
            line.Position2 = new Vector2(input[2].Value, input[3].Value);

            line.DrawOnCanvas(GraphicsCanvas);

            return null;
        }
        
        private IntNode CreateEllipse(List<IntNode> input)
        {
            if (!(_factory.Create(GraphicsType.Ellipse) is Ellipse ellipse)) return null;
            ellipse.Fill = true;
            ellipse.Position = new Vector2(input[0].Value, input[1].Value);
            ellipse.FillColour = _fillColour;
            ellipse.OutlineColour = _outlineColour;
            ellipse.Size = new Vector2(input[2].Value, input[3].Value);

            ellipse.DrawOnCanvas(GraphicsCanvas);

            return null;
        }
    }
}