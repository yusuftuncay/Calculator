using System.Data;

namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        // Property
        private string currentExpression = ""; // Store the current expression

        // Main
        public MainPage()
        {
            InitializeComponent();
        }

        // Clicked Method
        private void ButtonNumberClicked(object sender, EventArgs e)
        {
            // Store number press
            Button button = (Button)sender;
            string buttonText = button.Text;

            // Append the clicked number to the display
            string currentText = LabelOutput.Text;
            if (currentText == "0")
                LabelOutput.Text = buttonText;
            else
                LabelOutput.Text += buttonText;

            // Append the clicked number to the current expression
            currentExpression += buttonText;

            LengthCheck();

        }
        private void ButtonOperatorClicked(object sender, EventArgs e)
        {
            // Store operator press
            Button operatorButton = (Button)sender;
            string operatorText = operatorButton.Text;

            // Check if last pressed button was an operator
            if (currentExpression.EndsWith(" "))
            {
                currentExpression = currentExpression.Remove(currentExpression.Length - 3);
            }

            // Append the operator to the current expression
            currentExpression += " " + operatorText + " ";

            // Update the display
            LabelOutput.Text = currentExpression;

            LengthCheck();
        }
        private async void ButtonEqualsClicked(object sender, EventArgs e)
        {
            try
            {
                // Evaluate the current expression
                DataTable table = new();
                string result = table.Compute(currentExpression, null).ToString();

                // Display the result
                LabelOutput.Text = result;

                // Update the current expression with the result
                currentExpression = result;

                LengthCheck();
            } catch (Exception ex)
            {
                // Handle errors
                LabelOutput.Text = "Error";
                currentExpression = "";

                // Log or display the error message
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
        private void ButtonClearClicked(object sender, EventArgs e)
        {
            // Clear everything
            LabelOutput.Text = "0";
            currentExpression = "";
        }
        private async void LengthCheck()
        {
            if (currentExpression.Length >= 13)
            {
                // Clear everything
                LabelOutput.Text = "0";
                currentExpression = "";

                await DisplayAlert("Error", "Max Length", "OK");
                return;
            }
        }
    }
}