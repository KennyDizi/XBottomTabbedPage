using System;
using Android.Animation;
using Android.Views;

namespace BottomNavigationBar.Listeners
{
    /// <summary>
    /// ResizeTabAnimatorUpdateListener
    /// </summary>
    public class ResizeTabAnimatorUpdateListener : Java.Lang.Object, ValueAnimator.IAnimatorUpdateListener
	{
		private readonly View _tab;
		private readonly ValueAnimator _animator;

        /// <summary>
        /// ResizeTabAnimatorUpdateListener
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="animator"></param>
        public ResizeTabAnimatorUpdateListener (View tab, ValueAnimator animator)
		{
			_tab = tab;
			_animator = animator;
		}

        /// <summary>
        /// OnAnimationUpdate
        /// </summary>
        /// <param name="animation"></param>
        public void OnAnimationUpdate (ValueAnimator animation)
		{
            ViewGroup.LayoutParams pars = _tab.LayoutParameters;
			if (pars == null) return;

            pars.Width = (int)Math.Round((float)_animator.AnimatedValue);
			_tab.LayoutParameters = pars;
		}
	}
}

