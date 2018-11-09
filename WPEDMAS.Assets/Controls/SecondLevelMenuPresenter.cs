using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPEDMAS.Client.Core.ViewModels;

namespace WPEDMAS.Assets.Controls
{
    [TemplatePart(Name = "PART_MenuHost", Type = typeof(ListView))]
    public class SecondLevelMenuPresenter : Control
    {
        public static readonly DependencyProperty MenuItemsProperty =
            DependencyProperty.Register(nameof(MenuItems),
                typeof(IEnumerable<SecondLevelMenuViewModel>), typeof(SecondLevelMenuPresenter));
        public IEnumerable<SecondLevelMenuViewModel> MenuItems
        {
            get { return (IEnumerable<SecondLevelMenuViewModel>)GetValue(MenuItemsProperty); }
            set { SetValue(MenuItemsProperty, value); }
        }

        static SecondLevelMenuPresenter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SecondLevelMenuPresenter), new FrameworkPropertyMetadata(typeof(SecondLevelMenuPresenter)));
        }
        public SecondLevelMenuPresenter()
        {
            VisualStateManager.GoToState(this, "MenuView", true);
        }
        public override void OnApplyTemplate()
        {
            ListView menuHost = (ListView)GetTemplateChild("PART_MenuHost");
            menuHost.SelectionChanged += MenuHost_SelectionChanged;
            base.OnApplyTemplate();
        }

        private void MenuHost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectWidget(e.AddedItems?[0] as SecondLevelMenuViewModel);
        }
        private void SelectWidget(SecondLevelMenuViewModel widget)
        {
            var shell = IoC.Get<IShell>();
            shell.NavigateContent(Activator.CreateInstance(widget.NavigationType, widget));
        }
    }
}
