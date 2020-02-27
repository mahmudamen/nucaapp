using System;
using CoreGraphics;
using FFImageLoading;
using UIKit;

namespace nucaapp
{
    public partial class celladsViewController : UIViewController
    {
        private int Id;
        private UILabel headingLabel, subheadingLabel;
        private UITextView linkurl;
        private UIImageView imagepath;
        private UIWebView m;

        public celladsViewController(int id) : base("cellViewController", null)
        {
            Id = id;
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.NavigationItem.Title = "Nuca App";
            getadvs gn = new getadvs();
            var news = await gn.GetAdvsById(Id);
            headingLabel = new UILabel()
            {
                Text = news.title,
                Font = UIFont.FromName("Helvetica", 22f),
                TextColor = UIColor.Blue,
                BackgroundColor = UIColor.Clear,
                TextAlignment = UITextAlignment.Center
            };
            subheadingLabel = new UILabel()
            {
                Text = news.shortdesc,
                Font = UIFont.FromName("Helvetica", 20f),
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.Clear,
                TextAlignment = UITextAlignment.Right
            };
            linkurl = new UITextView()
            {
                Text = news.liqo,
                Font = UIFont.FromName("Helvetica", 20f),
                TextColor = UIColor.Blue,
                BackgroundColor = UIColor.Clear,
                TextAlignment = UITextAlignment.Left,
                Editable = false ,
                Selectable = true ,
                DataDetectorTypes = UIDataDetectorType.Link,
                UserInteractionEnabled =true
                
            };
            
            subheadingLabel.LineBreakMode = UILineBreakMode.WordWrap;
            subheadingLabel.Lines = 0;
            subheadingLabel.SizeToFit();

            linkurl.ShouldInteractWithUrl += delegate
            {
                return true;
            };

            linkurl.SizeToFit();
            imagepath = new UIImageView()
            {
                ContentMode = UIViewContentMode.ScaleAspectFit,

            };

            ImageService.Instance.LoadUrl(news.image).Into(imagepath);
            headingLabel.Frame = new CGRect(15, 75, this.View.Bounds.Size.Width - 30, 25);
            imagepath.Frame = new CGRect(this.View.Bounds.Size.Width / 4, 110, this.View.Bounds.Size.Width / 2, 400);
            subheadingLabel.Frame = new CGRect(15, 515, this.View.Bounds.Size.Width - 30, 50);
            linkurl.Frame = new CGRect(15, 570, this.View.Bounds.Size.Width - 30, 50);
            this.View.AddSubview(headingLabel.ViewForBaselineLayout);
            this.View.AddSubview(imagepath.ViewForBaselineLayout);
            this.View.AddSubview(subheadingLabel.ViewForBaselineLayout);
            this.View.AddSubview(linkurl.ViewForBaselineLayout);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }
    }
}