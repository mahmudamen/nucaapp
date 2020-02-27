using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;

namespace nucaapp
{
    public class ListCitiesModelSource : UITableViewSource
    {
        CitiesModel[] TableItems;
        string CellIdentifier = "TableCell";
        string search = "";
        CitiesViewController owner;
        private CitiesModel[] searchItems;
        public ListCitiesModelSource(CitiesModel[] allcities, CitiesViewController _owner, string _search)
        {
            if (_search == "")
            {
                TableItems = allcities;
            }
            else
            {
                TableItems = allcities.Where(x => x.name.ToLower().Contains(_search) || x.namear.ToLower().Contains(_search)).ToArray();
            }
            this.owner = _owner;
            this.searchItems = allcities.Where(x => x.name.ToLower().Contains(_search) || x.namear.ToLower().Contains(_search)).ToArray();
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
            var cell = tableView.DequeueReusableCell(CellIdentifier) as citiesTableViewCell;
            CitiesModel pm = TableItems[indexPath.Row];
            if (cell == null)
            { cell = new citiesTableViewCell(new NSString("tableviewcell")); }
            cell.UpdateCell(pm.namear, pm.name, pm.image);
            return cell;
        }
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            citiescellViewController cvc = new citiescellViewController(TableItems[indexPath.Row].id);
            owner.Title = "المدن";
            owner.NavigationController.PushViewController(cvc, true);
            tableView.DeselectRow(indexPath, true);
        }
    }
}
