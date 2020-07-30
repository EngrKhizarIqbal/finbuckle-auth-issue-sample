using Finbuckle.MultiTenant;
using HelpDesk.MultiTenant.MultiTenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace HelpDesk.MultiTenant.Web.MultiTenant
{
    public class DummyMultiTenantStore : IMultiTenantStore
    {
        private readonly QuodyMutiTenantDbContext _db;

        public DummyMultiTenantStore(QuodyMutiTenantDbContext db)
        {
            _db = db;
        }

        public Task<bool> TryAddAsync(TenantInfo tenantInfo)
        {
            throw new NotImplementedException();
        }

        public async Task<TenantInfo> TryGetAsync(string id)
        {
            return await _db.QuodyTenantInfo.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<TenantInfo> TryGetByIdentifierAsync(string identifier)
        {
            return await _db.QuodyTenantInfo.FirstOrDefaultAsync(f => f.Identifier.ToLower() == identifier.ToLower());
        }

        public Task<bool> TryRemoveAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryUpdateAsync(TenantInfo tenantInfo)
        {
            throw new NotImplementedException();
        }
    }
}
