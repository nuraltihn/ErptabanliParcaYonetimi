using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Erpyonetimi.Domain.Entities;
namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface IParcaService
    {
        List<Parca> GetAllParca();
        Parca? GetById(int id);
        Parca? GetByKod(string parcakodu);
        void AddParca(Parca parca);
        void UpdateParca(Parca parca);
        void RemoveParca(Parca parca);

    }
}
