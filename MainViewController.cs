using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using CoreGraphics;
using Firebase.Database;
using Plugin.Connectivity;
using UIKit;

namespace nucaapp
{
    public partial class MainViewController : UIViewController
    {
        UITableView table;
        UISearchBar searchBar;
        bool useRefreshControl = false;
        UIRefreshControl RefreshControl;
        UIActivityIndicatorView spinner;
        List<PhotoModel> allnews;
        public MainViewController(IntPtr handle) : base(handle)
        {
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            var btn = new UIButton(UIButtonType.Custom);
            btn.Frame = new CGRect(0, 0, 0, 0);

            showIndicator();

            btn.SetImage(UIImage.FromBundle("barlogo"), UIControlState.Normal);
            btn.ContentMode = UIViewContentMode.ScaleAspectFit;
            
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

            searchBar.SearchButtonClicked += (sender, e) =>
            {
              //  Search();
            };
            searchBar.TextChanged += (sender, e) =>
            {
                //this is the method that is called when the user searches  
                searchTable();
            };
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
            
            this.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(btn), true);
            this.NavigationItem.TitleView = searchBar;
            if (Reachability.IsHostReachable("https://newcities-newurban.firebaseio.com"))
            {
                getNews gn = new getNews();
                allnews = await gn.GetAllNews();
                if (allnews != null)
                {
                    table = new UITableView(new CGRect(0, 20, View.Bounds.Width - 5, View.Bounds.Height - 20));
                    table.AutoresizingMask = UIViewAutoresizing.All;
                    table.RowHeight = 300f;
                    table.EstimatedRowHeight = 300f;
                    // defaults to Plain style
                    table.Source = new ListsPhotoModelSource(allnews.ToArray(), this, "");
                    
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

                }
            }
            else
            {
                PhotoModel pm = new PhotoModel();
                pm.id = 0;
                pm.title = "يرجي التاكد من الاتصال بالانترنت";
                pm.image = "";
                pm.date = DateTime.Today.ToShortDateString();
                pm.dayx = "";
                pm.longdesc = "";
               // allnews.Add(pm);
                var alrt = UIAlertController.Create("Nuca", "يرجي التاكد من الاتصال بالانترنت", UIAlertControllerStyle.Alert);
                alrt.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                PresentViewController(alrt, true, null);
                hideIndicator();
            }
        }

        private void searchTable()
        {

            table.Source = new ListsPhotoModelSource(allnews.ToArray(), this, searchBar.Text);
            //tableSource.PerformSearch(searchBar.Text);
            
               
            table.ReloadData();
            
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
                    getNews gn = new getNews();
                    List<PhotoModel> allnews = await gn.GetAllNews();
                    
                    table.Source = new ListsPhotoModelSource(allnews.ToArray(), this, "");
                    await RefreshAsync();
                };
                useRefreshControl = true;
            }
        }
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
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
                    w.Center = this.View.Center;
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