using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FileHelpers;
using System.IO;

namespace DragAndDropTest
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isRectDragInProg;

        public MainWindow()
        {
            InitializeComponent();
            var engine = new FileHelperAsyncEngine<ShapeData>();
            if (File.Exists("ShapesPos.csv"))
            {
                using (engine.BeginReadFile("ShapesPos.csv"))
                {
                    foreach (ShapeData shape in engine)
                    {
                        foreach (var item in canvas.Children.OfType<Rectangle>())
                        {
                            if (shape.name == item.Name)
                            {
                                var converter = new System.Windows.Media.BrushConverter();
                                var brush = (Brush)converter.ConvertFromString(shape.color);
                                item.Fill = brush;
                                item.Width = shape.width;
                                item.Height = shape.height;
                                Canvas.SetLeft(item, shape.left);
                                Canvas.SetTop(item, shape.top);
                            }
                        }
                    }
                }
            }
        }
        private void rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rekt = sender as Rectangle;
            _isRectDragInProg = true;
            rekt.CaptureMouse();
        }
        private void rect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Rectangle rekt = sender as Rectangle;
            _isRectDragInProg = false;
            rekt.ReleaseMouseCapture();
            DataSave();
        }
        private void rect_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isRectDragInProg) return;
            Rectangle rekt = sender as Rectangle;
            var mousePos = e.GetPosition(canvas);

            double left = mousePos.X - (rect.ActualWidth / 2);
            double top = mousePos.Y - (rect.ActualHeight / 2);
            Canvas.SetLeft(rekt, left);
            Canvas.SetTop(rekt, top);
        }
        private void DataSave()
        {
            string text = null;
            System.IO.File.WriteAllText("ShapesPos.txt", text);
            var Datasaver = new FileHelperEngine<ShapeData>();
            var ScoreToSaves = Datasaver.ReadFile("ShapesPos.txt");
            Datasaver.WriteFile("ShapesPos.csv", ScoreToSaves);

            foreach (var item in canvas.Children.OfType<Rectangle>())
            {
                SaveTxt(item.Name, item.Fill.ToString(), item.Width, item.Height, Math.Round(Canvas.GetLeft(item)), Math.Round(Canvas.GetTop(item)));
                var Datasave = new FileHelperEngine<ShapeData>();
                var ScoreToSave = Datasave.ReadFile("ShapesPos.txt");
                Datasave.AppendToFile("ShapesPos.csv", ScoreToSave);
            }
        }
        public void SaveTxt(string name, string color,double Width, double Height, double left, double top)
        {
            string text = name +";"+ color + ";" + Width + ";" + Height + ";" + left + ";" + top;
            System.IO.File.WriteAllText("ShapesPos.txt", text);
        }
    }
}
