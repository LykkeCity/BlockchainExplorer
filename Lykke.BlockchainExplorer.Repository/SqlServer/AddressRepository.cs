using Lykke.BlockchainExplorer.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lykke.BlockchainExplorer.Core.Domain;
using Lykke.BlockchainExplorer.Core.Log;
using Lykke.BlockchainExplorer.Core.Utils;

namespace Lykke.BlockchainExplorer.Repository.SqlServer
{
    public class AddressRepository : IAddressRepository, IDisposable
    {
        private Orm.Entities _context;
        private readonly ILog _log;

        public AddressRepository(ILog log)
        {
            _log = log;
            _context = new Orm.Entities();
        }

        public async Task<Address> GetById(string id)
        {
            return await Task.Run<Address>(() =>
            {
                return GetAddressById(id);
            }); 
        }

        private Address GetAddressById(string id)
        {
            var addressRecord = _context.GetAddressById(id).SingleOrDefault();

            if (addressRecord == null) return null;

            var address = new Address()
            {
                Hash = addressRecord.Hash,
                ColoredAddress = addressRecord.ColoredAddress,
                UncoloredAddress = addressRecord.UncoloredAddress
            };

            return address;
        }

        public async Task Save(Address entity)
        {
            await SaveAddress(entity);
        }

        private async Task SaveAddress(Address entity)
        {
            try
            {
                _context.InsertAddress(entity.Hash, entity.ColoredAddress, entity.UncoloredAddress, entity.Balance);
            }
            catch (Exception e)
            {
                await _log.WriteFatalError("AddressRepository", "SaveAddress", entity.ToJson(), e);
            }
        }

        public async Task UpdateAddress(Address address)
        {
            await UpdateAddressData(address);
        } 

        private async Task UpdateAddressData(Address address)
        {
            try
            {
                _context.UpdateAddress(address.Hash, address.ColoredAddress, address.UncoloredAddress);
            }
            catch (Exception e)
            {

                await _log.WriteFatalError("AddressRepository", "UpdateAddressData", address.ToJson(), e);
            }
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}
