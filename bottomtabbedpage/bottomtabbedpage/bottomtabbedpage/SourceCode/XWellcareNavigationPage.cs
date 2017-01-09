using Xamarin.Forms;

namespace bottomtabbedpage.SourceCode
{
    public class XWellcareNavigationPage : NavigationPage
    {
        public XWellcareNavigationPage(Page root) : base(root)
        {
            Init();
            Title = root.Title;
            Icon = root.Icon;
        }

        public XWellcareNavigationPage()
        {
            Init();
        }

        private void Init()
        {
            BarTextColor = Color.White;
        }
    }
}