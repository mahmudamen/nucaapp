using System;
using CoreGraphics;
using FFImageLoading;
using UIKit;

namespace nucaapp
{
    public partial class AdsCellViewController : UIViewController
    {
        public AdsCellViewController() : base("AdsCellViewController", null)
        {
        }

        private int Id;
        private UILabel headingLabel, subheadingLabel;
        private UIImageView imagepath;
        public AdsCellViewController(int id) : base("cellViewController", null)
        {
            Id = id;
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            getNews gn = new getNews();
            var news = await gn.GetNewsById(Id);
            headingLabel = new UILabel()
            {
                Text = news.title,
                Font = UIFont.FromName("Helvetica", 17f),
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.Clear,
                TextAlignment = UITextAlignment.Right,
            };
            subheadingLabel = new UILabel()
            {
                Text = news.dayx + " " + news.date,
                Font = UIFont.FromName("Helvetica", 15f),
                TextColor = UIColor.Blue,
                BackgroundColor = UIColor.Clear,
                TextAlignment = UITextAlignment.Right
            };
            headingLabel.LineBreakMode = UILineBreakMode.WordWrap;
            headingLabel.Lines = 0;
            headingLabel.SizeToFit();
            imagepath = new UIImageView();

            ImageService.Instance.LoadUrl(news.image).Into(imagepath);
            imagepath.Frame = new CGRect(15, 100, this.View.Bounds.Size.Width - 30, 175);
            headingLabel.Frame = new CGRect(15, 290, this.View.Bounds.Size.Width - 30, 85);
            subheadingLabel.Frame = new CGRect(15, 375, this.View.Bounds.Size.Width - 30, 25);
            this.View.AddSubview(imagepath.ViewForBaselineLayout);
            this.View.AddSubview(headingLabel.ViewForBaselineLayout);
            this.View.AddSubview(subheadingLabel.ViewForBaselineLayout);

            //Add(imagepath);
            //Add(headingLabel);
            //Add(subheadingLabel);
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}