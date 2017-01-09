/**
* C# port created by Nikola D. on 3/15/2016.
*
* Credit goes to Nikola Despotoski:
* https://github.com/NikolaDespotoski
*/

using System;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using Android.Support.V4.View.Animation;
using Android.Views.Animations;
using Android.OS;

namespace BottomNavigationBar.Scrollswetness
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="V"></typeparam>
    public class BottomNavigationBehavior<V> : VerticalScrollingBehavior<V>
        where V : View
    {
        private static readonly IInterpolator INTERPOLATOR = new LinearOutSlowInInterpolator();
        private readonly int _bottomNavHeight;
        private readonly int _defaultOffset;
        private readonly bool _isShy;
        private readonly bool _isTablet;

        private ViewPropertyAnimatorCompat _mTranslationAnimator;
        private const int SnackbarHeight = -1;
        private readonly IBottomNavigationWithSnackbar _withSnackBarImpl;
        private bool _scrollingEnabled = true;

        /// <summary>
        /// BottomNavigationBehavior
        /// </summary>
        /// <param name="bottomNavHeight"></param>
        /// <param name="defaultOffset"></param>
        /// <param name="shy"></param>
        /// <param name="tablet"></param>
        public BottomNavigationBehavior(int bottomNavHeight, int defaultOffset, bool shy, bool tablet)
        {
            _bottomNavHeight = bottomNavHeight;
            _defaultOffset = defaultOffset;
            _isShy = shy;
            _isTablet = tablet;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                _withSnackBarImpl = new LollipopBottomNavWithSnackBarImpl(_isTablet, _isShy, SnackbarHeight, _bottomNavHeight, _defaultOffset);
            else
                _withSnackBarImpl = new PreLollipopBottomNavWithSnackBarImpl(_isTablet, _isShy, SnackbarHeight, _bottomNavHeight, _defaultOffset);
        }

        #region implemented abstract members of VerticalScrollingBehavior

        private void HandleDirection(V child, ScrollDirection scrollDirection)
        {
            if (!_scrollingEnabled)
                return;
            var bottomBar = (child as BottomBar);
            if (bottomBar != null && scrollDirection == ScrollDirection.SCROLL_DIRECTION_DOWN && bottomBar.Hidden)
            {
                bottomBar.Show(true);
//                    AnimateOffset(child, _defaultOffset);
            }
            else if (bottomBar != null && scrollDirection == ScrollDirection.SCROLL_DIRECTION_UP && !bottomBar.Hidden)
            {
                bottomBar.Hide(true);
//                    AnimateOffset(child, _bottomNavHeight + _defaultOffset);
            }
        }

        private void AnimateOffset(V child, int offset)
        {
            EnsureOrCancelAnimator(child);
            _mTranslationAnimator.TranslationY(offset).Start();
        }

        private void EnsureOrCancelAnimator(V child)
        {
            if (_mTranslationAnimator == null)
            {
                _mTranslationAnimator = ViewCompat.Animate(child);
                _mTranslationAnimator.SetDuration(300);
                _mTranslationAnimator.SetInterpolator(INTERPOLATOR);
            }
            else
            {
                _mTranslationAnimator.Cancel();
            }
        }

        /// <summary>
        /// OnNestedDirectionFling
        /// </summary>
        /// <param name="coordinatorLayout"></param>
        /// <param name="child"></param>
        /// <param name="target"></param>
        /// <param name="velocityX"></param>
        /// <param name="velocityY"></param>
        /// <param name="scrollDirection"></param>
        /// <returns></returns>
        protected override bool OnNestedDirectionFling(CoordinatorLayout coordinatorLayout, V child, View target, float velocityX, float velocityY, ScrollDirection scrollDirection)
        {
            HandleDirection(child, scrollDirection);
            return true;
        }

        /// <inheritdoc />
        public override void OnNestedVerticalOverScroll(CoordinatorLayout coordinatorLayout, V child, ScrollDirection direction, int currentOverScroll, int totalOverScroll)
        {
            
        }

        /// <inheritdoc />
        public override void OnDirectionNestedPreScroll(CoordinatorLayout coordinatorLayout, V child, View target, int dx, int dy, int[] consumed, ScrollDirection scrollDirection)
        {
            HandleDirection(child, scrollDirection);
        }

        /// <summary>
        /// LayoutDependsOn
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        /// <param name="dependency"></param>
        /// <returns></returns>
        public override bool LayoutDependsOn(CoordinatorLayout parent, Java.Lang.Object child, View dependency)
        {
            _withSnackBarImpl.UpdateSnackbar(parent, dependency, (View)child);
            return dependency is Snackbar.SnackbarLayout;
        }

        /// <summary>
        /// OnDependentViewRemoved
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        /// <param name="dependency"></param>
        public override void OnDependentViewRemoved(CoordinatorLayout parent, Java.Lang.Object child, View dependency)
        {
            UpdateScrollingForSnackbar(dependency, true);
            base.OnDependentViewRemoved(parent, child, dependency);
        }

        private void UpdateScrollingForSnackbar(View dependency, bool enabled)
        {
            if (!_isTablet && dependency is Snackbar.SnackbarLayout)
                _scrollingEnabled = enabled;
        }


        /// <summary>
        /// OnDependentViewChanged
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        /// <param name="dependency"></param>
        /// <returns></returns>
        public override bool OnDependentViewChanged(CoordinatorLayout parent, Java.Lang.Object child, View dependency)
        {
            UpdateScrollingForSnackbar(dependency, false);
            return base.OnDependentViewChanged(parent, child, dependency);
        }

        #endregion

        private interface IBottomNavigationWithSnackbar
        {
            void UpdateSnackbar(CoordinatorLayout parent, View dependency, View child);
        }

        private class PreLollipopBottomNavWithSnackBarImpl : IBottomNavigationWithSnackbar
        {
            private readonly bool _isShy;
            private readonly bool _isTablet;
            private int _snackbarHeight;
            private readonly int _defaultOffset;
            private readonly int _bottomNavHeight;

            public PreLollipopBottomNavWithSnackBarImpl(bool shy, bool tablet, int snackbarHeight, int bottomNavHeight, int defaultOffset)
            {
                _isShy = shy;
                _isTablet = tablet;
                _snackbarHeight = snackbarHeight;
                _bottomNavHeight = bottomNavHeight;
                _defaultOffset = defaultOffset;
            }

            public void UpdateSnackbar(CoordinatorLayout parent, View dependency, View child)
            {
                if (!_isTablet && _isShy && dependency is Snackbar.SnackbarLayout)
                {
                    if (_snackbarHeight == -1)
                        _snackbarHeight = dependency.Height;
                    if (Math.Abs(ViewCompat.GetTranslationY(child)) > double.Epsilon) 
                        return;

                    var targetPadding = _bottomNavHeight + _snackbarHeight - _defaultOffset;

                    var layoutParams = (ViewGroup.MarginLayoutParams)dependency.LayoutParameters;
                    layoutParams.BottomMargin = targetPadding;

                    child.BringToFront();
                    child.Parent.RequestLayout();

                    if (Build.VERSION.SdkInt < BuildVersionCodes.Kitkat)
                        ((View)child.Parent).Invalidate();

                }
            }
        }

        private class LollipopBottomNavWithSnackBarImpl : IBottomNavigationWithSnackbar
        {
            private readonly bool _isShy;
            private readonly bool _isTablet;
            private int _snackbarHeight;
            private readonly int _defaultOffset;
            private readonly int _bottomNavHeight;

            public LollipopBottomNavWithSnackBarImpl(bool shy, bool tablet, int snackbarHeight, int bottomNavHeight, int defaultOffset)
            {
                _isShy = shy;
                _isTablet = tablet;
                _snackbarHeight = snackbarHeight;
                _bottomNavHeight = bottomNavHeight;
                _defaultOffset = defaultOffset;
            }

            public void UpdateSnackbar(CoordinatorLayout parent, View dependency, View child)
            {
                if (!_isTablet && _isShy && dependency is Snackbar.SnackbarLayout)
                {
                    if (_snackbarHeight == -1)
                        _snackbarHeight = dependency.Height;
					
                    if (Math.Abs(ViewCompat.GetTranslationY(child)) > double.Epsilon) 
                        return;

                    var targetPadding = _snackbarHeight + _bottomNavHeight - _defaultOffset;

                    dependency.SetPadding(
                        dependency.PaddingLeft, dependency.PaddingTop, dependency.PaddingRight, targetPadding
                    );
                }
            }
        }
    }
}

