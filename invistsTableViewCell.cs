using UIKit;
using Foundation;
using CoreGraphics;
using FFImageLoading;
namespace nucaapp
{
    public class invistsTableViewCell : UITableViewCell
    {
        UILabel title, levelname, shortdesc, longdesc, space;
        UIImageView imagepath;

        public invistsTableViewCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
        {
            SelectionStyle = UITableViewCellSelectionStyle.Blue;
            ContentView.BackgroundColor = UIColor.White;
            title = new UILabel()
            {
                Font = UIFont.FromName("Helvetica", 18f),
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.Clear,
                TextAlignment = UITextAlignment.Right,
            };
            levelname = new UILabel()
            {
                Font = UIFont.FromName("Helvetica", 16f),
                TextColor = UIColor.White,
                BackgroundColor = UIColor.Red,
                TextAlignment = UITextAlignment.Center
            };
            shortdesc = new UILabel()
            {
                Font = UIFont.FromName("Helvetica", 16f),
                TextColor = UIColor.Blue,
                BackgroundColor = UIColor.Clear,
                TextAlignment = UITextAlignment.Right
            };
            longdesc = new UILabel()
            {
                Font = UIFont.FromName("Helvetica", 16f),
                TextColor = UIColor.Blue,
                BackgroundColor = UIColor.Clear,
                TextAlignment = UITextAlignment.Right
            };
            space = new UILabel()
            {
                Font = UIFont.FromName("Helvetica", 16f),
                TextColor = UIColor.Blue,
                BackgroundColor = UIColor.Clear,
                TextAlignment = UITextAlignment.Right
            };
            imagepath = new UIImageView();
            shortdesc.LineBreakMode = UILineBreakMode.WordWrap;
            shortdesc.Lines = 0;
            shortdesc.SizeToFit();
            ContentView.AddSubviews(new UIView[] { title, levelname, shortdesc, longdesc, space, imagepath });
        }
        public void UpdateCell(string _title, string _levelname, string _shortdesc, string _longdesc, string _space, string _imagepath)
        {
            title.Text = _title;
            levelname.Text = _levelname;
            shortdesc.Text = _shortdesc;
            longdesc.Text = _longdesc;
            space.Text = _space;
            ImageService.Instance.LoadUrl(_imagepath).Into(imagepath);
        }
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            imagepath.Frame = new CGRect(15, 10, 50, 50);
            title.Frame = new CGRect(-5, 20, ContentView.Bounds.Width, 25);
            shortdesc.Frame = new CGRect(-5, 55, ContentView.Bounds.Width, 25);
            levelname.Frame = new CGRect(15, 85, 100, 25);
            space.Frame = new CGRect(-5, 85, ContentView.Bounds.Width, 25);
        }
    }
}
