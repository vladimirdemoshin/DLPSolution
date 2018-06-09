
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Numerics;
using Utility;
namespace Client.DLPPages
{
    public partial class TestDLP : Page
    {
       #region Enumerations 
        private enum Algorithms { BabyStep = 0, RhoPollard, IndexCalculus };
        #endregion
        #region Properties
        private int currentAlgorithm;
        #endregion
        #region Methods
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
        public TestDLP()
        {
            InitializeComponent();
            InitializeProperties();
        }
        #region Event Handlers
        private void OnGeneratePBtnClick(object sender, RoutedEventArgs e)
        {
            int bitsLength = 1;
            bool isValid = int.TryParse(bitsLengthTxtBox.Text, out bitsLength);
            if(isValid)
            {
                var rand = new BigIntegerRandom();
                BigInteger start = BigInteger.Pow(2, bitsLength - 1);
                BigInteger finish = BigInteger.Pow(2, bitsLength) - 1;
                BigInteger range = finish - start + 1;
                BigInteger p = 1;
                do
                {
                    var shift = rand.Next(0, range);
                    p = start + shift;
                }
                while (BigIntegerPrimeTest.MillerRabinTest(p));
                pTxtBox.Text = p.ToString();
            }
        }
        private void OnGenerateGBtnClick(object sender, RoutedEventArgs e)
        {
            int p = 1;
            bool isValid = int.TryParse(pTxtBox.Text, out p);
            if (isValid)
            {
                var g = BigIntegerExtension.PrimitiveRoot(p);
                gTxtBox.Text = g.ToString();
            }
        }
        private void OnGenerateXBtnClick(object sender, RoutedEventArgs e)
        {
            int p = 1, g = 1;
            bool isValid = int.TryParse(pTxtBox.Text, out p) && int.TryParse(gTxtBox.Text, out g);
            if (isValid)
            {
                var rand = new BigIntegerRandom();
                var x = rand.Next(0, p-1);
                var h = BigInteger.ModPow(g, x, p);
                xTxtBox.Text = x.ToString();
                hTxtBox.Text = h.ToString();
            }
        }
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
                        int fbSize = 1, countOfLinEq = 1;
                        bool isValidForIndexCalculus = int.TryParse(factorBaseTxtBox.Text, out fbSize) && int.TryParse(linearEquatationsCountTxtBox.Text, out countOfLinEq);
                        if(isValidForIndexCalculus)
                        {
                            DLPAlgorithm.IndexCalculus.FactorBaseSize = fbSize;
                            DLPAlgorithm.IndexCalculus.LinearEquatationsCount = countOfLinEq;
                        }
                        x = Test.TestIndexCalculusDLP.SolveDLP(input.g, input.h, input.p);
                        elapsedTime = Test.TestIndexCalculusDLP.ElapsedTime;
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
        private void OnGetInputFromFileBtnClick(object sender, RoutedEventArgs e)
        {
            int bitsLength = 1;
            bool isValid = int.TryParse(bitsLengthTxtBox.Text, out bitsLength);
            if(isValid)
            {
                var primes = FileUtility.ReadArrayFromFile(String.Format(@"C:\Utility\Primes\primes{0}bits.txt", bitsLength));
                var generators = FileUtility.ReadArrayFromFile(String.Format(@"C:\Utility\DLPUtility\Generators\generators{0}bits.txt", bitsLength));
                var logs = FileUtility.ReadArrayFromFile(String.Format(@"C:\Utility\DLPUtility\Logs\x{0}bits.txt", bitsLength));
                var values = FileUtility.ReadArrayFromFile(String.Format(@"C:\Utility\DLPUtility\Values\h{0}bits.txt", bitsLength));

                var p = primes[0];
                var g = generators[0];
                var x = logs[0];
                var h = values[0];

                pTxtBox.Text = p.ToString();
                gTxtBox.Text = g.ToString();
                xTxtBox.Text = x.ToString();
                hTxtBox.Text = h.ToString();
            } 
        }
        #endregion
    }
}
