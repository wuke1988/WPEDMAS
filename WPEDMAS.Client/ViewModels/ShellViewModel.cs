using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPEDMAS.Client.Core.ViewModels;

namespace WPEDMAS.Client.ViewModels
{
    [Export(typeof(IShell)), PartCreationPolicy(CreationPolicy.Shared)]
    public class ShellViewModel : Screen, IShell
    {
        [ImportMany]
        public IEnumerable<TopLevelWidget> TopLevelWidgets { get; set; }

        private object _selectedSecondLevelMenu;
        public object SelectedSecondLevelMenu
        {
            get { return _selectedSecondLevelMenu; }
            set
            {
                _selectedSecondLevelMenu = value;
                OnSelectedSecondLevelMenuChanged();
            }
        }

        private object _activeContent;
        public object ActiveContent
        {
            get
            {
                return _activeContent;
            }
            set
            {
                this.SetProperty(ref _activeContent, value);
            }
        }

        [ImportingConstructor]
        public ShellViewModel()
        {
            DisplayName = "";
        }

        public void NavigateContent(object viewModel)
        {
            ActiveContent = viewModel;
        }
        public void lb_TopLevelMenu_MouseDown()
        {
            NavigateContent(SelectedSecondLevelMenu);
        }
        private void OnSelectedSecondLevelMenuChanged()
        {
            NavigateContent(SelectedSecondLevelMenu);
        }
    
    }
}
