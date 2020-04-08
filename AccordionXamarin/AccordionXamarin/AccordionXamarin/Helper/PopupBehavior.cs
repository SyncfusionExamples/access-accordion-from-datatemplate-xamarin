using Syncfusion.XForms.PopupLayout;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AccordionXamarin
{
    public class PopupBehavior : Behavior<SfPopupLayout>
    {
        SfPopupLayout popupLayout;
        Button button;
        Grid grid;
        protected override void OnAttachedTo(SfPopupLayout bindable)
        {
            popupLayout = bindable;
            popupLayout.ChildAdded += Bindable_ChildAdded;
            base.OnAttachedTo(bindable);
        }

        private void Bindable_ChildAdded(object sender, ElementEventArgs e)
        {
            if (e.Element is Grid)
            {
                grid = e.Element as Grid;
                button = grid.Children[0] as Button;
                button.Clicked += Button_Clicked;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            popupLayout.Show();
        }
        protected override void OnDetachingFrom(SfPopupLayout bindable)
        {
            button.Clicked -= Button_Clicked;
            popupLayout.ChildAdded -= Bindable_ChildAdded;
            button = null;
            grid = null;
            popupLayout = null;
            base.OnDetachingFrom(bindable);
        }
    }
}
