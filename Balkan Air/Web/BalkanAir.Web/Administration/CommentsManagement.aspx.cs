namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;
    using System.Web.UI;

    using Ninject;

    using Data.Models;
    using Services.Data.Contracts;

    public partial class CommentsManagement : Page
    {
        [Inject]
        public ICommentsServices CommentsServices { get; set; }

        public IQueryable<Comment> CommentsGridView_GetData()
        {
            return this.CommentsServices.GetAll()
                .OrderByDescending(c => c.DateOfComment);
        }

        public void CommentsGridView_UpdateItem(int id)
        {
            var comment = this.CommentsServices.GetComment(id);
            
            if (comment == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }

            TryUpdateModel(comment);
            if (ModelState.IsValid)
            {
                this.CommentsServices.UpdateComment(id, comment);
            }
        }

        public void CommentsGridView_DeleteItem(int id)
        {
            this.CommentsServices.DeleteComment(id);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}