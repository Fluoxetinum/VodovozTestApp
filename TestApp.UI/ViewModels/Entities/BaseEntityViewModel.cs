using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Common.Enums;
using TestApp.UI.Controllers;

namespace TestApp.UI.ViewModels.Entities
{
    public class BaseEntityViewModel : BaseValidatableViewModel
    {
        public ICompanyController CompanyController { get; set; }

        private Guid _id;
        public Guid Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private EEntityViewAction _action;
        public EEntityViewAction Action
        {
            get => _action;
            set
            {
                _action = value;
                OnPropertyChanged();
            }
        }

        protected void UpdateIfInstantEdit()
        {
            if (Action == EEntityViewAction.InstantEdit && !HasErrors)
            {
                CompanyController.Update(this);
            }
        }

        public BaseEntityViewModel()
        {

        }

        public BaseEntityViewModel(ICompanyController companyController)
        {
            CompanyController = companyController;
        }

        public BaseEntityViewModel(ICompanyController companyController, Guid id) 
            :this(companyController)
        {
            _id = id;
        }

        public virtual BaseEntityViewModel Clone()
        {
            return new BaseEntityViewModel(CompanyController, Id);
        }

        public virtual void Apply(BaseEntityViewModel viewModel)
        {
            var temp = Action;
            Action = EEntityViewAction.Update;

            Id = viewModel.Id;
            Action = viewModel.Action;

            Action = temp;
        }
    }
}
