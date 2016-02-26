using System;
using System.Windows.Controls;
using System.Windows.Input;
namespace DotNetConference.MarranadaWPF
{
    public class MyTextBox : TextBox
    {
        public MyTextBox()
        {
            //Defaults to 4
            TabSize = 4;
        }

        public int TabSize
        {
            get;
            set;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                String tab = new String(' ', TabSize);
                int caretPosition = base.CaretIndex;
                base.Text = base.Text.Insert(caretPosition, tab);
                base.CaretIndex = caretPosition + TabSize + 1;
                e.Handled = true;
            }
        }
    }
}