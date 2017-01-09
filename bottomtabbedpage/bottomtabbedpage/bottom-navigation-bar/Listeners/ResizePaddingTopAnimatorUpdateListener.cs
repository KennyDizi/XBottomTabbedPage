using System;
using Android.Animation;
using Android.Views;

namespace BottomNavigationBar.Listeners
{
	/// <summary>
	/// 
	/// </summary>
	public class ResizePaddingTopAnimatorUpdateListener : Java.Lang.Object, ValueAnimator.IAnimatorUpdateListener
	{
		private readonly View _icon;

        /// <summary>
        /// ResizePaddingTopAnimatorUpdateListener
        /// </summary>
        /// <param name="icon"></param>
        public ResizePaddingTopAnimatorUpdateListener (View icon)
		{
			_icon = icon;
		}

        /// <summary>
        /// OnAnimationUpdate
        /// </summary>
        /// <param name="animation"></param>
        public void OnAnimationUpdate (ValueAnimator animation)
		{
			_icon.SetPadding (_icon.PaddingLeft, (Int32)animation.AnimatedValue, _icon.PaddingRight, _icon.PaddingBottom);
		}
	}
}

