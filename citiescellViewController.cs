using System;
using CoreGraphics;
using FFImageLoading;
using UIKit;

namespace nucaapp
{
    public partial class citiescellViewController : UIViewController
    {
        private int Id;
        //private UILabel headingLabel, subheadingLabel;
        private UIImageView imagepath;
        public citiescellViewController(int id) : base("cellViewController", null)
        {
            Id = id;
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.NavigationItem.Title = "Nuca App";
            getCities gn = new getCities();
            var news = await gn.GetNewsById(Id);
            
            imagepath = new UIImageView();
            ImageService.Instance.LoadUrl(news.image).Into(imagepath);
            imagepath.Frame = new CGRect(15, 70, this.View.Bounds.Size.Width - 30, this.View.Bounds.Height -175);  
            this.View.AddSubview(imagepath.ViewForBaselineLayout);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }
    }
}