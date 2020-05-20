using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace WebStore.Domain.DTO.Identity
{
    /// <summary>
    /// Модель утверждения, которая может прикрепляться к пользователю или роли в любых кол-вах
    /// Приписывается инфа о поставщике идентификационных данных
    /// 
    /// </summary>
    public abstract class ClaimDTO:UserDTO
    {
        public IEnumerable<Claim> Claims { get; set; }

    }
    public class AddClaimDTO : ClaimDTO
    {

    }
    public class RemoveClaimDTO : ClaimDTO
    {

    }
    public class ReplaceClaimDTO : ClaimDTO
    {
        public Claim Claim { get; set; }
        public Claim NewClaim { get; set; }
    }
}
