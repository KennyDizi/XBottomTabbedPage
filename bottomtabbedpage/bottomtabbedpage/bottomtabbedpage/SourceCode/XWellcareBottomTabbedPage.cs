using Xamarin.Forms;

namespace bottomtabbedpage.SourceCode
{
	public class XWellcareBottomTabbedPage : TabbedPage
	{
		public bool FixedMode { get; set; }

		public void RaiseCurrentPageChanged()
		{
			OnCurrentPageChanged();
		}
	}
}