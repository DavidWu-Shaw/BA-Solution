using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BA.Web.Common;
using CRM.ShopComponent.Dto;
using Telerik.Web.UI;
using Web.Helpers.EventHandlers;

namespace BA.Web.Shop.UserControls
{
    public partial class ProductList : BaseControl
    {
        public event CommandEventHandler ItemActionCommand;
        public event ButtonInitilizedEventHandler ItemActionButtonInitilized;

        public IEnumerable<ProductInfoDto> Instances { get; set; }

        private string _itemActionText = null;
        public string ItemActionText
        {
            get
            {
                if (string.IsNullOrEmpty(_itemActionText) && IsInViewState(InstanceStateKey))
                {
                    _itemActionText = GetFromViewState(InstanceStateKey) as string;
                }

                return _itemActionText;
            }
            set
            {
                _itemActionText = value;
                SaveInViewState(value, InstanceStateKey);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Action_Command(object sender, CommandEventArgs e)
        {
            if (ItemActionCommand != null)
            {
                ItemActionCommand(sender, e);
            }
        }

        public void ListDataBind()
        {
            lvProd.Rebind();
        }

        protected void lvProd_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            lvProd.DataSource = Instances;
        }

        protected void lvProd_ItemCreated(object sender, RadListViewItemEventArgs e)
        {
            Button actButton = e.Item.FindControl("btnAction") as Button;
            if (actButton != null)
            {
                if (!string.IsNullOrEmpty(ItemActionText))
                {
                    actButton.Text = ItemActionText;
                    // There is a bug here. 
                    if (ItemActionButtonInitilized != null)
                    {
                        ItemActionButtonInitilized(sender, new ButtonInitilizedEventArgs(actButton));
                    }
                }
                else
                {
                    actButton.Visible = false;
                }
            }
        }

        public string GetNavigateUrl(object obj)
        {
            ProductInfoDto item = (ProductInfoDto)obj;
            string url = string.Format("{0}?{1}={2}", ProductPage.PageUrl, ProductPage.QryInstanceId, item.ProductId);
            return GetUrl(url);
        }

        public string GetNumberOfRatingsDisplay(object obj)
        {
            ProductInfoDto item = (ProductInfoDto)obj;
            return string.Format("{0} {1}", TextOfConst, item.NumberOfRatings);
        }

        private string _textOfConst;
        private string TextOfConst
        {
            get
            {
                if (_textOfConst == null)
                {
                    _textOfConst = Localize("Shop.UserControls.ProductList.NumberOfRatings", "Number of ratings:");
                }
                return _textOfConst;
            }
        }
    }
}