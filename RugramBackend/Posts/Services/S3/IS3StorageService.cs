namespace Posts.Services.S3;

/// <summary>
/// Контракт для работы с S3
/// </summary>
public interface IS3StorageService
{
	/// <summary>
	/// Проверить существует ли бакет 
	/// </summary>
	/// <param name="bucketIdentifier">уникальный идентификатор бакета</param>
	/// <returns>true если существует</returns>
	public Task<bool> BucketExistAsync(Guid bucketIdentifier);
	
	/// <summary>
	/// Созать бакет
	/// </summary>
	/// <param name="bucketIdentifier">уникальный идентификатор бакета</param>
	public Task CreateBucketAsync(Guid bucketIdentifier);

	/// <summary>
	/// Удалить бакет
	/// </summary>
	/// <param name="bucketIdentifier">уникальный идентификатор бакета</param>
	/// <returns></returns>
	public Task RemoveBucketAsync(Guid bucketIdentifier);

	/// <summary>
	/// Положить файл в бакет
	/// </summary>
	/// <param name="fileStream">Файл в виде <see cref="Stream"/></param>
	/// <param name="fileIdentifier">уникальный идентификатор файла</param>
	/// <param name="bucketIdentifier">уникальный идентификатор бакета</param>
	public Task PutFileInBucketAsync(Stream fileStream, Guid fileIdentifier, Guid bucketIdentifier);

	/// <summary>
	/// Удалить файл из бакета
	/// </summary>
	/// <param name="fileIdentifier">уникальный идентификатор файла</param>
	/// <param name="bucketIdentifier">уникальный идентификатор бакета</param>
	public Task RemoveFileFromBucketAsync(Guid fileIdentifier, Guid bucketIdentifier);

	/// <summary>
	/// Получить файл из бакета
	/// </summary>
	/// <param name="fileIdentifier">уникальный идентификатор файла</param>
	/// <param name="bucketIdentifier">уникальный идентификатор бакета</param>
	/// <returns>Файл в виде <see cref="Stream"/> или null если бакет или файл не найден. </returns>
	public Task<Stream> GetFileFromBucketAsync(Guid fileIdentifier, Guid bucketIdentifier);
}