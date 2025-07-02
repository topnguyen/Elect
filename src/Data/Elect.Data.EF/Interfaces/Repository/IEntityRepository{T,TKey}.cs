namespace Elect.Data.EF.Interfaces.Repository
{
    public interface IEntityRepository<T, TKey> : IBaseEntityRepository<T> where T : Models.Entity<TKey>, new() where TKey : struct
    {
        /// <summary>
        ///     Update Entities
        /// </summary>
        /// <param name="predicate">Update Where by match condition of predicate</param>
        /// <param name="entityNewData">New Data</param>
        /// <param name="changedProperties">Specific properties changed</param>
        /// <returns></returns>
        /// <remarks>
        ///     <para>We will auto set LastUpdatedTime to <c>DateTimeOffset.UtcNow</c> if not set before</para>
        ///     <para>You can override the LastUpdatedTime by override StandardizeEntities in UnitOfWork/BaseEntityUnitOfWork</para>
        /// </remarks>
        void UpdateWhere(Expression<Func<T, bool>> predicate, T entityNewData, params Expression<Func<T, object>>[] changedProperties);
        /// <summary>
        ///     Update Entities
        /// </summary>
        /// <param name="predicate">Update Where by match condition of predicate</param>
        /// <param name="entityNewData">New Data</param>
        /// <param name="changedProperties">Specific properties changed</param>
        /// <returns></returns>
        /// <remarks>
        ///     <para>We will auto set LastUpdatedTime to <c>DateTimeOffset.UtcNow</c> if not set before</para>
        ///     <para>You can override the LastUpdatedTime by override StandardizeEntities in UnitOfWork/BaseEntityUnitOfWork</para>
        /// </remarks>
        void UpdateWhere(Expression<Func<T, bool>> predicate, T entityNewData, params string[] changedProperties);
        /// <summary>
        ///     Delete Where by list id
        /// </summary>
        /// <param name="ids">list id use for identity the entities</param>
        /// <param name="isPhysicalDelete"></param>
        /// <remarks>
        ///     <para>When isPhysicalDelete is <c>true</c>, it's mean auto include soft delete record in query/predicate</para>
        ///     <para>We will auto set DeletedTime to <c>DateTimeOffset.UtcNow</c> if not set before</para>
        ///     <para>You can override the DeletedTime by override StandardizeEntities in UnitOfWork/BaseEntityUnitOfWork</para>
        /// </remarks>
        void DeleteWhere(List<TKey> ids, bool isPhysicalDelete = false);
    }
}
