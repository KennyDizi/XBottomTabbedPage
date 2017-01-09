/**
* C# port created by Nikola D. on 11/22/2015.
*
* Credit goes to Nikola Despotoski:
* https://github.com/NikolaDespotoski
*/

using Android.Support.Design.Widget;
using Android.Content;
using Android.Util;
using Android.Views;

namespace BottomNavigationBar.Scrollswetness
{
    /// <summary>
    /// 
    /// </summary>
    public enum ScrollDirection
    {
        SCROLL_DIRECTION_UP = 1,
        SCROLL_DIRECTION_DOWN = -1,
        SCROLL_NONE = 0
    }

    /// <summary>
    /// VerticalScrollingBehavior
    /// </summary>
    /// <typeparam name="TV"></typeparam>
    public abstract class VerticalScrollingBehavior<TV> : CoordinatorLayout.Behavior
        where TV : View
    {
		private int _totalDyUnconsumed;
		private int _totalDy;

		private ScrollDirection _overScrollDirection = ScrollDirection.SCROLL_NONE;
		private ScrollDirection _scrollDirection = ScrollDirection.SCROLL_NONE;

        /// <summary>
        /// VerticalScrollingBehavior
        /// </summary>
        protected VerticalScrollingBehavior()
        {
            
        }

        /// <summary>
        /// VerticalScrollingBehavior
        /// </summary>
        /// <param name="context"></param>
        /// <param name="attrs"></param>
        protected VerticalScrollingBehavior(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }

        /// <summary>
        /// Gets the over scroll direction.
        /// </summary>
        /// <returns>SCROLL_DIRECTION_UP, CROLL_DIRECTION_DOWN, SCROLL_NONE</returns>
        public ScrollDirection GetOverScrollDirection()
        {
            return _overScrollDirection;
        }

        /// <summary>
        /// Gets the scroll direction.
        /// </summary>
        /// <returns>SCROLL_DIRECTION_UP, SCROLL_DIRECTION_DOWN, SCROLL_NONE</returns>
        public ScrollDirection GetScrollDirection()
        {
            return _scrollDirection;
        }

        #region abstract methods

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
        protected abstract bool OnNestedDirectionFling(CoordinatorLayout coordinatorLayout, TV child, View target, float velocityX, float velocityY, ScrollDirection scrollDirection);

        /// <summary>
        /// OnNestedVerticalOverScroll
        /// </summary>
        /// <param name="coordinatorLayout"></param>
        /// <param name="child"></param>
        /// <param name="direction"></param>
        /// <param name="currentOverScroll"></param>
        /// <param name="totalOverScroll"></param>
        public abstract void OnNestedVerticalOverScroll(CoordinatorLayout coordinatorLayout, TV child, ScrollDirection direction, int currentOverScroll, int totalOverScroll);

        /// <summary>
        /// OnDirectionNestedPreScroll
        /// </summary>
        /// <param name="coordinatorLayout"></param>
        /// <param name="child"></param>
        /// <param name="target"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="consumed"></param>
        /// <param name="scrollDirection"></param>
        public abstract void OnDirectionNestedPreScroll(CoordinatorLayout coordinatorLayout, TV child, View target, int dx, int dy, int[] consumed, ScrollDirection scrollDirection);

        #endregion

        #region overrides

        /// <summary>
        /// OnStartNestedScroll
        /// </summary>
        /// <param name="coordinatorLayout"></param>
        /// <param name="child"></param>
        /// <param name="directTargetChild"></param>
        /// <param name="target"></param>
        /// <param name="nestedScrollAxes"></param>
        /// <returns></returns>
        public override bool OnStartNestedScroll(CoordinatorLayout coordinatorLayout, Java.Lang.Object child, View directTargetChild, View target, int nestedScrollAxes)
        {
            return (nestedScrollAxes & (int)ScrollAxis.Vertical) != 0;
        }

        /// <summary>
        /// OnNestedScroll
        /// </summary>
        /// <param name="coordinatorLayout"></param>
        /// <param name="child"></param>
        /// <param name="target"></param>
        /// <param name="dxConsumed"></param>
        /// <param name="dyConsumed"></param>
        /// <param name="dxUnconsumed"></param>
        /// <param name="dyUnconsumed"></param>
        public override void OnNestedScroll(CoordinatorLayout coordinatorLayout, Java.Lang.Object child, View target, int dxConsumed, int dyConsumed, int dxUnconsumed, int dyUnconsumed)
        {
            base.OnNestedScroll(coordinatorLayout, child, target, dxConsumed, dyConsumed, dxUnconsumed, dyUnconsumed);
            if (dyUnconsumed > 0 && _totalDyUnconsumed < 0)
            {
                _totalDyUnconsumed = 0;
                _overScrollDirection = ScrollDirection.SCROLL_DIRECTION_UP;
            }
            else if (dyUnconsumed < 0 && _totalDyUnconsumed > 0)
            {
                _totalDyUnconsumed = 0;
                _overScrollDirection = ScrollDirection.SCROLL_DIRECTION_DOWN;
            }
            _totalDyUnconsumed += dyUnconsumed;
            OnNestedVerticalOverScroll(coordinatorLayout, (TV)child, _overScrollDirection, dyConsumed, _totalDyUnconsumed);
        }

        /// <summary>
        /// OnNestedPreScroll
        /// </summary>
        /// <param name="coordinatorLayout"></param>
        /// <param name="child"></param>
        /// <param name="target"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="consumed"></param>
        public override void OnNestedPreScroll(CoordinatorLayout coordinatorLayout, Java.Lang.Object child, View target, int dx, int dy, int[] consumed)
        {
            base.OnNestedPreScroll(coordinatorLayout, child, target, dx, dy, consumed);
            if (dy > 0 && _totalDy < 0)
            {
                _totalDy = 0;
                _scrollDirection = ScrollDirection.SCROLL_DIRECTION_UP;
            }
            else if (dy < 0 && _totalDy >= 0)
            {
                _totalDy = 0;
                _scrollDirection = ScrollDirection.SCROLL_DIRECTION_DOWN;
            }
            _totalDy += dy;
            OnDirectionNestedPreScroll(coordinatorLayout, (TV)child, target, dx, dy, consumed, _scrollDirection);
        }

        /// <summary>
        /// OnNestedFling
        /// </summary>
        /// <param name="coordinatorLayout"></param>
        /// <param name="child"></param>
        /// <param name="target"></param>
        /// <param name="velocityX"></param>
        /// <param name="velocityY"></param>
        /// <param name="consumed"></param>
        /// <returns></returns>
        public override bool OnNestedFling(CoordinatorLayout coordinatorLayout, Java.Lang.Object child, View target, float velocityX, float velocityY, bool consumed)
        {
            base.OnNestedFling(coordinatorLayout, child, target, velocityX, velocityY, consumed);
            _scrollDirection = velocityY > 0 ? ScrollDirection.SCROLL_DIRECTION_UP : ScrollDirection.SCROLL_DIRECTION_DOWN;
            return OnNestedDirectionFling(coordinatorLayout, (TV)child, target, velocityX, velocityY, _scrollDirection);
        }

        #endregion
    }
}

