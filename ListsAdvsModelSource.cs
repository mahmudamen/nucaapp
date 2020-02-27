using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FFImageLoading;
using System.Linq;
using Foundation;
using UIKit;


namespace nucaapp
{
    public class ListsAdvsModelSource:UITableViewSource
    {
        advModel[] TableItems;
        string CellIdentifier = "TableCell";
        string search = "";
        AdsViewController owner;
        private advModel[] searchItems;
        public ListsAdvsModelSource(advModel[] alladvs, AdsViewController _owner , string _search)
        {
            if (_search == "")
            {
                TableItems = alladvs;
            }
            else
            {
                TableItems = alladvs.Where(x => x.title.Contains(_search)).ToArray();
                  
            }
            this.owner = _owner;
            this.searchItems = alladvs.Where(x => x.title.Contains(_search)).ToArray();

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
            var cell = tableView.DequeueReusableCell(CellIdentifier) as adsTableViewCell;
            advModel pm = TableItems[indexPath.Row];
            if (cell == null)
            { cell = new adsTableViewCell(new NSString("tableviewcell")); }
            cell.UpdateCell(pm.title, pm.shortdesc, pm.image);
            return cell;
        }
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            celladsViewController cvc = new celladsViewController(TableItems[indexPath.Row].id);
            owner.Title = "الاعلانات";
            owner.NavigationController.PushViewController(cvc, true);
            tableView.DeselectRow(indexPath, true);
        }
    }
}
