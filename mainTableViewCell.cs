using System;
using UIKit;
using Foundation;
using CoreGraphics;
using FFImageLoading;

namespace nucaapp
{
    public class mainTableViewCell: UITableViewCell
    {
        UILabel headingLabel, subheadingLabel ,sublongdesc;
        UIImageView imagepath;

        public mainTableViewCell(NSString cellId) : base (UITableViewCellStyle.Default, cellId)
        {
            SelectionStyle = UITableViewCellSelectionStyle.Blue;
            ContentView.BackgroundColor = UIColor.White;
            
            headingLabel = new UILabel()
            {
                Font = UIFont.FromName("Helvetica", 17f),
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.Clear,
                TextAlignment = UITextAlignment.Right,                
            };
            subheadingLabel = new UILabel()
            {
                Font = UIFont.FromName("Helvetica", 15f),
                TextColor = UIColor.Blue,
                BackgroundColor = UIColor.Clear,
                TextAlignment = UITextAlignment.Right
            };
            sublongdesc = new UILabel() {
                Font = UIFont.FromName("Helvetica", 15f),
                TextColor = UIColor.Blue,
                BackgroundColor = UIColor.Clear,
                TextAlignment = UITextAlignment.Right
            };
            imagepath = new UIImageView() { 
            ContentMode = UIViewContentMode.ScaleAspectFit,
            
            };
            
            headingLabel.LineBreakMode = UILineBreakMode.WordWrap;
            headingLabel.Lines = 0;
            headingLabel.SizeToFit();
            ContentView.AddSubviews(new UIView[] { headingLabel, subheadingLabel, imagepath ,sublongdesc});
        }
        public void UpdateCell(string _heading, string _subheading, string _imagepath,string _sublongdesc)
        {
            headingLabel.Text = _heading;
            subheadingLabel.Text = _subheading;
            ImageService.Instance.LoadUrl(_imagepath).Into(imagepath);
            sublongdesc.Text = _sublongdesc;
        }
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            imagepath.Frame = new CGRect(5, 50, ContentView.Bounds.Width, 145);
            headingLabel.Frame = new CGRect(-5, 200, ContentView.Bounds.Width, 65);
            subheadingLabel.Frame = new CGRect(-5, 270, ContentView.Bounds.Width, 25);
            sublongdesc.Frame = new CGRect(-5, 300, ContentView.Bounds.Width, 35);

        }
    }
}
