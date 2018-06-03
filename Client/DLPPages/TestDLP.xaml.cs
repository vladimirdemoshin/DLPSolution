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

using System.Numerics;

namespace Client.DLPPages
{
    public partial class TestDLP : Page
    {
       #region Enumerations 
        private enum Algorithms { BabyStep = 0, RhoPollard, IndexCalculus };
        #endregion
        #region Properties

        #region Private
        private int currentAlgorithm;
        #endregion

        #endregion

        #region Methods

        #region Private
        private void InitializeProperties()
        {
            currentAlgorithm = 0;
        }
        private void ShowMessage()
        {
            MessageBox.Show("Input all data before!");
        }
        private bool GetValidatedInput(ref Utility.DLPInput input)
        {
            BigInteger p = 1, g = 1, h = 1;
            var isValid = BigInteger.TryParse(pTxtBox.Text, out p) && BigInteger.TryParse(gTxtBox.Text, out g) && BigInteger.TryParse(hTxtBox.Text, out h);
            if (!isValid)
                return false;
            else
            {
                input.p = p;
                input.g = g;
                input.h = h;
                input.order = p - 1;
                return true;
            }
        }
        #endregion

        #endregion
        public TestDLP()
        {
            InitializeComponent();
            InitializeProperties();
        }

        #region Event Handlers
        private void OnSolveDLPBtnClick(object sender, RoutedEventArgs e)
        {
            var input = new Utility.DLPInput();
            bool isValid = GetValidatedInput(ref input);
            if (!isValid)
            {
                ShowMessage();
                return;
            }
            else
            {
                BigInteger x = -1;
                string elapsedTime = "";
                switch (currentAlgorithm)
                {
                    case (int)Algorithms.BabyStep:
                        x = Test.TestBabyStepGiantStepDLP.SolveDLP(input.g, input.h, input.p);
                        elapsedTime = Test.TestBabyStepGiantStepDLP.ElapsedTime;
                        break;
                    case (int)Algorithms.RhoPollard:
                        x = Test.TestRhoPollardDLP.SolveDLP(input.g, input.h, input.p);
                        elapsedTime = Test.TestRhoPollardDLP.ElapsedTime;
                        break;
                    case (int)Algorithms.IndexCalculus:

                        break;
                    default:
                        x = -1;
                        break;
                }
                logTxtBox.Text = x.ToString();
                timeTxtBox.Text = elapsedTime;
            } 
        }
        private void OnAlgorithmRadioBtnChecked(object sender, RoutedEventArgs e)
        {
            var radioBtn = sender as RadioButton;
            int row = Grid.GetRow(radioBtn); 
            int column = Grid.GetColumn(radioBtn);
            var gridChildren = AlgorithmChooseGrid.Children;
            foreach(var view in gridChildren)
                if (Grid.GetRow(view as UIElement) == row - 1 && Grid.GetColumn(view as UIElement) == column)
                {
                    currentAlgorithm = (int)Enum.Parse(typeof(Algorithms), (view as Label).Content.ToString());
                    break;
                }
        }

        private void OnGenerateBtnClick(object sender, RoutedEventArgs e)
        {

        }
        
        #endregion
    }
}
