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

namespace Client.ECDLPPages
{
    public partial class GenerateECDLP : Page
    {
        public GenerateECDLP()
        {
            InitializeComponent();
        }

        #region Enumerations
        private enum Algorithms { BabyStep = 0, RhoPollard };
        #endregion

        #region Properties

        #region Private
        private int currentAlgorithm;
        #endregion

        #endregion


        #region Event Handlers
        private void OnGenerateDataBtnClick(object sender, RoutedEventArgs e)
        {
            int start = 1, finish = 1, count = 1;
            bool isValid = int.TryParse(bitsStartTxtBox.Text, out start) && int.TryParse(bitsFinishTxtBox.Text, out finish) && int.TryParse(countTxtBox.Text, out count);
            if (!isValid) return;
            if(currentAlgorithm == (int)Algorithms.BabyStep)
            {
                Test.TestBabyStepGiantStepECDLP.SolveDLPRange(start, finish, count);
            }
            else if (currentAlgorithm == (int)Algorithms.RhoPollard)
            {
                Test.TestRhoPollardECDLP.SolveDLPRange(start, finish, count);
            }
            
        }

        private void BabyStepGiantStepRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            currentAlgorithm = (int)Algorithms.BabyStep;
        }

        private void PollardRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            currentAlgorithm = (int)Algorithms.RhoPollard;
        }
        #endregion
    }
}
