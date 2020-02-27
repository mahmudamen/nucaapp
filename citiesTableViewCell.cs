using System;
using UIKit;
using Foundation;
using CoreGraphics;
using FFImageLoading;
namespace nucaapp
{
    public class citiesTableViewCell : UITableViewCell
    {
        UILabel headingLabel, subheadingLabel;
        UIImageView imagepath;

        public citiesTableViewCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
        {
            SelectionStyle = UITableViewCellSelectionStyle.Blue;
            ContentView.BackgroundColor = UIColor.White;
            headingLabel = new UILabel()
            {
                Font = UIFont.FromName("Helvetica", 22f),
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.Clear,
                TextAlignment = UITextAlignment.Right,
            };
            subheadingLabel = new UILabel()
            {
                Font = UIFont.FromName("Helvetica", 20f),
                TextColor = UIColor.Blue,
                BackgroundColor = UIColor.Clear,
                TextAlignment = UITextAlignment.Left
            };

            //imagepath = new UIImageView();
            ContentView.AddSubviews(new UIView[] { headingLabel, subheadingLabel });
        }
        public void UpdateCell(string _heading, string _subheading, string _imagepath)
        {
            headingLabel.Text = _heading;
            subheadingLabel.Text = _subheading;
            ImageService.Instance.LoadUrl(_imagepath).Into(imagepath);
        }
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            //imagepath.Frame = new CGRect(10, 5, 45, 45);
            headingLabel.Frame = new CGRect(-5, 5, ContentView.Bounds.Width, 45);
            subheadingLabel.Frame = new CGRect(10, 50, ContentView.Bounds.Width, 45);
        }
    }
}
