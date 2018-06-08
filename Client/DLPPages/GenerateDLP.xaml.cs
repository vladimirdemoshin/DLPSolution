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

namespace Client.DLPPages
{
    public partial class GenerateDLP : Page
    {
        #region Enumerations
        private enum Algorithms { BabyStep = 0, RhoPollard, IndexCalculus };
        #endregion

        #region Properties

        #region Private
        private int currentAlgorithm;
        #endregion

        #endregion
        public GenerateDLP()
        {
            InitializeComponent();
        }

        #region Event Handlers
        private void OnSolveDLPBtnClick(object sender, RoutedEventArgs e)
        {
            int start = 1, finish = 1, count = 1;
            bool isValid = int.TryParse(bitsStartTxtBox.Text, out start) && int.TryParse(bitsFinishTxtBox.Text, out finish) && int.TryParse(countTxtBox.Text, out count);
            if (!isValid) return;
            if (currentAlgorithm == (int)Algorithms.BabyStep)
            {
                //Test.TestBabyStepGiantStepECDLP.SolveDLPRange(start, finish, count);
            }
            else if (currentAlgorithm == (int)Algorithms.RhoPollard)
            {
                //Test.TestRhoPollardECDLP.SolveDLPRange(start, finish, count);
            }
            else if (currentAlgorithm == (int)Algorithms.IndexCalculus)
            {
                int factorBaseSize = 1, linearEquatationsCount = 1;
                bool isValidFBTextBox = int.TryParse(factorBaseTxtBox.Text, out factorBaseSize);
                if (isValidFBTextBox) DLPAlgorithm.IndexCalculus.FactorBaseSize = factorBaseSize;
                bool isValidLinEqCountTextBox = int.TryParse(linearEquatationsCountTxtBox.Text, out linearEquatationsCount);
                if (isValidLinEqCountTextBox) DLPAlgorithm.IndexCalculus.LinearEquatationsCount = linearEquatationsCount;
                Test.TestIndexCalculusDLP.SolveDLPRange(start, finish, count);
            }
        }

        #endregion

        private void OnAlgorithmRadioBtnChecked(object sender, RoutedEventArgs e)
        {
            var radioBtn = sender as RadioButton;
            int row = Grid.GetRow(radioBtn);
            int column = Grid.GetColumn(radioBtn);
            var gridChildren = AlgorithmChooseGrid.Children;
            foreach (var view in gridChildren)
                if (Grid.GetRow(view as UIElement) == row - 1 && Grid.GetColumn(view as UIElement) == column)
                {
                    currentAlgorithm = (int)Enum.Parse(typeof(Algorithms), (view as Label).Content.ToString());
                    break;
                }
        }
    }
}
