using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreGraphics;
using UIKit;

namespace nucaapp
{
    public partial class CitiesViewController : UIViewController
    {
        UITableView table;
        bool useRefreshControl = false;
        UIRefreshControl RefreshControl;
        UISearchBar searchBar;
        List<CitiesModel> allnews;
        UIActivityIndicatorView spinner;

        public CitiesViewController(IntPtr handle) : base(handle)
        {
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            var btn = new UIButton(UIButtonType.Custom);
            btn.Frame = new CGRect(0, 0, 40, 40);
            btn.SetImage(UIImage.FromBundle("barlogo"), UIControlState.Normal);

            showIndicator();
            searchBar = new UISearchBar()
            {
                Placeholder = " ",
                Prompt = " ",
                ShowsScopeBar = true
            };
            searchBar.SizeToFit();
            searchBar.AutocorrectionType = UITextAutocorrectionType.No;
            searchBar.AutocapitalizationType = UITextAutocapitalizationType.None;
            searchBar.ReturnKeyType = UIReturnKeyType.Done;
            searchBar.EnablesReturnKeyAutomatically = false;
            this.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(btn), true);
            this.NavigationItem.TitleView = searchBar;
            searchBar.CancelButtonClicked += (sender, e) =>
            {
                searchBar.ResignFirstResponder();
            };
            searchBar.SelectedScopeButtonIndexChanged += (sender, e) => { searchBar.ResignFirstResponder(); };
            searchBar.SearchButtonClicked += (sender, e) =>
            {
                //  searchTable();
                searchBar.ResignFirstResponder();
            };
            searchBar.SearchButtonClicked += (sender, e) =>
            {
                //  Search();
            };
            searchBar.TextChanged += (sender, e) =>
            {
                //this is the method that is called when the user searches  
                searchTableAsync();
            };
            searchBar.CancelButtonClicked += (sender, e) =>
            {
                searchBar.ResignFirstResponder();
            };
            searchBar.SelectedScopeButtonIndexChanged += (sender, e) => { searchBar.ResignFirstResponder(); };

            getCities gn = new getCities();
            List<CitiesModel> allnews = await gn.GetAllNews();

            if (allnews != null)
            {
                table = new UITableView(View.Bounds);
                table.AutoresizingMask = UIViewAutoresizing.All;
                table.RowHeight = 100f;
                table.EstimatedRowHeight = 100f;
                table.Source = new ListCitiesModelSource(allnews.ToArray(), this, " ");
                await RefreshAsync();

                AddRefreshControl();
                //table.TableHeaderView = searchBar;
                Add(table);
                hideIndicator();
                table.Add(RefreshControl);
            }
            else
            {
                var alrt = UIAlertController.Create("Nuca", "يرجي التاكد من الاتصال بالانترنت", UIAlertControllerStyle.Alert);
                alrt.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                PresentViewController(alrt, true, null);
                hideIndicator();
            }
        }
        async Task RefreshAsync()
        {
            // only activate the refresh control if the feature is available  
            if (useRefreshControl)
                RefreshControl.BeginRefreshing();

            if (useRefreshControl)
                RefreshControl.EndRefreshing();

            table.ReloadData();
        }
        // This method will add the UIRefreshControl to the table view if  
        // it is available, ie, we are running on iOS 6+  
        void AddRefreshControl()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(6, 0))
            {
                // the refresh control is available, let's add it  
                RefreshControl = new UIRefreshControl();
                RefreshControl.ValueChanged += async (sender, e) =>
                {
                    getCities gn = new getCities();
                    List<CitiesModel> allnews = await gn.GetAllNews();

                    table.Source = new ListCitiesModelSource(allnews.ToArray(), this," ");
                    await RefreshAsync();
                };
                useRefreshControl = true;
            }
        }
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }
        private async Task searchTableAsync()
        {
            getCities gn = new getCities();
            List<CitiesModel> allnews = await gn.GetAllNews();
            table.Source = new ListCitiesModelSource(allnews.ToArray(), this, searchBar.Text);
            //tableSource.PerformSearch(searchBar.Text);


            table.ReloadData();

        }
        private void showIndicator()
        {
            if (spinner == null)
            {
                spinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
                spinner.HidesWhenStopped = true;
                spinner.Color = UIColor.Blue;
            }
            var windows = UIApplication.SharedApplication.Windows;
            Array.Reverse(windows);
            foreach (UIWindow w in windows)
            {
                if (w.WindowLevel == UIWindowLevel.Normal && !w.Hidden)
                {
                    spinner.Frame = new CGRect((float)w.Bounds.GetMidX(), (float)(.66 * w.Bounds.Height), 37, 37);
                    w.AddSubview(spinner);
                    w.BringSubviewToFront(spinner);
                    break;
                }
            }
            spinner.StartAnimating();
        }

        private void hideIndicator()
        {
            if (spinner == null)
                return;
            if (!spinner.IsAnimating)
                return;
            spinner.StopAnimating();
        }
    }
}