using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization.NumberFormatting;
using Microsoft.UI.Windowing;
using WinRT.Interop;
using Windows.Graphics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Calculator
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private String input1 = "";
        private String input2 = "";
        private float answer;
        private String op = "";
        private int num;
       
        
        public MainWindow()
        {
            InitializeComponent();

            this.Title = "Calculator";

            //get the appwindow for the current window
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            AppWindow appWindow = AppWindow.GetFromWindowId(wndId);

            //set size of window(width, height)
            appWindow.Resize(new SizeInt32(1200, 1475));

           


        }

        private void OnNumberClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;//Sender tracks what button is pressed. It is what sent the request to use this method basically
            string value = button.Content.ToString();

            if(num != 2)
            {
                input1 += value;
                input1text.Text = input1;   // <-- update UI
            }
            else
            {
                input2 += value;
                input2text.Text = input2;
            }
            

            System.Diagnostics.Debug.WriteLine("Input 1: " + input1);
            System.Diagnostics.Debug.WriteLine("Input 2: " + input2);
        }

        private void OnOpclick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;//Sender tracks what button is pressed. It is what sent the request to use this method basically
            string value = button.Content.ToString();
            if(value == "=")
            {
                if (checkVals())
                {
                    operation(sender, e);
                    input1 = answer.ToString();
                    input2 = "";
                    op = "";
                    num = 1;
                    input1text.Text = input1;
                    input2text.Text = input2;
                    operatortext.Text = op;
                }
                
            }
            else if (value == "AC")
            {
                input1 = "";
                input2 = "";
                op = "";
                num = 1;
                input1text.Text = input1;
                input2text.Text = input2;
                operatortext.Text = op;
                
            }
            else if (value == "CE")
            {
                clearEntry();
            }
            else if(value == "+/-")
            {
                plusMinus();
            }
            else
            {
                op = value;
                num = 2;
                System.Diagnostics.Debug.WriteLine("Operation: " +op);
                operatortext.Text = op;
            }

        }

        private void operation(object sender, RoutedEventArgs e)
        {
            if(op == "+")
            {
                answer = float.Parse(input1) + float.Parse(input2);
                System.Diagnostics.Debug.WriteLine("Answer: " +answer);
            }
            else if(op == "-")
            {
                answer = float.Parse(input1) - float.Parse(input2);
                System.Diagnostics.Debug.WriteLine("Answer: " +answer);
            }
            else if(op == "×")
            {
                answer = float.Parse(input1) * float.Parse(input2);
                System.Diagnostics.Debug.WriteLine("Answer: " + answer);
            }
            else if(op == "÷")
            {
                answer = float.Parse(input1) / float.Parse(input2);
                System.Diagnostics.Debug.WriteLine("Answer: " + answer);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Error: operation not defined yet. :(");
            }
        }

        private void clearEntry()
        {
            if(num != 2)
            {
                input1 = "";
                input1text.Text = input1;
            }
            else
            {
                input2 = "";
                input2text.Text = input2;
            }
        }

        private void plusMinus()
        {
            float temp = 0;
            if(num != 2)
            {
                temp = float.Parse(input1);
                temp *= -1;
                input1 = temp.ToString();
                input1text.Text = input1;
            }
            else
            {
                temp = float.Parse(input2);
                temp *= -1;
                input2 = temp.ToString();
                input2text.Text = input2;
            }
        }

        private bool checkVals()
        {
            float value;
            if (float.TryParse(input1, out value) && float.TryParse(input2, out value) && op != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            var textBlock = new TextBlock
            {
                Text = input1text.Text,
                FontSize = 18,
                Padding = new Thickness(5)
            };

            Saved.Children.Insert(0, textBlock);
        }

        private void OnEraseClick(object sender, RoutedEventArgs e)
        {
            Saved.Children.Clear();
        }

    }
}
