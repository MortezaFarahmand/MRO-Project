
using _0_Framework.Application;
using System.Collections.Generic;

namespace BasicDataManagement.Application.Contract.Entiti
{
    public interface IEntitiApplication
    {
        OperationResult Create(CreateEntiti command);
        OperationResult Edit(EditEntiti command);
        EditEntiti GetDetails(long id);
        List<EntitiViewModel> GetEntitis();
    }
}
