using Syncfusion.XForms.Accordion;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AccordionXamarin
{
    public class Behavior : Behavior<StackLayout>
    {
        StackLayout stackLayout;
        SfAccordion accordion;
        Button button;
        bool flag;
        protected override void OnAttachedTo(StackLayout bindable)
        {
            stackLayout = bindable;
            stackLayout.ChildAdded += StackLayout_ChildAdded; ;
        }
        private void StackLayout_ChildAdded(object sender, ElementEventArgs e)
        {
            if (e.Element is SfAccordion)
            {
                //Method 1 : Get SfAccordion reference using StackLayout.ChildAdded Event
                accordion = e.Element as SfAccordion;
            }
            else if (e.Element is Button)
            {
                button = e.Element as Button;
                button.Clicked += Button_Clicked;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //Method 2 : Get SfAccordion reference using FindByName
            accordion = stackLayout.FindByName<SfAccordion>("accordion");
            if(!flag)
            {
                App.Current.MainPage.DisplayAlert("Information", "Accordion instance is obtained and Header Icon Position is changed", "Ok");
                flag = true;
            }
            if (accordion.HeaderIconPosition == Syncfusion.XForms.Expander.IconPosition.Start)
            {
                accordion.HeaderIconPosition = Syncfusion.XForms.Expander.IconPosition.End;
            }
            else
            {
                accordion.HeaderIconPosition = Syncfusion.XForms.Expander.IconPosition.Start;
            }
        }

        protected override void OnDetachingFrom(StackLayout bindable)
        {
            button.Clicked -= Button_Clicked;
            stackLayout.ChildAdded -= StackLayout_ChildAdded;
            accordion = null;
            button = null;
            stackLayout = null;
            base.OnDetachingFrom(bindable);
        }
    }
}
