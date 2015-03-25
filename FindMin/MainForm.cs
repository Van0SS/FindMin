using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindMin
{
    public partial class MainForm : Form
    {
        readonly List<Example> _examples = new List<Example>();
        private Example _curExample;

        private readonly int u = 4;
        private void InitExamples()
        {
            var SinAbsKX = new Example(ETypeFunction.SinAbsKX, -0.74, 0.5, 2);
            _examples.Add(SinAbsKX);

            var Pow4XPlusE = new Example(ETypeFunction.Pow4XPlusE, 0, 1);
            _examples.Add(Pow4XPlusE);

            var Pow2kXminusA = new Example(ETypeFunction.Pow2kXminusA, 0, 2, 4, 1);
            _examples.Add(Pow2kXminusA);

            foreach (var example in _examples)
            {
                comboBoxFunction.Items.Add(example.ReturnTypeFunction());
            }
            comboBoxFunction.SelectedIndex = 0;
        }

        public MainForm()
        {
            InitializeComponent();
            InitExamples();
        }



        private void comboBoxFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var example in _examples)
            {
                if (comboBoxFunction.SelectedItem.ToString() != example.ReturnTypeFunction())
                    continue;
                _curExample = example;
                textBoxA.Text = example.MinX.ToString(CultureInfo.InvariantCulture);
                textBoxB.Text = example.MaxX.ToString(CultureInfo.InvariantCulture);

                if (example.Parameters.Count() > 0)
                {
                    textBoxParamK.Enabled = true;
                    textBoxParamK.Text = example.Parameters[0].ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    textBoxParamK.Enabled = false;
                }
                if (example.Parameters.Count() > 1)
                {
                    textBoxParamA.Enabled = true;
                    textBoxParamA.Text = example.Parameters[1].ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    textBoxParamA.Enabled = false;
                }
            }
        }

        private void DoubleRestrict(object sender, KeyPressEventArgs e)
        {
            if (IsNumberOrBackspace(e) || e.KeyChar == '.' || e.KeyChar == '-')
                return;
            e.Handled = true;
        }

        private void PositiveDoubleRestrict(object sender, KeyPressEventArgs e)
        {
            if (IsNumberOrBackspace(e) || e.KeyChar == '.')
                return;
            e.Handled = true;
        }

        private bool IsNumberOrBackspace(KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (char) Keys.Back) //8 - backspace
                return true;
            return false;
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
#if !DEBUG         
            try
            {
#endif
                    double error;
                    try
                    {
                        _curExample.MinX = Double.Parse(textBoxA.Text, CultureInfo.InvariantCulture);
                        _curExample.MaxX = Double.Parse(textBoxB.Text, CultureInfo.InvariantCulture);
                        if (_curExample.Parameters.Count() > 0)
                        {
                            _curExample.Parameters[0] = Double.Parse(textBoxParamK.Text, CultureInfo.InvariantCulture);
                        }
                        if (_curExample.Parameters.Count() > 1)
                        {
                            _curExample.Parameters[1] = Double.Parse(textBoxParamK.Text, CultureInfo.InvariantCulture);
                        }
                        
                        error = Double.Parse(textBoxError.Text, CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {
                        throw new Exception("Wrong parameters");
                    }


                    if (_curExample.MinX > _curExample.MaxX)
                        throw new Exception("Min > Max");

                    int interation;
                    var x = MethodFibonacci.FindFuncMinimum(_curExample, error, out interation);
                    var fX = _curExample.ReturnY(x);
                    textBoxOutput.Text = String.Format("{2} iteration" + Environment.NewLine +
                                                       "x={0}" + Environment.NewLine +
                                                       "f(x)={1}", x, fX, interation);
                
                    chartOutput.Series[0].Points.Clear();
                    for (double d = _curExample.MinX; d < _curExample.MaxX; d += 0.01)
                    {
                        chartOutput.Series[0].Points.AddXY(d, _curExample.ReturnY(d));
                    }
                    chartOutput.Series[1].Points.Clear();
                    chartOutput.Series[1].Points.AddXY(x, fX);
#if !DEBUG            
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
#endif            
        }


    }
}
