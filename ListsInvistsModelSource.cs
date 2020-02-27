using System;
using System.Linq;
using CoreGraphics;
using Foundation;
using UIKit;

namespace nucaapp
{
    public class ListsInvistsModelSource : UITableViewSource
    {
        InvestsModel[] TableItems;
        string CellIdentifier = "TableCell";
        InvestsViewController owner;
        private InvestsModel[] searchItems;
        public ListsInvistsModelSource(InvestsModel[] allnews, InvestsViewController _owner, string _search)
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
            this.searchItems = allnews.Where(x => x.title.Contains(_search)).ToArray();
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
            var cell = tableView.DequeueReusableCell(CellIdentifier) as invistsTableViewCell;
            InvestsModel pm = TableItems[indexPath.Row];
            if (cell == null)
            { cell = new invistsTableViewCell(new NSString("tableviewcell")); }
            
            cell.UpdateCell(pm.title, pm.levelname, pm.shortdesc, pm.longdesc, pm.space, pm.image);
            return cell;
        }
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            NSUrl url = new NSUrl(TableItems[indexPath.Row].longdesc, true);
            UIDocumentInteractionController udoc = UIDocumentInteractionController.FromUrl(url);
            udoc.PresentOpenInMenu(CGRect.Empty, owner.View, true);
            tableView.DeselectRow(indexPath, true);
        }
    }
}
