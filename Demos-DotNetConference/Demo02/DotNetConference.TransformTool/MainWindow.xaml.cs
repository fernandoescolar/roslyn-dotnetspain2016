using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
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

namespace DotNetConference.TransformTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            input.Text = @"           
        class Program
        {
            static void Main()
            {
                int i = 0;
                int sum = 0;

                while (i < 10)
                {
                    sum += i;
                    i++;
                }

                System.Console.WriteLine(sum);
            }
        }";

        }

        public static string Transform(string sourceText)
        {
            var sourceTree = SyntaxFactory.ParseSyntaxTree(sourceText);
            CustomVisitor visitor = new CustomVisitor(sourceTree);
            return visitor.Visit(sourceTree.GetRoot()).ToFullString();
        }

        private void transform_Click(object sender, RoutedEventArgs e)
        {

            output.Text = Transform(input.Text);

        }
    }
}
