using System;
using System.Drawing;
using CoreGraphics;
using FFImageLoading;
using Foundation;
using UIKit;

namespace nucaapp
{
    public partial class cellViewController : UIViewController
    {
        private int Id;
        private UILabel headingLabel, subheadingLabel;
        private UIImageView imagepath;
        private UITextView longdesc;
        private UIScrollView scrollView;
        private UIView uv;

        public cellViewController(int id) : base("cellViewController", null)
        {
            Id = id;
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.NavigationItem.Title = "Nuca App";
            var rightButton = new UIButton(UIButtonType.Custom);

            var rightBarButton = new UIBarButtonItem(rightButton);
            NavigationItem.SetRightBarButtonItems(new UIBarButtonItem[] { rightBarButton }, false);
            rightButton.TouchUpInside += (sender, e) =>
            {
                Console.WriteLine("This button is clicked");
            };

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
                Font = UIFont.FromName("Helvetica", 16f),
                TextColor = UIColor.Blue,
                BackgroundColor = UIColor.Clear,
                TextAlignment = UITextAlignment.Right
            };
            longdesc = new UITextView()
            {
                Text = news.longdesc,
                Font = UIFont.FromName("Helvetica", 16f),
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.Clear,
                TextAlignment = UITextAlignment.Right,
            };
            headingLabel.LineBreakMode = UILineBreakMode.WordWrap;
            headingLabel.Lines = 0;
            headingLabel.SizeToFit();
            longdesc.SizeToFit();
            longdesc.ScrollEnabled=true;
            
            imagepath = new UIImageView()
            {
                ContentMode = UIViewContentMode.ScaleAspectFit,
            }; 

            ImageService.Instance.LoadUrl(news.image).Into(imagepath);
            imagepath.Frame = new CGRect(15, 100, this.View.Bounds.Size.Width - 30, 175);
            headingLabel.Frame = new CGRect(15, 290, this.View.Bounds.Size.Width - 30, 85);
            subheadingLabel.Frame = new CGRect(15, 375, this.View.Bounds.Size.Width - 30, 25);
            longdesc.Frame = new CGRect(15, 400, this.View.Bounds.Size.Width - 30, 5000);

            
            uv = new UIView();

            uv.AddSubview(imagepath.ViewForBaselineLayout);
            uv.AddSubview(headingLabel.ViewForBaselineLayout);
            uv.AddSubview(subheadingLabel.ViewForBaselineLayout);
            uv.AddSubview(longdesc.ViewForBaselineLayout);

            scrollView = new UIScrollView(new CGRect(0, 0, View.Frame.Width, View.Frame.Height));
            scrollView.ContentSize = new CGSize(this.View.Bounds.Size.Width, scrollView.Bounds.Height + 5000);
            uv.TranslatesAutoresizingMaskIntoConstraints = false;
            
            scrollView.AddSubview(uv);
            this.View.AddSubview(scrollView);          
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }
    }
}

