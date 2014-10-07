using System;
using System.Collections.Generic;
using BA.Web.Common;
using BA.Web.Common.Helper;
using CRM.ShopComponent.Dto;
using Telerik.Web.UI;

namespace BA.Web.Shop.UserControls
{
    public partial class ReviewList : BaseControl
    {
        public event ObjectSavingEventHandler ReviewSaving;
        public bool AllowAdd { get; set; }
        public IEnumerable<ReviewInfoDto> Instances { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        private void InitControls()
        {
            if (AllowAdd)
            {
                divAdd.Visible = true;
                edContent.ToolsFile = ServerPath + "/Styles/BasicTools.xml";
                edContent.EditModes = Telerik.Web.UI.EditModes.Design;
                EditorModule moduleStatistics = new EditorModule();
                moduleStatistics.Name = "RadEditorStatistics";
                edContent.Modules.Add(moduleStatistics);
            }
            else
            {
                divAdd.Visible = false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string content = edContent.Content.Trim();
            if (content.Length > 0)
            {
                ReviewInfoDto Review = new ReviewInfoDto();
                Review.Rating = Rating1.Value;
                Review.Content = content;

                if (ReviewSaving != null)
                {
                    ReviewSaving(this, new ObjectSavingEventArgs(Review));
                }
            }
            else
            {

            }
        }

        public void ListDataBind()
        {
            rptItems.DataSource = Instances;
            rptItems.DataBind();
        }

        public string GetPostedTime(object obj)
        {
            ReviewInfoDto item = (ReviewInfoDto)obj;
            return string.Format("{0} {1}", TextOfPostedOn, item.IssuedTime.ToString(WebContext.Current.ApplicationOption.DateTimeFormat));
        }

        private string _textOfPostedOn;
        private string TextOfPostedOn
        {
            get
            {
                if (_textOfPostedOn == null)
                {
                    _textOfPostedOn = Localize("Shop.UserControls.ReviewList.PostedOn", "Posted on");
                }
                return _textOfPostedOn;
            }
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();
            lblTitle.Text = Localize("Shop.UserControls.ReviewList.lblTitle", "Review and Rating");
            lblTip1.Text = Localize("Shop.UserControls.ReviewList.lblTip1", "Put your review:");
            lblTip2.Text = Localize("Shop.UserControls.ReviewList.lblTip2", "Put your rating:");
            btnSubmit.Text = Localize("Shop.UserControls.ReviewList.btnSubmit", "Submit");
        }
    }
}