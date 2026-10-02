using _0_Framework.Application;
using PartManagement.Application.Contracts.Basic_data.EquipmentType;
using PartManagement.Domain.Basic_data.EquipmentTypeAgg;

namespace PartManagement.Application.Basic_data
{
    public class EquipmentTypeApplication : IEquipmentTypeApplication
    {
        private readonly IEquipmentTypeRepository _equipmentTypeRepository;

        public EquipmentTypeApplication(IEquipmentTypeRepository equipmentTypeRepository)
        {
            _equipmentTypeRepository = equipmentTypeRepository;
        }



        public OperationResult Create(CreateEquipmentType command)
        {
            var operation = new OperationResult();
            if(_equipmentTypeRepository.Exists(x => x.Name == command.Name || x.Code == command.Code || x.ColorCode == command.ColorCode)) 
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var equipmentType = new EquipmentType(command.Name, command.Code, command.Description,
                command.ColorCode, command.Picture);
            _equipmentTypeRepository.Create(equipmentType);
            _equipmentTypeRepository.SaveChanges();
            return operation.Succeeded();

        }

        public OperationResult Edit(EditEquipmentType command)
        {
            var operation = new OperationResult();
            var equipmentType = _equipmentTypeRepository.Get(command.Id);  

            if(equipmentType != null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_equipmentTypeRepository.Exists(x => x.Id == command.Id && x.Name == command.Name && x.Code == command.Code))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            equipmentType.Edit(command.Name, command.Code, command.Description,
                command.ColorCode, command.Picture);
            _equipmentTypeRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditEquipmentType GetDetails(long id)
        {
            return _equipmentTypeRepository.Getdetails(id);
        }

        public List<EquipmentTypeViewModel> GetEquipmentTypes()
        {
            return _equipmentTypeRepository.GetEquipmentTypes();
        }

        public List<EquipmentTypeViewModel> Search(EquipmentTypeSearchModel searchModel)
        {
            return _equipmentTypeRepository.Search(searchModel);
        }

        public OperationResult Deactivate(long id)
        {
            var operation = new OperationResult();
            var equipmentType = _equipmentTypeRepository.Get(id);
            if (equipmentType == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            equipmentType.Deactive();

            _equipmentTypeRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Activate(long id)
        {
            var operation = new OperationResult();
            var equipmentType = _equipmentTypeRepository.Get(id);
            if (equipmentType == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            equipmentType.Active();

            _equipmentTypeRepository.SaveChanges();
            return operation.Succeeded();
        }

    }
}
