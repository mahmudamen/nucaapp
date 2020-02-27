using System;
using CoreGraphics;
using FFImageLoading;
using Foundation;
using UIKit;

namespace nucaapp
{
    public class adsTableViewCell : UITableViewCell
    {
        UILabel headingLabel, subheadingLabel;
        UIImageView imagepath;

        public adsTableViewCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
        {
            SelectionStyle = UITableViewCellSelectionStyle.Blue;
            ContentView.BackgroundColor = UIColor.White;


            headingLabel = new UILabel()
            {
                Font = UIFont.FromName("Helvetica", 17f),
                TextColor = UIColor.Blue,
                BackgroundColor = UIColor.Clear,
                TextAlignment = UITextAlignment.Center
            };
            subheadingLabel = new UILabel()
            {
                Font = UIFont.FromName("Helvetica", 17f),
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.Clear,
                TextAlignment = UITextAlignment.Right
            };
            imagepath = new UIImageView()
            {
                ContentMode = UIViewContentMode.ScaleAspectFit,
                

            };

            subheadingLabel.LineBreakMode = UILineBreakMode.WordWrap;
            subheadingLabel.Lines = 0;
            subheadingLabel.SizeToFit();
            ContentView.AddSubviews(new UIView[] { headingLabel, subheadingLabel, imagepath });
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
            headingLabel.Frame = new CGRect(-5, 30, ContentView.Bounds.Width, 25);
            imagepath.Frame = new CGRect(ContentView.Bounds.Width / 4, 60, ContentView.Bounds.Width / 2, 300);
            subheadingLabel.Frame = new CGRect(-5, 365, ContentView.Bounds.Width, 65);

        }
    }
}
