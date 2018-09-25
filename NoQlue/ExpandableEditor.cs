using Xamarin.Forms;

namespace NoQlue
{
    public class ExpandableEditor : Editor
    {
        public ExpandableEditor()
        {
            TextChanged += OnTextChanged;
        }

        ~ExpandableEditor()
        {
            TextChanged -= OnTextChanged;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            InvalidateMeasure();
        }
    }
}
