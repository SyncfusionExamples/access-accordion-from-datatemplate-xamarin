# How to access a named Accordion inside a XAML DataTemplate in Xamarin.Forms (SfAccordion)?

You can access the named [SfAccordion](https://help.syncfusion.com/xamarin/accordion/getting-started?) defined inside [DataTemplate](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/templates/data-templates/) of [PopupLayout](https://help.syncfusion.com/xamarin/popup/overview?) by using [Behavior](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/behaviors/creating).

You can also refer the following article.

https://www.syncfusion.com/kb/11375/how-to-access-a-named-accordion-inside-a-xaml-datatemplate-in-xamarin-forms-sfaccordion 

**XAML**

In PopupLayout’s [PopupView](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfPopupLayout.XForms~Syncfusion.XForms.PopupLayout.PopupView.html?), behaviour added to parent of Accordion.

``` xml
<sfPopup:SfPopupLayout x:Name="popupLayout" Margin="{OnPlatform iOS='0,40,0,0'}">
    <sfPopup:SfPopupLayout.Behaviors>
        <local:PopupBehavior/>
    </sfPopup:SfPopupLayout.Behaviors>
    <sfPopup:SfPopupLayout.PopupView>
        <sfPopup:PopupView WidthRequest="400" HeightRequest="400" ShowFooter="False">
            <sfPopup:PopupView.ContentTemplate>
                <DataTemplate>
                    <ScrollView BackgroundColor="#EDF2F5" Grid.Row="1">
                        <StackLayout>
                            <StackLayout.Behaviors>
                                <local:Behavior/>
                            </StackLayout.Behaviors>
                            <Button HeightRequest="50" Text="Change Accordion Header Icon Position" x:Name="accordionButton" BackgroundColor="LightGray"/>
                            <syncfusion:SfAccordion Grid.Row="1" x:Name="accordion" ExpandMode="MultipleOrNone" >
                                <syncfusion:SfAccordion.Items>
                                    <syncfusion:AccordionItem>
                                        ...
                                    </syncfusion:AccordionItem>
                                    <syncfusion:AccordionItem>
                                        ...
                                    </syncfusion:AccordionItem>
                                </syncfusion:SfAccordion.Items>
                            </syncfusion:SfAccordion>
                        </StackLayout>
                    </ScrollView>
                </DataTemplate>
            </sfPopup:PopupView.ContentTemplate>
        </sfPopup:PopupView>
    </sfPopup:SfPopupLayout.PopupView>
    <sfPopup:SfPopupLayout.Content>
        <Grid x:Name="popupGrid">
            <Grid.RowDefinitions>
                <RowDefinition Height="50"/>
                <RowDefinition Height="*"/>
            </Grid.RowDefinitions>
            <Button x:Name="ShowPopup" Text="Bring Popup" />
        </Grid>
    </sfPopup:SfPopupLayout.Content>
</sfPopup:SfPopupLayout>
```

**C#**

In ChildAdded event you will get the instance of Accordion.

``` c#
public class Behavior : Behavior<StackLayout>
{
    StackLayout stackLayout;
    SfAccordion accordion;
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
    }
 
    protected override void OnDetachingFrom(StackLayout bindable)
    {
        stackLayout.ChildAdded -= StackLayout_ChildAdded;
        accordion = null;
        stackLayout = null;
        base.OnDetachingFrom(bindable);
    }
}
```

**C#**

You can also get the Accordion using [FindByName](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.element.findbyname?view=xamarin-forms) method from Parent element.

``` c#
public class Behavior : Behavior<StackLayout>
{
    StackLayout stackLayout;
    SfAccordion accordion;
    Button button;
    protected override void OnAttachedTo(StackLayout bindable)
    {
        stackLayout = bindable;
        stackLayout.ChildAdded += StackLayout_ChildAdded; ;
    }
    private void StackLayout_ChildAdded(object sender, ElementEventArgs e)
    {
        if (e.Element is Button)
        {
            button = e.Element as Button;
            button.Clicked += Button_Clicked;
        }
    }
 
    private void Button_Clicked(object sender, EventArgs e)
    {
        //Method 2 : Get SfAccordion reference using FindByName
        accordion = stackLayout.FindByName<SfAccordion>("accordion");
        App.Current.MainPage.DisplayAlert("Information", "Accordion instance is obtained and Header Icon Position is changed", "Ok");
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
```
