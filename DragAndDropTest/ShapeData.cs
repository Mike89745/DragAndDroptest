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
    [DelimitedRecord(";")]
    class ShapeData
    {
        public string name;
        public string color;
        public double width;
        public double height;
        public double left;
        public double top;
    }
}
