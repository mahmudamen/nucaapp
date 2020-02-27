using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FFImageLoading;
using System.Linq;
using Foundation;
using UIKit;



namespace nucaapp
{
    public class ListsPhotoModelSource:UITableViewSource
    {
        PhotoModel[] TableItems;
        string CellIdentifier = "TableCell";
        string search = "";
        MainViewController owner;
        private PhotoModel[] searchItems ;

        public ListsPhotoModelSource(PhotoModel[] allnews, MainViewController _owner, string _search)
        {
            if (_search == "")
            {
                TableItems = allnews;
            }
            else
            {
                TableItems = allnews.Where(x => x.title.Contains(_search)).ToArray();
            }
            this.owner = _owner;
            this.searchItems = allnews.Where(x=>x.title.Contains(_search)).ToArray();
        }
        
        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }
        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return TableItems.Length;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CellIdentifier) as mainTableViewCell;
            //string item = TableItems[indexPath.Row];
            PhotoModel pm = TableItems[indexPath.Row];
            //---- if there are no cells to reuse, create a new one
            if (cell == null)
            { cell = new mainTableViewCell(new NSString ("tableviewcell")); }
            //cell.TextLabel.Text = pm.title;
            //cell.DetailTextLabel.Text = pm.date + " " + pm.dayx;
            //ImageService.Instance.LoadUrl(pm.image).Into(cell.ImageView);
            //cell.ImageView.Image = LoadImage.LoadImageFromURL(pm.image);
            //cell.ImageView.Image = LoadImage.FromUrl(pm.image);
            //}
            cell.UpdateCell(pm.title, pm.dayx + " " + pm.date, pm.image,pm.longdesc);
            return cell;
        }
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            cellViewController cvc = new cellViewController(TableItems[indexPath.Row].id);            
            owner.NavigationController.PushViewController(cvc, true);
            //owner.NavigationItem.TitleView = new UIImageView(UIImage.FromBundle("barlogo")
              //  .ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal));
            tableView.DeselectRow(indexPath, true);
           
        }
        public void PerformSearch(string searchText)
        {
            search = searchText;
            searchText = searchText.ToLower();
            //    this.searchItems = TableItems.Where(x => x.Title.ToLower().Contains(searchText)).ToList();
            PhotoModel[] tst = TableItems.Where(x => x.title.Contains(searchText)).ToArray();
            this.searchItems = tst;

        }

    }
}
